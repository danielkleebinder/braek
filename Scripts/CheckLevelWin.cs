using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckLevelWin : MonoBehaviour
{
    public string playerName = "PLAYER";
    private MenuController MenuControllerSkript;

    void Start()
    {
        GameObject UI        = GameObject.Find("UI");
        MenuControllerSkript = (MenuController)UI.GetComponent(typeof(MenuController));
    }

    // Update is called once per frame
    void Update()
    {
        if (!ExistBricks(transform, "Brick"))
        {
            GameSettings.SetWinnerName(playerName);
            MenuControllerSkript.activateWinScreen();
        }
    }

    bool ExistBricks(Transform transform, string name)
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag(name))
            {
                return true;
            }

            if (ExistBricks(child, name))
            {
                return true;
            }
        }
        return false;
    }
}
