using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class SaveTexture : EditorWindow
{
    [MenuItem("Examples/Save Texture to file")]
    static void Apply()
    {
        //注意，保存的图片需要勾选了Read/Write Enable 为true
        Texture2D texture = Selection.activeObject as Texture2D; //选中一个图片
        if (texture == null)
        {  //如果没选图片，显示提示对话框
            EditorUtility.DisplayDialog(
                "Select Texture",
                "You Must Select a Texture first!",
                "Ok");
            return;
        }
        //获取路径， EditorUtility.SaveFilePanel --打开保存文件对话框，参数：弹窗的名称、路径、文件保存名称、格式
        string path = EditorUtility.SaveFilePanel(
                "Save texture as PNG",
                "",  //桌面 
                texture.name + ".png",
                "png");

        if (path.Length != 0)
        {
            // Convert the texture to a format compatible with EncodeToPNG
            if (texture.format != TextureFormat.ARGB32 && texture.format != TextureFormat.RGB24)
            {
                Texture2D newTexture = new Texture2D(texture.width, texture.height);
                newTexture.SetPixels(texture.GetPixels(0), 0);
                texture = newTexture;
            }
            var pngData = texture.EncodeToPNG();
            if (pngData != null)
                File.WriteAllBytes(path, pngData);
        }
    }
}