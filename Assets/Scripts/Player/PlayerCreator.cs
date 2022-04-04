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

            PlayerColoring playerColoring = playerPrefab.GetComponent<PlayerColoring>();
            playerColoring.SetMaterials(_playersPrefabs[i].playerMaterial);

            GameObject textBlock = Instantiate(_uiTextBlock, _parentToSpawnTextBlocks);
            //GetComponent<Отдельный класс> у которого получаем textmesh
            TextMeshProUGUI textBalance = textBlock.GetComponent<TextMeshProUGUI>();
            player.PlayerBalance.MoneyText = textBalance;
            player.PlayerBalance.StartMoney();


            _gameField.AddNewPlayer(player);
        }
    }
}
