using System;
using UnityEngine;
// FieldCells
public enum PlayerAnimationDirection
{ 
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerColoring))]
[RequireComponent(typeof(PlayerBalance))]
public class Player : MonoBehaviour
{
    private string _nickname = "";
    private bool _isBankrupt = false;

    private PlayerBalance _playerBalance;
    private PlayerMovement _playerMovement;
    private int _position = 0;

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

    public PlayerBalance PlayerBalance
    {
        get { return _playerBalance; }
        set { _playerBalance = value; }
    }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerBalance = GetComponent<PlayerBalance>();
    }

    public void Move(int movesCount) 
    {
        StartCoroutine(_playerMovement.Move(movesCount));
        if (Position + movesCount >= GameField.gameFieldSingleton.FieldCellsCount) 
        {
            Bank bank = GameField.gameFieldSingleton.Bank;
            _playerBalance.AddMoney(bank.CircleMoneyForPlayer);
            bank.AddMoneyToPlayerBalance(_playerBalance, bank.CircleMoneyForPlayer);
        }
    }
}
