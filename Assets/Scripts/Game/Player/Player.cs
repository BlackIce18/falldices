using System;
using System.Collections;
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
    public string NickName { get { return _nickname; } set { _nickname = value; } }
    private bool _isBankrupt = false;

    private PlayerBalance _balance;
    private PlayerMovement _movement;
    private PlayerOwnership _ownership;
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

    public PlayerBalance Balance
    {
        get { return _balance; }
        set { _balance = value; }
    }
    public PlayerOwnership Ownership => _ownership;
    public Color32 Color
    {
        get { return _color; }
        set { _color = value; }
    }

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _balance = GetComponent<PlayerBalance>();
        _ownership = GetComponent<PlayerOwnership>();
    }

    public IEnumerator Move(int movesCount) 
    {
        int startPosition = Position;
        yield return StartCoroutine(_movement.Move(movesCount));
        yield return StartCoroutine(WorkWithPlayerPositionAndBalance(startPosition, movesCount));
    }

    private IEnumerator WorkWithPlayerPositionAndBalance(int startPosition, int movesCount)
    {
        FieldCell fieldCell = GameField.gameFieldSingleton.GetActivePlayerCell();
        int fieldSize = GameField.gameFieldSingleton.FieldCellsCount;
        if (startPosition + movesCount >= fieldSize)
        {
            Bank bank = GameField.gameFieldSingleton.Bank;
            Balance.AddMoney(bank.CircleMoneyForPlayer);
            GameField.gameFieldSingleton.activePlayerMoney.text = Balance.Money.ToString();
            bank.AddMoneyToPlayerBalance(Balance, bank.CircleMoneyForPlayer);
            GameField.gameFieldSingleton.gameWindows.ShowWindow(WindowsEnum.NewCircle);
        }

        if(fieldCell.owner == null)
        {
            GameField.gameFieldSingleton.ShowCellButton();
        }
        else if (fieldCell.owner != null && fieldCell.owner != this)
        {
            Balance.AddMoney(-fieldCell.enterprise.CurrentRentPrice);
            fieldCell.owner.Balance.AddMoney(fieldCell.enterprise.CurrentRentPrice);
        }
        yield return fieldCell;
    }

    public bool TryToBuyEnterprise(Enterprise enterprise)
    {
        if (enterprise.IsAvailable)
        {
            bool isBuyed = _balance.TryBuy(enterprise.Price);
            if (isBuyed)
            {
                _ownership.AddToOwn(enterprise);
                enterprise.SetUnavailableToBuy();
                return true;
            }
        }
        return false;
    }
}
