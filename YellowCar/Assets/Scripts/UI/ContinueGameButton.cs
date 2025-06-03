using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using UnityEngine.SceneManagement;
using TMPro;

public class ContinueGameButton : MonoBehaviour
{
    [SerializeField] private Transform _uIYellCarAnimation;
    [SerializeField] private TextMeshProUGUI _text;

    private MasterSave _masterSave;

    [Inject]
    private void Constract (MasterSave masterSave)
    {
        _masterSave = masterSave;

    }

    void Start()
    {
        _uIYellCarAnimation.DORotate(new Vector3(0, 0, 360), 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        _text.text = "Level " + (_masterSave.SaveData.LastPastLevel + 1);
    }

    public void ContinueGameLevel()
    {
        SceneManager.LoadScene(_masterSave.SaveData.LastPastLevel + 1);
        
    }
}
