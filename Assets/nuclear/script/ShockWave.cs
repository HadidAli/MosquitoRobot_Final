using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exploder.Utils;

public class ShockWave : MonoBehaviour {


public AudioSource shockwaveSound;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	  void OnTriggerEnter(Collider other) {
          
			   //shockwaveSound.Play();
            ExploderSingleton.Instance.ExplodeObject(other.gameObject);

        
    }
}
