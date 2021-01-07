/*
/// 功能： 
/// 时间：
/// 版本：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoteLoader : MonoBehaviour
{
    public Button lastBtn;
    public Button nextBtn;

    public Button ctrlBtn;
    public Button undoBtn;
    public Text ctrlName;

    private int index;
    private const int NUM_COMMAND = 10;
    private ICommand[] commands;
    private string[] textinfos;

    private MoveCommand movexCmd;
    private MoveCommand moveyCmd;
    private MoveCommand movezCmd;
    private ColorChangeCommand redColorCmd;
    private ColorChangeCommand greenColorCmd;
    private ColorChangeCommand blueColorCmd;
    private TextChangeCommand textCmd;

    private string[] infos = { "A", "B", "C", "D", "E", "F" };
    public RemoteControl remoteCtrl;

    public GameObject cube;

    void Awake()
    {
        remoteCtrl = new RemoteControl();

        lastBtn.onClick.AddListener(OnLastBtnClicked);
        nextBtn.onClick.AddListener(OnNextBtnClicked);
        ctrlBtn.onClick.AddListener(remoteCtrl.OnCtrlBtnClicked);
        undoBtn.onClick.AddListener(remoteCtrl.OnUnDoBtnClicked);
    }

    void Start()
    {
        commands = new ICommand[NUM_COMMAND];
        textinfos = new string[NUM_COMMAND];

        textinfos[0] = "x方向移动";
        commands[0] = new MoveCommand(cube.transform, Vector3.right);

        textinfos[1] = "y方向移动";
        commands[1] = new MoveCommand(cube.transform, Vector3.up);

        textinfos[2] = "z方向移动";
        commands[2] = new MoveCommand(cube.transform, Vector3.forward);

        textinfos[6] = "变红";
        commands[6] = new ColorChangeCommand(Color.red, cube.GetComponent<Renderer>().material);

        textinfos[7] = "变绿";
        commands[7] = new ColorChangeCommand(Color.green, cube.GetComponent<Renderer>().material);

        textinfos[8] = "变蓝";
        commands[8] = new ColorChangeCommand(Color.blue, cube.GetComponent<Renderer>().material);

        textinfos[9] = "换信息";
        commands[9] = new TextChangeCommand(cube.GetComponentInChildren<TextMesh>(), infos);
    }

    public void SetText(string textinfo)
    {
        ctrlName.text = textinfo;
    }

    private void OnNextBtnClicked()
    {
        if (index == NUM_COMMAND || index == -1)
        {
            index = 0;
        }

        remoteCtrl.SetCommond(commands[index]);
        SetText(textinfos[index]);
        index++;
    }

    private void OnLastBtnClicked()
    {
        if (index == NUM_COMMAND || index == -1)
        {
            index = NUM_COMMAND - 1;
        }

        remoteCtrl.SetCommond(commands[index]);
        SetText(textinfos[index]);
        index--;
    }



}
