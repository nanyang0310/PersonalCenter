using System;
using UnityEngine;
using UIWidgets;
using UIWidgets.TMProSupport;

/// <summary>
/// Test Custom ListView.
/// </summary>
public class CustomListViewTest : MonoBehaviour
{
    //public ListViewHeight m_listViewHeight;
    public ListViewIcons m_listViewIcons;
    public CustomListView m_customListView;
    public CustomListViewIcons m_customListViewIcons;

    public int m_listCount = 300;

    System.Diagnostics.Stopwatch m_stopWatch = new System.Diagnostics.Stopwatch();

    public void Start()
    {
        m_stopWatch.Reset();
        m_stopWatch.Start();

        //if (m_listViewHeight != null)
        //{
        //    for (int i = 0; i < 250; i++)
        //    {
        //        ListViewItemDescription item = new ListViewItemDescription();
        //        item.Name = "条目" + i;
        //        item.Value = i;
        //        item.Data = i.ToString();

        //        m_listViewHeight.DataSource.Add(item);
        //        //m_customListView.Select(i);
        //    }
        //}

        if (m_listViewIcons != null)
        {
            for (int i = 0; i < m_listCount; i++)
            {
                ListViewIconsItemDescription item = new ListViewIconsItemDescription();
                item.Name = i.ToString();
                item.Value = i;
                //item.Data = i.ToString();

                m_listViewIcons.DataSource.Add(item);
                //m_customListView.Select(i);
            }
        }

        m_stopWatch.Stop();
        double seconds = m_stopWatch.Elapsed.TotalSeconds;
        Debug.LogWarning("Load ListViewIcons: " + seconds);

        m_stopWatch.Reset();
        m_stopWatch.Start();

        if (m_customListView != null)
        {
            for (int i = 0; i < m_listCount; i++)
            {
                CustomListViewItemDescription item = new CustomListViewItemDescription();
                item.Name = i.ToString();
                item.Value = i;
                item.Data = i.ToString();

                m_customListView.DataSource.Add(item);
                //m_customListView.Select(i);
            }
        }

        m_stopWatch.Stop();
        double seconds2 = m_stopWatch.Elapsed.TotalSeconds;
        Debug.LogWarning("Load CustomListView: " + seconds2);

        m_stopWatch.Reset();
        m_stopWatch.Start();

        if (m_customListViewIcons)
        {
            m_customListViewIcons.DataSource.BeginUpdate();
            for (int i = 0; i < m_listCount; i++)
            {
                CustomListViewItemDescription item = new CustomListViewItemDescription();
                item.Name = i.ToString();
                item.Value = i;
                item.Data = i.ToString();

                m_customListViewIcons.Add(item);
            }

            //for (int i = 0; i < 50; i++)
            //{
            //    m_customListViewIcons.Select(i);
            //}
        }
        m_customListViewIcons.DataSource.EndUpdate();

        m_stopWatch.Stop();
        double seconds3 = m_stopWatch.Elapsed.TotalSeconds;
        Debug.LogWarning("Load CustomListViewIcons: " + seconds3);
    }

    public void OnSelect(int index, ListViewItem item)
    {
        CustomListViewComponent componentItem = item as CustomListViewComponent;
        Debug.Log("Selected: " + index + "; name: " + componentItem.Item.Name + "; value: " + componentItem.Item.Value + "; data: " + componentItem.Item.Data);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_stopWatch.Reset();
            m_stopWatch.Start();

            m_listViewIcons.Clear();
            if (m_listViewIcons != null)
            {
                for (int i = 0; i < m_listCount; i++)
                {
                    ListViewIconsItemDescription item = new ListViewIconsItemDescription();
                    item.Name = i.ToString();
                    item.Value = i;
                    //item.Data = i.ToString();

                    m_listViewIcons.DataSource.Add(item);
                    //m_customListView.Select(i);
                }
            }

            m_stopWatch.Stop();
            double seconds = m_stopWatch.Elapsed.TotalSeconds;
            Debug.LogWarning("Load ListViewIcons: " + seconds);
        }


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_customListView.Clear();
            m_stopWatch.Reset();
            m_stopWatch.Start();

            if (m_customListView != null)
            {
                for (int i = 0; i < m_listCount; i++)
                {
                    CustomListViewItemDescription item = new CustomListViewItemDescription();
                    item.Name = i.ToString();
                    item.Value = i;
                    item.Height = m_customListViewIcons.GetDefaultItemHeight();
                    item.Data = i.ToString();

                    m_customListView.DataSource.Add(item);
                    //m_customListView.Select(i);
                }
            }

            m_stopWatch.Stop();
            double seconds2 = m_stopWatch.Elapsed.TotalSeconds;
            Debug.LogWarning("Load CustomListView: " + seconds2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_customListViewIcons.Clear();
            m_stopWatch.Reset();
            m_stopWatch.Start();

            m_customListViewIcons.DataSource.BeginUpdate();
            if (m_customListViewIcons)
            {
                for (int i = 0; i < m_listCount; i++)
                {
                    CustomListViewItemDescription item = new CustomListViewItemDescription();
                    item.Name = i.ToString();
                    item.Value = i;
                    item.Data = i.ToString();

                    m_customListViewIcons.Add(item);
                }

                //for (int i = 0; i < 50; i++)
                //{
                //    m_customListViewIcons.Select(i);
                //}
            }
            m_customListViewIcons.DataSource.EndUpdate();

            m_stopWatch.Stop();
            double seconds3 = m_stopWatch.Elapsed.TotalSeconds;
            Debug.LogWarning("Load CustomListViewIcons: " + seconds3);
        }
    }
}