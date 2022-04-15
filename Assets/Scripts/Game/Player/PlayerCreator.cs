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
        Create();
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
            playerInfoUI.Nickname.color = _playersPrefabs[i].playerMaterial.color;
            /* GameObject textBlock = Instantiate(_uiTextBlock, _parentToSpawnTextBlocks);
             TextMeshProUGUI textBalance = textBlock.GetComponent<TextMeshProUGUI>();*/
            //GetComponent<Отдельный класс> у которого получаем textmesh
            player.PlayerBalance.MoneyText = playerInfoUI.MoneyText;
            player.PlayerBalance.StartMoney();
            player.Color = _playersPrefabs[i].playerMaterial.color;


            _gameField.AddNewPlayerUIInfo(playerInfoUI);
            _gameField.AddNewPlayer(player);
        }
    }
}
