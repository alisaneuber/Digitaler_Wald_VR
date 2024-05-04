using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Snap_CheckIfRightCard : MonoBehaviour
{
    GameObject enteredCard;
    XRSocketInteractor socket;
    AudioSource audioData;
    public GameObject checkCard;
    ShowSuccess success;
    public GameObject snap;
    //Renderer rend;
    string cardName;

    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        audioData = GetComponent<AudioSource>();
        success = FindObjectOfType<ShowSuccess>();
        //rend = snap.GetComponent<Renderer>();
        
    }

    void checkSocket()
    {
        IXRSelectInteractable currentSocket = socket.GetOldestInteractableSelected();
        enteredCard = currentSocket.transform.gameObject;
        cardName = enteredCard.name;
    }

    public void checkIfRightCard()
    {
        snap.GetComponent<Renderer>().enabled = false;
        checkSocket();
        //string cardName = enteredCard.name;
        if (enteredCard == checkCard)
        {
            audioData.Play(0);
            success.cardEntered(cardName);
        }
    } 

    public void exitCard()
    {
        snap.GetComponent<Renderer>().enabled = true;
        success.cardExited(cardName);
    } 
}
