using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : MonoBehaviour
{
    private MasterSave _masterSave;

    public void ChangeScene(string sceneName)
    {
        _masterSave.SaveData.LastPastLevel = SceneManager.GetActiveScene().buildIndex;
        _masterSave.SaveAllData();
        SceneManager.LoadScene(sceneName);
    }

    [Inject]
    private void Constract(MasterSave masterSave)
    {
        _masterSave = masterSave;
    }
}
