using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float speed;
    private float time = 0f;
    private bool hasCollided = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
    }

    private void FixedUpdate()
    {
        if (!hasCollided)
        {
            Move(true);
        }
        else
        {
            Move(false);
        }
    }
    private void Update()
    {
        time += Time.deltaTime;
        if(time > 2f)
        {
            time = 0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PatrolPoint")
        {
            hasCollided = true;
            StartCoroutine(IdleWait());
            speed *= -1;
        }
    }
    IEnumerator IdleWait()
    {
        anim.SetBool("isRunning", false);
        yield return new WaitForSeconds(2f);
        anim.SetBool("isRunning", true);
        hasCollided = false;
        FlipSprite();
    }
    private void Move(bool move)
    {
        if (move)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
    }
    private void FlipSprite()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }
}
