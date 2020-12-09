using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tool : MonoBehaviour
{
    //检测的层
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
    /// 获取某个变量的名称
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
    /// 从min至max中获取数量为num个不重复的数值int
    /// </summary>
    /// <param name="num"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public int[] GetRandoms(int num, int min, int max)
    {
        int[] arr = new int[num];
        int j = 0;
        //表示键和值对的集合。
        Hashtable hashtable = new Hashtable();
        System.Random rm = new System.Random();
        while (hashtable.Count < num)
        {
            //返回一个min到max之间的随机数
            int nValue = rm.Next(min, max);
            // 是否包含特定值
            if (!hashtable.ContainsValue(nValue))
            {
                //把键和值添加到hashtable
                hashtable.Add(nValue, nValue);
                arr[j] = nValue;

                j++;
            }
        }
        return arr;
    }

}
