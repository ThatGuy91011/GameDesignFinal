using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject instantiatedPlayerTank;
    public GameObject playerTankPrefab;
    public GameObject icamera;
    public GameObject camera;
    public GameObject music;
    public GameObject imusic;
    public GameObject[] enemyTanks;
    public List<GameObject> playerSpawnPoints;
    public List<GameObject> instantiatedEnemyTanks;
    public List<GameObject> enemySpawnPoints;

    public int numPlayers;


    public float fxVol = .5f;
    public float musicVol = .5f;

    public int score;
    public Text highScore;
    public GameObject gameOver;
    public GameObject finalScore;
    // Runs before any Start() functions run
    void Awake()
    {
        //Makes sure there is only one GameManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("ERROR: There can only be one GameManager.");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    public void SpawnPlayer(GameObject spawnpoint)
    {
        instantiatedPlayerTank = Instantiate(playerTankPrefab, spawnpoint.transform.position, Quaternion.identity);
        SpawnCamera(spawnpoint);
        BackgroundMusic(spawnpoint);
    }

    public void SpawnCamera(GameObject spawnpoint)
    {
        icamera = Instantiate(camera, new Vector3(spawnpoint.transform.position.x, 52, spawnpoint.transform.position.z),
            new Quaternion(45, 0, 0, 45));
        Destroy(icamera.GetComponent<AudioListener>());
    }

    public void BackgroundMusic(GameObject spawnpoint)
    {
        imusic = Instantiate(music, spawnpoint.transform.position, Quaternion.identity);
    }
    public GameObject RandomSpawn(List<GameObject> SpawnPoints)
    {
        int spawnToGet = UnityEngine.Random.Range(0, SpawnPoints.Count - 1);
        return SpawnPoints[spawnToGet];
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemyTanks.Length; ++i)
        {
            if (i == 0)
            {

                GameObject tempRSpawn = enemySpawnPoints[0];
                GameObject instantiatedEnemyTank =
                    Instantiate(enemyTanks[i], tempRSpawn.transform.position, Quaternion.identity);
                GameManager.instance.enemySpawnPoints.Remove(tempRSpawn);
                instantiatedEnemyTanks.Add(instantiatedEnemyTank);
            }
            else
            {
                GameObject tempRSpawn = RandomSpawn(enemySpawnPoints);
                GameObject instantiatedEnemyTank =
                    Instantiate(enemyTanks[i], tempRSpawn.transform.position, Quaternion.identity);
                GameManager.instance.enemySpawnPoints.Remove(tempRSpawn);
                instantiatedEnemyTanks.Add(instantiatedEnemyTank);
            }
        }
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        finalScore.GetComponent<Text>().text = "Final Score: " + score;
        highScore.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("HighScore");

    }
}
