using System;
using UIWidgets;
using UnityEngine;

/// <summary>
/// CustomListView to binding data with variable height.
/// </summary>
public class CustomListView : ListViewCustomHeight<CustomListViewComponent, CustomListViewItemDescription>
{
    [Header("Sprite设置")]
    public TransitionType transitionType = TransitionType.Color;
    public Sprite defaultSprite;
    public Sprite selectSprite;
    private CustomListViewComponent[] customListViewComponents;

    public void SetTexMeshColor()
    {
        if (!this)
        {
            return;
        }
        if (this.SelectedItem == null)
        {
            return;
        }
        if (!this.SelectedItem.Name.Contains("</color>"))
        {
            this.SelectedItem.Name = "<color=#7A7A7AFF>" + this.SelectedItem.Name + "</color>";
            this.SetNewItems(this.DataSource);
        }
    }
}

public enum TransitionType
{
    Color,
    Sprite
}
