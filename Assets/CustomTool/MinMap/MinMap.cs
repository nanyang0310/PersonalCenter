/*
/// 功能： 
///     小地图的映射
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinMap : MonoBehaviour
{
    [Header("映射的地图图片")]
    public RectTransform m_miniMap;
    [Header("角色的图片")]
    public Image m_playerIcon;
    public ScrollRect m_mapScrollRect;
    //地图缩放比例
    public float m_mapScaleRatio;

    [Header("真实地形的初始点和结束点")]
    public Transform m_mapStartTra;
    public Transform m_mapEndTra;
    //用于计算地图的整体大小
    public Vector2 m_mapSize;
    [Header("每个楼层的自定义的中心点")]
    public Transform m_mapCenter;
    public Transform m_fps;

    private void Start()
    {
        StartCoroutine(UpdatePFSPos(0.01f));
    }



    IEnumerator UpdatePFSPos(float timer)
    {
        m_mapSize = new Vector2(m_mapStartTra.position.x - m_mapEndTra.position.x, m_mapStartTra.position.z - m_mapEndTra.position.z);
        Vector3 pos;
        float x;
        float y;
        //1.先计算人物相对于自定义原点的相对位置 (pos)
        //2.将pos映射到miniMap上，算出映射的位置，比例运算
        while (true)
        {
            pos = m_fps.position - m_mapCenter.position;
            x = pos.x / m_mapSize.x;
            y = pos.z / m_mapSize.y;
            m_playerIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(m_miniMap.rect.width * x, m_miniMap.rect.height * y);
            m_mapScrollRect.verticalScrollbar.value = y + 1 / m_mapScaleRatio;
            m_mapScrollRect.horizontalScrollbar.value = x + 1 / m_mapScaleRatio;

            yield return new WaitForSeconds(timer);
        }
    }
}
