using System.Collections.Generic;
using System.Linq;

public class SaveData
{
    public int Money;
    public int LightBuffCount;
    public int FreezeBuffCount;
    public int TaxiBuffCount;
    public LevelData[] SavedLevelData;
    

    public void AddMoney(int moneyCount)
    {
        Money += moneyCount;
    }

    public void SaveAllBonus(int lightningBonus, int freezeBonus, int taxiBonus)
    {
        LightBuffCount = lightningBonus;
        FreezeBuffCount = freezeBonus;
        TaxiBuffCount = taxiBonus;
    }

    public void SaveNewLevel(LevelData levelToSave)
    {
        if (SavedLevelData != null)
        {
            List<LevelData> levelData = SavedLevelData.ToList();
            levelData.Add(levelToSave);
            levelData.Add(new LevelData(levelToSave.SceneID + 1));
            SavedLevelData = levelData.ToArray();
        }
        else
        {
            SavedLevelData = new LevelData[] { levelToSave, new LevelData(levelToSave.SceneID + 1) };
            
        }

    }
}
