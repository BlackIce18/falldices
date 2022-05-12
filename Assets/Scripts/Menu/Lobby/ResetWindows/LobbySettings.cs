using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LobbySettings : LobbyWindow, ICanResetObjects
{
    [SerializeField] private Slider[] _sliders;
    [SerializeField] private TMP_Dropdown[] _dropdown;
    public override void ResetObjects()
    {
        ResetSlider();
        ResetDropDown();
    }

    private void ResetSlider()
    {
        for (int i = 0; i < _sliders.Length; i++)
        {
            _sliders[i].value = 0;
        }
    }
    private void ResetDropDown()
    {
        for(int i = 0; i < _dropdown.Length; i++)
        {
            _dropdown[i].value = 0;
        }
    }
}
