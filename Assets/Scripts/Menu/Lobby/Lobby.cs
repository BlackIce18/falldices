using UnityEngine;

public class Lobby : MonoBehaviour
{
    [SerializeField] private GameObject _lobbyUserItemPrefab;
    [SerializeField] private PrepareGame _prepareSingleGame;
    [SerializeField] private FirestoreDataBase _db;

    private void OnEnable()
    {
        AddPlayer(_db.ActiveUser.DisplayName, _prepareSingleGame.PlayerColor, _prepareSingleGame.PlayerModelId);
    }

    public void AddPlayer(string nickname, Color32 color, int modelId = 0)
    {
        LobbyUserItem playerInfo = Instantiate(_lobbyUserItemPrefab, this.transform).GetComponent<LobbyUserItem>();
        playerInfo.color = color;
        playerInfo.Nickname.color = color;
        playerInfo.Nickname.text = nickname;
        playerInfo.model = _prepareSingleGame.ModelsAssociation.GetModelById(modelId);
        playerInfo.Icon.sprite = playerInfo.model.Sprite;
        playerInfo.Icon.preserveAspect = true;
        PrepareGame.users.Add(playerInfo);
    }

    public void AddBots()
    {
        for (int i = 0; i < _prepareSingleGame.PlayersCount; i++)
        {
            Color32 randomColor = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
            AddPlayer("bot_" + i, randomColor, 0);
        }
    }
}
