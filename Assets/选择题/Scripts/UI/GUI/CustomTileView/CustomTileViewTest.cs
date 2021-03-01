using System;
using UnityEngine;
using UIWidgets;

/// <summary>
/// Test Custom TileView.
/// </summary>
public class CustomTileViewTest : MonoBehaviour
{
    public CustomTileView m_customTileView;

    public void Start()
    {
        if (m_customTileView != null)
        {
            for (int i = 0; i < 50; i++)
            {
                CustomTileViewItemDescription item = new CustomTileViewItemDescription();
                item.Name = "条目" + i;
                item.Value = 0;
                item.Data = i.ToString();

                m_customTileView.Add(item);
            }

            m_customTileView.Select(4);
        }
    }

    public void OnSelect(int index, ListViewItem item)
    {
        CustomTileViewComponent componentItem = item as CustomTileViewComponent;
        Debug.Log("Selected: " + index + "; name: " + componentItem.Item.Name + "; value: " + componentItem.Item.Value + "; data: " + componentItem.Item.Data);
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            int flashItemIndex = 6;
            CustomTileViewItemDescription item = m_customTileView.DataSource[flashItemIndex];
            item.EnableFlash = true;
            m_customTileView.DataSource[flashItemIndex] = item;
            //m_customTileView.UpdateItems();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            int flashItemIndex = 16;
            CustomTileViewItemDescription item = m_customTileView.DataSource[flashItemIndex];
            item.EnableFlash = true;
            m_customTileView.DataSource[flashItemIndex] = item;
            //m_customTileView.UpdateItems();
        }
    }
}