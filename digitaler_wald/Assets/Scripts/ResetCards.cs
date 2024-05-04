using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetCards : MonoBehaviour
{
    public GameObject[] cards;
    public GameObject[] sockets;
    private Vector3[] initialPositions;
    private new Quaternion[] initialRotations;
    private float[] startPos;

    void Start()
    {
        initialPositions = new Vector3[cards.Length]; 
        initialRotations = new Quaternion[cards.Length];
        
        // Loop through each card object and store its initial position
        for (int i = 0; i < cards.Length; i++)
        {
            initialPositions[i] = cards[i].transform.position;
            initialRotations[i] = cards[i].transform.rotation;
            Debug.Log(cards[i].name + " initialized");  
        }
    }

    public void resetCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            foreach (var socket in sockets)
            {
                socket.gameObject.SetActive(false);
            }

            cards[i].transform.position = initialPositions[i];
            cards[i].transform.rotation = initialRotations[i];
            Debug.Log(cards[i].name + " reset");

            foreach (var socket in sockets)
            {
                socket.gameObject.SetActive(true);
            }
        }
    }
}
