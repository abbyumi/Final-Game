using System.Collections;
using UnityEngine;

public class ShieldEffect : MonoBehaviour
{
    public Material shieldMaterial; 
    private Material originalMaterial; 
    private Renderer playerRenderer; 
    public float duration = 3f; 

    void Start()
    {
        playerRenderer = GetComponentInChildren<Renderer>();
        if (playerRenderer != null)
        {
            originalMaterial = playerRenderer.material;
        }
        else
        {
            Debug.LogError("No Renderer found on JohnLemon or its children!");
        }
    }

    public void ActivateShield()
    {
        if (playerRenderer != null && shieldMaterial != null)
        {
            playerRenderer.material = shieldMaterial; 
            StartCoroutine(ResetMaterialAfterDuration());
        }
        else
        {
            Debug.LogError("Shield Material or Renderer is not set!");
        }
    }

    private IEnumerator ResetMaterialAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        if (playerRenderer != null && originalMaterial != null)
        {
            playerRenderer.material = originalMaterial; 
        }
    }
}
