using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeShaderTest : MonoBehaviour
{
    public ComputeShader computeShader;
    public RenderTexture renderTexture;
    public Mesh mesh;
    public Material material;
    private Grid[] data;
    private List<GameObject> objects;
    public int count = 50;

    public struct Grid
    {
        public Vector3 position;
        public Color color;

    }

    public void CreateGrids()
    {
        objects = new List<GameObject>();
        data = new Grid[count * count];
        for (int x = 0; x < count; x++)
        {
            for (int y = 0; y < count; y++)
            {
                CreateGrid(x, y);
            }
            
        }
    }

    private void CreateGrid(int x, int y)
    {
        GameObject grid = new GameObject("Grid " + x * count + y, typeof(MeshFilter), typeof(MeshRenderer));
        grid.GetComponent<MeshFilter>().mesh = mesh;
        grid.GetComponent<MeshRenderer>().material = new Material(material);
        //grid.transform.position = new Vector3(x, Random.Range(-0.1f, 0.1f), y);
        grid.transform.position = new Vector3(x, 0, y);

        Color color = Random.ColorHSV();
        grid.GetComponent<MeshRenderer>().material.SetColor("_Color", color);

        objects.Add(grid);

        Grid gridData = new Grid();
        gridData.position = grid.transform.position;
        gridData.color = color;
        data[x * count + y] = gridData;
    }

    public void OnRandomise()
    {
        int colorSize = sizeof(float) * 4;
        int vector3Size = sizeof(float) * 3;
        int totalSize = colorSize + vector3Size;

        ComputeBuffer gridsBuffer = new ComputeBuffer(data.Length, totalSize);
        gridsBuffer.SetData(data);

        computeShader.SetBuffer(0, "grids", gridsBuffer);
        computeShader.SetFloat("resolution", data.Length);
        computeShader.Dispatch(0, data.Length / 10, 1, 1);

        gridsBuffer.GetData(data);

        for (int i = 0; i < objects.Count; i++)
        {
            GameObject obj = objects[i];
            Grid grid = data[i];
            obj.transform.position = grid.position;
            obj.GetComponent<MeshRenderer>().material.SetColor("_Color", grid.color);
        }
        gridsBuffer.Dispose();
    }
    void Start()
    {
        OnGUI();
    }

    private void OnGUI()
    {
        if (objects == null)
        {
            if (GUI.Button(new Rect(0, 0, 100, 50), "Create"))
            {
                CreateGrids();
            }
        }
        else
        {
            if (GUI.Button(new Rect(0, 0, 100, 50), "Shader"))
            {
                OnRandomise();
            }
        }
    }
}
