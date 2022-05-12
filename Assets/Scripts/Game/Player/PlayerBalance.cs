using UnityEngine;
using TMPro;
public class PlayerBalance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    public TextMeshProUGUI MoneyText { get { return _moneyText; } set { _moneyText = value; } }
    [SerializeField] private int _money;
    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
        }
    }
    public void AddMoney(int money)
    {
        Money += money;
        _moneyText.text = Money.ToString();
    }

    public bool TryBuy(int price)
    {
        if(Money - price >= 0) {
            Money -= price;
            MoneyText.text = Money.ToString();
            return true; 
        }
        return false;
    }
}
