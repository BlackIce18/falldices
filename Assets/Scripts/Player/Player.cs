using System;
using UnityEngine;
using TMPro;
// FieldCells
public enum PlayerAnimationDirection
{ 
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private GameField _gameField;
    [SerializeField] private Material _userColor; // Выбранный пользователем цвет
    private string _nickname = "";
    private int _money = 1000;
    public int Money {
        get { return _money; }
        set { 
            _money = value;
            _moneyText.text = _money.ToString();
        }
    }
    private PlayerMovement _playerMovement => GetComponent<PlayerMovement>();
    public int _position = 0;

    public int Position 
    {
        get { return _position; }
        set 
        {
            try
            {
                if (value < -1)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (value >= -1)
                {
                    _position = value;
                }
            }
            catch (ArgumentOutOfRangeException argumentException) 
            {
                Debug.Log("Значение должно быть 0 или выше");
            }
               
        } 
    }
    private void Start()
    {
        _moneyText.text = Money.ToString();    
    }

    public void Move(int movesCount) 
    {
        StartCoroutine(_playerMovement.Move(movesCount));
        if (Position + movesCount >= _gameField.FieldCellsCount) 
        {
            _gameField.AddCircleMoney();
        }
    }

    public void AddMoney(int money)
    {
        Money += money;
    }
}
