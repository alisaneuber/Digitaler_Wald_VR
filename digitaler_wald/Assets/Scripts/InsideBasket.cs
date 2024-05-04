using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideBasket : MonoBehaviour
{
    //FoodTable table;
    public GameObject table;

    void Start()
    {
        //table = FindObjectOfType<FoodTable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " entered basket");
        table.GetComponent<FoodTable>().insideBasket(other.name);
    }
}
