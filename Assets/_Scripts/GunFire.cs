using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GunFire : MonoBehaviour
{
    public GameObject Projectile;
    public GameObject Mesile;
    public Button Mesile_Buton;
    public float speed = 20, TimeToDestroy = 0.4f;
    public Transform pos1, pos2, pos3;
    public Transform Mesile_Pos1, Mesile_Pose2;

    public float health = 100;
    public float EnemyDistance = 50;

    public bool isFiring = false;
    public bool isMesileFire = false;

    public GameObject Temp_Target;
    public AudioClip GunShot_S, mesile_Fire_s;
    public GameObject Explosion;
    AudioSource AS;

    public GameObject[] Enemies;



    public float[] Cal_Dis;
    public float minDistance;


    public Image Health_Bar;


    // Start is called before the first frame update
    void Start()
    {
        
        Check_Enemi();
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Cal_Dis = new float[Enemies.Length];
        Projectile.GetComponent<EffectSettings>().Target = Temp_Target;
        //AS = GameObject.Find("Indicator Sound AudioSource").GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
            GamePlayManager.GM.Level_Fail();
            health = 1;
            return;
        }

        if (Enemies.Length > 0)
        {


            for (int i = 0; i < Enemies.Length; i++)
            {

                Cal_Dis[i] = Vector3.Distance(gameObject.transform.position, Enemies[i].transform.position);

            }
        }


        var indexoflowest = ArrayUtil.GetIndexOfLowestValue(Cal_Dis);
        // print(indexoflowest);


        if (Cal_Dis.Length > 0 && Cal_Dis[indexoflowest] < EnemyDistance)
        {

            Projectile.GetComponent<EffectSettings>().Target = Enemies[indexoflowest];

        }

        if (Cal_Dis.Length > 0 && Cal_Dis[indexoflowest] > EnemyDistance)
        {

            Projectile.GetComponent<EffectSettings>().Target = Temp_Target;

        }

        Health_Bar.fillAmount = health / 100;

    }





    public void Pointer_Down()
    {

        isFiring = true;
        StartCoroutine(firing());
    }

    public void Pointer_Up()
    {
        StopCoroutine(firing());
        isFiring = false;

    }

    public void Mesile_Pointer_Down() {

        if (isMesileFire == false)
        {
            StartCoroutine(Mesile_Fire());
            isMesileFire = true;
            Mesile_Buton.interactable = false;

        }



    }

    IEnumerator Mesile_Fire()
    {

        if (AS == null)
        {
            AS = GameObject.Find("Shoot_Audio_Source").GetComponent<AudioSource>();
        }

        if (Mesile_Pos1 != null)
        {
            Instantiate(Mesile, Mesile_Pos1.position, Mesile_Pos1.rotation);
            // mesi.transform.GetChild(0).gameObject.SetActive(true);
            Mesile.GetComponent<EffectSettings>().Target = Temp_Target;
            AS.PlayOneShot(mesile_Fire_s, 1);

        }

        if (Mesile_Pose2 != null)
        {
            Instantiate(Mesile, Mesile_Pose2.position, Mesile_Pose2.rotation);
            Mesile.GetComponent<EffectSettings>().Target = Temp_Target;


        }





        yield return new WaitForSeconds(5);
        Mesile_Buton.interactable = true;
        isMesileFire = false;


    }

    public void Check_Enemi() {
        System.Array.Clear(Enemies, 0, Enemies.Length);
        print("i am here to remove the array");
        Enemies = new GameObject[0];

        System.Array.Clear(Cal_Dis, 0, Cal_Dis.Length);
        


        StartCoroutine(New_enem_Array());

    }


    IEnumerator New_enem_Array() {
        yield return new WaitForSeconds(0.5f);
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Cal_Dis = new float[Enemies.Length];


    }






    IEnumerator firing() {
        while (isFiring)
        {

            if (AS==null)
            {
                AS = GameObject.Find("Shoot_Audio_Source").GetComponent<AudioSource>();
            }
            if (pos1 !=null)
            {

            Instantiate(Projectile, pos1.position, pos1.rotation);
                pos1.transform.GetChild(0).gameObject.SetActive(true);
                AS.PlayOneShot(GunShot_S, 1);
                
            }
            if (pos2 != null)
            {
                Instantiate(Projectile, pos2.position, pos2.rotation);
                pos2.transform.GetChild(0).gameObject.SetActive(true) ;
                //AS.PlayOneShot(GunShot_S, 1);
            }
            if (pos3 != null)
            {
                Instantiate(Projectile, pos3.position, pos3.rotation);
            }

            yield return new WaitForSeconds(10 / speed/4);

            if (pos1 != null)
            {
                pos1.transform.GetChild(0).gameObject.SetActive(false);
            }
            if (pos2 != null)
            {
                pos2.transform.GetChild(0).gameObject.SetActive(false);
            }


                yield return new WaitForSeconds(10/speed);
        }
   
    }

    


}



public static class ArrayUtil
{
    public static int GetIndexOfLowestValue(float[] arr)
    {
        float value = float.PositiveInfinity;
        int index = -1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] < value)
            {
                index = i;
                value = arr[i];
            }
        }
        return index;
    }
}
