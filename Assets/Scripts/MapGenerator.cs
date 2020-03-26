using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    public enum MapType
    {
        Seeded,
        Random,
        MapOfTheDay
    }

    public MapType mapType = MapType.Random;

    public int mapSeed;

    public int rows;

    public int columns;

    private float roomWidth = 50f;

    private float roomHeight = 50f;

    public GameObject[] gridPrefabs;

    public Room[,] grid;
    // Start is called before the first frame update
    void Start()
    {
        //Switches the types of map generation
        switch (mapType)
        {
            case MapType.MapOfTheDay:
                mapSeed = DateToInt(DateTime.Now.Date);
                break;
            case MapType.Random:
                mapSeed = DateToInt(DateTime.Now);
                break;
            case MapType.Seeded:
                break;
            default:
                Debug.LogError("[MapGenerator] Type not Implemented");
                break;
        }
        GenerateGrid();
        //Creates the player and enemies in the map
        GameManager.instance.SpawnPlayer(GameManager.instance.RandomSpawn(GameManager.instance.playerSpawnPoints));
        GameManager.instance.SpawnEnemies();
    }

    //Returns a random room
    public GameObject RandomRoom()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }
    //Makes today's date an integer to use
    public int DateToInt(DateTime dateToUse)
    {
        // Add our date up and return it
        return dateToUse.Year + dateToUse.Month + 
               dateToUse.Day + dateToUse.Hour + 
               dateToUse.Minute + dateToUse.Second + 
               dateToUse.Millisecond;
    }
    //Creates the map
    public void GenerateGrid()
    {
        //Use a seed as determined by the drop down menu
        UnityEngine.Random.seed = mapSeed;
        //Creates an empty grid using the designer's instructions
        grid = new Room[columns,rows];
        //For each row...
        for (int row = 0; row < rows; row++)
        {
            //For each column...
            for (int column = 0; column < columns; column++)
            {
                //The x and z positions of each room are next to each other locally
                float xPos = roomWidth * column;
                float zPos = roomHeight * row;
                Vector3 newPos = new Vector3(xPos, 0, zPos);

                //Creates a random room at that x and y coordinate
                GameObject tempObject = Instantiate(RandomRoom(), newPos, Quaternion.identity) as GameObject;
                tempObject.transform.parent = this.transform;
                //Names the room accordingly
                tempObject.name = "Room_" + row + "," + column;
                Room tempRoom = tempObject.GetComponent<Room>();

                //Lines 94-121 open the doors of the room if they are inside the map and closes them if they are outside the map
                if (row == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (row == rows - 1)
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }


                if (column == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (column == columns - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }
                //Creates the new map
                grid[column, row] = tempRoom;
            }
        }
    }
}
