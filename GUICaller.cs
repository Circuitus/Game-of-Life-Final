using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUICaller : MonoBehaviour
{
    public GameObject GameName;
    public GameObject InUnity;
    public GameObject GridImage;
    public GameObject ControlsButton;
    public GameObject RulesButton;
    public GameObject ReturnButton;
    public GameObject BeginGame;
    public GameObject ResumeGame;
    public GameObject Rules;
    public GameObject Controls;
    bool gameStarted;

    //startup GUI
    void Start()
    {
        GameName.SetActive(true);
        InUnity.SetActive(true);
        GridImage.SetActive(true);
        ControlsButton.SetActive(true);
        RulesButton.SetActive(true);
        BeginGame.SetActive(true);
        ResumeGame.SetActive(false);
        Rules.SetActive(false);
        Controls.SetActive(false);
        ReturnButton.SetActive(false);
        gameStarted = false;
    }

    //closes GUI
    public void onPlay()
    {
        GameName.SetActive(false);
        InUnity.SetActive(false);
        GridImage.SetActive(false);
        ControlsButton.SetActive(false);
        RulesButton.SetActive(false);
        BeginGame.SetActive(false);
        ResumeGame.SetActive(false);
        Rules.SetActive(false);
        Controls.SetActive(false);
        ReturnButton.SetActive(false);
        gameStarted = true;
    }

    //Shows the controls list
    public void ControlsList()
    {
        GameName.SetActive(false);
        InUnity.SetActive(false);
        GridImage.SetActive(true);
        ControlsButton.SetActive(false);
        RulesButton.SetActive(false);
        BeginGame.SetActive(false);
        ResumeGame.SetActive(false);
        Rules.SetActive(false);
        Controls.SetActive(true);
        ReturnButton.SetActive(true);
    }

    //shows the rules list
    public void RulesList()
    {
        GameName.SetActive(false);
        InUnity.SetActive(false);
        GridImage.SetActive(true);
        ControlsButton.SetActive(false);
        RulesButton.SetActive(false);
        BeginGame.SetActive(false);
        ResumeGame.SetActive(false);
        Rules.SetActive(true);
        Controls.SetActive(false);
        ReturnButton.SetActive(true);
    }

    //back to main pause menu
    public void ReturnToPause()
    {
        GameName.SetActive(true);
        InUnity.SetActive(true);
        GridImage.SetActive(true);
        ControlsButton.SetActive(true);
        RulesButton.SetActive(true);
        if (gameStarted == false)
        {
            BeginGame.SetActive(true);
            ResumeGame.SetActive(false);
        }
        else
        {
            BeginGame.SetActive(false);
            ResumeGame.SetActive(true);
        }
        Rules.SetActive(false);
        Controls.SetActive(false);
        ReturnButton.SetActive(false);
        
    }

    //in case esc is pushed
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) == true)
        {
            ReturnToPause();
        }
    }

    // Start is called before the first frame update
    /*void Start()
    {
        GameName.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }*/
}
