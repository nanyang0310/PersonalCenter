using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tool : MonoBehaviour
{
    //���Ĳ�
    public LayerMask m_layerMask = 1;
    public float m_dis = 1000;
    public Camera m_rayCamera;
    public bool m_isPrint = false;

    private void OnEnable()
    {
        if (m_rayCamera==null)
        {
            m_rayCamera = Camera.main;
        }
    }

    private void Start()
    {
        GetMemberName(() => m_layerMask);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& m_isPrint)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].gameObject.layer == m_layerMask)
                    {
                        Debug.Log(results[i].gameObject.name);
                    }
                    Debug.Log(results[i].gameObject);
                }
            }

        }
    }

    /// <summary>
    /// ��ȡĳ������������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="memberExpression"></param>
    /// <returns></returns>
    public static string GetMemberName<T>(System.Linq.Expressions.Expression<System.Func<T>> memberExpression)
    {
        System.Linq.Expressions.MemberExpression expressionBody = (System.Linq.Expressions.MemberExpression)memberExpression.Body;
        Debug.Log(expressionBody.Member.Name);
        return expressionBody.Member.Name;
    }

    /// <summary>
    /// ��min��max�л�ȡ����Ϊnum�����ظ�����ֵint
    /// </summary>
    /// <param name="num"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public int[] GetRandoms(int num, int min, int max)
    {
        int[] arr = new int[num];
        int j = 0;
        //��ʾ����ֵ�Եļ��ϡ�
        Hashtable hashtable = new Hashtable();
        System.Random rm = new System.Random();
        while (hashtable.Count < num)
        {
            //����һ��min��max֮��������
            int nValue = rm.Next(min, max);
            // �Ƿ�����ض�ֵ
            if (!hashtable.ContainsValue(nValue))
            {
                //�Ѽ���ֵ��ӵ�hashtable
                hashtable.Add(nValue, nValue);
                arr[j] = nValue;

                j++;
            }
        }
        return arr;
    }

}
