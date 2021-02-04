/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TestTask : MonoBehaviour
{
    Task task;

    private void Start()
    {
        task = new Task(TestDebug);
        task.Start();
    }

    public void TestDebug()
    {
        for (int i = 0; i < 100; i++)
        {
            Debug.Log(i);
        }
    }
}
