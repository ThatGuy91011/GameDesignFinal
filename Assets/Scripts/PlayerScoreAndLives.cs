using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreAndLives : MonoBehaviour
{
    public GameObject player;
    public TankData data;
    public int score;
    public float health;
    public Text scoreText;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        data = player.GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        //Constantly updates the player's HUD with their score and health count
        score = data.score;
        health = data.health;

        scoreText = GameObject.Find("Score").GetComponent<Text>();
        healthText = GameObject.Find("Health").GetComponent<Text>();

        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + health;


    }
}
