using UnityEngine;

public class LobbyUsers : LobbyWindow, ICanResetObjects
{
    [SerializeField] private GameObject _usersList;
    public override void ResetObjects()
    {
        for(int i = 0; i < _usersList.transform.childCount; i++)
        {
            Destroy(_usersList.transform.GetChild(i).gameObject);
        }
    }
}
