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
    public int CurrentRentPrice { get { return _currentRentPrice; } set { _currentRentPrice = value; } }
    public Monopoly Monopoly => _monopoly;

    public void SetUnavailableToBuy()
    {
        _isAvailable = false;
    }

    public void SetRentPrice(int newRentPrice)
    {
        CurrentRentPrice = newRentPrice;
    }

    public void SetStartRentPrice()
    {
        SetRentPrice(_monopoly.RentPrice[0]);
    }
}
public class Enterprises : MonoBehaviour
{
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
            enterprise.SetStartRentPrice();
            EnterprisePrefab enterprisePrefab = Instantiate(_UIPrefab, _parentUIPrefabs).GetComponent<EnterprisePrefab>();
            enterprisePrefab.Init(enterprise);
            enterprisePrefab.ButtonInit(enterprise, _buyWindow, _buildButton);
        }
    }

}
