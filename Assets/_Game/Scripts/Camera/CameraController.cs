using System;
using System.Collections.Generic;
using _Game.Scripts.CommandsSystem.Controller;
using _Game.Scripts.Infrastructure;
using _Game.Scripts.Infrastructure.Entity;
using _Game.Scripts.Team;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CommandController commandController;
        [SerializeField] private TeamController teamController;

        private UnityEngine.Camera _camera;
        private Action<Vector2> _onSelectPosition;
        private Action<EntityController> _onSelectAdventurer;

        private readonly List<Vector2> _points = new();
        private float minDistance { get; set; } = 0.3f;
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _onSelectPosition += commandController.SelectPosition;
            _onSelectAdventurer += commandController.SelectTarget;
            
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 0;
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
        }

        private void Update()
        {
            MoveCamera();
            TrackPoints();
            TrackClick();
        }

        private void MoveCamera()
        {
            var xAxisValue = Input.GetAxis("Horizontal");
            var yAxisValue = Input.GetAxis("Vertical");
            _camera.transform.Translate(new Vector3(xAxisValue * Time.deltaTime * 5f, yAxisValue * Time.deltaTime * 5f,
                0));
        }

        private void TrackClick()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (!Input.GetMouseButtonDown(0)) return;
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5f);
            _onSelectPosition.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
            var hit = Physics2D.GetRayIntersection(ray);
            if (!hit.collider) return;
            if (hit.collider.TryGetComponent(out EntityController entityController))
                _onSelectAdventurer.Invoke(entityController);
        }

        private void TrackPoints()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Vector2 position = _camera.ScreenToWorldPoint(Input.mousePosition);
            
            if (Input.GetMouseButtonDown(0))
            {
                _points.Clear();
                _points.Add(position);
                _lineRenderer.positionCount = 1;
                _lineRenderer.SetPosition(0, position);
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_points.Count < 2) return;
                _lineRenderer.positionCount = 0;
                teamController.PlaceAdventurers(_points);
                //_points.Clear();
            }

            if (Input.GetMouseButton(0))
            {
                if (Vector2.Distance(_points[^1], position) <
                      minDistance) return;

                _points.Add(position);
                _lineRenderer.positionCount = _points.Count;
                
                _lineRenderer.SetPosition(_points.Count-1, position);
                
            }
        }

    }
}