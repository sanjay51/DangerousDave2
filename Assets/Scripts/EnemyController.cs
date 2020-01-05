using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
