using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    private Vector3[] baseHeight;
    public GameObject Boat;

    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        if (baseHeight == null)
            baseHeight = mesh.vertices;

        Vector3[] vertices = new Vector3[baseHeight.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = baseHeight[i];
            vertices[i].y = SeaCalculator.GetWorldHeight(transform.TransformPoint(vertices[i]));
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        Vector3 newPos = new Vector3(Boat.transform.position.x, transform.position.y, Boat.transform.position.z);
        transform.position = newPos;
    }
}
