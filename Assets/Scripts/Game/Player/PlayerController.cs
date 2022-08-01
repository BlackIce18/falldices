using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Dice))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Singleton;

    private int _activePlayerIndex = 0;
    [SerializeField] private Player _activePlayer;
    public TextMeshProUGUI activePlayerNickname;
    public TextMeshProUGUI activePlayerMoney;

    [SerializeField] private List<Player> _players = new List<Player>();
    [SerializeField] private List<PlayerInfoUI> _playersUIInfo = new List<PlayerInfoUI>();
    [SerializeField] private Button _skipButton;
    [SerializeField] private Game.UIButtons _uiButtons;
    public int CircleMoneyForPlayer { get; } = 1000;
    private Dice _dices;
    public Player ActivePlayer => _activePlayer;
    public List<Player> Players => _players;
    public GameWindows gameWindows;

    private void Awake()
    {
        Singleton = this;
        _dices = GetComponent<Dice>();
    }
    private void Start()
    {
        ChangeActivePlayer(_activePlayerIndex); 
    }
    public void AddNewPlayer(Player player)
    {
        _players.Add(player);
    }
    public void ActivePlayerMove() 
    {
        if(ActivePlayer.CanMove == false) 
        { 
            return; 
        }

        int distance = _dices.RollDices();
        ActivePlayer.Move(distance);
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
        activePlayerNickname.color = ActivePlayer.Color;
        activePlayerMoney.text = ActivePlayer.Balance.Money.ToString();
        /*
        for(int i = 0; i < _playersUIInfo.Count; i++)
        {
            _playersUIInfo[i].ChangeBackgroundColor(_playerInfoUIDefaultColor);
        }
        _playersUIInfo[index].ChangeBackgroundColor(_playerInfoUIActiveColor);*/
    }
}
