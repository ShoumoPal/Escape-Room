using UnityEngine;

// Model for MVC

public class PlayerModel
{
    public float Speed { get; }
    public float JumpForce { get; }
    public int NumberOfJumps { get; }
    public LayerMask GroundMask { get; }
    public LayerMask PlatformMask { get; }

    public PlayerController PlayerController { get; set; }

    public PlayerModel(PlayerScriptableObject playerSO)
    {
        Speed = playerSO.Speed;
        JumpForce = playerSO.JumpForce;
        NumberOfJumps = playerSO.NumberOfJumps;
        GroundMask = playerSO.GroundMask;
        PlatformMask = playerSO.PlatformMask;
    }

    public void SetPlayerController(PlayerController playerController)
    {
        PlayerController = playerController;
    }
}
