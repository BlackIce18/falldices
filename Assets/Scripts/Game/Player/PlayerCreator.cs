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
    [SerializeField] private GameField _gameField;
    [SerializeField] private Transform _parentToSpawnplayerInfo;
    [SerializeField] private GameObject _playerInfoPrefab;
    void Awake()
    {
        for (int i = 0; i < GameData.users.Count; i++)
        {
            Create(i);
        }
    }

    private void Create()
    {
        for(int i = 0; i < _playersPrefabs.Length; i++)
        {
            GameObject playerPrefab = Instantiate(_playersPrefabs[i].playerPrefab, _parentToSpawnPlayers);
            Player player = playerPrefab.GetComponent<Player>();
            player.transform.position = _gameField.GetPointPosition(0);

            Coloring playerColoring = playerPrefab.GetComponent<Coloring>();
            playerColoring.SetMaterials(_playersPrefabs[i].playerMaterial);

            GameObject playerInfo = Instantiate(_playerInfoPrefab, _parentToSpawnplayerInfo);
            PlayerInfoUI playerInfoUI = playerInfo.GetComponent<PlayerInfoUI>();
            playerInfoUI.Nickname.text = player.NickName;
            playerInfoUI.MoneyText.text = GameData.startMoney.ToString();
            playerInfoUI.Nickname.color = _playersPrefabs[i].playerMaterial.color;
            /* GameObject textBlock = Instantiate(_uiTextBlock, _parentToSpawnTextBlocks);
             TextMeshProUGUI textBalance = textBlock.GetComponent<TextMeshProUGUI>();*/
            //GetComponent<Отдельный класс> у которого получаем textmesh
            player.Balance.MoneyText = playerInfoUI.MoneyText;
            player.Balance.Money = GameData.startMoney;
            player.Color = _playersPrefabs[i].playerMaterial.color;


            _gameField.AddNewPlayerUIInfo(playerInfoUI);
            _gameField.AddNewPlayer(player);
        }
    }

    private void Create(int userIndex)
    {
        GameObject playerPrefab = Instantiate(GameData.users[userIndex].model.Prefab, _parentToSpawnPlayers);
        Player player = playerPrefab.GetComponent<Player>();
        player.transform.position = _gameField.GetPointPosition(0);

        Coloring playerColoring = playerPrefab.GetComponent<Coloring>();
        Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        Color32 playerColor = GameData.users[userIndex].color;
        material.color = playerColor;
        playerColoring.SetMaterials(material);

        GameObject playerInfo = Instantiate(_playerInfoPrefab, _parentToSpawnplayerInfo);
        PlayerInfoUI playerInfoUI = playerInfo.GetComponent<PlayerInfoUI>();
        playerInfoUI.Nickname.text = GameData.users[userIndex].nickname;
        playerInfoUI.Nickname.color = playerColor;
        playerInfoUI.MoneyText.text = GameData.startMoney.ToString();
        playerInfoUI.PlayerIcon.sprite = GameData.users[userIndex].icon;
        playerInfoUI.PlayerIcon.preserveAspect = true;
        /* GameObject textBlock = Instantiate(_uiTextBlock, _parentToSpawnTextBlocks);
         TextMeshProUGUI textBalance = textBlock.GetComponent<TextMeshProUGUI>();*/
        //GetComponent<Отдельный класс> у которого получаем textmesh
        player.Balance.MoneyText = playerInfoUI.MoneyText;
        player.Balance.Money = GameData.startMoney;
        player.Color = playerColor;
        player.NickName = GameData.users[userIndex].nickname;

        _gameField.AddNewPlayerUIInfo(playerInfoUI);
        _gameField.AddNewPlayer(player);
    }
}
