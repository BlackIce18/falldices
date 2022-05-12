using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayersWindowAnimation : MonoBehaviour
{
    [SerializeField] private Image _playerIcon;
    [SerializeField] private Image _crossIcon;
    private Vector3 _startPosition;
    private RectTransform _rectTransform;

    private bool _isShowedWindow = false;
    private void Awake()
    {
        _rectTransform = transform.GetComponent<RectTransform>();
        _startPosition = _rectTransform.anchoredPosition;
        DOTween.Init();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        transform.DOLocalMove(new Vector3(_startPosition.x * -1 + _rectTransform.rect.width, _startPosition.y, _startPosition.z), 1);
        _isShowedWindow = true;
    }

    private void Hide()
    {
        transform.DOLocalMove(new Vector3(transform.localPosition.x + _rectTransform.rect.width, _startPosition.y, _startPosition.z), 1).OnComplete(()=> { gameObject.SetActive(false); });
        _isShowedWindow = false;
    }

    public void ToggleWindow()
    {
        if(_isShowedWindow)
        {
            Hide();
            ShowAndHideImages(_playerIcon, _crossIcon);
        }
        else
        {
            Show();
            ShowAndHideImages(_crossIcon, _playerIcon);
        }
    }

    private void ShowAndHideImages(Image toShow, Image toHide)
    {
        toShow.gameObject.SetActive(true);
        toHide.gameObject.SetActive(false);
    }
}
