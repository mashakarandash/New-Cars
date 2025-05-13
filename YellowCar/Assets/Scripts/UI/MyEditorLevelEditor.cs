using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditorLevelsNumber))]
public class MyEditorLevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorLevelsNumber targetComponent = (EditorLevelsNumber)target;
        if (GUILayout.Button("Создать уровень"))
        {
            targetComponent.AddLevel();
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(targetComponent);
            }

        }    
    }
}
