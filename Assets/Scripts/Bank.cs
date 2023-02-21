using TMPro;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    private int coins = 0;

    public void AddCoins(int nbCoins)
    {
        coins += nbCoins;

        if (coins < 10)
            coinsText.text = "x0" + coins;
        else
            coinsText.text = "x" + coins;
    }
}
