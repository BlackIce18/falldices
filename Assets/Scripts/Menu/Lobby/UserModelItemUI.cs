using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserModelItemUI : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Image _image;
    [SerializeField] private Toggle _toggle;

    public int Id => _id;

    public void DisableToggle()
    {
        _toggle.gameObject.SetActive(false);
    }

    public void EnableToggle()
    {
        _toggle.gameObject.SetActive(true);
    }
}
