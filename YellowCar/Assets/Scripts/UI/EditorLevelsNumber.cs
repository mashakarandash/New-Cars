using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorLevelsNumber : MonoBehaviour
{
    [SerializeField] private GameObject _levelPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _currentAddedLevel;

    
    [ContextMenu("AddLevel")]
    public void AddLevel()
    {
        var prefab = Instantiate(_levelPrefab, _parent);
        prefab.GetComponentInChildren<TextMeshProUGUI>().text = _currentAddedLevel.ToString();
        prefab.GetComponent<Button>().onClick.AddListener(() => Debug.Log(1));
        _currentAddedLevel++;
    }
}
