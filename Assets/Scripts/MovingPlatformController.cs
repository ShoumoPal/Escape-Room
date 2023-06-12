using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private bool isMoving;
    [SerializeField] private float speed;
    private Transform currentTarget;
    
    private void Start()
    {
        transform.position = pointA.position;
        currentTarget = pointB;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (Vector2.Distance(transform.position, currentTarget.position) < 0.02f)
            {
                isMoving = false;
                Invoke("ChangeDirection", 2f);
            }
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
    private void ChangeDirection()
    {
        if(currentTarget == pointB)
        {
            currentTarget = pointA;
        }
        else
        {
            currentTarget = pointB;
        }
        isMoving = true;
    }
}
