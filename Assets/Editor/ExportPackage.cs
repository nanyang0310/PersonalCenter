using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExportPackage : EditorWindow
{
    [MenuItem("Examples/Export Packages")]
    static void ExportPackages()
    {
        var path = EditorUtility.SaveFilePanel("Save unitypackage", "", "", "unitypackage");
        Debug.Log(path);
        if (path == "")
            return;
        //选中的文件
        var assetPathNames = new string[Selection.objects.Length];
        for (var i = 0; i < assetPathNames.Length; i++)
        {
            assetPathNames[i] = AssetDatabase.GetAssetPath(Selection.objects[i]);
        }

        //获取选中文件的依赖项 
        assetPathNames = AssetDatabase.GetDependencies(assetPathNames);
        //导出package包
        AssetDatabase.ExportPackage(assetPathNames, path, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
    }
}
