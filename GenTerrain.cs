using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenTerrain : MonoBehaviour
{
    public byte size;
    public float moveX;
    public float moveY;
    public float scale;
    float[,] map;
    public bool autoUpdate;
    GameObject parent;
    public Material material;

    public void Generate()
    {
        Clear();
        map = new float[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                map[i, j] = 10 * Mathf.PerlinNoise(moveX + (float)i / size * scale, moveY + (float)j / size * scale); 
            }
        }

        for (int i = 0; i < size - 1; i++)
            for (int j = 0; j < size - 1; j++)
            {
                for (byte k = 0; k < 2; k++)
                {
                    GameObject side = new GameObject("side");
                    Mesh mesh = new Mesh();
                    side.AddComponent<MeshFilter>();
                    side.AddComponent<MeshRenderer>();

                    Vector3[] vertices = new Vector3[3];
                    if (k == 0)
                    {
                        vertices[0] = new Vector3(i, map[i, j], j);
                        vertices[1] = new Vector3(i, map[i, j + 1], j + 1);
                        vertices[2] = new Vector3(i + 1, map[i + 1, j], j);
                    }
                    else
                    {
                        vertices[0] = new Vector3(i, map[i, j + 1], j + 1);
                        vertices[1] = new Vector3(i + 1, map[i + 1, j + 1], j + 1);
                        vertices[2] = new Vector3(i + 1, map[i + 1, j], j);
                    }

                    mesh.vertices = vertices;

                    Vector2[] uvs = new Vector2[vertices.Length];
                    for (int n = 0; n < uvs.Length; n++) uvs[n] = new Vector2(vertices[n].x, vertices[n].z);
                    mesh.uv = uvs;

                    mesh.triangles = new int[3] {0, 1, 2};
                    /*
                    Vector3[] normals = mesh.normals;
                    Quaternion rotation = new Quaternion();
                    for (int i = 0; i < normals.Length; i++) normals[i] = rotation * normals[i];
                    mesh.normals = normals;
                    */
                    side.GetComponent<MeshFilter>().mesh = mesh;
                    side.GetComponent<MeshRenderer>().material = material;
                    side.transform.parent = parent.transform;
                }

            }
    }

    public void Clear()
    {
        DestroyImmediate(parent);
        parent = new GameObject("Storage");
    }
}
