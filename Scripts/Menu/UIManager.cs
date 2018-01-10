using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject optionsScreen;
    [SerializeField]
    private GameObject camera1;
    [SerializeField]
    private GameObject camera2;
    private GameObject otherScreen;
    private Button backButton;


    // Use this for initialization
    void Start()
    {
       Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void openOptions(GameObject callingPanel)
    {
        otherScreen = callingPanel;

        otherScreen.SetActive(false);
        optionsScreen.SetActive(true);

        backButton = GameObject.Find("backButtonOfOptions").GetComponent(typeof(Button)) as Button;

        backButton.onClick.AddListener(returnToOtherScreen);
    }

    void returnToOtherScreen()
    {
        otherScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }

    public void SetClassicCamera(bool cameraClassic)
    {
        GameSettings.cameraClassic = cameraClassic;
    }

    void setCamera(int settingNr)
    {
        switch (settingNr)
        {
            case 1:
                camera1.SetActive(false);
                camera2.SetActive(true);
                break;

            case 2:
                camera1.SetActive(true);
                camera2.SetActive(false);
                break;
        }
    }

}

