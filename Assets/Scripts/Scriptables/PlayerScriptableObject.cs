using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "SO's/PlayerSO")]
public class PlayerScriptableObject : ScriptableObject
{
    public float Speed;
    public float JumpForce;
    public int NumberOfJumps;
    public LayerMask GroundMask;
    public LayerMask PlatformMask;
}
