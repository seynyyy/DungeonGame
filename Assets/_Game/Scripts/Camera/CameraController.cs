using UnityEngine;

namespace _Game.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        private UnityEngine.Camera _camera;
    
        private Vector3 fp;   //First touch position
        private Vector3 lp;   //Last touch position
        private float dragDistance;  //minimum distance for a swipe to be registered

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
            dragDistance = Screen.height * 15 / 100;
        }

        private void FixedUpdate()
        {
            if (Input.touchCount == 1) // user is touching the screen with a single touch
            {
                Touch touch = Input.GetTouch(0); // get the touch
                if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
                {
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
                {
                    lp = touch.position;  //last touch position. Ommitted if you use list

                    //Check if drag distance is greater than 20% of the screen height
                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    {//It's a drag
                        //check if the drag is vertical or horizontal
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {   //If the horizontal movement is greater than the vertical movement...
                            if ((lp.x > fp.x))  //If the movement was to the right)
                            {   //Right swipe
                                _camera.gameObject.transform.position = new Vector3(_camera.gameObject.transform.position.x+10, _camera.gameObject.transform.position.y, _camera.gameObject.transform.position.z);
                            }
                            else
                            {   //Left swipe
                                _camera.gameObject.transform.position = new Vector3(_camera.gameObject.transform.position.x-10, _camera.gameObject.transform.position.y, _camera.gameObject.transform.position.z);
                            }
                        }
                        else
                        {   //the vertical movement is greater than the horizontal movement
                            if (lp.y > fp.y)  //If the movement was up
                            {   //Up swipe
                                _camera.gameObject.transform.position = new Vector3(_camera.gameObject.transform.position.x, _camera.gameObject.transform.position.y+1, _camera.gameObject.transform.position.z);
                            }
                            else
                            {   //Down swipe
                                _camera.gameObject.transform.position = new Vector3(_camera.gameObject.transform.position.x, _camera.gameObject.transform.position.y-1, _camera.gameObject.transform.position.z);
                            }
                        }
                    }
                    else
                    {   //It's a tap as the drag distance is less than 20% of the screen height
                        _camera.orthographicSize += 1f;
                    }
                }
            }
        }
    }
}
