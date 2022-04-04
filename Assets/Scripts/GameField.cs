using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Dice))]
public class GameField : MonoBehaviour
{
    public static GameField gameFieldSingleton;

    [SerializeField] private Bank _bank;
    public Bank Bank { get { return _bank; }}

    private int _activePlayerIndex = 0;
    [SerializeField] private Player _activePlayer;
    [SerializeField] private List<Player> _players = new List<Player>();

    [SerializeField] private FieldCell[] _fieldCells;
    private Dice _dice;
    public int FieldCellsCount => _fieldCells.Length;
    public void ActivePlayerRollDices() 
    {
        int[] values = _dice.RollDices();
        _activePlayer.Move(values[0]+values[1]);
        ChangeActivePlayer(IncreaseActivePlayerIndex());
    }

    public Vector3 GetPointPosition(int pointNumber) 
    {
        if (pointNumber == FieldCellsCount) 
        {
            pointNumber -= FieldCellsCount;
        }

        return _fieldCells[pointNumber].transform.position;
    }

    public PlayerAnimationDirection GetPointPlayerAnimationDirection(int pointNumber)
    {
        if (pointNumber == FieldCellsCount)
        {
            pointNumber -= FieldCellsCount;
        }

        return _fieldCells[pointNumber].Direction;
    }

    private void Awake()
    {
        _dice = GetComponent<Dice>();
        gameFieldSingleton = this;
    }

    private void Start()
    {
        ChangeActivePlayer(_activePlayerIndex);
    }

    public void AddNewPlayer(Player player)
    {
        _players.Add(player);
    }

    private void ChangeActivePlayer(int index)
    {
        _activePlayer = _players[index];
    }

    private int IncreaseActivePlayerIndex()
    {
        if (_activePlayerIndex + 1 == _players.Count)
        {
            _activePlayerIndex = 0;
        }
        else
        {
            _activePlayerIndex += 1;
        }
        return _activePlayerIndex;
    }
}
