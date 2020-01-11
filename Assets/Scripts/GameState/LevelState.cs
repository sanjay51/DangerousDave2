using System;
public class LevelState
{

    public bool hasCollectedCup { get; set; } = false;
    public float jetpackPower { get; set; } = 0.0f;
    public bool isJetpackEnabled = false;
    public bool hasGun { get; set; } = false;

    public LevelState()
    {
    }
}
