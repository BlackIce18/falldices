using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserModelUI : MonoBehaviour
{
    [SerializeField] private UserModelItemUI[] _userModelsItemUI;
    [SerializeField] private UserModelItemUI _choosedUserModelItemUI;
    public UserModelItemUI ChoosedUserModelItemUI { get { return _choosedUserModelItemUI; } }

    public void DisableToggles() 
    { 
        for(int i = 0; i < _userModelsItemUI.Length; i++)
        {
            _userModelsItemUI[i].DisableToggle();
        }
    }

    public void ChooseUserModel(UserModelItemUI userModelItemUI)
    {
        _choosedUserModelItemUI = userModelItemUI;
    }
}
