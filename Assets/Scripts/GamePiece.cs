using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePiece : MonoBehaviour
{
    public const string DRAGGABLE_TAG = "UIDraggable";
    public bool UIPinned;
    public bool UIActive;
    public GameObject UIPanel;
    public DragableUI dragableUI;
    public int energy, energyCapacity, energyProduction;
    public int pressure, pressureMax;
    public int heat, heatPoint, heatCapacity, heatRate;
    public int weight, HP, maxHP, armor;
    public bool controllable;
    public bool scrapHarvester;
    public bool scrapSource;

    public class ResourceClass
    {
        public int Health, currentHealth;
        public int Armor, currentArmor;
        public int Weight, currentWeight;
        public int Scrap, currentScrap;
        public int Energy, currentEnergy, energyGeneration, energyDissipation, energyCapacity;
        public int Pressure, currentPressure, pressureCapacity;
        public int Heat, currentHeat, heatGenetation, heatDissipation, heatSpot, heatLimit, heatDeath;
    }

    public ResourceClass resources;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] tr = GetComponentInParent<Transform>().GetComponentsInChildren<Transform>();
        foreach (Transform t in tr)
        {
            if (t.tag == DRAGGABLE_TAG)
            {
                UIPanel = t.gameObject;
                break;
            }
        }
        if (UIPanel == null) Debug.Log("No panel found!!!");
        if (UIPanel != null) //Debug.Log(UIPanel.name);
        dragableUI = UIPanel.GetComponent<DragableUI>();
        if (dragableUI == null) Debug.Log("No dragable script found!!!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClick()
    {
        if (UIPinned)
        {
            Debug.Log("UI is pinned, cant do anything");
        }
        else
        {
            if (UIActive == true)
            {
                Debug.Log("UI is active, input false");
                dragableUI.SetActive(false);
                UIActive = false;
            }
            else
            {
                Debug.Log("UI is inactive, input true");
                UIPanel.SetActive(true);
                dragableUI.SetActive(true);
                UIActive = true;
            }
        }
    }

    public void pinPanel()
    {
        dragableUI.SetPinned(!UIPinned);
    }

    public bool isPinned()
    {
        return UIPinned;
    }

}
