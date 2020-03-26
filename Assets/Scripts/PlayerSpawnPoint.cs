using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.instance.playerSpawnPoints.Add(this.gameObject);
    }

    void OnDestroy()
    {
        GameManager.instance.playerSpawnPoints.Remove(this.gameObject);
    }
}
