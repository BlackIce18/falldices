using UnityEngine;

public class LobbyReset : MonoBehaviour
{
    [SerializeField] private LobbyWindow[] _lobbyWindows;
    [SerializeField] private GameObject _startLobbyCreateWindow;

    public void ResetWindows()
    {
        for(int i = 0; i < _lobbyWindows.Length; i++)
        {
            _lobbyWindows[i].ResetObjects();
            _lobbyWindows[i].gameObject.SetActive(false);
        }

        _startLobbyCreateWindow.SetActive(true);
    }
}
