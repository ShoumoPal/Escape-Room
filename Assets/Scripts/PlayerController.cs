using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Animator playerAnim;
    private SpriteRenderer playerSprite;
    private Rigidbody2D playerRb;


    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal != 0f)
        {
            //Vector3 temp = transform.position;
            //temp.x += speed * horizontal * Time.deltaTime;
            //transform.position = temp;
            
            playerAnim.SetBool("isRunning", true);
            playerSprite.flipX = (horizontal < 0f) ? true : false;
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }
    }
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        playerRb.AddForce(new Vector2(speed * horizontal, 0.0f));
    }
}
