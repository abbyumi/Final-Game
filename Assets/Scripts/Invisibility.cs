using System.Collections;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    public float invisibilityDuration = 5f; 
    private SkinnedMeshRenderer playerRenderer;
    private Material playerMaterial;
    private Color originalColor; 
    private bool isInvisible = false;

    void Start()
    {
        playerRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if (playerRenderer != null)
        {
            playerMaterial = playerRenderer.material;

            if (playerMaterial.HasProperty("_Color"))
            {
                originalColor = playerMaterial.color;
            }
            else
            {
                Debug.LogWarning("Material does not have a '_Color' property. Invisibility will not affect color.");
            }
        }
        else
        {
            Debug.LogError("No SkinnedMeshRenderer found on the player or its children!");
        }
    }

    public void ActivateInvisibility()
    {
        if (!isInvisible && playerRenderer != null)
        {
            StartCoroutine(InvisibilityCoroutine());
        }
    }

    private IEnumerator InvisibilityCoroutine()
    {
        Debug.Log("Invisibility Activated!");
        isInvisible = true;

        if (playerMaterial != null && playerMaterial.HasProperty("_Color"))
        {
            playerMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.3f);
        }

        yield return new WaitForSeconds(invisibilityDuration);

        isInvisible = false;

        if (playerMaterial != null && playerMaterial.HasProperty("_Color"))
        {
            playerMaterial.color = originalColor;
        }

        Debug.Log("Invisibility Deactivated!");
    }
}
