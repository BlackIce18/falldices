using UnityEngine;
using TMPro;
public class NewCircleWindow : Window, IShowWindow
{
    [SerializeField] private TextMeshProUGUI _textObject;

    private void OnEnable()
    {
        Show();
    }
    public override void Show()
    {
        SetText("+" + PlayerController.Singleton.CircleMoneyForPlayer);
    }
    private void SetText(string textToPlace)
    {
        _textObject.text = textToPlace;
    }
}
