using UnityEngine;
using TMPro;

public class PlayerBalance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    public PlayerInfoUI infoUI { get; set; }
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
        MoneyText.text = Money.ToString();
        infoUI.MoneyText.text = Money.ToString();
    }

    public bool TryBuy(Enterprise enterprise)
    {
        var price = enterprise.Price;
        if (Money - price >= 0) {
            AddMoney(-price);
            return true; 
        }
        return false;
    }
}
