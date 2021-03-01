using UnityEngine;
using UIWidgets;

/// <summary>
/// CustomTileView.
/// </summary>
public class CustomTileView : TileViewCustom<CustomTileViewComponent, CustomTileViewItemDescription>
{
    bool isTileViewSampleInited = false;

    public System.Action<CustomTileViewComponent> OnPointerDownAction;
    public System.Action OnEndDragAction;
    public System.Action OnDragAction;

    /// <summary>
    /// Init this instance.
    /// </summary>
    public override void Init()
    {
        if (isTileViewSampleInited)
        {
            return;
        }
        isTileViewSampleInited = true;

        base.Init();

        //DataSource.Comparison = itemsComparison;
    }
}