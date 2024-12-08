using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManger : MonoBehaviour
{
    public Camera cam;
    PlayerController player;
    public float lerpTime = 0.2f;
    public bool shouldLerp = true;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.Instance;
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CombatManager.Instance.battleState != BattleState.None) return;
        if (shouldLerp)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), lerpTime * Time.deltaTime);
        }
        else
        {
            cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }

    }
}
