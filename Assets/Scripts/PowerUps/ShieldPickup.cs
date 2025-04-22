using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        // if object hits player tagged object 
        if (other.CompareTag("Player"))
        {
            // enable shield script
            ShieldActivate playerShield = other.GetComponent<ShieldActivate>();
            if (playerShield != null)
            {
                playerShield.EnableShield();
            }
            // remove shield orb
            Destroy(gameObject); 
        }
    }
    
}
