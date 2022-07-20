using System.Collections.Generic;
using UnityEngine;

public class Lobby : LobbyWindow, ICanResetObjects
{
    [SerializeField] private GameObject _usersList;
    [SerializeField] private GameObject _lobbyUserItemPrefab;
    [SerializeField] private PrepareGame _prepareGame;
    private List<LobbyUser> _lobbyUsers = new List<LobbyUser>();
    public List<LobbyUser> LobbyUsers { get { return _lobbyUsers; } }
    public override void ResetObjects()
    {
        for(int i = 0; i < _usersList.transform.childCount; i++)
        {
            Destroy(_usersList.transform.GetChild(i).gameObject);
        }
    }

    private void AddPlayer(string nickname, Color32 color, int modelId = 0)
    {
        LobbyUserItem playerInfo = Instantiate(_lobbyUserItemPrefab, _usersList.transform).GetComponent<LobbyUserItem>();
        playerInfo.Initialize(nickname, color, ModelsAssociation.ModelsAssociationSingleton.GetModelById(modelId));
        var lobbyUser = new LobbyUser(nickname, color, modelId);
        _lobbyUsers.Add(lobbyUser);
    }

    private void GeneratePlayers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            string nickName = "player_" + i;
            Color32 randomColor = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
            int modelId = UnityEngine.Random.Range(0, ModelsAssociation.ModelsAssociationSingleton.GetModelsCount());

            AddPlayer(nickName, randomColor, modelId);
        }
    }
    public void OnEnable()
    {
        AddPlayer(_prepareGame.Nickname, _prepareGame.PlayerColor, _prepareGame.PlayerModelId);
        GeneratePlayers(_prepareGame.PlayersCount);
    }
}
