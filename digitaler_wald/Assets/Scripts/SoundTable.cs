using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundTable : MonoBehaviour
{
    public TMP_Text infoLabel;
    public Button buttonA;
    public Button buttonB;
    public Button buttonC;
    public AudioSource src;
    public AudioClip success, fail;
    public GameObject logic;

    void Start()
    {
        buttonA.onClick.AddListener(onClickA);
        buttonB.onClick.AddListener(onClickB);
        buttonC.onClick.AddListener(onClickC);
    }

    void Update()
    {
        if(GameLogic.activeAnimal == "Deer") 
        {
            infoLabel.text = "Welcher Ton passt zu einem Hirsch?";  
        } else if (GameLogic.activeAnimal == "Fox") 
        {
            infoLabel.text = "Welcher Ton passt zu einem Fuchs?";
        } else if (GameLogic.activeAnimal == "Wolf")
        {
            infoLabel.text = "Welcher Ton passt zu einem Wolf?";
        }
    }

    void onClickA()
    {
        if(GameLogic.activeAnimal == "Fox") 
        {
            src.clip = success;
            logic.GetComponent<GameLogic>().setSoundcompleted();
        } else 
        {
            src.clip = fail;
        }

        src.Play();
    }

    void onClickB()
    {
        if(GameLogic.activeAnimal == "Deer") 
        {
            src.clip = success;
            logic.GetComponent<GameLogic>().setSoundcompleted();
        } else 
        {
            src.clip = fail;
        }

        src.Play();
    }

    void onClickC()
    {
        if(GameLogic.activeAnimal == "Wolf") 
        {
            src.clip = success;
            logic.GetComponent<GameLogic>().setSoundcompleted();
        } else 
        {
            src.clip = fail;
        }
        src.Play();
    }
}
