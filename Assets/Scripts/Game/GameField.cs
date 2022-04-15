using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Dice))]
[RequireComponent(typeof(Timer))]
public class GameField : MonoBehaviour
{
    public static GameField gameFieldSingleton;

    [SerializeField] private Bank _bank;
    public Bank Bank { get { return _bank; }}

    private int _activePlayerIndex = 0;
    [SerializeField] private Player _activePlayer;
    [SerializeField] private TextMeshProUGUI _activePlayerNickname;
    [SerializeField] private TextMeshProUGUI _activePlayerMoney;

    [SerializeField] private List<Player> _players = new List<Player>();
    [SerializeField] private List<PlayerInfoUI> _playersUIInfo = new List<PlayerInfoUI>();
    [SerializeField] private Color32 _playerInfoUIActiveColor;
    [SerializeField] private Color32 _playerInfoUIDefaultColor;
    [SerializeField] private FieldCell[] _fieldCells;
    [SerializeField] private Button _skipButton;
    private Dice _dice;
    private Timer _timer;
    public int FieldCellsCount => _fieldCells.Length;
    private void Awake()
    {
        _dice = GetComponent<Dice>();
        _timer = GetComponent<Timer>();
        gameFieldSingleton = this;
    }

    private void Start()
    {
        ChangeActivePlayer(_activePlayerIndex);
    }

    public void ActivePlayerRollDices() 
    {
        _dice.ShowDices();
        HideActivePlayerInfo();
        int[] values = _dice.RollDices();
        _activePlayer.Move(values[0]+values[1]);
        StartCoroutine(HideDices());
        _timer.ResetTimer();
        //NextPlayerTurn();
    }

    private void HideActivePlayerInfo()
    {
        _activePlayerNickname.gameObject.SetActive(false);
        _activePlayerMoney.gameObject.SetActive(false);
    }
    private void ShowActivePlayerInfo()
    {
        _activePlayerNickname.gameObject.SetActive(true);
        _activePlayerMoney.gameObject.SetActive(true);
    }

    private IEnumerator HideDices()
    {
        yield return new WaitForSeconds(2f);
        _dice.HideDices();
        yield return new WaitForSeconds(0.2f);
        ShowActivePlayerInfo();
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
        ChangeActivePlayer(IncreaseActivePlayerIndex());
    }

    private void ChangeActivePlayer(int index)
    {
        _activePlayer = _players[index];

        _activePlayerNickname.text = _activePlayer.NickName;
        _activePlayerNickname.color = _playersUIInfo[index].Nickname.color;
        _activePlayerMoney.text = _activePlayer.PlayerBalance.Money.ToString();
        _skipButton.gameObject.SetActive(false);
        /*
        for(int i = 0; i < _playersUIInfo.Count; i++)
        {
            _playersUIInfo[i].ChangeBackgroundColor(_playerInfoUIDefaultColor);
        }
        _playersUIInfo[index].ChangeBackgroundColor(_playerInfoUIActiveColor);*/
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

    public bool ActivePlayerTryToBuyEnterprise(Enterprise enterprise)
    {
        if(_fieldCells[_activePlayer.Position].IsAvailableToBuild == false)
        {
            return false;
        }
        return _activePlayer.TryToBuyEnterprise(enterprise);
    }

    public void ShowCellButton()
    {
        _fieldCells[_activePlayer.Position].UIToShow.SetActive(true);
        _skipButton.gameObject.SetActive(true);
    }

    public FieldCell GetActivePlayerCell()
    {
        return _fieldCells[_activePlayer.Position];
    }

    public Color32 GetActivePlayerColor()
    {
        return _activePlayer.Color;
    }
}
