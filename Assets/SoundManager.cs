using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SM;


    public AudioClip GamePlaySound, MainMenu;
    public AudioClip ClickSound;
    AudioSource AS;
    // Start is called before the first frame update
    void Start()
    {
        if (!SM)
        {
            SM = this;
        }

        DontDestroyOnLoad(this.gameObject);
        AS = gameObject.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClickSound() 
    {
        AS.PlayOneShot(ClickSound,1);

    }

    public void PlayMMSound() 
    {
        AS.clip = MainMenu;
        AS.Play();
    }

    IEnumerator Sound_Delay()
    {
        yield return new WaitForSeconds(3);

        AS.clip = GamePlaySound;
        AS.Play();
        
        
    }

    public void PlayGPSound() 
    {
        StartCoroutine(Sound_Delay());
    }
}
