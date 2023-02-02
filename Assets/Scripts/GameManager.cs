using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    private GameObject player;
    
    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is null :(");
            }

            return _instance;
        }
    }

    public int collectedCoins = 0;

    public bool gameOver
    {
        set
        {
            _gameOver = value;
            if (_gameOver)
            {
                Destroy(player);
                player = Instantiate(playerPrefab);
                tpToCheckpoint(currentCheckpoint);
            }
        }
        get { return _gameOver; }
    }

    public int currentCheckpoint = 0;

    private bool _gameOver = false;

    bool tpToCheckpoint(int number)
    {
        bool found = false;
        var scene = SceneManager.GetActiveScene();
        GameObject[] rootGameObjects = scene.GetRootGameObjects();
        foreach (GameObject rootGameObject in rootGameObjects)
        {
            Checkpoint checkpoint;
            for (int i = 0; i < rootGameObject.transform.childCount; i++)
            {
                GameObject gameObject = rootGameObject.transform.GetChild(i).gameObject;
                if (gameObject.TryGetComponent<Checkpoint>(out checkpoint))
                {
                    if (checkpoint.checkpoint == number)
                    {
                        _gameOver = false;
                        player.transform.parent = gameObject.transform.parent.parent;
                        player.transform.position = checkpoint.transform.position;
                        found = true;
                    }

                }
                if (found)
                {
                    break;
                }
            }
            if (found)
            {
                break;
            }
            if (rootGameObject.TryGetComponent<Checkpoint>(out checkpoint))
            {
                if (checkpoint.checkpoint == number)
                {
                    _gameOver = false;
                    player.transform.parent = rootGameObject.transform;
                    player.transform.position = checkpoint.transform.position;
                    break;
                }
            }
        }
        // if (found == false)
        // {
        //     GameObject newPlayer = Instantiate(playerPrefab);
        //     newPlayer.transform.parent = scene.GetRootGameObjects()[0].transform;
        // }
        return found;
    }
    
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        player = Instantiate(playerPrefab);
        tpToCheckpoint(0);
    }
}