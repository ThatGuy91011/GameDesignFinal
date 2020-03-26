using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpltScreenCamera : MonoBehaviour
{
    //Reference to the camera
    private Camera camera;
    public TankData data;
    // Start is called before the first frame update
    void Start()
    {
       
        camera = gameObject.GetComponent<Camera>();
        data = GetComponentInParent<TankData>();
        //Singleplayer or Multiplayer?
        //Single- change nothing
        //Multi- check if player 1 or player 2
        if (GameManager.instance.numPlayers > 1)
        {
            
            if (data.playerNumber == 1)
            {
                //player 1 on top
                camera.rect = new Rect(0, .5f, 1, .5f);
            }
            else
            {
                //player 2 on bottom
                camera.rect = new Rect(0, 0, 1, .5f);
            }
                
        }
        //Player 1- camera on top
        //Player 2- camera on bottom
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
