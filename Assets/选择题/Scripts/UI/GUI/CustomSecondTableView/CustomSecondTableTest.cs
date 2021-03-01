using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidgetsSamples;

public class CustomSecondTableTest : MonoBehaviour
{

    public CustomSecondTable table;
    public int Num;
    void Start()
    {
        for (int i = 0; i < Num; i++)
        {
            CustomSecondTableRow item = new CustomSecondTableRow();
            item.Name1 = "taskname " + i;
            //item.StepIndex = i;
            //item.StepContent = "内容" + i;
            //item.PartName = "部件名称" + i;
            //item.OperationFaultReason = "";
            //item.Score = i;
            table.Add(item);
        }
        table.Select(1);
    }
}
