using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
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
        DaveController daveController = collision.gameObject.GetComponent<DaveController>();
        if (daveController != null)
        {
            daveController.levelManager.hasGun = true;
            Destroy(gameObject);
        }
    }
}
