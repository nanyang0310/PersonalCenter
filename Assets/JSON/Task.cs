using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace NY
{
    [System.Serializable]
    public class TaskRoot
    {
        [SerializeField]
        public List<Task> tasks ;
    }

    [System.Serializable]
    public class Task
    {
        [SerializeField] public int m_id;
        [SerializeField] public string m_taskName;
        [SerializeField] public int m_taskCode;
        [SerializeField] public int m_similarWith;
        [SerializeField] public string m_description;
        [SerializeField] public string m_other;
        [SerializeField] public bool m_isShow = true;
        [SerializeField] public string m_spritePath;
        [SerializeField] public List<TaskStep> m_taskStepList = new List<TaskStep>();

        public TaskStep GetTaskStep(int startStepIndex, string hotspotName)
        {
            for (int i = startStepIndex; i < m_taskStepList.Count; ++i)
            {
                if (m_taskStepList[i].m_hotspotName == hotspotName)
                {
                    return m_taskStepList[i];
                }
            }

            return null;
        }
    }

    [System.Serializable]
    public enum StepType
    {
        /// <summary>
        /// 目视检查
        /// </summary>
        VisualCheck = 0,

        /// <summary>
        /// 操作部件
        /// </summary>
        OperationHotspot,

        /// <summary>
        /// 穿戴设备
        /// </summary>
        Wearabl,

        /// <summary>
        /// 打开工具面板
        /// </summary>
        OpenUITool,

        /// <summary>
        /// 与站长对话,领取任务
        /// </summary>
        Talk,

        /// <summary>
        /// 部件组选择
        /// </summary>
        DeviceGroups,

        /// <summary>
        /// 单部件自定义选择
        /// </summary>
        DeviceCustomChoice,

        Plot,//剧情
    }
    [System.Serializable]
    public enum JumpStepType
    {
        DefaultJump, //相对当前步骤 跳步的步数
        SpecialJump, //直接跳步到指定的步骤
    }

    [System.Serializable]
    public class TaskStepRoot
    {
        [SerializeField]
        public List<TaskStep> taskSteps;
    }

    [System.Serializable]
    public class TaskStep
    {
        [SerializeField]
        [XmlAttribute("ID")]
        public int m_ID;

        [SerializeField]
        [XmlAttribute("TaskID")]
        public int m_taskID;

        [SerializeField]
        [XmlAttribute("StepIndex")]
        public int m_stepIndex;

        [SerializeField]
        [XmlAttribute("Role")]
        public Role m_role;

        [SerializeField]
        [XmlAttribute("StepType")]
        public StepType m_stepType;

        [SerializeField]
        [XmlElement("PreconditionStepIndexList")]
        public List<int> m_preconditionStepIndexList = new List<int>();

        [SerializeField]
        [XmlAttribute("StepContent")]
        public string m_stepContent;

        [SerializeField]
        [XmlAttribute("SpeechContent")]
        public string m_speechContent;

        [SerializeField]
        [XmlAttribute("PartName")]
        public string m_partName;

        [SerializeField]
        [XmlAttribute("HotspotName")]
        public string m_hotspotName;

        [SerializeField]
        [XmlElement("ToolIDList")]
        public List<int> m_toolIDList = new List<int>();

        [SerializeField]
        [XmlElement("MaterialID")]
        public List<int> m_materialID = new List<int>();

        [SerializeField]
        [XmlAttribute("TaskStepOption")]
        public TaskStepOption m_taskStepOption;

        [SerializeField]
        public JumpStepType m_jumpStepType;

        [SerializeField]
        public string m_jumpStepNum;

        [SerializeField]
        [XmlAttribute("FinalState")]
        public string m_finalState;             //满足步骤完成的状态值 

        [SerializeField]
        public string m_partAniName;

        [SerializeField]
        public string m_toolAniName;

        [SerializeField]
        public bool m_needInitCamera;

        [SerializeField]
        [XmlAttribute("OperationScore")]
        public float m_operationScore = 0;    //该步骤操作分数 

        [SerializeField]
        public int m_stepLevel = 0;    //该步骤等级

        [SerializeField]
        [XmlAttribute("IsCompleted")]
        public bool m_isCompleted = false;
    }

    [System.Serializable]
    public class TaskStepOption
    {
        [System.Serializable]
        public enum TaskStepOptionType
        {
            ChoiceQuestion,
            Text,
            Image,
            Video,
            SpecialOperation,
        }

        [SerializeField]
        public int m_optionNum;
        [SerializeField]
        public List<TaskStepOptionType> m_taskStepOptionTypeList;
        [SerializeField]
        public string m_optionDatas;
        [SerializeField]
        public string m_optionRes;
    }

    [System.Serializable]
    public class ToolInfoRoot
    {
        [SerializeField]
        public List<ToolInfo> toolInfos;
    }

    [System.Serializable]
    public class ToolInfo
    {
        [SerializeField]
        public int m_id;
        [SerializeField]
        public string m_toolName;
        [SerializeField]
        public int m_toolID;
        [SerializeField]
        public int m_isShow;
    }

    [System.Serializable]
    public class TaskHotspotGroupRoot
    {
        [SerializeField]
        public List<TaskHotspotGroup> taskHotspotGroups;
    }

    [System.Serializable]
    public class TaskHotspotGroup
    {
        [SerializeField]
        public int m_id;
        [SerializeField]
        public bool m_isPlot;
        [SerializeField]
        public string m_hotspotName;
        [SerializeField]
        public string m_optionDatas;
        [SerializeField]
        public string m_groupDatas;
        [SerializeField]
        public string m_remark;
    }

    [System.Serializable]
    public enum Role
    {
        Protagonist = 0,//主角
        Narrator,//旁白
        NPC,
    }
}
