using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[Serializable]
class Balance
{
    public TextMeshProUGUI textToShowMoney;
    public PlayerBalance playerBalance;

    public Balance() { }
    public Balance(TextMeshProUGUI textToShowMoney, PlayerBalance playerBalance) 
    {
        this.textToShowMoney = textToShowMoney;
        this.playerBalance = playerBalance;
    }

    public void AddToBalance(int money)
    {
        playerBalance.AddMoney(money);
        textToShowMoney.text = playerBalance.Money.ToString();
    }
}
public class Bank : MonoBehaviour
{
    private List<Balance> _playerBalances = new List<Balance>();
    [SerializeField] private int _circleMoneyForPlayer = 1000;
    public int CircleMoneyForPlayer { get { return _circleMoneyForPlayer; } }

    private void Awake()
    {
        _circleMoneyForPlayer = GameData.moneyForCircle;
    }

    public void AddMoneyToPlayerBalance(PlayerBalance playerBalance, int money)
    {
        foreach(Balance balance in _playerBalances)
        {
            if(balance.playerBalance == playerBalance)
            {
                balance.AddToBalance(money);
            }
        }
    }

    public void AddPlayerBalance(TextMeshProUGUI textMeshPro, PlayerBalance playerBalance)
    {
        Balance balance = new Balance(textMeshPro, playerBalance);
        _playerBalances.Add(balance);
    }
}
