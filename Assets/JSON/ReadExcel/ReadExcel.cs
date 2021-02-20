using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Excel;
using System.Data;

namespace NY
{
    public class ReadExcel
    {
        public string ExcelPathName;

        /// <summary>
        /// 只读Excel方法
        /// </summary>
        /// <param name="ExcelPath"></param>
        /// <returns></returns>
        public static List<TaskStep> GameReadExcel(string ExcelPath)
        {
            List<TaskStep> taskStepInfoList = new List<TaskStep>();
            FileStream stream = File.Open(Application.streamingAssetsPath + ExcelPath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            int columns = result.Tables[0].Columns.Count;//获取列数
            int rows = result.Tables[0].Rows.Count;//获取行数

            string[] m_temp;
            int m_tempNum;

            //从第二行开始读                                     
            for (int i = 1; i < rows; i++)
            {
                TaskStep taskStep = new TaskStep();
                taskStep.m_ID = Int32.Parse(result.Tables[0].Rows[i][0].ToString());
                taskStep.m_taskID = Int32.Parse(result.Tables[0].Rows[i][1].ToString());
                taskStep.m_stepIndex = Int32.Parse((result.Tables[0].Rows[i][2]).ToString());
                //前提步骤
                m_temp = (result.Tables[0].Rows[i][3]).ToString().Split(',');
                foreach (var item in m_temp)
                {
                    taskStep.m_preconditionStepIndexList.Add(int.Parse(item));
                }
                taskStep.m_role = (Role)Int32.Parse((result.Tables[0].Rows[i][4]).ToString());
                taskStep.m_stepType = (StepType)Int32.Parse(result.Tables[0].Rows[i][5].ToString());
                taskStep.m_stepContent = (result.Tables[0].Rows[i][6]).ToString();
                taskStep.m_speechContent = (result.Tables[0].Rows[i][7]).ToString();
                taskStep.m_partName = (result.Tables[0].Rows[i][8]).ToString();
                taskStep.m_hotspotName = (result.Tables[0].Rows[i][9]).ToString();
                //工具ID
                m_temp = (result.Tables[0].Rows[i][10]).ToString().Split(',');
                foreach (var item in m_temp)
                {
                    taskStep.m_toolIDList.Add(int.Parse(item));
                }
                //物料ID
                m_temp = (result.Tables[0].Rows[i][11]).ToString().Split(',');
                foreach (var item in m_temp)
                {
                    taskStep.m_materialID.Add(int.Parse(item));
                }

                m_tempNum = Int32.Parse((result.Tables[0].Rows[i][12]).ToString());
                //选项
                if (m_tempNum > 0)
                {
                    taskStep.m_taskStepOption = new TaskStepOption();
                    taskStep.m_taskStepOption.m_optionNum = m_tempNum;

                    if (!string.IsNullOrEmpty(result.Tables[0].Rows[i][13].ToString()))
                    {
                        m_temp = result.Tables[0].Rows[i][13].ToString().Replace('，', ',').Split(',');
                        taskStep.m_taskStepOption.m_taskStepOptionTypeList = new List<TaskStepOption.TaskStepOptionType>();
                        foreach (var item in m_temp)
                        {
                            if (string.IsNullOrEmpty(item)) continue;
                            taskStep.m_taskStepOption.m_taskStepOptionTypeList.Add((NY.TaskStepOption.TaskStepOptionType)Int32.Parse(item));
                        }
                    }
                    taskStep.m_taskStepOption.m_optionDatas = (result.Tables[0].Rows[i][14].ToString());
                    taskStep.m_taskStepOption.m_optionRes = (result.Tables[0].Rows[i][15].ToString());
                }
                //跳步
                taskStep.m_jumpStepType = (JumpStepType)Int32.Parse(result.Tables[0].Rows[i][16].ToString());
                taskStep.m_jumpStepNum = (result.Tables[0].Rows[i][17].ToString());
                taskStep.m_finalState = (result.Tables[0].Rows[i][18].ToString());
                taskStep.m_partAniName = (result.Tables[0].Rows[i][19].ToString());
                taskStep.m_toolAniName = (result.Tables[0].Rows[i][20].ToString());
                taskStep.m_needInitCamera = Convert.ToBoolean(int.Parse(result.Tables[0].Rows[i][21].ToString()));
                taskStep.m_operationScore = float.Parse(result.Tables[0].Rows[i][22].ToString());
                taskStep.m_stepLevel = int.Parse(result.Tables[0].Rows[i][23].ToString());

                taskStepInfoList.Add(taskStep);
            }
            Debug.Log(taskStepInfoList.Count);
            return taskStepInfoList;
        }

        public static List<TaskStep> ReadExcelTypeOfXLS(string ExcelPath)
        {
            List<TaskStep> TaskStepInfoList = new List<TaskStep>();
            FileStream stream = File.Open(Application.streamingAssetsPath + ExcelPath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            DataSet result = excelReader.AsDataSet();
            int columns = result.Tables[0].Columns.Count;//获取列数
            int rows = result.Tables[0].Rows.Count;//获取行数

            string[] m_temp;
            int m_tempNum;

            //从第二行开始读
            for (int i = 1; i < rows; i++)
            {
                TaskStep taskStep = new TaskStep();
                taskStep.m_ID = Int32.Parse(result.Tables[0].Rows[i][0].ToString());
                taskStep.m_taskID = Int32.Parse(result.Tables[0].Rows[i][1].ToString());
                taskStep.m_stepIndex = Int32.Parse((result.Tables[0].Rows[i][2]).ToString());
                //前提步骤
                m_temp = (result.Tables[0].Rows[i][3]).ToString().Split(',');
                foreach (var item in m_temp)
                {
                    taskStep.m_preconditionStepIndexList.Add(int.Parse(item));
                }
                taskStep.m_role = (Role)Int32.Parse((result.Tables[0].Rows[i][4]).ToString());
                taskStep.m_stepType = (StepType)Int32.Parse(result.Tables[0].Rows[i][5].ToString());
                taskStep.m_stepContent = (result.Tables[0].Rows[i][6]).ToString();
                taskStep.m_speechContent = (result.Tables[0].Rows[i][7]).ToString();
                taskStep.m_partName = (result.Tables[0].Rows[i][8]).ToString();
                taskStep.m_hotspotName = (result.Tables[0].Rows[i][9]).ToString();
                //工具ID
                m_temp = (result.Tables[0].Rows[i][10]).ToString().Split(',');
                foreach (var item in m_temp)
                {
                    taskStep.m_toolIDList.Add(int.Parse(item));
                }
                //物料ID
                m_temp = (result.Tables[0].Rows[i][11]).ToString().Split(',');
                foreach (var item in m_temp)
                {
                    taskStep.m_materialID.Add(int.Parse(item));
                }

                m_tempNum = Int32.Parse((result.Tables[0].Rows[i][12]).ToString());
                //选项
                if (m_tempNum > 0)
                {
                    taskStep.m_taskStepOption = new TaskStepOption();
                    taskStep.m_taskStepOption.m_optionNum = m_tempNum;

                    m_temp = result.Tables[0].Rows[i][13].ToString().Split(',');
                    taskStep.m_taskStepOption.m_taskStepOptionTypeList = new List<TaskStepOption.TaskStepOptionType>();
                    foreach (var item in m_temp)
                    {
                        taskStep.m_taskStepOption.m_taskStepOptionTypeList.Add((NY.TaskStepOption.TaskStepOptionType)Int32.Parse(item));
                    }

                    taskStep.m_taskStepOption.m_optionDatas = (result.Tables[0].Rows[i][14].ToString());
                    taskStep.m_taskStepOption.m_optionRes = (result.Tables[0].Rows[i][15].ToString());
                }
                //跳步
                taskStep.m_jumpStepType = (JumpStepType)Int32.Parse(result.Tables[0].Rows[i][16].ToString());
                taskStep.m_jumpStepNum = (result.Tables[0].Rows[i][17].ToString());
                taskStep.m_finalState = (result.Tables[0].Rows[i][18].ToString());
                taskStep.m_partAniName = (result.Tables[0].Rows[i][19].ToString());
                taskStep.m_toolAniName = (result.Tables[0].Rows[i][20].ToString());
                taskStep.m_needInitCamera = bool.Parse(result.Tables[0].Rows[i][21].ToString());
                taskStep.m_operationScore = float.Parse(result.Tables[0].Rows[i][22].ToString());
                taskStep.m_stepLevel = int.Parse(result.Tables[0].Rows[i][23].ToString());

                TaskStepInfoList.Add(taskStep);
            }
            return TaskStepInfoList;
        }

        public static List<Task> GameReadTaskExcel(string ExcelPath)
        {
            List<Task> taskList = new List<Task>();
            FileStream stream = File.Open(Application.streamingAssetsPath + ExcelPath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            int columns = result.Tables[0].Columns.Count;//获取列数
            int rows = result.Tables[0].Rows.Count;//获取行数

            //从第二行开始读                                     
            for (int i = 1; i < rows; i++)
            {
                //if (i<=2)
                {
                    Task task = new Task();
                    task.m_id = Int32.Parse(result.Tables[0].Rows[i][0].ToString());
                    task.m_taskName = result.Tables[0].Rows[i][1].ToString();
                    task.m_taskCode = Int32.Parse((result.Tables[0].Rows[i][2]).ToString());
                    task.m_similarWith = Int32.Parse((result.Tables[0].Rows[i][3]).ToString());
                    task.m_description = (result.Tables[0].Rows[i][4].ToString());
                    task.m_other = (result.Tables[0].Rows[i][5].ToString());
                    task.m_isShow = Convert.ToBoolean(int.Parse(result.Tables[0].Rows[i][6].ToString()));
                    task.m_spritePath = (result.Tables[0].Rows[i][7].ToString());
                    taskList.Add(task);
                }
            }
            Debug.Log(taskList.Count);
            stream.Close();
            return taskList;
        }

        public static List<ToolInfo> ReadToolExcel(string ExcelPath)
        {
            List<ToolInfo> toolList = new List<ToolInfo>();
            FileStream stream = File.Open(Application.streamingAssetsPath + ExcelPath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            int columns = result.Tables[0].Columns.Count;//获取列数
            int rows = result.Tables[0].Rows.Count;//获取行数

            //从第二行开始读                                     
            for (int i = 1; i < rows; i++)
            {
                try
                {
                    ToolInfo toolInfo = new ToolInfo();
                    toolInfo.m_id = Int32.Parse(result.Tables[0].Rows[i][0].ToString());
                    toolInfo.m_toolName = result.Tables[0].Rows[i][1].ToString();
                    toolInfo.m_toolID = Int32.Parse(result.Tables[0].Rows[i][2].ToString());
                    toolInfo.m_isShow = int.Parse(result.Tables[0].Rows[i][3].ToString());
                    toolList.Add(toolInfo);
                }
                catch (Exception e)
                {
                    Debug.LogError(e + "index:" + i);
                    throw;
                }

            }
            Debug.Log(toolList.Count);
            stream.Close();
            return toolList;
        }

        public static List<TaskHotspotGroup> ReadTaskHotspotGroup(string ExcelPath)
        {
            List<TaskHotspotGroup> taskHotspotGroupList = new List<TaskHotspotGroup>();
            FileStream stream = File.Open(Application.streamingAssetsPath + ExcelPath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            int columns = result.Tables[0].Columns.Count;//获取列数
            int rows = result.Tables[0].Rows.Count;//获取行数

            //从第二行开始读                                     
            for (int i = 1; i < rows; i++)
            {
                TaskHotspotGroup taskHotspotGroup = new TaskHotspotGroup();
                taskHotspotGroup.m_id = Int32.Parse(result.Tables[0].Rows[i][0].ToString());
                bool isPlot = (result.Tables[0].Rows[i][1].ToString()) == "0" ? false : true;
                taskHotspotGroup.m_isPlot = isPlot;
                taskHotspotGroup.m_hotspotName = result.Tables[0].Rows[i][2].ToString();
                taskHotspotGroup.m_optionDatas = result.Tables[0].Rows[i][3].ToString();
                taskHotspotGroup.m_groupDatas = result.Tables[0].Rows[i][4].ToString();
                taskHotspotGroup.m_remark = result.Tables[0].Rows[i][5].ToString();
                taskHotspotGroupList.Add(taskHotspotGroup);
            }
            Debug.Log(taskHotspotGroupList.Count);
            stream.Close();
            return taskHotspotGroupList;
        }
    }
}
