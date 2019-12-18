using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragableUI : MonoBehaviour
{
    const string BUTTON_CLOSE_TAG = "ButtonClose";
    const string BUTTON_PIN_TAG = "ButtonPin";
    Button ButtonClose;
    Button ButtonPin;
    public Text stats;
    bool statsChange;
    bool pinned;
    bool pinChange;
    bool active;
    bool activeChange;
    public Color pinnedColor;
    public Color unpinnedColor;
    public GamePiece parentGamePiece;
    //public GamePiece gamePiece;
    void Start()
    {
        pinned = false;
        active = true;
        pinChange = false;
        activeChange = false;
        //Debug.Log(GetComponentInParent<Transform>().transform.name);
        parentGamePiece = GetComponentInParent<GamePiece>();
       // Debug.Log("here!" + parentGamePiece.transform.gameObject.name);
        Button[] buttons = transform.GetComponentsInChildren<Button>();
        foreach (Button b in buttons)
        {
            if (b.tag == BUTTON_CLOSE_TAG)  //TODO tag it instead of name - switching
            {
                ButtonClose = b;
            }
            if (b.tag == BUTTON_PIN_TAG)
            {
                ButtonPin = b;
            }
        }
        parentGamePiece.UIPinned = pinned;
        parentGamePiece.UIActive = active;
        stats.text = "Energy: " + parentGamePiece.energy + "/" + parentGamePiece.energyCapacity + ((parentGamePiece.energyProduction >= 0) ? " +" : " -") + parentGamePiece.energyProduction + "\n";
        stats.text += "Pressure: " + parentGamePiece.pressure + "/" + parentGamePiece.pressureMax + "\n";
        stats.text += "Heat: " + parentGamePiece.heat + ((parentGamePiece.heatRate > 0)?" +":" -") + parentGamePiece.heatRate + " (" + parentGamePiece.heatPoint + "/" + parentGamePiece.heatCapacity + ")\n";
        stats.text += "Weight: " + parentGamePiece.weight + "\n";
        stats.text += "HP: " + parentGamePiece.HP + "/" + parentGamePiece.maxHP + "\n";
        stats.text += "Armor: " + parentGamePiece.armor + "\n";
    }

    // Update is called once per frame
    void Update()
    {
        if (pinChange)
        {
            pinChange = false;
            if (pinned)
            {
                pinned = false;
                parentGamePiece.UIPinned = false;
            }
            else
            {
                pinned = true;
                parentGamePiece.UIPinned = true;
                /*
                ColorBlock cb = ButtonPin.colors;
                cb.normalColor = pinnedColor;
                cb.highlightedColor = pinnedColor;
                cb.selectedColor = pinnedColor;
                cb.disabledColor = pinnedColor;
                cb.pressedColor = pinnedColor;
                ButtonPin.colors = cb;
                */
            }
        }
        if (activeChange)
        {
            activeChange = false;
            if (active)
            {
                active = false;
                parentGamePiece.UIActive = false;
                transform.gameObject.SetActive(false);
            }
            else
            {
                active = true;
                parentGamePiece.UIActive = true;
                transform.gameObject.SetActive(true);
            }
        }
    }

    public GamePiece getGamePiece()
    {
        return GetComponentInParent<Transform>().GetComponentInParent<GamePiece>();
    }
    

    public bool isPinned()
    {
        return pinned;
    }

    public bool isActive()
    {
        return active;
    }

    public void SetPinned(bool p)
    {
        Debug.Log("SetPinned " + ((p)?"true":"false"));
        if (p != pinned)
        {
            Debug.Log("change");
            pinChange = true;
        }
    }

    public void SetActive(bool p)
    {
        Debug.Log("SetActive " + ((p) ? "true" : "false"));
        if (p != active)
        {
            Debug.Log("change");
            activeChange = true;
        }
    }
}
