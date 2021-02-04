/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{
    List<string> list1 = new List<string>();
    List<string> list2 = new List<string>();

    private void Start()
    {
        Rigidbody rigidbody = this.transform.AddAndGetComponent<Rigidbody>();
        rigidbody.SetActive(false);

        list1.Add("555");
        list2.Add("7777",  "555");
        list1.AddRange(list2);
        list1.EliminateRepeat();
        List<string> list3 = list1.GetRepeat(list2);
        list1.CopyToEliminateRepeat(list2);
        foreach (var item in list1)
        {
            Debug.Log(item);
        }
    }
}
