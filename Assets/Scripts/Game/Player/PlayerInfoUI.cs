using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nickname;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Image _playerIcon;
    [SerializeField] private Image _background;

    public TextMeshProUGUI Nickname { get { return _nickname; } }
    public TextMeshProUGUI MoneyText { get { return _moneyText; } }
    public Image PlayerIcon { get { return _playerIcon; } }

    public void ChangeBackgroundColor(Color32 color)
    {
        _background.color = color;
    }
}
