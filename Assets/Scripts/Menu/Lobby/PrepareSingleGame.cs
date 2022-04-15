using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrepareSingleGame : MonoBehaviour
{
    public Color32 PlayerColor { get { return _previewColorImage.color; } }

    public int ModelId { get { return _userModelUI.ChoosedUserModelItemUI.Id; } }


    [SerializeField] private Slider _playersCount;
    [SerializeField] private TMP_Dropdown _startMoney;
    [SerializeField] private TMP_Dropdown _moneyForCircle;
    [SerializeField] private Image _previewColorImage;
    [SerializeField] private UserModelUI _userModelUI;
    [SerializeField] private ModelsAssociation _modelsAssociation;
    public int PlayersCount { get { return Convert.ToInt32(_playersCount.value); } }    
    public int StartMoney { get { return Convert.ToInt32(_startMoney.value); } }    
    public int MoneyForCircle { get { return Convert.ToInt32(_moneyForCircle.value); } }
    public ModelsAssociation ModelsAssociation { get { return _modelsAssociation; } }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
