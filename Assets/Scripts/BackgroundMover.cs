using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    private SpriteRenderer backgroundSprite;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float moveAmount;
    private float position;

    private void Start()
    {
        backgroundSprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        position += speed;
        if(position > moveAmount)
        {
            position -= moveAmount;
        }
        transform.position = new Vector3(position, transform.position.y, transform.position.z);
    }
}
