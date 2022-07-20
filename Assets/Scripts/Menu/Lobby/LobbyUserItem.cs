using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LobbyUserItem : MonoBehaviour
{
    private Color32 _color;
    private Models _model;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _nickname;

    public void Initialize(string nickname, Color32 color, Models model)
    {
        _nickname.text = nickname;
        _color = color;
        _model = model;
        _image.sprite = model.Sprite;
        _image.preserveAspect = true;
        _nickname.color = color;
    }
}