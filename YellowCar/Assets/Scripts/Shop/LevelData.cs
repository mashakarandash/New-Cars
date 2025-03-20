public class LevelData 
{
    public int StarsInLevel;
    public bool IsLevelPast;
    public int SceneID;
    public bool IsLevelUnlock;

    public LevelData(int levelSceneID)
    {
        SceneID = levelSceneID;
        IsLevelUnlock = true;
    }

}
