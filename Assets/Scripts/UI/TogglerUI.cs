using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class TogglerUI : MonoBehaviour
{
    private Toggle _toggle;
    [SerializeField] private RectTransform _handler;
    [SerializeField] private Image _Background;
    [SerializeField] private Color32 _BackgroundDefaultColor;
    [SerializeField] private Color32 _BackgroundTargetColor;

    private float _togglerWidth => _toggle.GetComponent<RectTransform>().rect.width;
    private float _handlerWidth => _handler.rect.width;

    private void Start()
    {
        _toggle = GetComponent<Toggle>();
    }

    public void OnValueChanged()
    {
        if(_toggle.isOn)
        {
            ChangeBackgroundColor(_BackgroundTargetColor);
            ChangeHandlerPosition(new Vector3(_togglerWidth - _handlerWidth / 2, _handler.localPosition.y, _handler.localPosition.z));
        }
        else
        {
            ChangeBackgroundColor(_BackgroundDefaultColor);
            ChangeHandlerPosition(new Vector3(_handlerWidth / 2, _handler.localPosition.y, _handler.localPosition.z));
        }
    }

    private void ChangeBackgroundColor(Color32 color)
    {
        _Background.color = color;
    }

    private void ChangeHandlerPosition(Vector3 targetPosition)
    {
        _handler.anchoredPosition = targetPosition;
    }
}
