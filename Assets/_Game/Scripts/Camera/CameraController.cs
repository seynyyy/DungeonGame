using System;
using _Game.Scripts.CommandsSystem.Controller;
using _Game.Scripts.Infrastructure;
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

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _onSelectPosition += commandController.SelectPosition;
            _onSelectAdventurer += commandController.SelectTarget;
        }

        private void FixedUpdate()
        {
            var xAxisValue = Input.GetAxis("Horizontal");
            var yAxisValue = Input.GetAxis("Vertical");
            _camera.transform.Translate(new Vector3(xAxisValue * Time.deltaTime * 5f, yAxisValue * Time.deltaTime * 5f,
                0));

            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) return;
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5f);
            _onSelectPosition.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
            var hit = Physics2D.GetRayIntersection(ray);
            if (!hit.collider) return;
            if (hit.collider.TryGetComponent(out EntityController entityController))
                _onSelectAdventurer.Invoke(entityController);
        }
    }
}