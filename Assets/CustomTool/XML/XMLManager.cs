using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class XMLManager
{
    public Action<Type> XMLLoadedAction;

    /// <summary>
    /// 创建XML数据表
    /// </summary>
    /// <typeparam name="T">类</typeparam>
    /// <param name="xmlName">xml表名称，带.xml</param>
    /// <param name="t"></param>
    public static void CreatSerializerXML<T>(string xmlName, T t) where T : class
    {
        FileStream fileStream = new FileStream(Application.dataPath + "/" + xmlName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        StreamWriter sw = new StreamWriter(fileStream, System.Text.Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(t.GetType());
        xmlSerializer.Serialize(sw, t);
        sw.Close();
        fileStream.Close();
        Debug.Log(xmlName + "生成");
    }

    public IEnumerator Get_XML_Coroutine<T>(string fileName, T t) where T : class
    {
        fileName = Application.streamingAssetsPath + fileName;
        WWW w = new WWW(fileName);
        if (!System.IO.File.Exists(fileName))
        {
            Debug.LogError("not find file");
        }
        yield return w;
        Debug.Log("w.text" + w.text);
        string xml = XMLManager.get_uft8(w.text);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml);
        if (xmlDoc != null)
        {
            string allXML = Regex.Replace(xmlDoc.InnerXml, "\"true\"", "\"true\"", RegexOptions.IgnoreCase);
            allXML = Regex.Replace(allXML, "\"false\"", "\"false\"", RegexOptions.IgnoreCase);
            t = (XmlUtil.Deserialize(typeof(T), allXML) as T);
        }
    }

    public static T GetXML<T>(string filePath) where T : class
    {
        T t = null;
        try
        {
            string m_fileName = Application.streamingAssetsPath + filePath;
            if (!Directory.Exists(m_fileName))
            {
                Debug.LogError(m_fileName + "is not Exists");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_fileName);

            if (xmlDoc != null)
            {
                string allXML = Regex.Replace(xmlDoc.InnerXml, "\"true\"", "\"true\"", RegexOptions.IgnoreCase);
                allXML = Regex.Replace(allXML, "\"false\"", "\"false\"", RegexOptions.IgnoreCase);
                t = XmlUtil.Deserialize(typeof(T), allXML) as T;
            }
        }
        catch (System.Exception err)
        {
            Debug.LogError("GetDataFromXML fail! " + err.Message);
        }
        return t;
    }

    public static string get_uft8(string unicodeString)
    {
        System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
        //byte[] aaa = 
        byte[] encodedBytes = utf8.GetBytes(unicodeString);
        if (encodedBytes.Length <= 3) return utf8.GetString(encodedBytes);

        byte[] newBytes;
        if ((encodedBytes[0] == 0xef) && (encodedBytes[1] == 0xbb) && (encodedBytes[2] == 0xbf))  //去除BOM
        {
            newBytes = new byte[encodedBytes.Length - 3];
            System.Array.Copy(encodedBytes, 3, newBytes, 0, newBytes.Length);
        }
        else
            newBytes = encodedBytes;
        string decodedString = utf8.GetString(newBytes);
        return decodedString;
    }
}

public class XmlUtil
{
    #region 反序列化
    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="xml">XML字符串</param>
    /// <returns></returns>
    public static object Deserialize(System.Type type, string xml)
    {
        try
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(type);
                return xmldes.Deserialize(sr);
            }
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="type"></param>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static object Deserialize(System.Type type, Stream stream)
    {
        XmlSerializer xmldes = new XmlSerializer(type);
        return xmldes.Deserialize(stream);
    }
    #endregion

    #region 序列化
    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="obj">对象</param>
    /// <returns></returns>
    public static string Serializer(System.Type type, object obj)
    {
        MemoryStream Stream = new MemoryStream();
        XmlSerializer xml = new XmlSerializer(type);

        try
        {
            XmlWriterSettings xs = new XmlWriterSettings();
            xs.Encoding = System.Text.Encoding.UTF8;
            xs.Indent = true;
            var xw = XmlWriter.Create(Stream, xs);
            //序列化对象
            xml.Serialize(xw, obj);
            //xml.Serialize(Stream, obj);
        }
        catch (System.InvalidOperationException)
        {
            throw;
        }

        Stream.Position = 0;
        StreamReader sr = new StreamReader(Stream);
        string str = sr.ReadToEnd();

        sr.Dispose();
        Stream.Dispose();
        return str;
    }

    #endregion
}
