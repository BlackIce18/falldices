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

public enum PlayerStatus
{
    Normal,
    Bankrupt,
    InPrison,
    FreeFromTaxes
}

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Coloring))]
[RequireComponent(typeof(PlayerBalance))]
[RequireComponent(typeof(PlayerOwnership))]
public class Player : MonoBehaviour
{
    [SerializeField] private string _nickname = "";
    private PlayerStatus _playerStatus;
    private bool _canMove = true;
    private PlayerBalance _balance;
    private PlayerMovement _movement;
    private PlayerOwnership _ownership;
    private int _position = 0;

    private Color32 _color;
    public string NickName { get { return _nickname; } set { _nickname = value; } }
    public int Position 
    {
        get { return _position; }
        set 
        {
            if (value <= -1)
            {
                _position = GameController.Singleton.FieldCellsCount - 1;
            }
            else if (value >= 0 && value < GameController.Singleton.FieldCellsCount)
            {
                _position = value;
            }
            else if (value >= GameController.Singleton.FieldCellsCount)
            {
                _position = value - GameController.Singleton.FieldCellsCount;
            }
        } 
    }

    public FieldCell currentCell;

    public PlayerBalance Balance
    {
        get { return _balance; }
        set { _balance = value; }
    }
    public PlayerStatus PlayerStatus => _playerStatus;
    public PlayerOwnership Ownership => _ownership;
    public bool CanMove {get { return _canMove;} set { _canMove = value; } }
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
        _playerStatus = PlayerStatus.Normal;
    }


    public Coroutine Move(int distance)
    {
        return StartCoroutine(MoveCoroutine(distance));
    }
    private IEnumerator MoveCoroutine(int distance) 
    {
        int startPosition = Position;
        yield return StartCoroutine(_movement.Move(this, distance));
        GameController.Singleton.CheckNewCircle(this, distance);
        yield return StartCoroutine(GameController.Singleton.WorkWithCell(this));
    }
    public Coroutine MoveBack(int distance)
    {
        return StartCoroutine(MoveBackCoroutine(distance));
    }
    private IEnumerator MoveBackCoroutine(int distance)
    {
        yield return StartCoroutine(_movement.MoveBack(this, distance));
       yield return StartCoroutine(GameController.Singleton.WorkWithCell(this));
    }

    public void ChangeStatusToPrisoner()
    {
        _playerStatus = PlayerStatus.InPrison;
    }

    public bool TryToBuyEnterprise(Enterprise enterprise)
    {
        if (currentCell.IsAvailableToBuild == false)
        {
            return false;
        }
        if (enterprise.IsAvailable)
        {
            bool isBuyed = Balance.TryBuy(enterprise);
            if (isBuyed)
            {
                Ownership.AddToOwn(enterprise);
                enterprise.SetUnavailableToBuy();
                currentCell.BuildEnterprise(this, enterprise);
                PlayerController.Singleton.activePlayerMoney.text = Balance.Money.ToString();
                return true;
            }
        }
        return false;
    }
}
