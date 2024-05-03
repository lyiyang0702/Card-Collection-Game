using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager>
{
    public GameObject playerPrefab;
    public Transform playerSpawner;
    public GameObject mainCamera;
    public GameObject Canvas;
    public GameObject combatManager;

    // Start is called before the first frame update
    void Start()
    {
        //if (GameObject.FindGameObjectWithTag("Player") == null)
        //{
        //    var player = Instantiate(playerPrefab);
        //    player.transform.position = playerSpawner.position;
        //    CombatManager.Instance.playerPrefab = player;
        //}
        //if (GameObject.FindGameObjectWithTag("MainCamera") == null)
        //{
        //    Instantiate(mainCamera);
        //}
        //if (GameObject.FindGameObjectWithTag("UI") == null)
        //{
        //    Instantiate(Canvas).transform.position = Vector3.zero;
        //}
        
        //    Instantiate(combatManager).transform.position = Vector3.zero;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
