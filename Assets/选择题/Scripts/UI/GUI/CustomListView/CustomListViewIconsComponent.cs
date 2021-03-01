using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UIWidgets;

/// <summary>
/// CustomListView component with icons.
/// </summary>
public class CustomListViewIconsComponent : CustomListViewComponent
{
    /// <summary>
    /// This image will be shown on click.
    /// </summary>
    [SerializeField]
    public Sprite[] Sprites = new Sprite[2];

    /// <summary>
    /// This image will be shown on click.
    /// </summary>
    [SerializeField]
    public Image Image;

    /// <summary>
    /// CustomListView.
    /// </summary>
    public CustomListViewIcons ParentList;

    /// <summary>
    /// Sets component data with specified item.
    /// </summary>
    /// <param name="item">Item.</param>
    public override void SetData(CustomListViewItemDescription item)
    {
        base.SetData(item);
    }

    /// <summary>
    /// Set graphics colors.
    /// </summary>
    /// <param name="foregroundColor">Foreground color.</param>
    /// <param name="backgroundColor">Background color.</param>
    /// <param name="fadeDuration">Fade duration.</param>
    public override void GraphicsColoring(Color foregroundColor, Color backgroundColor, float fadeDuration = 0.0f)
    {
        base.GraphicsColoring(foregroundColor, backgroundColor, fadeDuration);

        if (!ParentList)
        {
            return;
        }

        if (foregroundColor == ParentList.DefaultColor && backgroundColor == ParentList.DefaultBackgroundColor)
        {
            Image.sprite = Sprites[0];
        }
        else if (foregroundColor == ParentList.SelectedColor && backgroundColor == ParentList.SelectedBackgroundColor)
        {
            Image.sprite = Sprites[1];
        }
    }
}