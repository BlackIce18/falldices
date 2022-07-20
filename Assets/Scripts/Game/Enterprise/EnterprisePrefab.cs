using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class EnterprisePrefab : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _type;
    [SerializeField] private TextMeshProUGUI _defaultRentPrice;
    [SerializeField] private TextMeshProUGUI _upgradePrice;
    [SerializeField] private TextMeshProUGUI[] _rentPrices;
    [SerializeField] private Button _buy;

    public void Init(Enterprise enterprise)
    {
        _name.text = enterprise.Name;
        _priceText.text = enterprise.Price.ToString();
        _image.sprite = enterprise.Sprite;
        _type.text = enterprise.Monopoly.MonopolyName;

        for (int i = 0; i < _rentPrices.Length; i++)
        {
            _rentPrices[i].text = enterprise.Monopoly.RentPrice[i].ToString();
        }

        _defaultRentPrice.text = enterprise.Monopoly.RentPrice[0].ToString();
        _upgradePrice.text = enterprise.Monopoly.PriceToUpgrade.ToString();
    }

    public void ButtonInit(Enterprise enterprise, GameObject buyWindow, GameObject buildButton)
    {
        _buy.onClick.AddListener(() => {
            if (GameField.gameFieldSingleton.ActivePlayerTryToBuyEnterprise(enterprise))
            {
                //GameField.gameFieldSingleton.ShowCellButton();
                _buy.enabled = false;
                _priceText.text = "Куплено";
                buyWindow.SetActive(false);
                buildButton.SetActive(false);
                RotateGameScene.AllowRotate();

                Player activePlayer = GameField.gameFieldSingleton.ActivePlayer;
                Instantiate(enterprise.Prefab, activePlayer.fieldCell.transform);
                activePlayer.fieldCell.TileObject.GetComponent<MeshRenderer>().material.color = activePlayer.Color;
            }
        });
    }

}
