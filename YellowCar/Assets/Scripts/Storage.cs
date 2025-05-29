using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage  
{
    public ReactiveProperty<int> Money = new ReactiveProperty<int>();
    public ReactiveProperty<int> LightningBonusCount = new ReactiveProperty<int>();
    public ReactiveProperty<int> FreezeBonusCount = new ReactiveProperty<int>();
    public ReactiveProperty<int> TaxiBonusCount = new ReactiveProperty<int>();

    public List<LevelData> LevelsInformation = new List<LevelData>();
    public int CurrentLevelID;
    public bool IsSigneShowed;
    internal int NextSceneToUnlock;
    public void InitializeStorage(SaveData saveData)
    {
        Money.Value = saveData.Money;
        LightningBonusCount.Value = saveData.LightBuffCount;
        FreezeBonusCount.Value = saveData.FreezeBuffCount;
        TaxiBonusCount.Value = saveData.TaxiBuffCount;
        CurrentLevelID = saveData.LastPastLevel;
        NextSceneToUnlock = CurrentLevelID + 1;
    }
}
