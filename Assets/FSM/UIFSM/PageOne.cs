/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageOne : BasePanel
{
    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        this.gameObject.SetActive(false);
    }

    public override void OnEnter(string prevState)
    {
        base.OnEnter(prevState);
        this.gameObject.SetActive(true);
    }
}
