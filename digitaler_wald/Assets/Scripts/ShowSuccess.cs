using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSuccess : MonoBehaviour
{
    public GameObject cardsFirstAnimal;
    public GameObject cardsSecondAnimal;
    public GameObject cardsThirdAnimal;
    public GameObject puzzleFirstAnimal;
    public GameObject puzzleSecondAnimal;
    public GameObject puzzleThirdAnimal;
    public GameObject logic;
    public GameObject checkmark;
    public AudioSource src;

    //different backgrounds for 6 cards or 9 cards
    public GameObject UI;
    public Sprite ui0;
    public Sprite ui9;
    public Sprite ui6;
    
    //variables for storing if all 6/9 cards are in right spot
    private bool is00 = false;
    private bool is01 = false;
    private bool is02 = false;
    private bool is10 = false;
    private bool is11 = false;
    private bool is12 = false;
    private bool is20 = false;
    private bool is21 = false;
    private bool is22 = false;    

    void Update()
    {
        if (GameLogic.activeAnimal == "Deer") 
        {
            puzzleFirstAnimal.gameObject.SetActive(true);
            cardsFirstAnimal.gameObject.SetActive(true);
            puzzleSecondAnimal.gameObject.SetActive(false);
            cardsSecondAnimal.gameObject.SetActive(false);
            puzzleThirdAnimal.gameObject.SetActive(false);
            cardsThirdAnimal.gameObject.SetActive(false);
            UI.GetComponent<Image>().sprite = ui9;
        }

        if (GameLogic.activeAnimal == "Fox") 
        {
            puzzleFirstAnimal.gameObject.SetActive(false);
            cardsFirstAnimal.gameObject.SetActive(false);
            puzzleSecondAnimal.gameObject.SetActive(true);
            cardsSecondAnimal.gameObject.SetActive(true);
            puzzleThirdAnimal.gameObject.SetActive(false);
            cardsThirdAnimal.gameObject.SetActive(false);
            UI.GetComponent<Image>().sprite = ui6;
        }

        if (GameLogic.activeAnimal == "Wolf") 
        {
            puzzleFirstAnimal.gameObject.SetActive(false);
            cardsFirstAnimal.gameObject.SetActive(false);
            puzzleSecondAnimal.gameObject.SetActive(false);
            cardsSecondAnimal.gameObject.SetActive(false);
            puzzleThirdAnimal.gameObject.SetActive(true);
            cardsThirdAnimal.gameObject.SetActive(true);
            UI.GetComponent<Image>().sprite = ui6;
        }

        if (GameLogic.activeAnimal == "End") 
        {
            //all puzzles disabled at the end
            puzzleFirstAnimal.gameObject.SetActive(false);
            cardsFirstAnimal.gameObject.SetActive(false);
            puzzleSecondAnimal.gameObject.SetActive(false);
            cardsSecondAnimal.gameObject.SetActive(false);
            puzzleThirdAnimal.gameObject.SetActive(false);
            cardsThirdAnimal.gameObject.SetActive(false);
            checkmark.gameObject.SetActive(false);
            UI.GetComponent<Image>().sprite = ui0;
        }

        checkTicker();
    }

    void checkTicker()
    {
        bool check = puzzleSolved();
        if (check)
        {
            checkmark.gameObject.SetActive(true);
            src.Play();
            logic.GetComponent<GameLogic>().setPuzzlecompleted();
            resetPuzzle();
        }
    }

    public void cardEntered(string cardName)
    {
        switch (cardName)
        {
        case "card 00":
            is00 = true;
            break;
        case "card 01":
            is01 = true;
            break;
        case "card 02":
            is02 = true;
            break;
        case "card 10":
            is10 = true;
            break;
        case "card 11":
            is11 = true;
            break;
        case "card 12":
            is12 = true;
            break;
        case "card 20":
            is20 = true;
            break;
        case "card 21":
            is21 = true;
            break;
        case "card 22":
            is22 = true;
            break;
        default:
            break;
        }
        checkmark.gameObject.SetActive(false);
    }

    public void cardExited(string cardName)
    {
        switch (cardName)
        {
        case "card 00":
            is00 = false;
            break;
        case "card 01":
            is01 = false;
            break;
        case "card 02":
            is02 = false;
            break;
        case "card 10":
            is10 = false;
            break;
        case "card 11":
            is11 = false;
            break;
        case "card 12":
            is12 = false;
            break;
        case "card 20":
            is20 = false;
            break;
        case "card 21":
            is21 = false;
            break;
        case "card 22":
            is22 = false;
            break;
        default:
            break;
        }
    }

    public void resetPuzzle()
    {
        is00 = false;
        is01 = false;
        is02 = false;
        is10 = false;
        is11 = false;
        is12 = false;
        is20 = false;
        is21 = false;
        is22 = false;
    }

    bool puzzleSolved()
    {
        if(GameLogic.activeAnimal == "Deer")
        {
            if(is00 && is01 && is02 && is10 && is11 && is12 && is20 && is21 && is22)
            {
                return true;
            } else {
                return false;
            }
        } else if((GameLogic.activeAnimal == "Fox") || (GameLogic.activeAnimal == "Wolf"))
        {
            if(is00 && is01 && is02 && is10 && is11 && is12 )
            {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }
}
