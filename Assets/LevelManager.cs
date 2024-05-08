using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int areaID;
    public DoorInteractable doorToPreviousArea;
    public DoorInteractable doorToNextArea;
    public Transform playerSpawnPoint;
    private void Start()
    {
        UIManager.Instance.allLevelMaps.Add(gameObject);
        gameObject.SetActive(false);
        if(GameManager.Instance.currentArea == areaID)
        {
            gameObject.SetActive(true);
        }

        
    }
    void OnEnable()
    {
        UIManager.Instance.levelMap = gameObject;
        PlayerController.Instance.transform.position = playerSpawnPoint.position;
        if (doorToPreviousArea != null)
        {
            doorToPreviousArea.ToggleDoor(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        //UIManager.Instance.levelMap = null;
        if (doorToNextArea != null)
        {
            doorToNextArea.currentInteractState = Interactable.InteractState.CanInteract;
        }

        if (doorToPreviousArea != null)
        {
            doorToPreviousArea.currentInteractState = Interactable.InteractState.CanInteract;
        }

    }
}
