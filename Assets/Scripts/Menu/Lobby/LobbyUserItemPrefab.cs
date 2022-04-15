using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LobbyUserItemPrefab : MonoBehaviour
{
    [SerializeField] private Image _playerIcon;
    [SerializeField] private TextMeshProUGUI _nickname;

    public Image PlayerIcon => _playerIcon;
    public TextMeshProUGUI Nickname => _nickname;
}
