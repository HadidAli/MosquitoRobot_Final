using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dron_Portal : MonoBehaviour
{
    public GameObject NewEnemies;
    private void OnEnable()
    {
        GamePlayManager.GM.isPortalLevel = true;
    }
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
        if (other.gameObject.tag == "Player" && GamePlayManager.GM.isDrone)
        {
            other.gameObject.transform.position = GamePlayManager.GM.Pipe_Pos[GamePlayManager.GM.Level_Number].position;
            NewEnemies.SetActive(true);
            other.gameObject.GetComponent<GunFire>().Check_Enemi();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Player" && GamePlayManager.GM.isRobot)
        {
            {
                GamePlayManager.GM.objective_panel.SetActive(true);
                GamePlayManager.GM.Obj_text.text = "warning! \n you have to <color=red>transform</color> yourself to enter this portal";
            }
        }
    }
}
