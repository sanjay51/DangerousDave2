using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float speed = 4.0f;

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


        HandleJetpack();
        Move();
        HandleExit();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position.x += speed * horizontal * Time.deltaTime;

        if (vertical != 0.0f && levelManager.isJetpackRunning())
        {
            position.y += speed * vertical * Time.deltaTime;
        }

        transform.position = position;
    }

    void Jump()
    {
        if (levelManager.isJetpackRunning()) return;

        Debug.Log("Jumping");
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position, Vector2.down, 0.9f, LayerMask.GetMask("Tiles"));

        if (hit.collider == null) { Debug.Log("NO raycast"); return; }

        rigidbody2D.AddForce(transform.up * 350);
    }

    void HandleJetpack()
    {
        if (Input.GetKeyDown(KeyCode.LeftCommand))
        {
            levelManager.ToggleJetpack();
        }

        if (levelManager.isJetpackRunning())
        {
            levelManager.jetpackPower -= 5.0f * Time.deltaTime;
            Debug.Log("Jetpack power left:" + levelManager.jetpackPower);

            rigidbody2D.gravityScale = 0;
        } else rigidbody2D.gravityScale = 1;
    }

    void HandleExit()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Application.Quit();
    }

    public void Die()
    {
        levelManager.Restart();
    }
}
