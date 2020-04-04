using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip loopMusic;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        GetComponent<AudioSource>().volume = .5f;
    }

    // Update is called once per frame
    void Update()
    {
        //Replaces the beginning music with proper looping music depending on how long the game goes on for
        if (!audio.isPlaying)
        {
            audio.clip = loopMusic;
            audio.loop = true;
            audio.Play();
        }
    }
}
