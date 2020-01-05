using System;
using UnityEngine.SceneManagement;

public class LevelManager
{
    public static int level = 1;
    public bool hasCollectedCup { get; set; }
    public float jetpackPower { get; set; }
    private bool isJetpackEnabled;

    public LevelManager()
    {
        Reset();
    }

    public void nextLevel()
    {
        level++;
        this.hasCollectedCup = false;
        SceneManager.LoadScene("Level" + level);
    }

    public void Restart()
    {
        Reset();
        SceneManager.LoadScene("Level" + level);
    }

    public void ToggleJetpack()
    {
        this.isJetpackEnabled = !isJetpackEnabled;
    }

    public bool isJetpackRunning()
    {
        return this.isJetpackEnabled && jetpackPower > 0;
    }

    public void Reset()
    {
        hasCollectedCup = false;
        jetpackPower = 0;
    }

    public void ResetFromBeginning()
    {
        level = 1;

        Restart();
    }
}
