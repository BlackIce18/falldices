using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrepareGame : MonoBehaviour
{
    public static PrepareGame prepareGameSingleton;
    public Color32 PlayerColor { get { return _previewColorImage.color; } set { PlayerColor = value; } }

    public int ModelId { get { return _userModelUI.ChoosedUserModelItemUI.Id; } set { ModelId = value; } }


    [SerializeField] private Slider _playersCount;
    [SerializeField] private TMP_Dropdown _startMoney;
    [SerializeField] private TMP_Dropdown _moneyForCircle;
    [SerializeField] private Image _previewColorImage;
    [SerializeField] private UserModelUI _userModelUI;
    [SerializeField] private ModelsAssociation _modelsAssociation;
    public int PlayersCount { get { return Convert.ToInt32(_playersCount.value); } set { PlayersCount = value; } }    
    public int StartMoney { get { return Convert.ToInt32(_startMoney.value); } set { StartMoney = value; } }    
    public int MoneyForCircle { get { return Convert.ToInt32(_moneyForCircle.value); } set { MoneyForCircle = value; } }
    public ModelsAssociation ModelsAssociation { get { return _modelsAssociation; } }

    public void SaveData()
    {
        /*PlayerColor = _previewColorImage.color;
        ModelId = _userModelUI.ChoosedUserModelItemUI.Id;
        PlayersCount = Convert.ToInt32(_playersCount.value);
        StartMoney = Convert.ToInt32(_startMoney.value);
        MoneyForCircle = Convert.ToInt32(_moneyForCircle.value);*/
    }

    private void Awake()
    {
        if (!prepareGameSingleton)
        {
            prepareGameSingleton = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        //Text ui = GameObject.Find("/Canvas/Text").GetComponent<Text>();
        Debug.Log("scene was loaded");
        Debug.Log(prepareGameSingleton.PlayersCount);
    }
}
