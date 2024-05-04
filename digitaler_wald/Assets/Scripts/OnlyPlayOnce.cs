using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyPlayOnce : MonoBehaviour
{
    private AudioSource audioSource;
    private bool canInteract = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if(canInteract)
        {
            audioSource.Play();
        }
        
        canInteract = false;
        Invoke("ResetInteractability", 10f); // Cool off time of 10 seconds
    }

    private void ResetInteractability()
    {
        canInteract = true;
    }
}
