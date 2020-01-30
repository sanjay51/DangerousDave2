using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyController : MonoBehaviour
{
    public PathCreator path;
    public float speed = 3.0f;
    public float rotationSpeed = 1.0f;
    public float shootSpeed = 0.0f;

    public float distanceTravelled;
    public AudioClip enemyBlastAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (path != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(distanceTravelled);
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
            DaveController.PlaySound(enemyBlastAudioClip);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
