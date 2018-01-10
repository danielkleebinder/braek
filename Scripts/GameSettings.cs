using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static bool cameraClassic = true;
    public static string winnerName = "PLAYER";
    public static string playerOneName = "Player 1";
    public static string playerTwoName = "Player 2";

    public static void SetClassicCamera(bool cameraClassic)
    {
        GameSettings.cameraClassic = cameraClassic;
    }

    public static void SetWinnerName(string winnerName)
    {
        GameSettings.winnerName = winnerName.ToUpper();
    }
}
