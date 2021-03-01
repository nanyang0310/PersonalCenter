using System.Collections;
using System.Collections.Generic;
using UIWidgets;
using UnityEngine;

public class CustomTileViewTiKa : TileViewCustom<CustomTileViewComponentTiKa, CustomTileViewItemDescription>
{
    bool isTileViewSampleInited = false;

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
