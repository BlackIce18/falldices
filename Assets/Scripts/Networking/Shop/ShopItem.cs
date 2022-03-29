using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Image _picture;
    [SerializeField] private Image _imageGems;
    [SerializeField] private Button _button;

    public void SetPurchasedItem() 
    {
        _imageGems.gameObject.SetActive(false);
        _button.interactable = false;
        _price.text = "Куплено";
    }

    public void SetPrice(string newPrice)
    {
        _price.text = newPrice;
    }

    public void ChangeSprite(Sprite sprite)
    {
        _picture.sprite = sprite;
    }
}
