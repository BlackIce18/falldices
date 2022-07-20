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
    private bool _canMove = true;

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
            if (value <= -1)
            {
                _position = GameField.gameFieldSingleton.FieldCellsCount - 1;
            }
            else if (value >= 0 && value < GameField.gameFieldSingleton.FieldCellsCount)
            {
                _position = value;
            }
            else if (value >= GameField.gameFieldSingleton.FieldCellsCount)
            {
                _position = 0;
            }
        } 
    }

    public FieldCell fieldCell;

    public PlayerBalance Balance
    {
        get { return _balance; }
        set { _balance = value; }
    }
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
    }

    public Coroutine Move(int distance)
    {
        return StartCoroutine(MoveCoroutine(distance));
    }
    public Coroutine MoveBack(int distance)
    {
        return StartCoroutine(MoveBackCoroutine(distance));
    }
    private IEnumerator MoveCoroutine(int distance) 
    {
        int startPosition = Position;
        yield return StartCoroutine(_movement.Move(distance));
        fieldCell = GameField.gameFieldSingleton.GetFieldCell(Position);
        //yield return StartCoroutine(WorkWithPlayerPositionAndBalance(startPosition, movesCount));
        CanMove = false;
    }
    private IEnumerator MoveBackCoroutine(int distance)
    {
        int startPosition = Position;
        yield return StartCoroutine(_movement.MoveBack(distance));
        //yield return StartCoroutine(WorkWithPlayerPositionAndBalance(startPosition, movesCount));
        CanMove = false;
        fieldCell = GameField.gameFieldSingleton.GetFieldCell(Position);
    }
}
