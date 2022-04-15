using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class EnterprisePrefab : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _type;
    [SerializeField] private TextMeshProUGUI _defaultRentPrice;
    [SerializeField] private TextMeshProUGUI _upgradePrice;
    [SerializeField] private TextMeshProUGUI[] _rentPrices;
    [SerializeField] private Button _buy;
    public Image Image { get { return _image; } set { _image = value; } }
    public TextMeshProUGUI PriceText => _price;
    public TextMeshProUGUI Name => _name;
    public TextMeshProUGUI Type => _type;
    public TextMeshProUGUI DefaultRentPrice => _defaultRentPrice;
    public TextMeshProUGUI UpgradePrice => _upgradePrice;
    public TextMeshProUGUI[] RentPrices => _rentPrices;
    public Button Buy => _buy;
}
