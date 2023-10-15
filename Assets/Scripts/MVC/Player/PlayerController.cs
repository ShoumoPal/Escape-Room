using UnityEngine;

public class PlayerController
{
    public PlayerModel PlayerModel;
    public PlayerView PlayerView;

    // Constructor

    public PlayerController(PlayerModel playerModel, PlayerView playerView)
    {
        PlayerModel = playerModel;
        PlayerView = playerView;
    }

    public void PlayerAnimation(float horizontal, float vertical, SpriteRenderer _playerSprite, Animator _playerAnim)
    {
        bool _playerFacingRight = horizontal > 0f;
        if (horizontal != 0f)
        {
            _playerAnim.SetBool("isRunning", true);
            _playerSprite.flipX = (!_playerFacingRight) ? true : false;
        }
        else
        {
            _playerAnim.SetBool("isRunning", false);
        }
    }
}
