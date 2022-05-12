using TMPro;
using UnityEngine;

public class BankRobberyWindow : Window, IShowWindow
{
    [SerializeField] private TextMeshProUGUI _textChanceOfRobbery;
    private Robbery _robbery;
    private void Awake()
    {
        _robbery = new Robbery();
    }
    private void OnEnable()
    {
        Show();
    }
    public override void Show()
    {
        SetText(_robbery.GenerateRobberyChance()+"%");
    }
    private void SetText(string textToPlace)
    {
        _textChanceOfRobbery.text = textToPlace;
    }
}
