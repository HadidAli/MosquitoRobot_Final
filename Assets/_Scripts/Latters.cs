using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Latters : MonoBehaviour
{
    public AudioClip tic;
    public float letterPause;
    string message;
    Text textComp;
    AudioSource tictic;
    // Start is called before the first frame update
    void Start()
    {
        textComp = gameObject.GetComponent<Text>();
        message = textComp.text;
        textComp.text = "";
        tictic = GetComponent<AudioSource>();

        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            textComp.text += letter;
            tictic.PlayOneShot(tic);
            yield return new WaitForSeconds(letterPause);
        }
      

    }
}
