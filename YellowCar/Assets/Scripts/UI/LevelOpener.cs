using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOpener : MonoBehaviour
{
    public void OpenLevel()
    {
        SceneManager.LoadScene(Int32.Parse(GetComponentInChildren<TextMeshProUGUI>().text));
    }
}
