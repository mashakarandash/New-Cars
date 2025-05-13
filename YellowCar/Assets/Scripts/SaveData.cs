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

    public LevelData[] SavedLevelData;
    public int LastPastLevel;

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
