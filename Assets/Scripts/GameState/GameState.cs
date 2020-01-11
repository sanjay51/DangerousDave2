using System;
using UnityEngine.SceneManagement;

public class GameState
{
    public static int level = 1;
    public bool isLevelTransitioning = false;
    public LevelState levelState;

    public GameState()
    {
        levelState = new LevelState();
        isLevelTransitioning = false;
    }

    public void nextLevel()
    {
        levelState = new LevelState();
        if (isLevelTransitioning)
        {
            level++;
            SceneManager.LoadScene("Level" + level);
            this.levelState.hasCollectedCup = false;
            isLevelTransitioning = false;
        } else
        {
            SceneManager.LoadScene("LevelTransition");
            isLevelTransitioning = true;
            levelState.hasCollectedCup = true;
        }
    }

    public void ToggleJetpack()
    {
        this.levelState.isJetpackEnabled = !levelState.isJetpackEnabled;
    }

    public bool isJetpackRunning()
    {
        return this.levelState.isJetpackEnabled && levelState.jetpackPower > 0;
    }

    public bool isClimbing()
    {
        return false;
    }

    public void ResetLevel()
    {
        levelState = new LevelState();
        SceneManager.LoadScene("Level" + level);
    }

    public void ResetGame()
    {
        level = 1;
        ResetLevel();
    }
}
