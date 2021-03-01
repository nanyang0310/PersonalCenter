/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using xxl.hanm;

public class ScreenShotTool : Singleton<ScreenShotTool>
{
    protected ScreenShotTool() { }

    //四个点的顺序为 左下-右下-右上-左上 四个点位置即为截图区域，顺序很重要
    public RectTransform m_left_down;
    public RectTransform m_right_down;
    public RectTransform m_right_up;
    public RectTransform m_left_up;

    public Image showImg;

    public string m_picName;
    public Dictionary<string, string> m_picDic = new Dictionary<string, string>();

    private Doc_Report myDoc;
    private Coroutine m_coroutine;

    private void Start()
    {
        m_picDic.Clear();
        myDoc = new Doc_Report();
        myDoc.init("Doc/Model");

        Init(m_left_down, m_right_down, m_right_up, m_left_up, "Pic1");
        StartScreenShot();
    }

    public void Init(RectTransform left_down, RectTransform right_down, RectTransform right_up, RectTransform left_up, string picName)
    {
        m_left_down = left_down;
        m_right_down = right_down;
        m_right_up = right_up;
        m_left_up = left_up;
        m_picName = picName;
    }

    public void StartScreenShot()
    {
        Rect rect = new Rect(m_left_down.position.x, m_right_down.position.y, m_right_up.position.x, m_left_up.position.y);
        if (m_coroutine != null)
        {
            StopCoroutine(m_coroutine);
        }
        m_coroutine = StartCoroutine(CustomCaptrue(rect));
    }

    private IEnumerator CustomCaptrue(Rect rect)
    {
        int t_width = (int)Mathf.Abs(m_right_down.position.x - m_left_down.position.x);
        int t_length = (int)Mathf.Abs(m_right_up.position.y - m_right_down.position.y);

        yield return new WaitForEndOfFrame();
        Texture2D t = new Texture2D(t_width, t_length, TextureFormat.RGB24, true);//需要正确设置好图片保存格式  
        t.ReadPixels(rect, 0, 0, false);//按照设定区域读取像素；注意是以左下角为原点读取  
        t.Apply();
        byte[] byt = t.EncodeToPNG();

#if UNITY_EDITOR
        File.WriteAllBytes(Application.streamingAssetsPath + "/PNG/" + Time.time + ".png", byt);
        Sprite target = Sprite.Create(t, new Rect(0, 0, t_width, t_length), Vector2.zero);
        showImg.sprite = target;
#endif  

        string base64String = Convert.ToBase64String(byt);
        if (m_picDic.ContainsKey(m_picName))
        {
            m_picDic[m_picName] = base64String;
        }
        else
        {
            m_picDic.Add(m_picName, base64String);
        }

#if UNITY_EDITOR
        System.IO.File.WriteAllText(Application.streamingAssetsPath + "/123.html", myDoc.file_Content, System.Text.Encoding.UTF8);
#endif
    }

    /// <summary>
    /// 替换文档数据、下载 
    /// </summary>
    public void DowFile()
    {
        foreach (var item in m_picDic)
        {
            myDoc.set_Text(item.Key, item.Value);
        }
        myDoc.Down_File("report.doc");
    }
}
