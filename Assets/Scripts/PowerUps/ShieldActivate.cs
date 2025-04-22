using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActivate : MonoBehaviour
{
    public GameObject shieldVisual; // Assign your shield GameObject in the inspector
    public bool shieldIsActive = false;

    // Sound
    private AudioSource audioSource; 
    public AudioClip activationSound;
    public AudioClip breakSound;

    // Start is called before the first frame update
    void Start()
    {
        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to enable the shield
    public void EnableShield()
    {
        shieldIsActive = true;
        if (shieldVisual != null)
        {
            // Activate the shield visual
            shieldVisual.SetActive(true);  

            // Play activation sound
            if (activationSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(activationSound);
            }
        }
        Debug.Log("Shield is Active");
    }

    // Method to break the shield
    public void BreakShield()
    {
        if (!shieldIsActive) return; // Already inactive, do nothing
        shieldIsActive = false;
        if (shieldVisual = null)
        {
            // Deactivate the shield visual
            shieldVisual.SetActive(false);
        }
        // Deactivate the shield visual
        audioSource.PlayOneShot(breakSound);
        Debug.Log("Shield is broken");

    }

    // Method to check if the shield is active
    public bool IsShieldActive()
    {
        return shieldIsActive;
    }
}