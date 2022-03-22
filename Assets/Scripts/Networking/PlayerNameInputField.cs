using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    private const string _playerNamePrefKey = "PlayerName";
    // Start is called before the first frame update
    private void Start()
    {
        string defaultName = string.Empty;
        TMP_InputField _inputField = this.GetComponent<TMP_InputField>();

        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(_playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(_playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName(string value)
    {
        TMP_InputField _inputField = this.GetComponent<TMP_InputField>();
        if (string.IsNullOrEmpty(_inputField.text))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = _inputField.text;

        PlayerPrefs.SetString(_playerNamePrefKey, _inputField.text);
    }
}
