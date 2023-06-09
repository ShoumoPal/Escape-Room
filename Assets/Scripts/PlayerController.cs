using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private int numberOfJumps;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask platformMask;
    [SerializeField]
    private RectTransform detectionBar;
    [SerializeField]
    private Animator detectionBarAnim;
    [SerializeField]
    private GameObject gameOverPanel;

    private Animator playerAnim;
    private SpriteRenderer playerSprite;
    private Rigidbody2D playerRb;
    private Vector3 velocity = Vector3.zero;
    private bool isGrounded;

    private float horizontal;
    private float vertical;

    private bool playerFacingRight = true;

    [SerializeField]
    private bool jump = false;
    private int currentJumps;

    //For detection bar
    private float _fullwidth;
    [SerializeField]
    private float Value;
    [SerializeField] 
    private float MaxValue;
    private bool isSafe = true;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
        numberOfJumps = 2;
        isGrounded = true;
        _fullwidth = detectionBar.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        playerFacingRight = horizontal > 0f;
        PlayerMoveAnimation(horizontal, vertical);
        if (Input.GetButtonDown("Jump") && (currentJumps < numberOfJumps))
        {
            jump = true;
            currentJumps++;
            SoundManager.Instance.PlaySFX(SoundTypes.JumpSound);
            playerAnim.SetBool("isJumping", true);
        }

        //For detecttion Bar
        if(isSafe)
        {
            detectionBarAnim.SetBool("Fade", true);
            detectionBarAnim.SetBool("Show", false);
        }
        else
        {
            if(Value <= 0f)
            {
                gameOverPanel.SetActive(true);
            }
            detectionBarAnim.SetBool("Show", true);
        }

    }
    public void PlayerMoveAnimation(float _horizontal, float _vertical)
    {
        if (_horizontal != 0f)
        {
            //Vector3 temp = transform.position;
            //temp.x += speed * horizontal * Time.deltaTime;
            //transform.position = temp;

            playerAnim.SetBool("isRunning", true);
            playerSprite.flipX = (!playerFacingRight) ? true : false;
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }
    }
    public void PlayerMovement(float _horizontal)
    {
        bool wasGrounded = isGrounded;
        if(horizontal != 0f && wasGrounded)
        {
            SoundManager.Instance.PlayFootsteps();
        }
        else
        {
            SoundManager.Instance.StopFootsteps();
        }
        Vector3 targetVelocity = new Vector2(speed * horizontal * Time.fixedDeltaTime * 10f, playerRb.velocity.y);
        playerRb.velocity = Vector3.SmoothDamp(playerRb.velocity, targetVelocity, ref velocity, 0.05f);

        //For ground check
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, whatIsGround);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                currentJumps = 0;
                if(!wasGrounded)
                {
                    playerAnim.SetBool("isJumping", false);
                }
            }
        }
        if (jump && (currentJumps < numberOfJumps))
        {
            jump = false;
            isGrounded = false;
            playerRb.AddForce(new Vector2(0.0f, jumpForce));
        }
    }
    private void FixedUpdate()
    {
        PlayerMovement(horizontal);
    }
    //For detecting collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isSafe = false;
        //Debug.DrawLine(gameObject.transform.position, collision.transform.position);
        Light2D directionalLight = collision.GetComponent<Light2D>();
        if(directionalLight != null)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, (collision.gameObject.transform.position - gameObject.transform.position).normalized, 25f, platformMask);
            if (hit)
            {
                isSafe = true;
            }
            if(isSafe)
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
                detectionBar.sizeDelta = new Vector2(targetValue, detectionBar.rect.height);
                Debug.Log("Player dead...");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Light2D>() != null)
        {
            isSafe = true;
        }
    }
    private void ChangeValue(float amount)
    {
        Value = Mathf.Clamp(Value + amount, 0, MaxValue);
    }
}
