using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PrepareGame : MonoBehaviour
{
    [SerializeField] private Slider _playersCount;
    [SerializeField] private TMP_Dropdown _startMoney;
    [SerializeField] private TMP_Dropdown _moneyForCircle;
    [SerializeField] private UserModelUI _userModelUI;
    [SerializeField] private Image _previewColorImage;
    [SerializeField] private TMP_InputField _nicknameField;
    [SerializeField] private ModelsAssociation _modelsAssociation;
    [SerializeField] private Lobby _lobby;
    public Color32 PlayerColor { get { return _previewColorImage.color; } set { PlayerColor = value; } }
    public int PlayerModelId { get { return _userModelUI.ChoosedUserModelItemUI.Id; } set { PlayerModelId = value; } }
    public int StartMoney { get { return Convert.ToInt32(_startMoney.options[_startMoney.value].text); } set { StartMoney = value; } }    
    public int MoneyForCircle { get { return Convert.ToInt32(_moneyForCircle.options[_moneyForCircle.value].text); } set { MoneyForCircle = value; } }
    public ModelsAssociation ModelsAssociation { get { return _modelsAssociation; } }
    public int PlayersCount { get { return Convert.ToInt32(_playersCount.value); } }
    public string Nickname { get { return _nicknameField.text; } }

    public void SaveData()
    {
        GameData.startMoney = StartMoney;
        GameData.moneyForCircle = MoneyForCircle;
        
        for(int i = 0; i < _lobby.LobbyUsers.Count; i++)
        {
            GameData.AddUser(_lobby.LobbyUsers[i]);
        }
    }

}