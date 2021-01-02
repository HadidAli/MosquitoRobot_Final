using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager GM;

    public GameObject Mosquito_Cam;
    public int Level_Number;
    public GameObject[] Levels, Player_Positions;
    public int[] Per_Level_Time;
    public int[] EnemiesCount;
    public int[] HealthKits;
    public Transform[] HealthKitPos, PostalPos, Pipe_Pos;
    public string[] Objective_Texts;
    public GameObject AnimatedRobot;
    public GameObject Player_Robot, Player_Mosquito;
    public float time;
    bool timerFlag = true;
    public Text timeText;
    public Text enemyCounter_Text, Enemy_Killed, Enemy_Dronskilled, Enemy_DronesTokill;
    public GameObject LevelCompelte, LevelFail, objective_panel;
    public GameObject healthKit;
    public Button Mosquito_Transform_btn;

    bool isLevelComplete, isHealthKitCheck, isNotified;
    public Text Obj_text;

    public bool isPortalLevel = false;
    bool isPoratlOn = false;

    public bool isRobot, isDrone;
    [SerializeField]
    public EnemiValues[] EV;
    // Start is called before the first frame update
    void Start()
    {
        if (GM == null)
        {

            GM = this;
        }
        isLevelComplete = false;

        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i].SetActive(false);
        }
        
        objective_panel.SetActive(true);
        Obj_text = objective_panel.transform.GetChild(1).GetComponent<Text>();

        Obj_text.text = Objective_Texts[Level_Number];

        Level_Number = PlayerPrefs.GetInt("Level");

        ResetAllGMValues();
        PreLoad();
        OnLevelWasLoaded(Level_Number);
        HealthKitSpwan();


        GameManager.Total_Level_Enemy = EV[Level_Number].TotalEnemyTokill;
        GameManager.HealthKitToPic = EV[Level_Number].HealthKitToPic;
        
        enemyCounter_Text.text = EV[Level_Number].RobotToKill.ToString();
        Enemy_DronesTokill.text = EV[Level_Number].DronToKill.ToString();







    }

    public void CheckPoratal() 
    {
        if (!isPoratlOn)
        {
        isPoratlOn = true;

        }
    }

    // Update is called once per frame
    void Update()
    {


        Timer();

        if (Player_Mosquito.transform.GetChild(0).transform.position.z < -300 || Player_Mosquito.transform.GetChild(0).transform.position.y > 30)
        {
            Mosquito_Transform_btn.interactable = false;
        }
        else {
            Mosquito_Transform_btn.interactable = true;
        }

    }

    void HealthKitSpwan()
    {

        for (int i = 0; i < EV[Level_Number].HealthKitToPic; i++)
        {
            int temp = Random.Range(0, HealthKitPos.Length);
            Instantiate(healthKit, HealthKitPos[temp].position, Quaternion.identity);
        }
    }

    public void CheckLevel_Progression() {




        //if (GameManager.EnemyDied >= GameManager.Total_Level_Enemy && GameManager.healthKitPicked < GameManager.HealthKitToPic && !isHealthKitCheck)
        //{
        //    isHealthKitCheck = true;
        //    objective_panel.SetActive(true);
        //    Obj_text.text = "Well Done! \n Picup <color=red>Health</color> Kits to compelete the Level";

        //}

        if (GameManager.EnemyDied >= GameManager.Total_Level_Enemy && !isLevelComplete)
        {
            isLevelComplete = true;
            Level_Complete();
            print("Awen idhar kun a raha hun me");
            print(" dusham Mar gaey " + GameManager.EnemyDied);
            print("Tota Dushman    "+GameManager.Total_Level_Enemy);
        }

        if (isPortalLevel)
        {

            if (GameManager.RobotKilled >= GameManager.RobotToKill && !isNotified)
            {
                if (EV[Level_Number].DronToKill > 0)
                {

                    objective_panel.SetActive(true);
                    Obj_text.text = "Well Done! \n Now Go to the <color=red>Portal</color> transform yourself and kill Drones";
                    isNotified = true;
                    EV[Level_Number].Portal.SetActive(true);
                }
                
                

            }

        }

       


    }

    void ResetAllGMValues() 
    {
        GameManager.EnemyDied = 0;
        GameManager.Total_Level_Enemy = 0;
        GameManager.healthKitPicked = 0;
        GameManager.HealthKitToPic = 0;
        GameManager.RobotKilled = 0;
        GameManager.RobotToKill = 0;
        GameManager.DroneKilled = 0;
        GameManager.DroneTokill = 0;

    }

    public void Robot_To_Mosquito() {
        StartCoroutine(Robot_To_Dron());
    
    }

    

    public void Mosquito_To_Robot() {

        StartCoroutine(Dron_To_Robot());
    }

    IEnumerator Dron_To_Robot() {
        Player_Mosquito.SetActive(false);
        AnimatedRobot.transform.position = Player_Robot.transform.GetChild(0).transform.position;
        AnimatedRobot.SetActive(true);
        AnimatedRobot.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Robot");
        AnimatedRobot.transform.GetChild(1).GetComponent<Animator>().enabled = true;

        yield return new WaitForSeconds(3);

        Player_Robot.SetActive(true);
        Player_Robot.transform.GetChild(0).transform.position = Player_Mosquito.transform.GetChild(0).transform.position;
        Mosquito_Cam.SetActive(false);
        AnimatedRobot.SetActive(false);
        isRobot = true;
        isDrone = false;
        //AnimatedRobot.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Exit");

        yield return new WaitForSeconds(1);
        var enemies = GameObject.FindObjectsOfType<AI_Contoller>();
        foreach (var item in enemies)
        {
            item.Check_Player();
        }

    }

    void Timer() {

        if (time < 0)
        {
            timerFlag = false;
            // Level Has been Failed due to time out;
        }
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;

        if (timerFlag)
            time += Time.deltaTime;
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator Robot_To_Dron() 
    {
        Player_Robot.SetActive(false);
        AnimatedRobot.transform.position = Player_Robot.transform.position;
        AnimatedRobot.SetActive(true);
        AnimatedRobot.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Mosquito");
        AnimatedRobot.transform.GetChild(1).GetComponent<Animator>().enabled = true;



        yield return new WaitForSeconds(3);

        Player_Mosquito.SetActive(true);
        Player_Mosquito.transform.GetChild(0).transform.position = Player_Robot.transform.GetChild(0).transform.position;
        Mosquito_Cam.SetActive(true);
        AnimatedRobot.SetActive(false);
        AnimatedRobot.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Exit");
        isRobot = false;
        isDrone = true;
        yield return new WaitForSeconds(1);
        var enemies = GameObject.FindObjectsOfType<AI_Contoller>();
        foreach (var item in enemies)
        {
            item.Check_Player();
        }


    }

    void PreLoad()
    {

        Player_Mosquito.SetActive(false);
        Mosquito_Cam.SetActive(false);
        Player_Robot.SetActive(false);
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i].SetActive(false);
        }



    }

    private void OnLevelWasLoaded(int level)
    {
        isRobot = true;
        Player_Robot.SetActive(true);
        Levels[level].SetActive(true);
        Player_Robot.transform.GetChild(0).transform.position = Player_Positions[level].transform.position;
        GameManager.DroneTokill = EV[level].DronToKill;
        GameManager.RobotToKill = EV[level].RobotToKill;
        GameManager.Total_Level_Enemy = EV[level].TotalEnemyTokill;





        //if (level==0)
        //{
        //    Player_Robot.SetActive(true);
        //    Levels[level].SetActive(true);
        //    Player_Robot.transform.GetChild(0).transform.position = Player_Positions[level].transform.position;
        //    GameManager.DroneTokill= EV[level].DronToKill;
        //    GameManager.RobotToKill = EV[level].RobotToKill;
        //    GameManager.Total_Level_Enemy = EV[level].TotalEnemyTokill;

        //}
        //if (level==1)
        //{
        //    Mosquito_Cam.SetActive(true);
        //    Player_Mosquito.SetActive(true);
        //    Levels[Level_Number].SetActive(true);
        //    Player_Mosquito.transform.GetChild(0).transform.position = Player_Positions[Level_Number].transform.position; 
        //}
        //if (level==2)
        //{
        //    Player_Robot.SetActive(true);
        //    Levels[Level_Number].SetActive(true);
        //    Player_Robot.transform.GetChild(0).transform.position = Player_Positions[Level_Number].transform.position;

        //    GameManager.DroneTokill = 2;
        //    GameManager.RobotToKill = 2;
        //}
        //if (level==3)
        //{
        //    Mosquito_Cam.SetActive(true);
        //    Player_Mosquito.SetActive(true);
        //    Levels[Level_Number].SetActive(true);
        //    Player_Mosquito.transform.GetChild(0).transform.position = Player_Positions[Level_Number].transform.position;
        //}
        //if (level==4)
        //{
        //    Player_Robot.SetActive(true);
        //    Levels[Level_Number].SetActive(true);
        //    Player_Robot.transform.GetChild(0).transform.position = Player_Positions[Level_Number].transform.position;
        //}
        //if (level==6)
        //{

        //}
        //if (level==7)
        //{

        //}
        //if (level==8)
        //{

        //}
        //if (level==9)
        //{

        //}
    }

    public void Home() {
        Time.timeScale = 1;
        Application.LoadLevel("MainMenu");
        SoundManager.SM.PlayClickSound();

    }

    public void Restart() {
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevel);
        SoundManager.SM.PlayClickSound();

    }

    public void _Resume() {
        Time.timeScale = 1;
        SoundManager.SM.PlayClickSound();

    }

    public void _Pause() {

        Time.timeScale = 0.000001f;
        SoundManager.SM.PlayClickSound();
    }

    public void Level_Complete()
    {
        StartCoroutine(Compelet());
       
    }

    IEnumerator Compelet() {
        yield return new WaitForSeconds(2);
        PreLoad();
        Player_Robot.transform.GetChild(1).gameObject.SetActive(false);
        Player_Mosquito.transform.GetChild(1).gameObject.SetActive(false);
        LevelCompelte.SetActive(true);
    }


    
    public void Level_Fail() {

        StartCoroutine(fail());
    }

    IEnumerator fail() {
        yield return new WaitForSeconds(2);
        PreLoad();
        Player_Robot.transform.GetChild(1).gameObject.SetActive(false);
        Player_Mosquito.transform.GetChild(1).gameObject.SetActive(false);
        LevelFail.SetActive(true);
    }

    public void Update_EnmyKilledCOunter() {

        Enemy_Killed.text = GameManager.RobotKilled.ToString();
        Enemy_Dronskilled.text = GameManager.DroneKilled.ToString();
    }

    public void Update_HealthKit_Counter() {

        //HealthkitPicked.text = GameManager.healthKitPicked.ToString();
    }


  

}

[Serializable]
public class EnemiValues
{

   public int RobotToKill;
    public int DronToKill;
    public int HealthKitToPic;
    public int TotalEnemyTokill;
    public GameObject Portal;

}



