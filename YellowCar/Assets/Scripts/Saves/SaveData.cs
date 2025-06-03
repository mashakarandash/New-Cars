using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

[Serializable]
public class SaveData
{
    private int _money;
    private int _lightBuffCount;
    private int _freezeBuffCount;
    private int _taxiBuffCount;


    #region FEATURES

    public int Money
    {
        get => _money;
        set
        {
            if (value <= 0)
            {
                _money = 0;
            }
            else
            {
                _money = value;
            }
        }
    }

    public int LightBuffCount
    {
        get => _lightBuffCount;
        set
        {
            if (value <= 0)
            {
                _lightBuffCount = 0;
            }
            else
            {
                _lightBuffCount = value;
            }
        }
    }

    public int FreezeBuffCount
    {
        get => _freezeBuffCount;
        set
        {
            if (value <= 0)
            {
                _freezeBuffCount = 0;
            }
            else
            {
                _freezeBuffCount = value;
            }
        }
    }

    public int TaxiBuffCount
    {
        get => _taxiBuffCount;
        set
        {
            if (value <= 0)
            {
                _taxiBuffCount = 0;
            }
            else
            {
                _taxiBuffCount = value;
            }
        }
    }
    #endregion

    public LevelData[] SavedLevelData = new LevelData[]
    {
        new LevelData(1, true),
        new LevelData(2),
        new LevelData(3),
        new LevelData(4),
        new LevelData(5),
        new LevelData(6),
        new LevelData(7),
        new LevelData(8),
        new LevelData(9),
        new LevelData(10),
        new LevelData(11),
        new LevelData(12),
        new LevelData(13),
        new LevelData(14),
        new LevelData(15),
        new LevelData(16),
    };

    public int LastPastLevel;
    public CarShopModelData[] CarSaves;
    public bool IsPrefabVariant2Activated;

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

    public void SaveLevelCondition(int levelID, int ganeStar)
    {
        LevelData levelData = SavedLevelData.FirstOrDefault(name => name.SceneID == levelID);
        if (levelData != null)
        {
            levelData.IsLevelPast = true;
            levelData.StarsInLevel = ganeStar;
            LevelData levelDataNext = SavedLevelData.FirstOrDefault(name => name.SceneID == levelID + 1);

            if (levelDataNext != null)
            {
                levelDataNext.IsLevelUnlock = true;
            }
        }
        else
        {
            throw new System.Exception("уровень под номером " + levelID + " не найден ");
        }
    }
}
