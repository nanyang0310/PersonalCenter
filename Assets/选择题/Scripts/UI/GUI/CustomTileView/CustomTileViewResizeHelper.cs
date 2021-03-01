using UnityEngine;
using UIWidgets;

/// <summary>
/// TileView resize helper. Resize all items components when size one of them is changed.
/// </summary>
[RequireComponent(typeof(CustomTileViewComponent))]
[RequireComponent(typeof(Resizable))]
public class CustomTileViewResizeHelper : MonoBehaviour
{
    /// <summary>
    /// TileView.
    /// </summary>
    [SerializeField]
    protected CustomTileView Tiles;

    /// <summary>
    /// Add listeners.
    /// </summary>
    protected virtual void Start()
    {
        GetComponent<Resizable>().OnEndResize.AddListener(OnResize);
    }

    /// <summary>
    /// Remove listenerss.
    /// </summary>
    protected virtual void OnDestroy()
    {
        var r = GetComponent<Resizable>();
        if (r != null)
        {
            r.OnEndResize.AddListener(OnResize);
        }
    }

    /// <summary>
    /// Handle resize event.
    /// </summary>
    /// <param name="item">Item.</param>
    protected virtual void OnResize(Resizable item)
    {
        var size = (item.transform as RectTransform).rect.size;
        Tiles.ForEachComponent(x =>
        {
            if (x.gameObject == item.gameObject)
            {
                return;
            }
            var rect = x.transform as RectTransform;

            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        });

        Tiles.Resize();
    }
}