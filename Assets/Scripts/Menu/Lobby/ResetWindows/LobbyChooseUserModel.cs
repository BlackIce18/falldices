using UnityEngine;

public class LobbyChooseUserModel : LobbyWindow, ICanResetObjects
{
    [SerializeField] private UserModelUI _userModelUI;
    public override void ResetObjects() 
    {
        _userModelUI.DisableToggles();
        _userModelUI.ChooseUserModel(_userModelUI.UserModels[0]);
    }
}
