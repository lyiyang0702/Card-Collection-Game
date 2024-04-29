using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManger : MonoBehaviour
{
    public Camera cam;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
