using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputs : MonoBehaviour
{

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
        FarwardValue = (Input.GetKey(KeyCode.W)) ? 1 : 0;
        droneMovementScript.customFeed_forward = FarwardValue;

        BackwardValue = (Input.GetKey(KeyCode.S)) ? 1 : 0;
        droneMovementScript.customFeed_backward = BackwardValue;

        LeftValue = (Input.GetKey(KeyCode.A)) ? 1 : 0;
        droneMovementScript.customFeed_leftward = LeftValue;

        RightValue = (Input.GetKey(KeyCode.D)) ? 1 : 0;
        droneMovementScript.customFeed_rightward = RightValue;

    }
}
