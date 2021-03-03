/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading the Code Monkey Utilities
    I hope you find them useful in your projects
    If you have any questions use the contact form
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatsRadarChart : MonoBehaviour
{

    [SerializeField] private Material radarMaterial;
    [SerializeField] private Texture2D radarTexture2D;

    private Stats stats;
    private CanvasRenderer radarMeshCanvasRenderer;

    public float radarChartSize = 145f;

    public int[] m_maxValues;


    private void Awake()
    {
        radarMeshCanvasRenderer = transform.Find("radarMesh").GetComponent<CanvasRenderer>();
    }

    private void Start()
    {
        StartCoroutine(DrawLine());
    }

    IEnumerator DrawLine()
    {
        while (true)
        {
            List<int> value = new List<int>();
            for (int i = 0; i < m_maxValues.Length; i++)
            {
                int temp = Random.Range(0, m_maxValues[i]);
                value.Add(temp);
            }
            Stats stats = new Stats(value[0], value[1], value[2], value[3], value[4], m_maxValues);
            SetStats(stats);
            yield return new WaitForSeconds(1);
        }
    }

    public void SetStats(Stats stats)
    {
        this.stats = stats;
        stats.OnStatsChanged += Stats_OnStatsChanged;
        UpdateStatsVisual();
    }

    private void Stats_OnStatsChanged(object sender, System.EventArgs e)
    {
        UpdateStatsVisual();
    }

    private void UpdateStatsVisual()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[6];
        Vector2[] uv = new Vector2[6];
        int[] triangles = new int[3 * 5];

        float angleIncrement = 360f / 5;

        Vector3 attackVertex = Quaternion.Euler(0, 0, -angleIncrement * 0) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Attack, m_maxValues[0]);
        int attackVertexIndex = 1;
        Vector3 defenceVertex = Quaternion.Euler(0, 0, -angleIncrement * 1) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Defence, m_maxValues[1]);
        int defenceVertexIndex = 2;
        Vector3 speedVertex = Quaternion.Euler(0, 0, -angleIncrement * 2) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Speed, m_maxValues[2]);
        int speedVertexIndex = 3;
        Vector3 manaVertex = Quaternion.Euler(0, 0, -angleIncrement * 3) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Mana, m_maxValues[3]);
        int manaVertexIndex = 4;
        Vector3 healthVertex = Quaternion.Euler(0, 0, -angleIncrement * 4) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Health, m_maxValues[4]);
        int healthVertexIndex = 5;

        vertices[0] = Vector3.zero;
        vertices[attackVertexIndex] = attackVertex;
        vertices[defenceVertexIndex] = defenceVertex;
        vertices[speedVertexIndex] = speedVertex;
        vertices[manaVertexIndex] = manaVertex;
        vertices[healthVertexIndex] = healthVertex;

        uv[0] = Vector2.zero;
        uv[attackVertexIndex] = Vector2.one;
        uv[defenceVertexIndex] = Vector2.one;
        uv[speedVertexIndex] = Vector2.one;
        uv[manaVertexIndex] = Vector2.one;
        uv[healthVertexIndex] = Vector2.one;

        triangles[0] = 0;
        triangles[1] = attackVertexIndex;
        triangles[2] = defenceVertexIndex;

        triangles[3] = 0;
        triangles[4] = defenceVertexIndex;
        triangles[5] = speedVertexIndex;

        triangles[6] = 0;
        triangles[7] = speedVertexIndex;
        triangles[8] = manaVertexIndex;

        triangles[9] = 0;
        triangles[10] = manaVertexIndex;
        triangles[11] = healthVertexIndex;

        triangles[12] = 0;
        triangles[13] = healthVertexIndex;
        triangles[14] = attackVertexIndex;


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        radarMeshCanvasRenderer.SetMesh(mesh);
        radarMeshCanvasRenderer.SetMaterial(radarMaterial, radarTexture2D);
    }
}
