using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public Transform target;


    public VirtualJoystick cameraJoystick;
    public Transform lookAt;
    public Quaternion rotation;
    public float distance = 10f;
    public float distanceY = 10f;
    private float currentX = 0f;
    private float currentY = 0f;
    private float sensitivityX = 3f;
    private float sensitivityY = 1f;



    private void Start()
    {
        lookAt = PlayerManager.instance.player.transform;

        //targetPos = target.position + position.targetPosOffset;
        //destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0) * -Vector3.forward * position.distanceFromTarget;
        //destination += targetPos;
        //transform.position = destination;

        // new starts here
        SetCameraTarget(lookAt);

        MoveToTarget();

        collision.Initialize(Camera.main);
        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);

    }

    void SetCameraTarget(Transform t)
    {
        target = t;

        if(target != null)
        {

        }
        else
        {
            Debug.LogError("Camera needs a target!");
        }
    }
    private void Update()
    {
        GetInput();
        OrbitTarget();
        ZoomInOnTarget();
 


    }
    private void FixedUpdate()
    {
        //Vector3 dir = new Vector3(0, -distanceY, -distance);
        //rotation = Quaternion.Euler(currentY, currentX, 0);
        //transform.position = lookAt.position + rotation * dir;
        //transform.LookAt(lookAt);

        MoveToTarget();
        LookAtTarget();

        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);
        //draw debug lines
        for (int i = 0; i < 5; i++)
        {
            if (debugSettings.drawDesiredCollisionLines)
            {
                Debug.DrawLine(targetPos, collision.desiredCameraClipPoints[i], Color.red);
                Debug.DrawLine(targetPos, collision.adjustedCameraClipPoints[i], Color.green);
            }
        }
        collision.CheckColliding(targetPos);
        position.adjustmentDistance = collision.GetAdjustedDistanceWithRayFrom(targetPos);
    }
    void GetInput()
    {
        vOrbitInput = cameraJoystick.InputVector.x * orbit.hOrbitSmooth * Time.deltaTime; 
        hOrbitInput = cameraJoystick.InputVector.z * orbit.vOrbitSmooth * Time.deltaTime;
     
        zoomInput = Input.GetAxis("Mouse ScrollWheel");
    }

    void MoveToTarget()
    {
     targetPos = target.position + position.targetPosOffset;
    ///    destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0) * -Vector3.forward * position.distanceFromTarget;
        destination += targetPos;

        if (collision.colliding)
        {
        adjustedDestination = Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0) * Vector3.forward * position.adjustmentDistance;
            adjustedDestination += targetPos;
            if (position.smoothFollow)
            {
                transform.position = Vector3.SmoothDamp(transform.position, adjustedDestination, ref camVel, position.smooth);
            }
            else
                transform.position = adjustedDestination;
        }
        else
        {
            if (position.smoothFollow)
            {
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVel, position.smooth);
            }
            else
                transform.position = destination;
        }


    }
    void LookAtTarget()
    {
      Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, position.lookSmooth * Time.deltaTime);
    }
    void OrbitTarget()
    {

        //currentX += cameraJoystick.InputVector.x * sensitivityX;
        //currentY += cameraJoystick.InputVector.z * sensitivityY;
        //currentY = Mathf.Clamp(currentY, -25f, 40f);
  
        orbit.xRotation += hOrbitInput;
        orbit.yRotation += vOrbitInput;
        orbit.xRotation = Mathf.Clamp(orbit.xRotation, orbit.minXRotation, orbit.maxXRotation);


    }
    void ZoomInOnTarget()
    {
        position.distanceFromTarget += zoomInput * position.zoomSmooth * Time.deltaTime;
        position.distanceFromTarget = Mathf.Clamp(position.distanceFromTarget, position.minZoom, position.maxZoom);
    }
    [System.Serializable]
    public class PositionSettings
    {
        public Vector3 targetPosOffset = new Vector3(0,0.5f, 0);
        public float lookSmooth = 100f;
        public float distanceFromTarget = -4;
        public float zoomSmooth = 10;
        public float maxZoom = -2;
        public float minZoom = -15;

        public bool smoothFollow = true;
        public float smooth = 0.05f;
        [HideInInspector]
        public float newDistance = -8;
        [HideInInspector]
        public float adjustmentDistance = -8;
    }

    [System.Serializable]
    public class OrbitSettings
    {
        public float xRotation = -20;
        public float yRotation = -180;
        public float maxXRotation = 25;
        public float minXRotation = -85;
        public float vOrbitSmooth = 150;
        public float hOrbitSmooth = 150;
    }


    [System.Serializable]
    public class DebugSettings
    {
        public bool drawDesiredCollisionLines = true;
        public bool drawAdjustedCollisionLines = true;
    }
    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public DebugSettings debugSettings = new DebugSettings();
    public CollisionHandler collision = new CollisionHandler();

    Vector3 targetPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;

    Vector3 adjustedDestination = Vector3.zero;
    Vector3 camVel = Vector3.zero;

    [System.Serializable]
    public class CollisionHandler
    {
        public LayerMask collisionLayer;

        [HideInInspector]
        public bool colliding = false;
        [HideInInspector]
        public Vector3[] adjustedCameraClipPoints;
        [HideInInspector]
        public Vector3[] desiredCameraClipPoints;

        Camera camera;

        public void Initialize(Camera cam)
        {
            camera = cam;
            adjustedCameraClipPoints = new Vector3[5];
            desiredCameraClipPoints = new Vector3[5];
        }
        public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray)
        {
            if (!camera) return;
            //clearing the contents of into array
            intoArray = new Vector3[5];

            float z = camera.nearClipPlane;
            float x = Mathf.Tan(camera.fieldOfView / 3.41f) * z;
            float y = x / camera.aspect;

            //top left
            intoArray[0] = (atRotation * new Vector3(-x, y, z)) + cameraPosition; //Added and rotated point relative to cam
            //top right
            intoArray[1] = (atRotation * new Vector3(x, y, z)) + cameraPosition; //Added and rotated point relative to cam
            //bottom left
            intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + cameraPosition; //Added and rotated point relative to cam
            //bottom right
            intoArray[3] = (atRotation * new Vector3(x, -y, z)) + cameraPosition; //Added and rotated point relative to cam
            //cam pos
            intoArray[4] = cameraPosition - camera.transform.forward; //Added and rotated point relative to cam
        }
        bool CollisionDetectedAtClipPoints(Vector3[] clipPoints, Vector3 fromPosition)
        {
            for (int i = 0; i < clipPoints.Length; i++)
            {
                Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition) ;
                float distance = Vector3.Distance(clipPoints[i], fromPosition);
                if(Physics.Raycast(ray, distance, collisionLayer))
                {
                    return true;
                }
               
            }
            return false;
        }
       
        public float GetAdjustedDistanceWithRayFrom(Vector3 from)
        {
            float distance = -1;

            for (int i = 0; i < desiredCameraClipPoints.Length; i++)
            {
                Ray ray = new Ray(from,desiredCameraClipPoints[i] - from);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit))
                {
                    if (distance == -1)
                        distance = hit.distance;
                    else
                    {
                        if(hit.distance < distance)
                        {
                            distance = hit.distance;
                        }
                    }
                }
            }

            if (distance == -1)
                return 0;
            else
            return distance;
        }
        public void CheckColliding(Vector3 targetPosition)
        {
            if (CollisionDetectedAtClipPoints(desiredCameraClipPoints, targetPosition))
                colliding = true;
                
            else
                colliding = false;
        }
    }
}
