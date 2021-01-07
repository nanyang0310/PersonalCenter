using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TextChangeCommand : ICommand
{
    private Stack<string> lastInfos = new Stack<string>();
    private IEnumerator<string> datas;
    private TextMesh m_Textmesh;

    public TextChangeCommand(TextMesh textMesh, ICollection<string> texts)
    {
        datas = texts.GetEnumerator();
        m_Textmesh = textMesh;
    }

    public void Execute()
    {
        if (!datas.MoveNext())
        {
            datas.Reset();
            datas.MoveNext();
        }
        lastInfos.Push(m_Textmesh.text);
        m_Textmesh.text = datas.Current;
    }

    public void UnDo()
    {
        m_Textmesh.text = lastInfos.Pop();
    }
}


