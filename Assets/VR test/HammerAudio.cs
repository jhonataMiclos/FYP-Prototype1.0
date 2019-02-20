using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAudio : MonoBehaviour {

    public AudioClip soundClip;
    public AudioSource soundSource;
	// Use this for initialization
	void Start () {
        soundSource.clip = soundClip;
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("play sound");
        if (collision.gameObject.name.Equals("Chisel"))
        {
            soundSource.Play();

        }
       
    }
    // Update is called once per frame
    void Update () {
		
	}
}
