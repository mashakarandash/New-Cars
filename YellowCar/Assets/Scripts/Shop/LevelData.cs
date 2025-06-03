public class LevelData 
{
    public int StarsInLevel;
    public bool IsLevelPast;
    public int SceneID;
    public bool IsLevelUnlock;

    public LevelData(int levelSceneID, bool isLevelUnlock = false)
    {
        SceneID = levelSceneID;
        IsLevelUnlock = isLevelUnlock;
    }

}
