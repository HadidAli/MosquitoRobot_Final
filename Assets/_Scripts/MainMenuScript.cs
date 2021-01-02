using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject Loading, LevelSelectionp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Level_Selection(int LevelNumber) {
        Loading.SetActive(true);
        PlayerPrefs.SetInt("Level",LevelNumber);
        LevelSelectionp.SetActive(false);
        Application.LoadLevel("GamePlay_newEnv");
        SoundManager.SM.PlayGPSound();
    }

    public void ClickSound() {
        if (SoundManager.SM)
        {
            SoundManager.SM.PlayClickSound();
        }
        
    
    }


  
}
