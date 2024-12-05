using System.Collections;
using UnityEngine;
using TMPro;

public class PowerUpStoreUI : MonoBehaviour
{
    public GameObject storePanel; 
    public CoinCollection coinCollection; 
    public TextMeshProUGUI storeCoinText; 
    public int powerUpCost = 1; 

    private PlayerMovement playerMovement; 

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found in the scene!");
        }
    }

    private void Update()
    {
        if (storeCoinText != null && coinCollection != null)
        {
            storeCoinText.text = "Coins: " + coinCollection.PlayerCoins;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleStore();
        }
    }

    public void ToggleStore()
    {
        if (storePanel != null)
        {
            storePanel.SetActive(!storePanel.activeSelf);
        }
    }

    public void PurchaseSpeedBoost()
    {
        if (coinCollection.SpendCoins(powerUpCost))
        {
            Debug.Log("Speed Boost Purchased!");
            if (playerMovement != null)
            {
                playerMovement.ActivateSpeedBoost();
            }
            else
            {
                Debug.LogError("PlayerMovement script reference is missing!");
            }
        }
        else
        {
            Debug.Log("Not enough coins for Speed Boost.");
        }
    }

    public void PurchaseShield()
    {
        if (coinCollection.SpendCoins(powerUpCost))
        {
            Debug.Log("Shield Purchased!");
            FindObjectOfType<ShieldEffect>()?.ActivateShield();
        }
        else
        {
            Debug.Log("Not enough coins for Shield.");
        }
    }

    public void PurchaseInvisibility()
    {
        if (coinCollection.SpendCoins(powerUpCost))
        {
            Debug.Log("Invisibility Purchased!");
            FindObjectOfType<Invisibility>()?.ActivateInvisibility();
        }
        else
        {
            Debug.Log("Not enough coins for Invisibility.");
        }
    }
}
