using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Dice))]
public class GameField : MonoBehaviour
{
    public static GameField gameFieldSingleton;

    private int _activePlayerIndex = 0;
    [SerializeField] private Player _activePlayer;
    public TextMeshProUGUI activePlayerNickname;
    public TextMeshProUGUI activePlayerMoney;

    [SerializeField] private List<Player> _players = new List<Player>();
    [SerializeField] private List<PlayerInfoUI> _playersUIInfo = new List<PlayerInfoUI>();
    [SerializeField] private Color32 _playerInfoUIActiveColor;
    [SerializeField] private Color32 _playerInfoUIDefaultColor;
    [SerializeField] private FieldCell[] _fieldCells;
    [SerializeField] private Button _skipButton;
    [SerializeField] private Game.UIButtons _uiButtons;
    public int CircleMoneyForPlayer { get; } = 1000;
    private Dice _dice;
    public int FieldCellsCount => _fieldCells.Length;
    public Player ActivePlayer => _activePlayer;
    public GameWindows gameWindows;

    private void Awake()
    {
        _dice = GetComponent<Dice>();
        gameFieldSingleton = this;
    }

    private void Start()
    {
        ChangeActivePlayer(_activePlayerIndex);
    }

    public void ActivePlayerMove() 
    {
        if(ActivePlayer.CanMove == false) 
        { 
            return; 
        }

        _dice.ShowDices();
        int distance = _dice.RollDices();
        StartCoroutine(MoveCoroutine(distance));
        _dice.HideDicesAfterTime(2f);
    }
    private IEnumerator MoveCoroutine(int distance)
    {
        int startPosition = ActivePlayer.Position;
        yield return ActivePlayer.Move(distance);
        CheckNewCircle(startPosition, distance, FieldCellsCount);
        if (ActivePlayer.fieldCell.owner == null)
        {
            ShowCellButton();
        }
        else if (ActivePlayer.fieldCell.owner != null && ActivePlayer.fieldCell.owner != this)
        {
            ActivePlayer.Balance.AddMoney(-ActivePlayer.fieldCell.enterprise.CurrentRentPrice);
            ActivePlayer.fieldCell.owner.Balance.AddMoney(ActivePlayer.fieldCell.enterprise.CurrentRentPrice);
        }
    }

    private bool CheckNewCircle(int currentPosition, int distance, int fieldSize) 
    {
        Debug.Log(currentPosition+" "+distance+" "+fieldSize);
        if (currentPosition + distance >= fieldSize)
        {
            ActivePlayer.Balance.AddMoney(CircleMoneyForPlayer);
            activePlayerMoney.text = ActivePlayer.Balance.Money.ToString();
            gameWindows.ShowWindow(WindowsEnum.NewCircle);
            return true;
        }
        return false;
    }

    public FieldCell GetFieldCell(int index)
    {
        if(index <= -1 || index > FieldCellsCount)
        {
            throw new ArgumentOutOfRangeException();
        } 

        return _fieldCells[index];
    }

    public void AddNewPlayer(Player player)
    {
        _players.Add(player);
    }

    public void AddNewPlayerUIInfo(PlayerInfoUI playerInfoUI)
    {
        _playersUIInfo.Add(playerInfoUI);
    }
    
    public void NextPlayerTurn()
    {
        if (_activePlayerIndex + 1 == _players.Count)
        {
            _activePlayerIndex = 0;
        }
        else
        {
            _activePlayerIndex += 1;
        }

        ActivePlayer.CanMove = true;
        _uiButtons.Reset();
        ChangeActivePlayer(_activePlayerIndex);
    }

    private void ChangeActivePlayer(int index)
    {
        _activePlayer = _players[index];

        activePlayerNickname.text = ActivePlayer.NickName;
        activePlayerNickname.color = _playersUIInfo[index].Nickname.color;
        activePlayerMoney.text = ActivePlayer.Balance.Money.ToString();
        /*
        for(int i = 0; i < _playersUIInfo.Count; i++)
        {
            _playersUIInfo[i].ChangeBackgroundColor(_playerInfoUIDefaultColor);
        }
        _playersUIInfo[index].ChangeBackgroundColor(_playerInfoUIActiveColor);*/
    }

    public bool ActivePlayerTryToBuyEnterprise(Enterprise enterprise)
    {
        if (ActivePlayer.fieldCell.IsAvailableToBuild == false)
        {
            return false;
        }
        if (enterprise.IsAvailable)
        {
            bool isBuyed = ActivePlayer.Balance.TryBuy(enterprise);
            if (isBuyed)
            {
                ActivePlayer.Ownership.AddToOwn(enterprise);
                enterprise.SetUnavailableToBuy();
                ActivePlayer.fieldCell.BuildEnterprise(ActivePlayer, enterprise);
                activePlayerMoney.text = ActivePlayer.Balance.Money.ToString();
                return true;
            }
        }
        return false;
    }

    public void ShowCellButton()
    {
        _skipButton.gameObject.SetActive(true);
        FieldCell activePlayerCell = ActivePlayer.fieldCell;
        if (activePlayerCell.IsAvailableToBuild == false && activePlayerCell.enterprise.IsAvailable != false)
        {
            return;
        }
        activePlayerCell.UIToShow.SetActive(true);
    }
}
