using System.Collections.Generic;
using UnityEngine;

public class ColorChangeCommand : ICommand
{
    private Stack<Color> m_OriginColor = new Stack<Color>();
    private Color m_Color;
    private Material m_Material;

    public ColorChangeCommand(Color color, Material material)
    {
        m_Color = color;
        m_Material = material;
    }

    public void Execute()
    {
        m_OriginColor.Push(m_Material.color);
        m_Material.color = m_Color;
    }

    public void UnDo()
    {
        m_Material.color = m_OriginColor.Pop();
    }
}

