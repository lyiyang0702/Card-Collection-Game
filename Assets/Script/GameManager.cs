using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : UnitySingleton<GameManager>
{
    public int currentArea = 1;
    public bool isDebug = false;
    // Start is called before the first frame update
    void Start()
    {
        LoadAllScenes(5);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void UnloadScene(int areaID)
    {
        SceneManager.UnloadSceneAsync("Area" + areaID.ToString() + "Scene");
    }
    public void LoadAreaScene(int areaID)
    {
        SceneManager.LoadScene("Area"+areaID.ToString()+"Scene",LoadSceneMode.Additive); 

    }

    public void SwitchAreaScene(int areaID)
    {
        currentArea = areaID;
        UIManager.Instance.levelMap.SetActive(false);
        UIManager.Instance.allLevelMaps[areaID].SetActive(true);
        UIManager.Instance.allLevelMaps[areaID].GetComponent<LevelManager>().SpawnPlayer();
        //UIManager.Instance.allLevelMaps[areaID-1].SetActive(true);
        //UIManager.Instance.allLevelMaps[areaID - 1].GetComponent<LevelManager>().SpawnPlayer();
    }

    void LoadAllScenes(int areaCount)
    {
        for (int i = 0; i < areaCount; i++)
        {
            LoadAreaScene(i);
        }
    }

    void UnloadAllScene(int areaCount)
    {
        for (int i = 0;i < areaCount; i++)
        {
            UnloadScene(i); 
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
