using System;
using _Game.Scripts.CommandsSystem;
using _Game.Scripts.CommandsSystem.Controller;
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

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _onSelectPosition += commandController.SelectPosition;
        }

        private void FixedUpdate()
        {
            var xAxisValue = Input.GetAxis("Horizontal");
            var yAxisValue = Input.GetAxis("Vertical");
            _camera.transform.Translate(new Vector3(xAxisValue * Time.deltaTime * 5f, yAxisValue * Time.deltaTime * 5f,
                0));

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) //TODO: Переробити визначення позиції
                _onSelectPosition.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
            
        }
    }
}