using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueToText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Slider _slider;


    public void ChangeTextValue() 
    {
        _text.text = _slider.value.ToString();
    }
}
