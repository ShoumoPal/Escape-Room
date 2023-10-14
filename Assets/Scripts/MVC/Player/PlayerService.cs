using System;
using UnityEngine;

public class PlayerService : GenericLazySingleton<PlayerService>
{
    [HideInInspector]
    public PlayerController Player;

    [SerializeField] private PlayerScriptableObject _playerSO;
    [SerializeField] private PlayerView _playerPrefab;

    private void Start()
    {
        SpawnPlayer(LevelManager.Instance.GetLocationFromCurrentLevel());
    }

    private void SpawnPlayer(Vector3 location)
    {
        PlayerModel model = new PlayerModel(_playerSO);
        PlayerView view = Instantiate<PlayerView>(_playerPrefab, location, Quaternion.identity);
        
    }
}
