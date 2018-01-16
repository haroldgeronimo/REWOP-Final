using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
    #region "TopView"
    public bool Freeze = false;

    //public float moveSpeed = 16.0f;
    //public float drag = 0.5f;
    //public float terminalRotationSpeed = 25.0f;

    //public VirtualJoystick joystick;
    //private Rigidbody controller;
    //private Transform cameraT;

    //private void Start()
    //{
    //    controller = GetComponent<Rigidbody>();
    //    controller.maxAngularVelocity = terminalRotationSpeed;
    //    controller.drag = drag;
    //    cameraT = Camera.main.transform;
    //        }

    //private Vector3 lastdir = Vector3.zero;
    //private void FixedUpdate()
    //{
    //    Vector3 dir = Vector3.zero;

    //    if (!Freeze)
    //    {
    //        dir.x = joystick.Horizontal();//Input.GetAxis("Horizontal"); 
    //        dir.z = joystick.Vertical();// Input.GetAxis("Vertical");
    //    }

    //    if (dir != Vector3.zero)
    //    {

    //        lastdir = dir;
    //        this.transform.rotation = Quaternion.LookRotation(lastdir);

    //    }




    //    if (dir.magnitude > 1)
    //        dir.Normalize();
    //    controller.AddForce(dir * moveSpeed);


    //    //check if player getoutofbounds
    //    if (transform.position.y < 0)
    //    {
    //        Debug.Log("Player died: Out of Game Bounds!");
    //        PlayerManager.instance.KillPlayer();
    //    }

    //}
    #endregion

    #region "ThirdPerson"
    public VirtualJoystick joystick;

    //controls
    public float walkspeed = 4f;
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
  public float currentSpeed;
    public Vector2 inputDir;
    public Transform camT;
    private void Start()
    {
        camT = Camera.main.transform;
    
    }
    private void FixedUpdate()
    {
        Vector2 input = new Vector2(joystick.Horizontal(), joystick.Vertical());
       inputDir = input;
        if(inputDir != Vector2.zero) {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + camT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y,targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
    
        float targetSpeed = walkspeed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        

        #endregion
    }
  
    private void SetPlayerTransform(Vector3 position,Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }
}
