using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject options;
    public GameObject mainmenu;
    public GameObject playerSelect;

    public AudioSource audio;

    public GameObject fxSliderObject;
    public Slider fxRealSlider;
    public float fxSliderVol = .5f;

    public GameObject musicSliderObject;
    public Slider musicRealSlider;
    public float musicSliderVol = .5f;

    public Dropdown mapChoice;
    public GameObject generator;
    private void Start()
    {
        //Starts off with the main menu active
        options.gameObject.SetActive(false);
        mainmenu.gameObject.SetActive(true);

        generator = GameObject.Find("MapGenerator");
        audio = GameObject.Find("Click").GetComponent<AudioSource>();
    }
    public void StartGame()
    {
        audio.Play();
        //Creates the player and enemies in the map
        GameManager.instance.SpawnPlayer(GameManager.instance.RandomSpawn(GameManager.instance.playerSpawnPoints));
        GameManager.instance.SpawnEnemies();

        //Starts the game and hides all of the menus
        playerSelect.SetActive(false);

        //Destroys unneeded objects
        Destroy(GameObject.Find("Menu Music"));
        Destroy(GameObject.Find("Initial Camera"));
        Destroy(GameObject.Find("Camera"));

    }

public void quitGame()
    {
        Application.Quit();
    }

    public void OptionsMenu()
    {
        audio.Play();
        mainmenu.SetActive(false);
        options.SetActive(true);
    }

    public void BackToMenu()
    {
        audio.Play();

        //Grabs the value of the sliders once the player is done messing with the settings
        fxSliderObject = GameObject.FindWithTag("FX Slider");
        fxRealSlider = fxSliderObject.GetComponent<Slider>();
        fxSliderVol = fxRealSlider.value;
        
        musicSliderObject = GameObject.FindWithTag("Music Slider");
        musicRealSlider = musicSliderObject.GetComponent<Slider>();
        musicSliderVol = musicRealSlider.value;
       
        //Dropdown menu gives the choice of motd or random map
        mapChoice = GameObject.Find("Map Choice").GetComponent<Dropdown>();
        generator.GetComponent<MapGenerator>().MapChoice(mapChoice.value);

        options.SetActive(false);
        mainmenu.SetActive(true);
    }
    private void Update()
    {
        //Always updates the game manager with the current value of the sliders
        GameManager.instance.fxVol = fxSliderVol;
        GameManager.instance.musicVol = musicSliderVol;
    }
    public void Restart()
    {
        audio.Play();
        //Reloads the scene
        SceneManager.LoadScene("SampleScene");
    }

    public void AlmostThere()
    {
        audio.Play();
        mainmenu.SetActive(false);
        playerSelect.SetActive(true);
    }
    public void TwoPlayer()
    {
        audio.Play();
        //If it's a two player game, switch to two player scene
        SceneManager.LoadScene("SplitScreenPrototype");
    }
}
