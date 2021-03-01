using UnityEngine;
using System;

/// <summary>
/// CustomListViewIcons item description.
/// </summary>
[Serializable]
public class CustomTileViewItemDescription
{
    /// <summary>
    /// The icon.
    /// </summary>
    [SerializeField]
    public Sprite Icon;

    /// <summary>
    /// The name.
    /// </summary>
    [SerializeField]
    public string Name;

    /// <summary>
    /// The area.
    /// </summary>
    [SerializeField]
    public int Value;

    /// <summary>
    /// The capital.
    /// </summary>
    [SerializeField]
    public string Data;

    /// <summary>
    /// The capital.
    /// </summary>
    [SerializeField]
    public bool EnableFlash = false;
}