using Cinemachine;
using UnityEngine;

public class PlayerService : GenericLazySingleton<PlayerService>
{
    [HideInInspector]
    public PlayerController Player;

    [SerializeField] private PlayerScriptableObject _playerSO;
    [SerializeField] private PlayerView _playerPrefab;
    [SerializeField] private CinemachineVirtualCamera _playerVirtualCamera;

    private void Start()
    {
        SpawnPlayer(LevelManager.Instance.GetLocationFromCurrentLevel());
    }

    private void SpawnPlayer(Vector3 location)
    {
        PlayerModel model = new PlayerModel(_playerSO);
        PlayerView view = Instantiate<PlayerView>(_playerPrefab, location, Quaternion.identity);
        //making the camera follow the player
        _playerVirtualCamera.Follow = view.gameObject.transform;
        Player = new PlayerController(model, view);

        //Linking rest of the MVC
        model.SetPlayerController(Player);
        view.SetPlayerController(Player);
    }
}
