using UnityEngine;
using TMPro;
public class UsernameReseter : LobbyWindow, ICanResetObjects
{
    [SerializeField] private TMP_InputField _nickname;
    public override void ResetObjects()
    {
        _nickname.text = "";
    }
}
