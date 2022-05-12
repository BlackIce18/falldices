using UnityEngine;

public class RotateGameScene : MonoBehaviour
{
    private static bool canRotate = true;

    public Transform target;
    public Camera mainCamera;
    [Range(0.1f, 5f)]
    [Tooltip("How sensitive the mouse drag to camera rotation")]
    public float mouseRotateSpeed = 0.8f;
    [Range(0.01f, 100)]
    [Tooltip("How sensitive the touch drag to camera rotation")]
    public float touchRotateSpeed = 17.5f;
    [Tooltip("Smaller positive value means smoother rotation, 1 means no smooth apply")]
    public float slerpValue = 0.25f;
    public enum RotateMethod { Mouse, Touch };
    [Tooltip("How do you like to rotate the camera")]
    public RotateMethod rotateMethod = RotateMethod.Mouse;


    private Vector2 _swipeDirection; //swipe delta vector2
    private Quaternion _cameraRot; // store the quaternion after the slerp operation
    private Touch _touch;
    private float _distanceBetweenCameraAndTarget;

    private float _minXRotAngle = -80; //min angle around x axis
    private float _maxXRotAngle = 80; // max angle around x axis

    //Mouse rotation related
    private float _rotX; // around x
    private float _rotY; // around y
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        SetCamPos(); 
        _distanceBetweenCameraAndTarget = Vector3.Distance(mainCamera.transform.position, target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            if (rotateMethod == RotateMethod.Mouse)
            {
                if (Input.GetMouseButton(0))
                {
                    _rotX += -Input.GetAxis("Mouse Y") * mouseRotateSpeed; // around X
                    _rotY += Input.GetAxis("Mouse X") * mouseRotateSpeed;
                    Vector3 dir = new Vector3(0, 0, -_distanceBetweenCameraAndTarget); //assign value to the distance between the maincamera and the target

                    Quaternion newQ; // value equal to the delta change of our mouse or touch position
                    if (rotateMethod == RotateMethod.Mouse)
                    {
                        newQ = Quaternion.Euler(_rotX, _rotY, 0); //We are setting the rotation around X, Y, Z axis respectively
                    }
                    else
                    {
                        newQ = Quaternion.Euler(_swipeDirection.y, -_swipeDirection.x, 0);
                    }
                    _cameraRot = Quaternion.Slerp(_cameraRot, newQ, slerpValue);  //let cameraRot value gradually reach newQ which corresponds to our touch
                    mainCamera.transform.position = target.position + _cameraRot * dir;
                    mainCamera.transform.LookAt(target.position);
                }

                if (_rotX < _minXRotAngle)
                {
                    _rotX = _minXRotAngle;
                }
                else if (_rotX > _maxXRotAngle)
                {
                    _rotX = _maxXRotAngle;
                }
            }
            else if (rotateMethod == RotateMethod.Touch)
            {
                if (Input.touchCount > 0)
                {
                    _touch = Input.GetTouch(0);
                    if (_touch.phase == TouchPhase.Began)
                    {
                        //Debug.Log("Touch Began");

                    }
                    else if (_touch.phase == TouchPhase.Moved)
                    {
                        _swipeDirection += _touch.deltaPosition * Time.deltaTime * touchRotateSpeed;
                    }
                    else if (_touch.phase == TouchPhase.Ended)
                    {
                        //Debug.Log("Touch Ended");
                    }
                }

                if (_swipeDirection.y < _minXRotAngle)
                {
                    _swipeDirection.y = _minXRotAngle;
                }
                else if (_swipeDirection.y > _maxXRotAngle)
                {
                    _swipeDirection.y = _maxXRotAngle;
                }


            }
        }
    }

    private void LateUpdate()
    {
        if(canRotate)
        {
            Vector3 dir = new Vector3(-5, _distanceBetweenCameraAndTarget, -5); //assign value to the distance between the maincamera and the target

            Quaternion newQ; // value equal to the delta change of our mouse or touch position
            if (rotateMethod == RotateMethod.Mouse)
            {
                newQ = Quaternion.Euler(_rotX, _rotY, 0); //We are setting the rotation around X, Y, Z axis respectively
            }
            else
            {
                newQ = Quaternion.Euler(_swipeDirection.y, -_swipeDirection.x, 0);
            }
            _cameraRot = Quaternion.Slerp(_cameraRot, newQ, slerpValue);  //let cameraRot value gradually reach newQ which corresponds to our touch
            mainCamera.transform.position = target.position + _cameraRot * dir ;
            mainCamera.transform.LookAt(target.position);
        }
    }

    public void SetCamPos()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        mainCamera.transform.position = new Vector3(-5, 28, -5);
    }
    public static void AllowRotate()
    {
        canRotate = true;
    }

    public static void ProhibitRotate()
    {
        canRotate = false;
    }
}
