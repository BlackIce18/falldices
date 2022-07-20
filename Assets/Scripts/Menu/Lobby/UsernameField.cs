using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
public class UsernameField : MonoBehaviour
{
    private readonly string _regexp = @"^[a-zA-Z0-9_-]{1,16}$";
    [SerializeField] private TMP_InputField _field;
    [SerializeField] private TextMeshProUGUI _warning;
    [SerializeField] private GameObject _nextPage;

    public void TryChangePage()
    {
        if (Regex.IsMatch(_field.text, _regexp))
        {
            transform.gameObject.SetActive(false);
            _warning.gameObject.SetActive(false);
            _nextPage.SetActive(true);
        }
        else
        {
            _warning.gameObject.SetActive(true);
        }
    }
}
