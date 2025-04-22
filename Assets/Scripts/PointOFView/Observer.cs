using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public GameObject shieldVisual; // Assign your shield GameObject in the inspector
    public bool shieldIsActive = false;

    // Shield Sound
    private AudioSource audioSource;
    public AudioClip warningSound;
    private bool hasPlayedSound = false;  // Ensure sound playes once 

    public Transform player;
    public GameEnding gameEnding;

    bool m_IsPlayerInRange;



    void Start()
    {
        // initialized audio 
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
            hasPlayedSound = false;  // Reset sound
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
            hasPlayedSound = false;  // Reset sound
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange && !hasPlayedSound)  // Only play shield sound if it hasn't been played yet
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    // Shield Check
                    ShieldActivate shield = player.GetComponent<ShieldActivate>();  

                    // Check if the shield is active
                    if (shield != null && shield.IsShieldActive())
                    {
                        Debug.Log("Shield active, playing warning sound"); 

                        // Play sound only once
                        audioSource.PlayOneShot(warningSound);
                        hasPlayedSound = true;  // Prevent further sound playing

                        // Break shield 
                        StartCoroutine(HandleShieldBreak(shield));
                    }
                    // If no shield, you die :(
                    else
                    {
                        Debug.Log("Player takes damage");
                        gameEnding.CaughtPlayer();
                    
                    }
                }
            }
        }
    }

    // Coroutine to break shield after 3 seconds
    private IEnumerator HandleShieldBreak(ShieldActivate shield)
    {
        Debug.Log("Shield is breaking");

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // then, break the shield
        shield.BreakShield();
        Debug.Log("Shield is  fully broken");

        // Deactivate the shield visual
        shieldVisual.SetActive(false);
    }
}