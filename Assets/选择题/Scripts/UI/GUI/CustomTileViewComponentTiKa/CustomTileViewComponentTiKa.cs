using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIWidgets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomTileViewComponentTiKa : ListViewItem, IViewData<CustomTileViewItemDescription>
{
    /// <summary>
    /// Background graphics for coloring.
    /// </summary>
    public override Graphic[] GraphicsForeground
    {
        get
        {
            return new Graphic[] { Name };
        }
    }

    /// <summary>
    /// The icon.
    /// </summary>
    [SerializeField]
    public Image Icon;

    /// <summary>
    /// The icon.
    /// </summary>
    [SerializeField]
    public Sprite[] m_sprites;

    /// <summary>
    /// The text.
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI Name;

    /// <summary>
    /// The Value.
    /// </summary>
    [SerializeField]
    public int Value;

    /// <summary>
    /// The Data.
    /// </summary>
    [SerializeField]
    public string Data;

    /// <summary>
    /// Set icon native size.
    /// </summary>
    public bool SetNativeSize = true;

    /// <summary>
    /// TileView.
    /// </summary>
    public CustomTileViewTiKa Tiles;

    /// <summary>
    /// Current item.
    /// </summary>
    public CustomTileViewItemDescription Item;

    protected Coroutine m_flashCoroutine;
    protected bool m_enableFlash = false;
    public float m_flashInterval = 0.5f;

    /// <summary>
    /// Duplicate current item in TileView.DataSource.
    /// </summary>
    public void Duplicate()
    {
        Tiles.DataSource.Add(Item);
    }

    /// <summary>
    /// Remove current item from TileView.DataSource.
    /// </summary>
    public void Remove()
    {
        Tiles.DataSource.RemoveAt(Index);
    }

    /// <summary>
    /// Sets component data with specified item.
    /// </summary>
    /// <param name="item">Item.</param>
    public void SetData(CustomTileViewItemDescription item)
    {
        gameObject.SetActive(item != null);

        Item = item;
        if (Item == null)
        {
            if (Icon != null)
            {
                Icon.sprite = null;
            }
            if (Name != null)
            {
                Name.text = string.Empty;
            }
        }
        else
        {
            name = item.Name;
            if (Icon != null)
            {
                Icon.sprite = Item.Icon;
            }
            if (Name != null)
            {
                Name.text = Item.Name;
            }
        }

        if (Icon != null)
        {
            if (SetNativeSize)
            {
                Icon.SetNativeSize();
            }

            //set transparent color if no icon
            //Icon.color = (Icon.sprite == null) ? Color.clear : Color.white;
            Icon.sprite = m_sprites[Item.Value];
        }

        // 判断是否闪烁
        m_enableFlash = item.EnableFlash;
        if (m_enableFlash)
        {
            FlashOn();
        }
        else
        {
            FlashOff();
        }
    }


    public virtual void FlashOn()
    {
        if (m_flashCoroutine != null)
        {
            m_enableFlash = false;
            StopCoroutine(m_flashCoroutine);
        }
        m_enableFlash = true;
        m_flashCoroutine = StartCoroutine(Flash());
    }

    protected IEnumerator Flash()
    {
        while (m_enableFlash)
        {
            // 使指定条目高亮显示
            Highlight(Tiles.HighlightedColor, Tiles.HighlightedBackgroundColor, m_flashInterval * 0.5f);

            yield return new WaitForSeconds(m_flashInterval);

            // 恢复指定条目状态
            Highlight(Tiles.DefaultColor, Tiles.DefaultBackgroundColor, m_flashInterval * 0.5f);

            yield return new WaitForSeconds(m_flashInterval);
        }
    }

    public virtual void FlashOff()
    {
        m_enableFlash = false;
        if (m_flashCoroutine != null)
        {
            StopCoroutine(m_flashCoroutine);
            m_flashCoroutine = null;
        }
    }

    /// <summary>
    /// Set this item highlight.
    /// </summary>
    /// <param name="foregroundColor">Foreground color.</param>
    /// <param name="backgroundColor">Background color.</param>
    /// <param name="fadeDuration">Fade duration.</param>
    public virtual void Highlight(Color foregroundColor, Color backgroundColor, float fadeDuration = 0.0f)
    {
        GraphicsForeground.ForEach(x =>
        {
            if (x != null)
            {
                x.CrossFadeColor(foregroundColor, fadeDuration, true, true);
            }
        });
        GraphicsBackground.ForEach(x =>
        {
            if (x != null)
            {
                x.CrossFadeColor(backgroundColor, fadeDuration, true, true);
            }
        });
    }

    /// <summary>
    /// Set this item unhighlight.
    /// </summary>
    public virtual void Unhighlight()
    {
        // reset default color to white, otherwise it will look darker than specified color,
        // because actual color = Text.color * Text.CrossFadeColor
        GraphicsForeground.ForEach(GraphicsReset);
        GraphicsBackground.ForEach(GraphicsReset);
    }

    /// <summary>
    /// Set graphics colors.
    /// </summary>
    /// <param name="foregroundColor">Foreground color.</param>
    /// <param name="backgroundColor">Background color.</param>
    /// <param name="fadeDuration">Fade duration.</param>
    public override void GraphicsColoring(Color foregroundColor, Color backgroundColor, float fadeDuration = 0.0f)
    {
        if (!Tiles)
        {
            return;
        }

        if (backgroundColor == Tiles.SelectedBackgroundColor)
        {
            FlashOff();
            Item.EnableFlash = false;
        }

        base.GraphicsColoring(foregroundColor, backgroundColor, fadeDuration);
    }

}
