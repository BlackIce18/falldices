using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

[Serializable]
public class Monopoly
{
    [SerializeField] private int _monopolyId;
    [SerializeField] private string _monopolyName;
    [SerializeField] private string _monopolyShortName;
    [SerializeField] private List<int> _rentPrice = new List<int>();
    [SerializeField] private int _priceToUpgrade;

    public string MonopolyName => _monopolyName;
    public List<int> RentPrice => _rentPrice;
    public int PriceToUpgrade => _priceToUpgrade;
}

[Serializable]
public class Enterprise
{
    [SerializeField] private bool _isAvailable = true;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private int _currentRentPrice;

    [SerializeField] private Player _owner;
    [SerializeField] private Monopoly _monopoly;

    public bool IsAvailable { get { return _isAvailable; } set { _isAvailable = value; } }
    public GameObject Prefab => _prefab;
    public string Name => _name;
    public Sprite Sprite => _sprite;
    public int Price => _price;
    public int CurrentRentPrice => _currentRentPrice;
    public Monopoly Monopoly => _monopoly;

    public void SetUnavailableToBuy()
    {
        _isAvailable = false;
    }
}
public class Enterprises : MonoBehaviour
{
    [SerializeField] private GameField _gameField;
    [SerializeField] private List<Enterprise> _enterprises = new List<Enterprise>();
    [SerializeField] private GameObject _UIPrefab;
    [SerializeField] private GameObject _buyWindow;
    [SerializeField] private GameObject _buildButton;
    [SerializeField] private Transform _parentUIPrefabs;
    [SerializeField] private GameObject _hotelPrefab;
    [SerializeField] private Color32 _startLVLHotel;
    [SerializeField] private Color32 _firstLVLHotel;
    [SerializeField] private Color32 _secondLVLHotel;
    [SerializeField] private Color32 _maxLVLHotel;
    private void Awake()
    {
        Init();
    }

    private void Init() 
    {
        foreach(Enterprise enterprise in _enterprises) 
        { 
            EnterprisePrefab enterprisePrefab = Instantiate(_UIPrefab, _parentUIPrefabs).GetComponent<EnterprisePrefab>();
            enterprisePrefab.Image.sprite = enterprise.Sprite;
            enterprisePrefab.PriceText.text = enterprise.Price.ToString();
            enterprisePrefab.Name.text = enterprise.Name;
            enterprisePrefab.Type.text = enterprise.Monopoly.MonopolyName;

            for(int i = 0; i < enterprisePrefab.RentPrices.Length; i++)
            {
                enterprisePrefab.RentPrices[i].text = enterprise.Monopoly.RentPrice[i].ToString();
            }

            enterprisePrefab.DefaultRentPrice.text = enterprise.CurrentRentPrice.ToString();
            enterprisePrefab.UpgradePrice.text = enterprise.Monopoly.PriceToUpgrade.ToString();
            enterprisePrefab.Buy.onClick.AddListener(()=> {
                if (_gameField.ActivePlayerTryToBuyEnterprise(enterprise))
                {
                    _gameField.ShowCellButton();
                    enterprisePrefab.Buy.enabled = false;
                    _buyWindow.SetActive(false);
                    _buildButton.SetActive(false);
                    BuildEnterprise(enterprise);
                }
            });
        }
    }

    private void BuildEnterprise(Enterprise enterprise)
    {
        FieldCell activePlayerfieldCell = _gameField.GetActivePlayerCell();
        Instantiate(enterprise.Prefab, activePlayerfieldCell.transform);
        activePlayerfieldCell.TileObject.GetComponent<MeshRenderer>().material.color = _gameField.GetActivePlayerColor();

    }
}
