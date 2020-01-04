using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float speed = 3.0f;

    public LevelManager levelManager = new LevelManager();

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        Move();
        ProcessExit();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector2 position = transform.position;
        position.x += speed * horizontal * Time.deltaTime;
        transform.position = position;
    }

    void Jump()
    {
        Debug.Log("Jumping");
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position, Vector2.down, 0.9f, LayerMask.GetMask("Tiles"));

        if (hit.collider == null) { Debug.Log("NO raycast"); return; }

        rigidbody2D.AddForce(transform.up * 350);
    }

    void ProcessExit()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Application.Quit();
    }

    public void Die()
    {
        levelManager.Restart();
    }
}
