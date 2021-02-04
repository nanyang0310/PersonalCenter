//Transform的扩展工具类
//http://wiki.unity3d.com/index.php/QuaternionExtensions
// This script provide a few useful extensions to the inbuilt 'Quaternion' struct.
// The Exponent, Magnitude, and Scalar Multiplication methods are useful for writing specialised Quaternion math functions, and the Power method is used to rotate a given quaternion an exact multiple of iteslf.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TransformExtensions
{
    // 扩展了只修改一个值的
    public static void SetPositionX(this Transform t, float newX)
    {
        t.position = new Vector3(newX, t.position.y, t.position.z);
    }

    public static void SetPositionY(this Transform t, float newY)
    {
        t.position = new Vector3(t.position.x, newY, t.position.z);
    }

    public static void SetPositionZ(this Transform t, float newZ)
    {
        t.position = new Vector3(t.position.x, t.position.y, newZ);
    }

    public static void SetLocalPositionX(this Transform t, float newX)
    {
        t.localPosition = new Vector3(newX, t.localPosition.y, t.localPosition.z);
    }

    public static void SetLocalPositionY(this Transform t, float newY)
    {
        t.localPosition = new Vector3(t.localPosition.x, newY, t.localPosition.z);
    }

    public static void SetLocalPositionZ(this Transform t, float newZ)
    {
        t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, newZ);
    }

    /// <summary>
    /// 重置默认
    /// </summary>
    /// <param name="t"></param>
    public static void Identity(this Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }

    /// <summary>
    /// 是否激活
    /// </summary>
    /// <param name="t"></param>
    /// <param name="isShow"></param>
    public static void SetActive(this Transform t, bool isShow)
    {
        t.gameObject.SetActive(isShow);
    }

    public static T AddAndGetComponent<T>(this Transform tra) where T : Component
    {
        T com;
        if (tra.GetComponent<T>())
        {
            com = tra.GetComponent<T>();
        }
        else
        {
            com = tra.gameObject.AddComponent<T>();
        }
        return com;
    }

    // 扩展了更快的获取一个值
    public static float GetPositionX(this Transform t)
    {
        return t.position.x;
    }

    public static float GetPositionY(this Transform t)
    {
        return t.position.y;
    }

    public static float GetPositionZ(this Transform t)
    {
        return t.position.z;
    }

    public static void CopyFromWorld(this Transform t, Transform source)
    {
        t.position = source.position;
        t.rotation = source.rotation;
        // 注意：此处使用世界坐标系下的缩放系数赋予给局部坐标系下的缩放系数
        t.localScale = source.lossyScale;
    }

    public static void CopyFromLocal(this Transform t, Transform source)
    {
        t.localPosition = source.localPosition;
        t.localRotation = source.localRotation;
        t.localScale = source.localScale;
    }

    public static Bounds GetMaxBounds(this Transform t)
    {
        Bounds b = new Bounds();
        Renderer[] childRenderers = t.gameObject.GetComponentsInChildren<Renderer>();
        if (childRenderers.Length > 0)
        {
            b = childRenderers[0].bounds;
            for (int i = 1; i < childRenderers.Length; ++i)
            {
                b.Encapsulate(childRenderers[i].bounds);
            }
        }
        else
        {
            b.center = t.position;
        }

        return b;
    }
}

public static class QuaternionExtensions
{
    // returns quaternion raised to the power pow.
    // This is useful for smoothly multiplying a Quaternion by a given floating-point value.
    public static Quaternion Pow(this Quaternion input, float power)
    {
        float inputMagnitude = input.Magnitude();
        Vector3 nHat = new Vector3(input.x, input.y, input.z).normalized;
        Quaternion vectorBit = new Quaternion(nHat.x, nHat.y, nHat.z, 0)
            .ScalarMultiply(power * Mathf.Acos(input.w / inputMagnitude))
                .Exp();
        return vectorBit.ScalarMultiply(Mathf.Pow(inputMagnitude, power));
    }

    // returns euler's number raised to quaternion
    public static Quaternion Exp(this Quaternion input)
    {
        float inputA = input.w;
        Vector3 inputV = new Vector3(input.x, input.y, input.z);
        float outputA = Mathf.Exp(inputA) * Mathf.Cos(inputV.magnitude);
        Vector3 outputV = Mathf.Exp(inputA) * (inputV.normalized * Mathf.Sin(inputV.magnitude));
        return new Quaternion(outputV.x, outputV.y, outputV.z, outputA);
    }

    // returns the float magnitude of quaternion
    public static float Magnitude(this Quaternion input)
    {
        return Mathf.Sqrt(input.x * input.x + input.y * input.y + input.z * input.z + input.w * input.w);
    }

    // returns quaternion multiplied by scalar
    public static Quaternion ScalarMultiply(this Quaternion input, float scalar)
    {
        return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
    }
}

public static class ComponentExtensions
{
    public static void SetActive(this Component com, bool isShow)
    {
        com.gameObject.SetActive(isShow);
    }
}

public static class ListExtensions
{
    public static void Add<T>(this List<T> list, params T[] ts)
    {
        foreach (var item in ts)
        {
            list.Add(item);
        }
    }

    /// <summary>
    /// 取出随机值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T Random<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            return default;
        }
        int index = UnityEngine.Random.Range(0, list.Count);
        return list[index];
    }

    /// <summary>
    /// 剔除掉重复的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void EliminateRepeat<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = list.Count - 1; j > i; j--) //j>i 的意思是:>i前面的已经比较过了
            {
                var A = list[i];
                var B = list[j];
                if (A.Equals(B))
                {
                    list.RemoveAt(j);
                }
            }
        }
    }

    /// <summary>
    /// 获取重复的对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="compareList"></param>
    /// <returns></returns>
    public static List<T> GetRepeat<T>(this List<T> list, List<T> compareList)
    {
        List<T> result = new List<T>();
        foreach (var item in list)
        {
            foreach (var pare in compareList)
            {
                if (item.Equals(pare))
                {
                    result.Add(item);
                }
            }
        }
        return result;
    }

    /// <summary>
    /// 复制并剔除重复的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="copyList"></param>
    public static void CopyToEliminateRepeat<T>(this List<T> list, List<T> copyList)
    {
        list.AddRange(copyList);
        list.EliminateRepeat();
    }
}
