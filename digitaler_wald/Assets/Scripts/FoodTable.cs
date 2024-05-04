using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodTable : MonoBehaviour
{
    public TMP_Text infoLabel;
    public AudioClip success, fail;
    public GameObject logic;
    //GameLogic food;
    private string currentFood;
    public AudioSource src;

    void Start()
    {
        //food = FindObjectOfType<GameLogic>();
    }

    void Update()
    {
        if(GameLogic.activeAnimal == "Deer") 
        {
            infoLabel.text = "Was frisst ein Hirsch?";  
        } else if (GameLogic.activeAnimal == "Fox") 
        {
            infoLabel.text = "Was frisst ein Fuchs?";
        } else if (GameLogic.activeAnimal == "Wolf")
        {
            infoLabel.text = "Was frisst ein Wolf?";
        }
    }

    public void insideBasket(string food) 
    {
        if(food == "Steak")
        {
            Debug.Log("Steak inside");
            currentFood = "Steak";
        } else if(food == "Pear")
        {
            Debug.Log("Pear inside");
            currentFood = "Pear";
        } else if(food == "Mushroom")
        {
            Debug.Log("Mushroom inside");
            currentFood = "Mushroom";
        }

        if(GameLogic.activeAnimal == "Deer" && currentFood == "Pear") {
            src.clip = success;
            logic.GetComponent<GameLogic>().setFoodcompleted();
        } else if(GameLogic.activeAnimal == "Fox" && currentFood == "Mushroom") {
            src.clip = success;
            logic.GetComponent<GameLogic>().setFoodcompleted();
        } else if(GameLogic.activeAnimal == "Wolf" && currentFood == "Steak") {
            src.clip = success;
            logic.GetComponent<GameLogic>().setFoodcompleted();
        } else 
        {
            src.clip = fail;
        }
        
        src.Play();
    }
}
