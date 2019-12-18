using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    private int turnCount = 1;
    public Text turnCountText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turnCountText.text = "TURN COUNT : " + turnCount;
        //if () button here
    }
}
