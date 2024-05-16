using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    public GameObject player;
    public GameObject tutorialUI;
    public TextMeshProUGUI TutorialBattleText;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tutorialUI.SetActive(false);
            player.GetComponent<PlayerController>().enabled = true;
        }

        

    }

}
