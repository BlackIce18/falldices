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
[RequireComponent(typeof(Coloring))]
[RequireComponent(typeof(PlayerBalance))]
[RequireComponent(typeof(PlayerOwnership))]
public class Player : MonoBehaviour
{
    [SerializeField] private string _nickname = "";
    public string NickName { get { return _nickname; } }
    private bool _isBankrupt = false;

    private PlayerBalance _playerBalance;
    private PlayerMovement _playerMovement;
    private PlayerOwnership _playerOwnership;
    private int _position = 0;

    private Color32 _color;

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

    public Color32 Color
    {
        get { return _color; }
        set { _color = value; }
    }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerBalance = GetComponent<PlayerBalance>();
        _playerOwnership = GetComponent<PlayerOwnership>();
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

    public bool TryToBuyEnterprise(Enterprise enterprise)
    {
        if (enterprise.IsAvailable)
        {
            bool isBuyed = _playerBalance.TryBuy(enterprise.Price);
            if (isBuyed)
            {
                _playerOwnership.AddToOwn(enterprise);
                enterprise.SetUnavailableToBuy();
                return true;
            }
        }
        return false;
    }
}
