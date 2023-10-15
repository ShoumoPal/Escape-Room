using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerView : MonoBehaviour
{
    public PlayerController PlayerController { get; set; }
    private float _horizontal;
    private float _vertical;
    private int currentJumps;
    private bool jump = false;
    private bool isSafe = true;
    private Vector3 velocity = Vector3.zero;
    private bool isGrounded;

    // For detection bar
    private float _fullwidth;
    [SerializeField] private float MaxValue;
    [SerializeField] private float Value = 100f;

    [SerializeField] private Animator _playerAnim;
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Animator _detectionBarAnim;   
    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private RectTransform _detectionBar;

    private void Start()
    {
        isGrounded = true;
        _fullwidth = _detectionBar.rect.width;
    }

    private void Update()
    {
        GetInput();
        PlayerController.PlayerAnimation(_horizontal, _vertical, _playerSprite, _playerAnim);
        JumpCheck();
        DetectionCheck();
    }

    private void DetectionCheck()
    {
        if (isSafe)
        {
            _detectionBarAnim.SetBool("Fade", true);
            _detectionBarAnim.SetBool("Show", false);
        }
        else
        {
            if (Value <= 0f)
            {
                UIManager.Instance._gameOverPanel.SetActive(true);
            }
            _detectionBarAnim.SetBool("Show", true);
        }
    }
    private void JumpCheck()
    {
        if (Input.GetButtonDown("Jump") && (currentJumps < PlayerController.PlayerModel.NumberOfJumps))
        {
            jump = true;
            currentJumps++;
            SoundManager.Instance.PlaySFX(SoundTypes.JumpSound);
            _playerAnim.SetBool("isJumping", true);
        }
    }
    private void FixedUpdate()
    {
        PlayerMovement(_horizontal);
    }

    public void SetPlayerController(PlayerController playerController)
    {
        PlayerController = playerController;
    }


    public void PlayerMovement(float _horizontal)
    {
        bool wasGrounded = isGrounded;

        if (_horizontal != 0f && isGrounded)
        {
            SoundManager.Instance.PlayFootsteps();
        }
        else
        {
            SoundManager.Instance.StopFootsteps();
        }
        Vector3 targetVelocity = new Vector2(PlayerController.PlayerModel.Speed * _horizontal * Time.fixedDeltaTime * 10f, _playerRb.velocity.y);
        _playerRb.velocity = Vector3.SmoothDamp(_playerRb.velocity, targetVelocity, ref velocity, 0.05f);

        //For ground check
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, 0.2f, PlayerController.PlayerModel.GroundMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                currentJumps = 0;
                if (!wasGrounded)
                {
                    _playerAnim.SetBool("isJumping", false);
                }
            }
        }
        if (jump && (currentJumps < PlayerController.PlayerModel.NumberOfJumps))
        {
            jump = false;
            isGrounded = false;
            _playerRb.AddForce(new Vector2(0.0f, PlayerController.PlayerModel.JumpForce));
        }
    }

    // For player input
    private void GetInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isSafe = false;
        //Debug.DrawLine(gameObject.transform.position, collision.transform.position);
        Light2D directionalLight = collision.GetComponent<Light2D>();
        if (directionalLight != null)
        {

            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, (collision.gameObject.transform.position - gameObject.transform.position).normalized, 25f, PlayerController.PlayerModel.PlatformMask);
            if (hit)
            {
                isSafe = true;
            }
            if (isSafe)
            {
                //detectionBarAnim.SetBool("Fade", true);
                //detectionBarAnim.SetBool("Show", false);
                Debug.Log("Player safe!");
            }
            else
            {

                //detectionBarAnim.SetBool("Show", true);
                SoundManager.Instance.PlaySFX(SoundTypes.HurtSound);
                ChangeValue(-20);
                float targetValue = Value * _fullwidth / MaxValue;
                _detectionBar.sizeDelta = new Vector2(targetValue, _detectionBar.rect.height);
                Debug.Log("Player dead...");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Light2D>() != null)
        {
            isSafe = true;
        }
    }

    private void ChangeValue(float amount)
    {
        Value = Mathf.Clamp(Value + amount, 0, MaxValue);
    }
}
