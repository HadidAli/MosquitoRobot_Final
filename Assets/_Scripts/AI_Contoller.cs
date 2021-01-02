using Exploder.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI_Contoller : MonoBehaviour
{

    public enum EnemiesType 
    { 
    Mech_Robot,
    Humaniod,
    Drone,
    Mosquito
    
    };

    public enum AssigningEnemy 
    {
    Mech_Robot,
    Humaniod,
    drone,
    Mosquito
    };


    public enum PlayerCheck 
    {
    Car_Robot,
    Drone
    };

    public PlayerCheck CheckPlayer;

    [SerializeField]
    float destinationReachedTreshold;
    [SerializeField]
    float PlayerDetectionTreshold;
    public GameObject WayPoints;
    NavMeshAgent AI;
    [SerializeField]
    int Index;
    public float distance;
    [SerializeField]
    bool isPlayerDetect;
    bool isFiring;
    GameObject Player;

    [SerializeField]
    Transform Pose1;
    [SerializeField]
    Transform Pose2;

    [SerializeField]
    Rigidbody Projectile;

    AudioSource AS;
    [SerializeField]
    AudioClip GunShot_S;
    [SerializeField]
    AudioClip Explosion_Sound;

    [SerializeField]
    GameObject Explosion;
   public Image EnergyBar;

    [SerializeField]
    float speed;

    public float Health = 100;
    

    public AssigningEnemy AssignEnemy;

    bool isDiead;

   
    //public EnemiesType EnemyType;

    // Start is called before the first frame update
    void Start()
    {
        
        
        if (AS == null)
        {
            AS = GameObject.Find("Shoot_Audio_Source").GetComponent<AudioSource>();
        }

        Check_Player();
        

        AI = gameObject.GetComponent<NavMeshAgent>();
        AI.SetDestination(WayPoints.transform.GetChild(0).transform.position);



        Set_AnimTrigger();


    }

    // Update is called once per frame
    void Update()
    {
        if (!isDiead)
        {
            if (Health > 0)
            {


                Detect_Player();
                if (isPlayerDetect)
                {
                    gameObject.transform.LookAt(Player.transform);
                }
                else
                {
                    Check_Destination_Reached();

                }

                EnergyBar.fillAmount = Health / 100;
            }
            else
            {
                //gameObject.SetActive(false);
                ExploderSingleton.Instance.ExplodeObject(gameObject);
                Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
                AS.PlayOneShot(Explosion_Sound, 1);
                isDiead = true;
               // print("Enemi Died");
                Destroy(this.gameObject);
                GameManager.EnemyDied++;
                Player.GetComponent<GunFire>().Check_Enemi();

                checkEnemyTypeCount();                
                GamePlayManager.GM.Update_EnmyKilledCOunter();
                GamePlayManager.GM.CheckLevel_Progression();
                return;

            }
        }

       
    }

    void checkEnemyTypeCount() {
        if (AssignEnemy == AssigningEnemy.Humaniod)
        {
            GameManager.RobotKilled++;
        }
        if (AssignEnemy == AssigningEnemy.drone)
        {
            GameManager.DroneKilled++;
        }

    }


    void Check_Destination_Reached() 
    {

        float distanceToTarget = Vector3.Distance(transform.position, WayPoints.transform.GetChild(Index).transform.position);
        
        if (isPlayerDetect)
        {
            AI.SetDestination(Player.transform.position);
        }
        else
        {
            if (distanceToTarget < destinationReachedTreshold)
            {
                Index = Random.Range(0, WayPoints.transform.childCount);
                AI.SetDestination(WayPoints.transform.GetChild(Index).transform.position);
                Set_AnimTrigger();
            }
        }
    }

    void Detect_Player() 
    {
        float distanceToTarget = Vector3.Distance(transform.position, Player.transform.position);
        distance = distanceToTarget;
        if (distanceToTarget<PlayerDetectionTreshold)
        {
            isPlayerDetect = true;
            AI.isStopped = true;

            if (!isFiring)
            {
                isFiring = true;
                StartCoroutine(Firing());
                //print(" Shoootingggg");
            }
        }
        else
        if (distanceToTarget>PlayerDetectionTreshold)
        {
            isPlayerDetect = false;
            AI.isStopped = false;
            //Check_Destination_Reached();
            //print("Plyaer Out of Reach");
            if (isFiring)
            {
                isFiring = false;
                StopCoroutine(Firing());
                //("Stop Shoootingggg ");
            }
        }

      
    }


    IEnumerator Firing()
    {
        while (isFiring)
        {

           // print("Firing Start   ");
            if (Pose1 != null)

            {

             Rigidbody RB =   Instantiate(Projectile, Pose1.position, Pose1.rotation) as Rigidbody;
                RB.velocity = transform.TransformDirection(0,0,speed);
                Pose1.transform.GetChild(0).gameObject.SetActive(true);
                AS.PlayOneShot(GunShot_S, 1);

            }
            if (Pose2 != null)
            {
                Rigidbody RB = Instantiate(Projectile, Pose2.position, Pose2.rotation) as Rigidbody;
                RB.velocity = transform.TransformDirection(0, 0, speed);
                Pose2.transform.GetChild(0).gameObject.SetActive(true);
                //AS.PlayOneShot(GunShot_S, 1);
            }
          

            yield return new WaitForSeconds(10 / speed / 4);

            if (Pose1 != null)
            {
                Pose1.transform.GetChild(0).gameObject.SetActive(false);
            }
            if (Pose2 != null)
            {
                Pose2.transform.GetChild(0).gameObject.SetActive(false);
            }


            yield return new WaitForSeconds(10 / speed);
        }




    }

    void Set_AnimTrigger() 
    {

        if (AssignEnemy == AssigningEnemy.Humaniod)
        {
            if (gameObject.GetComponent<Animator>())
            {
                gameObject.GetComponent<Animator>().SetTrigger("Walking");
            }
            
        }
        else
           if (AssignEnemy == AssigningEnemy.drone)
        {

        }
        else
           if (AssignEnemy == AssigningEnemy.drone)
        {

        }
        else
           if (AssignEnemy == AssigningEnemy.drone)
        {

        }
    }

   public void Check_Player() {
        if (CheckPlayer== PlayerCheck.Car_Robot)
        {
            Player = GameObject.FindObjectOfType<GunFire>().gameObject;
           
        }
        else
            if (CheckPlayer== PlayerCheck.Drone)
        {
            Player = GameObject.FindObjectOfType<GunFire>().gameObject;
        }
    
    }
}
