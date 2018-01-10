using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public GameObject PauseScreen;
    public GameObject WinScreen;

	  // Use this for initialization
	  void Start ()
    {
        PauseScreen.SetActive(false);
        WinScreen.SetActive(false);
    }
	
	  // Update is called once per frame
	  void Update ()
    {
	      if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseScreen.gameObject.activeInHierarchy == false)
            {
                pauseGame(true);
            }
            else
            {
                resumeGame(true);
            }
        }
	  }

    public void activateWinScreen()
    {
        WinScreen.SetActive(true);
        pauseGame(false);
    }

    public void LoadByIndex(int sceneIndex)
    {
         UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void resumeGame (bool isPauseScreen)
    {
        if (isPauseScreen)
        {
            PauseScreen.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    void pauseGame(bool isPauseScreen)
    {
        if (isPauseScreen)
        {
          PauseScreen.SetActive(true);
        }
        Time.timeScale = 0f;
    }



}