using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldDuration = 3f; 
    private bool isShieldActive = false;
    private Renderer playerRenderer;
    private Color originalColor;

    void Start()
    { 
        playerRenderer = GetComponentInChildren<Renderer>();
        if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color; 
        }
        else
        {
            Debug.LogError("No Renderer found on JohnLemon or its children!");
        }
    }

    public void ActivateShield()
    {
        if (!isShieldActive && playerRenderer != null)
        {
            StartCoroutine(ShieldCoroutine());
        }
    }

    private IEnumerator ShieldCoroutine()
    {
        Debug.Log("Shield Activated!");
        isShieldActive = true;

        if (playerRenderer != null)
        {
            playerRenderer.material.color = Color.yellow;
        }

        yield return new WaitForSeconds(shieldDuration); 

        isShieldActive = false;

        if (playerRenderer != null)
        {
            playerRenderer.material.color = originalColor;
        }
        Debug.Log("Shield Deactivated!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isShieldActive && collision.gameObject.CompareTag("Ghost")) 
        {
            Destroy(collision.gameObject); 
            isShieldActive = false; 

            if (playerRenderer != null)
            {
                playerRenderer.material.color = originalColor;
            }
            Debug.Log("Shield Consumed!");
        }
    }
}
