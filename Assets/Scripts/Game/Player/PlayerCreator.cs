using System;
using UnityEngine;
using TMPro;
[Serializable]
public struct ChoosedPlayerPrefabAndColor
{
    public GameObject playerPrefab;
    public Material playerMaterial;
}
public class PlayerCreator : MonoBehaviour
{
    [SerializeField] private ChoosedPlayerPrefabAndColor[] _playersPrefabs;
    [SerializeField] private Transform _parentToSpawnPlayers;
    [SerializeField] private GameObject _uiTextBlock;
    [SerializeField] private Transform _parentToSpawnTextBlocks;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _parentToSpawnplayerInfo;
    [SerializeField] private GameObject _playerInfoPrefab;
    [SerializeField] private Transform _spawnPosition;
    void Awake()
    {
        for (int i = 0; i < GameData.lobbyUsers.Count; i++)
        {
            Create(i);
        }
    }

    private void Create(int userIndex)
    {

        Color32 playerColor = GameData.lobbyUsers[userIndex].Color;

        GameObject playerInfo = Instantiate(_playerInfoPrefab, _parentToSpawnplayerInfo);
        PlayerInfoUI playerInfoUI = playerInfo.GetComponent<PlayerInfoUI>();
        playerInfoUI.Nickname.text = GameData.lobbyUsers[userIndex].Nickname;
        playerInfoUI.Nickname.color = playerColor;
        playerInfoUI.MoneyText.text = GameData.startMoney.ToString();
        playerInfoUI.PlayerIcon.sprite = GameData.lobbyUsers[userIndex].Model.Sprite;
        playerInfoUI.PlayerIcon.preserveAspect = true;
        /* GameObject textBlock = Instantiate(_uiTextBlock, _parentToSpawnTextBlocks);
         TextMeshProUGUI textBalance = textBlock.GetComponent<TextMeshProUGUI>();*/
        //GetComponent<Отдельный класс> у которого получаем textmesh
        GameObject playerPrefab = Instantiate(GameData.lobbyUsers[userIndex].Model.Prefab, _parentToSpawnPlayers);
        playerPrefab.GetComponent<Coloring>().SetMaterials(CreateMaterial(playerColor));
        Player player = playerPrefab.GetComponent<Player>();
        player.transform.position = _spawnPosition.position;
        player.Balance.infoUI = playerInfoUI;
        player.Balance.MoneyText = playerInfoUI.MoneyText;
        player.Balance.Money = GameData.startMoney;
        player.Color = playerColor;
        player.NickName = playerInfoUI.Nickname.text;
        player.currentCell = GameController.Singleton.GetFieldCell(0);
        _playerController.AddNewPlayer(player);
    }

    private Material CreateMaterial(Color32 color)
    {
        Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        material.color = color;
        return material;
    }
}
