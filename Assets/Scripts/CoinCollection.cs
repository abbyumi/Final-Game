using System.Collections;
using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    private int coin = 0;

    public int PlayerCoins => coin;

    public TextMeshProUGUI coinText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coin++;
            UpdateCoinText();
            Destroy(other.gameObject);
        }
    }

    public bool SpendCoins(int amount)
    {
        if (coin >= amount)
        {
            coin -= amount;
            UpdateCoinText();
            return true;
        }
        return false;
    }

    private void UpdateCoinText()
    {
        coinText.text = "Collectable: " + coin.ToString();
    }
}
