using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenVisualizer : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GameObject.Find("WinnerName").GetComponent<Text>().text = GameSettings.winnerName;
    }
}
