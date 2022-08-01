using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class GameController : MonoBehaviour
{
    [SerializeField] private Button _skipButton;
    [SerializeField] private FieldCell[] _fieldCells;
    public static GameController Singleton;
    public int FieldCellsCount => _fieldCells.Length;
    private PlayerController _playerController;

    private void Awake()
    {
        Singleton = this;
        _playerController = GetComponent<PlayerController>();
    }

    private void ShowCellButton()
    {
        _skipButton.gameObject.SetActive(true);
        FieldCell ActivePlayerCell = _playerController.ActivePlayer.currentCell;
        if (ActivePlayerCell.IsAvailableToBuild == false && ActivePlayerCell.enterprise.IsAvailable != false)
        {
            return;
        }
        ActivePlayerCell.UIToShow.SetActive(true);
    }
    public FieldCell GetFieldCell(int index)
    {
        if (index == -1)
        {
            index = FieldCellsCount - 1;
        }
        else if (index >= FieldCellsCount)
        {
            index = index - FieldCellsCount;
        }
        return _fieldCells[index];
    }
    public IEnumerator WorkWithCell(Player player)
    {
        FieldCell cell = GetFieldCell(player.Position);
        if (cell.owner == null)
        {
           ShowCellButton();
        }
        else if (cell.owner != this)
        {
            player.Balance.AddMoney(-cell.enterprise.CurrentRentPrice);
            cell.owner.Balance.AddMoney(cell.enterprise.CurrentRentPrice);
        }
        //player.CanMove = false;
        yield return null;
    }

    public bool CheckNewCircle(Player player, int distance)
    {
        Debug.Log(player.Position + " " + distance + " " + FieldCellsCount);
        if (player.Position - distance <= -1)
        {
            player.Balance.AddMoney(PlayerController.Singleton.CircleMoneyForPlayer);
            PlayerController.Singleton.activePlayerMoney.text = player.Balance.Money.ToString();
            PlayerController.Singleton.gameWindows.ShowWindow(WindowsEnum.NewCircle);
            return true;
        }
        return false;
    }
}
