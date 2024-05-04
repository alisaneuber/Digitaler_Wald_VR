using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    //current animal (either Deer, Fox, Wolf; End)
	public static string activeAnimal = "";

    public GameObject firstAnimal;
    public GameObject secondAnimal;
    public GameObject thirdAnimal;

    public GameObject popup;
    public GameObject popupEnd;

    public GameObject foodPear;
    public GameObject foodSteak;
    public GameObject foodMushroom;

    //Scripts for animal movement
    Fox_Movement fox;
    Deer_Movement deer;
    Wolf_Movement wolf;

    //indicators for completed minigames (skeletonpuzzle, Soundguessing, foodguessing)
    private bool puzzleCompleted = false;
    private bool soundCompleted = false;
    private bool foodCompleted = false;

    void Start()
    {
        GameLogic.activeAnimal = "Deer";
        fox = FindObjectOfType<Fox_Movement>();
        deer = FindObjectOfType<Deer_Movement>();
        wolf = FindObjectOfType<Wolf_Movement>();
    }

    void Update()
    {
        switch(GameLogic.activeAnimal)
        {
        case "Deer":
            firstAnimal.gameObject.SetActive(true);
            secondAnimal.gameObject.SetActive(false);
            thirdAnimal.gameObject.SetActive(false);
            break;
        case "Fox":
            firstAnimal.gameObject.SetActive(false);
            secondAnimal.gameObject.SetActive(true);
            thirdAnimal.gameObject.SetActive(false);
            break;
        case "Wolf":
            firstAnimal.gameObject.SetActive(false);
            secondAnimal.gameObject.SetActive(false); 
            thirdAnimal.gameObject.SetActive(true);
            break;    
        case "End":
            //all three animals activa and walking around at the end
            firstAnimal.gameObject.SetActive(true);
            secondAnimal.gameObject.SetActive(true);
            thirdAnimal.gameObject.SetActive(true);
            fox.enableAI();
            deer.enableAI();
            wolf.enableAI();
            break;
        default:
            break;
        }
        
        if(puzzleCompleted && soundCompleted && foodCompleted)
        {
            //advances to next animal when all puzzles are solved
            switch(GameLogic.activeAnimal)
            {
            case "Deer":
                GameLogic.activeAnimal = "Fox";
                popup.gameObject.SetActive(true);
                resetFood();
                break;
            case "Fox":
                GameLogic.activeAnimal = "Wolf";
                popup.gameObject.SetActive(true);
                resetFood();
                break;
            case "Wolf":
                GameLogic.activeAnimal = "End";
                popupEnd.gameObject.SetActive(true);
                resetFood();
                break;    
            default:
                break;
            }
            puzzleCompleted = false;
            soundCompleted = false;
            foodCompleted = false;
        }
    }

    public void setPuzzlecompleted()
    {
        puzzleCompleted = true;
    }

    public void setSoundcompleted()
    {
        soundCompleted = true;
    }

    public void setFoodcompleted()
    {
        foodCompleted = true;
    }

    private void resetFood()
    {
        foodPear.transform.position = new Vector3(-48f,0.2f,0.3f);
        foodSteak.transform.position = new Vector3(-50f,0.1f,1.3f);
        foodMushroom.transform.position = new Vector3(-51.7f,0.1f,-1f);
    }
}
