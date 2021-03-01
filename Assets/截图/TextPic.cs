/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TextPic : MonoBehaviour
{
    private int capBeginX;
    private int capBeginY;
    private int capFinishX;
    private int capFinishY;

    public Image showImg;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector2 beginPos = new Vector2(mousePos.x, mousePos.y);
            capBeginX = (int)mousePos.x;
            capBeginY = (int)mousePos.y;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector2 finishPos = new Vector2(mousePos.x, mousePos.y);
            capFinishX = (int)mousePos.x;
            capFinishY = (int)mousePos.y;
            //重新计算截取的位置
            int capLeftX = (capBeginX < capFinishX) ? capBeginX : capFinishX;
            int capRightX = (capBeginX < capFinishX) ? capFinishX : capBeginX;
            int capLeftY = (capBeginY < capFinishY) ? capBeginY : capFinishY;
            int capRightY = (capBeginY < capFinishY) ? capFinishY : capBeginY;

            Rect rect = new Rect(capLeftX, capLeftY, capRightX, capRightY);
            StartCoroutine(Captrue(rect));
        }
    }

    IEnumerator Captrue(Rect rect)
    {
        int t_width = Mathf.Abs(capFinishX - capBeginX);
        int t_length = Mathf.Abs(capFinishY - capBeginY);

        yield return new WaitForEndOfFrame();
        Texture2D t = new Texture2D(t_width, t_length, TextureFormat.RGB24, true);//需要正确设置好图片保存格式  
        t.ReadPixels(rect, 0, 0, false);//按照设定区域读取像素；注意是以左下角为原点读取  
        t.Apply();
        byte[] byt = t.EncodeToPNG();
        File.WriteAllBytes(Application.streamingAssetsPath + "/PNG/" + Time.time + ".png", byt);

        Sprite target = Sprite.Create(t, new Rect(0, 0, t_width, t_length), Vector2.zero);
        showImg.sprite = target;
    }
}
