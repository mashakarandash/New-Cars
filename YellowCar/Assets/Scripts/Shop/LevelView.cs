using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelView : MonoBehaviour
{
    public int SceneToOpen;
    private Storage _storage;

    [Inject]
    public void Constract(Storage storage)
    {
        _storage = storage;
    }

    public void OpenScene()
    {
        LevelData levelInfo = _storage.LevelsInformation.FirstOrDefault(x => x.SceneID == SceneToOpen);
        if (levelInfo == null)
        {
            _storage.LevelsInformation.Add(new LevelData(SceneToOpen));
            _storage.CurrentLevelID = SceneToOpen;
        }
        else
        {
            _storage.CurrentLevelID = SceneToOpen;
        }
        SceneManager.LoadScene(SceneToOpen);
    }
}
