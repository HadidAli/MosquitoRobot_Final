using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntinaRotation : MonoBehaviour
{

    public float speed=80;
    // Start is called before the first frame update
    void Start()
    {

        speed = speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x,(gameObject.transform.eulerAngles.y+speed),gameObject.transform.eulerAngles.z);
    }
}
