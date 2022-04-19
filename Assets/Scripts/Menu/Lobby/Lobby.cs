using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    [SerializeField] private GameObject _lobbyUserItemPrefab;
    [SerializeField] private PrepareGame _prepareSingleGame;
    [SerializeField] private FirestoreDataBase _db;

    private void OnEnable()
    {
        AddUsers(_db.ActiveUser.DisplayName, _prepareSingleGame.PlayerColor, _prepareSingleGame.ModelId);

        for (int i = 0; i < _prepareSingleGame.PlayersCount; i++)
        {
            Color32 randomColor = new Color32((byte)Random.Range(0,255), (byte)Random.Range(0, 255),(byte)Random.Range(0, 255), 255);
            AddUsers("bot_"+i, randomColor, 0);
        }
    }

    public void AddUsers(string nickname, Color32 color, int modelId = 0)
    {
        LobbyUserItemPrefab lobbyUserItemPrefab = Instantiate(_lobbyUserItemPrefab, this.transform).GetComponent<LobbyUserItemPrefab>();
        lobbyUserItemPrefab.Nickname.color = color;
        lobbyUserItemPrefab.Nickname.text = nickname;
        lobbyUserItemPrefab.PlayerIcon.sprite = _prepareSingleGame.ModelsAssociation.GetModelById(modelId).Sprite;
        lobbyUserItemPrefab.PlayerIcon.preserveAspect = true;
    }
}
