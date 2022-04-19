using UnityEngine;
using TMPro;
public class PlayerBalance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    public TextMeshProUGUI MoneyText { get { return _moneyText; } set { _moneyText = value; } }
    private int _money = 1000;
    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            MoneyText.text = _money.ToString();
        }
    }
    public void AddMoney(int money)
    {
        Money += money;
        _moneyText.text = Money.ToString();
    }

    public void StartMoney()
    {
        _moneyText.text = Money.ToString();
    }

    public bool TryBuy(int price)
    {
        if(Money - price >= 0) {
            Money -= price;
            return true; 
        }
        return false;
    }
}