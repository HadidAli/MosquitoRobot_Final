using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cut_Scene : MonoBehaviour
{

    public GameObject fireball, NuclearAttack, DirectionalLight, Canvas;
    public float nextLevelTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startCutScene());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator startCutScene() {
        fireball.SetActive(true);
        yield return new WaitForSeconds(5);
        NuclearAttack.SetActive(true);
        DirectionalLight.SetActive(false);

        yield return new WaitForSeconds(nextLevelTime);
        Canvas.SetActive(true);
        SoundManager.SM.PlayMMSound();



    }

    public void Next_Level() {

        Application.LoadLevel("MainMenu");

    }
}
