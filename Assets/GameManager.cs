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
        UIManager.Instance.allLevelMaps[areaID - 1].GetComponent<LevelManager>().SpawnPlayer();
    }

    //public void UpdateInventorySizeBasedOnArea(int areaID)
    //{
    //    switch (areaID)
    //    {
    //        case 1:
    //            PlayerController.Instance.inventory.size = 40;
    //            break;
    //        case 2:
    //            PlayerController.Instance.inventory.size = 50;
    //            break;
    //        case 3:
    //            PlayerController.Instance.inventory.size = 60;
    //            break;
    //        case 4:
    //            PlayerController.Instance.inventory.size = 80;
    //            break;

    //    }

    //}
}
