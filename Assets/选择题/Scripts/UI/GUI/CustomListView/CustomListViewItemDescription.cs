using UnityEngine;
using System;
using System.ComponentModel;
using UIWidgets;
using UnityEngine.Serialization;

/// <summary>
/// Custom ListView item description.
/// </summary>
[Serializable]
public class CustomListViewItemDescription : IItemHeight, INotifyPropertyChanged
{
    [SerializeField]
    [FormerlySerializedAs("Name")]
    protected string name;

    /// <summary>
    /// The name.
    /// </summary>
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
            //Changed("Name");
        }
    }

    [System.NonSerialized]
    protected string localizedName;

    /// <summary>
    /// The localized name.
    /// </summary>
    public string LocalizedName
    {
        get
        {
            return localizedName;
        }
        set
        {
            localizedName = value;
            Changed("LocalizedName");
        }
    }

    [SerializeField]
    protected float height;

    /// <summary>
    /// Item height.
    /// </summary>
    public float Height
    {
        get
        {
            return height;
        }
        set
        {
            height = value;
        }
    }

    [SerializeField]
    [FormerlySerializedAs("Value")]
    protected int val;

    /// <summary>
    /// The value.
    /// </summary>
    public int Value
    {
        get
        {
            return val;
        }
        set
        {
            val = value;
            Changed("Value");
        }
    }

    [SerializeField]
    [FormerlySerializedAs("Data")]
    protected string data;

    /// <summary>
    /// The Data.
    /// </summary>
    public string Data
    {
        get
        {
            return data;
        }
        set
        {
            data = value;
            Changed("Data");
        }
    }

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged = (x, y) => { };

    protected void Changed(string propertyName)
    {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}