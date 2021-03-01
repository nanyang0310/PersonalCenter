using UnityEngine;
using System;
using System.ComponentModel;
using UIWidgets;
using UnityEngine.Serialization;
using System.Collections.Generic;

[Serializable]
public class CustomOptionListViewItemDescription : IItemHeight, INotifyPropertyChanged
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
    [FormerlySerializedAs("TitleName")]
    protected string titleName;
    /// <summary>
    /// The value.
    /// </summary>
    public string TitleName
    {
        get
        {
            return titleName;
        }
        set
        {
            titleName = value;
            Changed("TitleName");
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

    [SerializeField]
    [FormerlySerializedAs("OptionNameA")]
    protected string optionNameA;
    /// <summary>
    /// The Data.
    /// </summary>
    public string OptionNameA
    {
        get
        {
            return optionNameA;
        }
        set
        {
            optionNameA = value;
            Changed("OptionNameA");
        }
    }

    [SerializeField]
    [FormerlySerializedAs("OptionNameB")]
    protected string optionNameB;
    /// <summary>
    /// The Data.
    /// </summary>
    public string OptionNameB
    {
        get
        {
            return optionNameB;
        }
        set
        {
            optionNameB = value;
            Changed("OptionNameB");
        }
    }

    [SerializeField]
    [FormerlySerializedAs("OptionNameC")]
    protected string optionNameC;
    /// <summary>
    /// The Data.
    /// </summary>
    public string OptionNameC
    {
        get
        {
            return optionNameC;
        }
        set
        {
            optionNameC = value;
            Changed("OptionNameC");
        }
    }

    [SerializeField]
    [FormerlySerializedAs("OptionNameD")]
    protected string optionNameD;
    /// <summary>
    /// The Data.
    /// </summary>
    public string OptionNameD
    {
        get
        {
            return optionNameD;
        }
        set
        {
            optionNameD = value;
            Changed("OptionNameD");
        }
    }

    [SerializeField]
    [FormerlySerializedAs("OptionNameE")]
    protected string optionNameE;
    /// <summary>
    /// The Data.
    /// </summary>
    public string OptionNameE
    {
        get
        {
            return optionNameE;
        }
        set
        {
            optionNameE = value;
            Changed("OptionNameE");
        }
    }

    [SerializeField]
    [FormerlySerializedAs("OptionNameAnswer")]
    protected string optionNameAnswer;
    /// <summary>
    /// The OptionNameAnswer.用数值
    /// </summary>
    public string OptionNameAnswer
    {
        get
        {
            return optionNameAnswer;
        }
        set
        {
            optionNameAnswer = value;
            Changed("OptionNameAnswer");
        }
    }

    [SerializeField]
    [FormerlySerializedAs("Correct")]
    protected bool correct;
    /// <summary>
    /// The Data.
    /// </summary>
    public bool Correct
    {
        get
        {
            return correct;
        }
        set
        {
            correct = value;
            //Changed("Correct"); 每对应的值发送变化 会调用setdata方法 
        }
    }

    [SerializeField]
    [FormerlySerializedAs("SelectBtnIndex")]
    protected List<int> selectBtnIndex = new List<int>();
    /// <summary>
    /// The Data.
    /// </summary>
    public List<int> SelectBtnIndex
    {
        get
        {
            return selectBtnIndex;
        }
        set
        {
            selectBtnIndex = value;
        }
    }

    [SerializeField]
    [FormerlySerializedAs("ShowAnswerTip")]
    protected bool showAnswerTip =false;
    /// <summary>
    /// The Data.
    /// </summary>
    public bool ShowAnswerTip
    {
        get
        {
            return showAnswerTip;
        }
        set
        {
            showAnswerTip = value;
        }
    }

    [SerializeField]
    [FormerlySerializedAs("IsMultiple")]
    protected bool isMultiple = false;
    /// <summary>
    /// The Data.
    /// </summary>
    public bool IsMultiple
    {
        get
        {
            return isMultiple;
        }
        set
        {
            isMultiple = value;
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
