using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelChooseView : MonoBehaviour
{
    [SerializeField] private List<LevelViewUI> _levelViews;
    private MasterSave _masterSave;

    [Inject]
    private void Constract(MasterSave masterSave)
    {
        _masterSave = masterSave;
    }

    private void Start()
    {
        int i = 0;
        foreach (LevelData level in _masterSave.SaveData.SavedLevelData)
        {
            
                _levelViews[i].Initialize(level.StarsInLevel, level.IsLevelUnlock);
            
            i++;
        }
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
