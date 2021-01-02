using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_AI : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Player;
    bool isLooking;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLooking)
        {
            gameObject.transform.LookAt(Player.gameObject.transform, Vector3.forward);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       // print(other.gameObject.tag);
        if (other.gameObject.CompareTag("Mosquito"))
        {
            Player = other.gameObject;
            Debug.Log(" I am here to fight You");

            isLooking = true;

            gameObject.GetComponent<GunFire>().Pointer_Down();
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Mosquito")
        {
            isLooking = false;
            //Player = null;
            gameObject.GetComponent<GunFire>().Pointer_Up();

        }
    }
}
