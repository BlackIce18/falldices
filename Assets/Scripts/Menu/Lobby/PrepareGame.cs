using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PrepareGame : MonoBehaviour
{
    public static PrepareGame prepareGameSingleton;
    public Color32 PlayerColor { get { return _previewColorImage.color; } set { PlayerColor = value; } }

    public int PlayerModelId { get { return _userModelUI.ChoosedUserModelItemUI.Id; } set { PlayerModelId = value; } }

    
    [SerializeField] private Slider _playersCount;
    [SerializeField] private TMP_Dropdown _startMoney;
    [SerializeField] private TMP_Dropdown _moneyForCircle;
    [SerializeField] private Image _previewColorImage;
    [SerializeField] private UserModelUI _userModelUI;
    [SerializeField] private ModelsAssociation _modelsAssociation;
    public int PlayersCount { get { return Convert.ToInt32(_playersCount.value); } set { PlayersCount = value; } }    
    public int StartMoney { get { return Convert.ToInt32(_startMoney.options[_startMoney.value].text); } set { StartMoney = value; } }    
    public int MoneyForCircle { get { return Convert.ToInt32(_moneyForCircle.options[_moneyForCircle.value].text); } set { MoneyForCircle = value; } }
    public ModelsAssociation ModelsAssociation { get { return _modelsAssociation; } }

    public static List<LobbyUserItem> users = new List<LobbyUserItem>();

    public void SaveData()
    {
        GameData.playersCount = PlayersCount;
        GameData.startMoney = StartMoney;
        GameData.moneyForCircle = MoneyForCircle;
        
        for(int i = 0; i < users.Count; i++)
        {
            GameData.AddUser(users[i].color, users[i].model, users[i].Icon.sprite, users[i].Nickname.text);
        }
    }
}
