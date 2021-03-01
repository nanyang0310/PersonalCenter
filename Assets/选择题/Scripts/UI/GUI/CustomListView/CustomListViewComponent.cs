using System;
using UnityEngine;
using UnityEngine.UI;
using UIWidgets;
using UIWidgetsSamples;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// Custom ListView component.
/// </summary>
public class CustomListViewComponent : ListViewItem, IViewData<CustomListViewItemDescription>
{
    public Image icon;
    [SerializeField]
    protected int value;

    [SerializeField]
    protected string data;

    /// <summary>
    /// Foreground graphics for coloring.
    /// </summary>
    public override Graphic[] GraphicsForeground
    {
        get
        {
            return new Graphic[] { TextTMPro, };
        }
    }

    /// <summary>
    /// Text.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI TextTMPro;

    /// <summary>
    /// Item.
    /// </summary>
    [NonSerialized]
    public CustomListViewItemDescription Item;

    /// <summary>
    /// Sets component data with specified item.
    /// </summary>
    /// <param name="item">Item.</param>
    public virtual void SetData(CustomListViewItemDescription item)
    {
        Item = item;
        value = Item.Value;
        data = Item.Data;

        name = item.Name;

        if (Item == null)
        {
            TextTMPro.text = string.Empty;
        }
        else
        {
            String text = Item.LocalizedName ?? Item.Name;
            TextTMPro.text = text.Replace("\\n", "\n");
        }
    }

    protected LayoutGroup layoutGroup;

    /// <summary>
    /// Current layout.
    /// </summary>
    public LayoutGroup LayoutGroup
    {
        get
        {
            if (layoutGroup == null)
            {
                layoutGroup = GetComponent<LayoutGroup>();
            }
            return layoutGroup;
        }
    }
}