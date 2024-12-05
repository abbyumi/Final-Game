using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player; // Reference to the player
    public GameEnding gameEnding; // Reference to the game-ending script

    private bool m_IsPlayerInRange; // Tracks if the player is within the trigger range
    private bool ignorePlayer; // Tracks if the player is invisible

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player && !ignorePlayer)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange && !ignorePlayer)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }

    // Method to make the enemy ignore the player
    public void IgnorePlayer(bool shouldIgnore)
    {
        ignorePlayer = shouldIgnore;

        if (shouldIgnore)
        {
            Debug.Log(gameObject.name + " is now ignoring the player.");
            m_IsPlayerInRange = false; // Immediately stop tracking
        }
        else
        {
            Debug.Log(gameObject.name + " can now detect the player.");
        }
    }
}
