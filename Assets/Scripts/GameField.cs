using UnityEngine;

[RequireComponent(typeof(Dice))]
public class GameField : MonoBehaviour
{
    [SerializeField] private Player _activePlayer;
    [SerializeField] private FieldCell[] _fieldCells;
    [SerializeField] private GameObject[] _playersPrefabs; // Игроки созданы и помещаются фишки (у каждого игрока своя фишка)
    [SerializeField] private Player[] _players; // GetComponent из playersPrefabs
    [SerializeField] private int _circleMoneyForPlayer = 1000;
    private Dice _dice => GetComponent<Dice>();
    public int FieldCellsCount => _fieldCells.Length;
    public void ActivePlayerRollDices() 
    {
        int[] values = _dice.RollDices();
        _activePlayer.Move(values[0]+values[1]);
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

    public void AddCircleMoney()
    {
        _activePlayer.AddMoney(_circleMoneyForPlayer);
    }
}
