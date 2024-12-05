using System.Collections;

using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float speedMultiplier = 2f; 
    public float boostDuration = 5f; 
    private PlayerMovement playerMovement; 

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found on the GameObject!");
        }
    }

    public void ActivateSpeedBoost()
    {
        if (playerMovement != null)
        {
            Debug.Log("Speed Boost Activated!");
            playerMovement.speed *= speedMultiplier; // increase speed
            Invoke("DeactivateSpeedBoost", boostDuration);
        }
    }

    private void DeactivateSpeedBoost()
    {
        if (playerMovement != null)
        {
            Debug.Log("Speed Boost Deactivated!");
            playerMovement.speed /= speedMultiplier; // reset speed
        }
    }
}
