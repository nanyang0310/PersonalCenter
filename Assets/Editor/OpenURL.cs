using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;

public class OpenURL : EditorWindow
{
    [MenuItem("Examples/OpenURL %,")]
    static void Apply()
    {
        //纯打开
        //Application.OpenURL("file://" + Application.dataPath);
        Texture2D texture = Selection.activeObject as Texture2D;
        if (texture == null)
        {
            EditorUtility.DisplayDialog("Select Texture", "You must select a texture first!", "OK");
            return;
        }

        string path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
        if (path.Length != 0)
        {
            var fileContent = File.ReadAllBytes(path);
            texture.LoadImage(fileContent);
        }
    }
}

