using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag=="Player")
        {
            other.gameObject.GetComponent<GunFire>().health = 100;
            other.gameObject.GetComponent<GunFire>().Health_Bar.fillAmount = 1;
            GameManager.healthKitPicked++;
            GamePlayManager.GM.Update_HealthKit_Counter();
            gameObject.transform.parent.transform.GetChild(2).gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
