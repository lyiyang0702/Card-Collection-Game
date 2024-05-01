using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManger : MonoBehaviour
{
    public Camera cam;
    PlayerController player;
    public float lerpTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        player = PlayerController.Instance;
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CombatManager.Instance.battleState != BattleState.None) return;
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), lerpTime * Time.deltaTime);
        //cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
