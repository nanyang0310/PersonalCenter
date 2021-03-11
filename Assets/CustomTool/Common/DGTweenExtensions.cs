/*
/// 功能： DoTween 的方法扩展
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public static class DGTweenExtensions
{
    public static Tweener DoText_TMP(this TMPro.TextMeshProUGUI textPro, string target, float duration, bool richTextEnable = true, ScrambleMode scramble = ScrambleMode.None, string scrambleChars = null)
    {
        return DOTween.To(() => textPro.text, x => textPro.text = x, target, duration).SetOptions(richTextEnable, scramble, scrambleChars);
    }
}
