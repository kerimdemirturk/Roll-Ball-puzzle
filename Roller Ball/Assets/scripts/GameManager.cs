using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    
    private GroundControl[] groundParts;

    void Start()
    {
        SetupNewLevel();
    }
    private void SetupNewLevel()
    {
        groundParts = FindObjectsOfType<GroundControl>();


    }
    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else if(singleton != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinihedLoading;
    }
    private void OnLevelFinihedLoading(Scene scene , LoadSceneMode mode )
    {
        SetupNewLevel();
    }
    public void checkGroundComplete()
    {
        bool isFinished = true;

        for (int i =  0; i < groundParts.Length; i++)
        {
            if(groundParts[i].isÝtRolling == false)
            {
                isFinished = false;
                break;
            }
        }
        if(isFinished)
        {
            nextLevel();
        }
        

    }
    private void nextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex==4)
        {
            SceneManager.LoadScene(0);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
