using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreAndLives : MonoBehaviour
{
    public GameObject player;
    public GameObject player1;
    public GameObject player2;
    public TankData data;
    public TankData data1;
    public TankData data2;
    public int score;
    public int score1;
    public int score2;
    public float health;
    public float health1;
    public float health2;
    public Text scoreText;
    public Text scoreText1;
    public Text scoreText2;
    public Text healthText;
    public Text healthText1;
    public Text healthText2;

    // Start is called before the first frame update
    void Start()
    {
        
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            player1 = GameObject.Find("Player 1");
            player2 = GameObject.Find("Player 2");

            data1 = player1.GetComponent<TankData>();
            data2 = player2.GetComponent<TankData>();
        }
        else
        {
            player = GameObject.FindWithTag("Player");
            data = player.GetComponent<TankData>();
        }
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            //Constantly updates the player's HUD with their score and health count
            score1 = data1.score;
            health1 = data1.health;

            scoreText1 = GameObject.Find("Score1").GetComponent<Text>();
            healthText1 = GameObject.Find("Health1").GetComponent<Text>();

            scoreText1.text = "Score: " + score1;
            healthText1.text = "Health: " + health1;

            score2 = data2.score;
            health2 = data2.health;

            scoreText2 = GameObject.Find("Score2").GetComponent<Text>();
            healthText2 = GameObject.Find("Health2").GetComponent<Text>();

            scoreText2.text = "Score: " + score2;
            healthText2.text = "Health: " + health2;
        }
        else
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
}
