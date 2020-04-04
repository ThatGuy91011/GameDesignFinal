using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        //Volume is determined by the game manager which gets it value from the slider
        GetComponent<AudioSource>().volume = GameManager.instance.musicVol;
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
