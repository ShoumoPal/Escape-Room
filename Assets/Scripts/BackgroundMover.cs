using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    private float length;
    private float startPosition;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallax;

    private void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void Update()
    {
        float temp = cam.transform.position.x * (1 - parallax);
        float distance = cam.transform.position.x * parallax;

        transform.position = new Vector3 (startPosition + distance, transform.position.y, transform.position.z);

        if(temp > startPosition + length)
        {
            startPosition += length;
        }
        else if(temp < startPosition - length)
        {
            startPosition -= length;
        }
    }
}
