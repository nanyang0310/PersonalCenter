using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace xxl.hanm
{
    public class Doc_Report : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void download(string str, string filename, string Strtype);

        public string file_Content;

        public void init(string FileName)
        {
            TextAsset textFile = Resources.Load(FileName) as TextAsset;
            file_Content = textFile.text;
        }

        public void set_Text(string posString, string Content)
        {
            file_Content = file_Content.Replace(posString, Content);
        }

        public void Down_File(string FileName)
        {
            download("data:," + file_Content, FileName, "");
        }
    }
}