using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{
    public Sprite open;
    public Sprite close;
    public SpriteRenderer sprite;
    public int areaNum = 2;
    public override void InteractAction()
    {
        base.InteractAction();
        GameManager.Instance.SwitchAreaScene(areaNum);
        ToggleDoor(true);
    }

    public void ToggleDoor(bool isOpend)
    {
        if (isOpend)
        {
            sprite.sprite = open;
        }
    }
}
