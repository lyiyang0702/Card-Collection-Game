using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : UnitySingleton<GameManager>
{
    public int currentArea = 1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 5; i++)
        {
            LoadAreaScene(i);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void UnloadCurrentArea()
    {
        SceneManager.UnloadSceneAsync("Area" + currentArea.ToString() + "Scene");
    }
    public void LoadAreaScene(int areaID)
    {
        SceneManager.LoadScene("Area"+areaID.ToString()+"Scene",LoadSceneMode.Additive); 

    }

    public void SwitchAreaScene(int areaID)
    {
        currentArea = areaID;
        UIManager.Instance.levelMap.SetActive(false);
        UIManager.Instance.allLevelMaps[areaID-1].SetActive(true);
    }
}
