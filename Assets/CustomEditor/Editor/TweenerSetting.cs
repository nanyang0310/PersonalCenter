using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using NY;


public class TweenerSetting : EditorWindow
{
    public static TweenerSetting m_instance;

    public Transform m_model;
    public GameObject m_pre;
    public TMP_FontAsset m_TMP_FontAsset;
    public Font m_font;

    public Transform m_hotspot;
    public bool m_isShowSetModel = false;

    public Transform[] m_allGuideStep;
    public bool m_isShowGuideStep = false;
    public bool m_isSort = false;

    public Transform[] m_allPointData;

    public Transform[] m_customPoint;
    public string m_pointName;

    private Vector2 m_pos;

    public CreatConfigItems m_creatConfigItems;

    [MenuItem("Custom/Tweener Setting")]
    public static void SetTweener()
    {
        TweenerSetting window = (TweenerSetting)EditorWindow.GetWindow(typeof(TweenerSetting), false, "Seeting Tweeners");
        window.Show();
        EditorWindow.GetWindow(typeof(TweenerSetting));
    }
    void OnGUI()
    {
        GUILayout.BeginVertical();
        m_pos = GUILayout.BeginScrollView(m_pos);

        GUIStyle style = new GUIStyle(GUI.skin.GetStyle("popup"));
        style.fontSize = 12;
        style.fixedHeight = 18;
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        m_model = (Transform)EditorGUILayout.ObjectField("Model: ", m_model, typeof(Transform), true);
        m_hotspot = (Transform)EditorGUILayout.ObjectField("Hotspot: ", m_hotspot, typeof(Transform), true);
        m_isShowSetModel = EditorGUILayout.Toggle("m_isShowSetModel: ", m_isShowSetModel);
        m_isShowGuideStep = EditorGUILayout.Toggle("m_isShowGuideStep: ", m_isShowGuideStep);
        m_isSort = EditorGUILayout.Toggle("是否排序: ", m_isSort);
        m_pre = (GameObject)EditorGUILayout.ObjectField("Pre: ", m_pre, typeof(GameObject), true);
        m_TMP_FontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField("Model: ", m_TMP_FontAsset, typeof
            (TMP_FontAsset), true);
        m_font = (Font)EditorGUILayout.ObjectField("Model: ", m_font, typeof(Font), true);
        m_pointName = EditorGUILayout.TextField("Name", m_pointName);
        m_creatConfigItems= (CreatConfigItems)EditorGUILayout.ObjectField("CreatConfigItems: ", m_creatConfigItems, typeof(CreatConfigItems), true);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("修改字体"))
        {
            TextMeshProUGUI[] textMeshProUGUIs = m_model.GetComponentsInChildren<TextMeshProUGUI>(true);
            for (int i = 0; i < textMeshProUGUIs.Length; i++)
            {
                textMeshProUGUIs[i].font = m_TMP_FontAsset;
            }
            Text[] texts = m_model.GetComponentsInChildren<Text>(true);
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].font = m_font;
            }
        }
        if (GUILayout.Button("去掉碰撞"))
        {
            Collider[] colliders = m_model.GetComponentsInChildren<Collider>(true);
            for (int i = 0; i < colliders.Length; i++)
            {
                DestroyImmediate(colliders[i]);
            }
        }

        if (GUILayout.Button("修改位置"))
        {
            Hotspot[] hotspots = m_model.GetComponentsInChildren<Hotspot>(true);
            for (int i = 0; i < hotspots.Length; i++)
            {
                hotspots[i].transform.GetChild(0).localPosition = Vector3.zero;
                hotspots[i].transform.GetChild(0).gameObject.name = "T_GF_FeiJing_01";
            }
        }

        if (GUILayout.Button("删除"))
        {
            Hotspot[] hotspots = m_model.GetComponentsInChildren<Hotspot>(true);
            for (int i = 0; i < hotspots.Length; i++)
            {
                DestroyImmediate(hotspots[i].transform.GetChild(0).gameObject);
                //hotspots[i].transform.GetChild(0).gameObject.name = "T_GF_FeiJing_01";
            }
        }

        if (GUILayout.Button("选中Hotspot"))
        {
            Transform[] transforms = Selection.transforms;
            if (transforms[0].GetComponent<Hotspot>())
            {
                m_hotspot = transforms[0];
            }
        }

        if (GUILayout.Button("选中Model"))
        {
            Transform[] transforms = Selection.transforms;
            m_model = transforms[0];
        }

        if (m_isShowSetModel)
        {
            //m_model = HierarchyMenuHelper.m_modelTra;
            //m_hotspot = HierarchyMenuHelper.m_hotspotTra;
        }

        if (GUILayout.Button("赋值"))
        {
            m_hotspot.GetComponent<Hotspot>().m_model = m_model.gameObject;
        }

        if (GUILayout.Button("修改"))
        {
            for (int i = 0; i < m_model.childCount; i++)
            {
                if (m_model.GetChild(i).Find("闪烁 (1)"))
                {
                    DestroyImmediate(m_model.GetChild(i).Find("闪烁 (1)").gameObject);
                }
                if (m_model.GetChild(i).Find("Text (2)"))
                {
                    m_model.GetChild(i).gameObject.name = m_model.GetChild(i).Find("Text (2)").GetComponent<TextMeshProUGUI>().text;
                }

            }

        }

        if (GUILayout.Button("删除ButtonWithAudio"))
        {
            //ButtonWithAudio[] buttonWithAudios = m_model.GetComponentsInChildren<ButtonWithAudio>(true);
            //for (int i = 0; i < buttonWithAudios.Length; i++)
            //{
            //    DestroyImmediate(buttonWithAudios[i]);
            //}
        }
        if (GUILayout.Button("删除AudioSource"))
        {
            AudioSource[] audioSources = m_model.GetComponentsInChildren<AudioSource>(true);
            for (int i = 0; i < audioSources.Length; i++)
            {
                DestroyImmediate(audioSources[i]);
            }
        }



        if (m_isShowGuideStep)

        {
            //ConnectLineItemUI[] pointDatas = m_model.GetComponentsInChildren<ConnectLineItemUI>(true);
            //List<ConnectLineItemUI> tempList = pointDatas.ToList();
            //m_customPoint = new Transform[pointDatas.Length];
            //for (int i = 0; i < tempList.Count; i++)
            //{
            //    m_customPoint[i] = tempList[i].transform;
            //}

            //for (int i = 0; i < m_customPoint.Length; i++)
            //{
            //    if (m_customPoint[i].GetComponent<ConnectLineItemUI>().m_connectPointGroupsName == m_pointName)
            //    {
            //        Transform stepTra = (Transform)EditorGUILayout.ObjectField(m_customPoint[i].gameObject.name + ":", m_customPoint[i], typeof(Transform), true);
            //    }
            //}
        }

        if (GUILayout.Button("创建PartShowXML"))
        {
            //CustomTool.CreateXML(m_creatConfigItems, "PartShow");
        }


        GUILayout.EndScrollView();

        GUILayout.EndVertical();
    }
}


