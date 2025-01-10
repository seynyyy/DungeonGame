using _Game.Scripts.CommandsSystem;
using _Game.Scripts.CommandsSystem.Controller;
using _Game.Scripts.Team;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        private UnityEngine.Camera _camera;
        [SerializeField] private CommandController commandController;
        [SerializeField] private TeamController teamController;
        [SerializeField] private LayerMask layerMask;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        private void FixedUpdate()
        {
            var xAxisValue = Input.GetAxis("Horizontal");
            var yAxisValue = Input.GetAxis("Vertical");
            _camera.transform.Translate(new Vector3(xAxisValue * Time.deltaTime * 5f, yAxisValue * Time.deltaTime * 5f,
                0));
            
            if (Input.GetMouseButtonDown(0) && teamController.SelectionState == SelectionState.PositionSelection) // TODO: Переробити визначення позиції
            {
                commandController.SelectPosition(_camera.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
}