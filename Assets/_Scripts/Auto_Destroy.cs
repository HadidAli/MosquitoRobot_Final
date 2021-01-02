using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_Destroy : MonoBehaviour
{
    public GameObject spark;
    public string PlayerTag;
    public float Distroy_Time = 1.5f;
    public float Demag;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, Distroy_Time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(spark, pos, rot);
        Destroy(this.gameObject);

        //print(collision.gameObject.tag);

        if (collision.gameObject.CompareTag (PlayerTag))
        {
            collision.gameObject.GetComponent<GunFire>().health -= Demag;
          //  print("Shooting to Player with decreasing health power");
        }
    }
}
