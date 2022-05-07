using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LobbyUserItem : MonoBehaviour
{
    [HideInInspector] public Color32 color;
    [HideInInspector] public Models model;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nickname;

    public Image Icon => _icon;
    public TextMeshProUGUI Nickname => _nickname;
}
