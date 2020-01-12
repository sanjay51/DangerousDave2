using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] points;
    public float speed = 3.0f;
    public float rotationSpeed = 1.0f;

    private int currentPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (points != null && points.Length != 0)
        {
            if (transform.position != points[currentPoint].position)
            {
                Vector2 pos = Vector2.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);
                transform.position = pos;
            } else
            {
                currentPoint = (currentPoint + 1) % points.Length;
            }
        }

        if (rotationSpeed != 0)
        {
            transform.eulerAngles += Vector3.forward * rotationSpeed;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleDaveCollision(collision);
        HandleBulletCollision(collision);
    }

    private void HandleDaveCollision(Collider2D collision)
    {
        DaveController daveController = collision.gameObject.GetComponent<DaveController>();
        if (daveController != null)
        {
            daveController.Die();
        }
    }

    private void HandleBulletCollision(Collider2D collision)
    {
        BulletController bulletController = collision.gameObject.GetComponent<BulletController>();
        if (bulletController != null)
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
