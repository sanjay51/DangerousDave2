using System;
using UnityEngine.SceneManagement;

public class LevelManager
{
    public static int level = 1;
    public bool hasCollectedCup { get; set; }

    public LevelManager()
    {
    }

    public void nextLevel()
    {
        level++;
        this.hasCollectedCup = false;
        SceneManager.LoadScene("Level" + level);
    }

    public void Restart()
    {
        hasCollectedCup = false;
        SceneManager.LoadScene("Level" + level);
    }

    public void ResetFromBeginning()
    {
        level = 1;
        Restart();
    }
}
