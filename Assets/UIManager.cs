using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : UnitySingleton<UIManager>
{
    public GameObject cameraCanvas;
    public GameObject BattleHUD;
    public GameObject inventoryUI;
    public GameObject selectedCardParent;
    public List<CardScriptableObject> tempSelectedCards = new List<CardScriptableObject>();
    // Start is called before the first frame update

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
