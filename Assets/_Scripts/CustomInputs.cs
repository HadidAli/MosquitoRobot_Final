using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputs : MonoBehaviour
{

    public Joystick joystick1, Joystick2;

    public DroneMovement droneMovementScript;
    public float  FarwardValue = 0;
    public float  BackwardValue =0;
    public float  LeftValue =0;
    public float  RightValue =0;

    // Start is called before the first frame update
    void Start()
    {

        
        droneMovementScript = GetComponent<DroneMovement>();
    }

    // Update is called once per frame
    void Update()
    {

       // print(Joystick.Horizontal);
       // FarwardValue = (ControlFreak2.CF2Input.GetKey(KeyCode.W)) ? 1 : 0;
        droneMovementScript.customFeed_forward = joystick1.Vertical;

       // BackwardValue = (ControlFreak2.CF2Input.GetKey(KeyCode.S)) ? 1 : 0;
        droneMovementScript.customFeed_backward = joystick1.Vertical * (-1);

       // LeftValue = (ControlFreak2.CF2Input.GetKey(KeyCode.A)) ? 1 : 0;
        droneMovementScript.customFeed_leftward = joystick1.Horizontal * (-1);

       // RightValue = (ControlFreak2.CF2Input.GetKey(KeyCode.D)) ? 1 : 0;
        droneMovementScript.customFeed_rightward = joystick1.Horizontal;




        // print(Joystick.Horizontal);
        // FarwardValue = (ControlFreak2.CF2Input.GetKey(KeyCode.W)) ? 1 : 0;
        droneMovementScript.customFeed_upward = Joystick2.Vertical;

        // BackwardValue = (ControlFreak2.CF2Input.GetKey(KeyCode.S)) ? 1 : 0;
        droneMovementScript.customFeed_downward = Joystick2.Vertical * (-1);

        // LeftValue = (ControlFreak2.CF2Input.GetKey(KeyCode.A)) ? 1 : 0;
        droneMovementScript.customFeed_rotateLeft = Joystick2.Horizontal * (-1);

        // RightValue = (ControlFreak2.CF2Input.GetKey(KeyCode.D)) ? 1 : 0;
        droneMovementScript.customFeed_rotateRight = Joystick2.Horizontal;

    }
}
