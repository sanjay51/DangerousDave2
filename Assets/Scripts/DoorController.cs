using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DaveController daveController = collision.gameObject.GetComponent<DaveController>();
        if (daveController != null)
        {
            if (DaveController.gameState.levelState.hasCollectedCup)
            {
                Destroy(gameObject);
                Debug.Log("Istransitioning:" + DaveController.gameState.isLevelTransitioning);
                DaveController.gameState.nextLevel();
            }
        }
    }
}
