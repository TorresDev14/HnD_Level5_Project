using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSensor : MonoBehaviour
{
    [Header("flaots")]
    public float distance = 10;
    public float angle = 30f;
    public float height = 1.0f;
    public float ScanInterval;
    public float ScanTimer;

    [Header("Colour")]

    public Color meshcolour = Color.red;

    [Header("Mesh")]
    Mesh mesh;


    [Header("Intergers")]
    public int ScanFrequency = 30;
    public int Count;

    [Header("LayerMask")]
    public LayerMask Layers;

    [Header("Colliders")]
    Collider[] colliders = new Collider[50];

    // Start is called before the first frame update
    void Start()
    {
        ScanInterval = 1.0f / ScanFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        ScanTimer -= Time.deltaTime;
        
        if (ScanTimer < 0)
        {
            ScanTimer += ScanInterval;
            Scan();
        }
    }

    private void Scan()
    {
        
    }

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int Segments = 10;
        int numTriangles = (Segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] verticies = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 BottomCenter = Vector3.zero;
        Vector3 BottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 Bottomright = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 TopCentre = BottomCenter + Vector3.up * height;
        Vector3 TopLeft = BottomLeft + Vector3.up * height;
        Vector3 TopRight = Bottomright + Vector3.up * height;

        int Vert = 0;

        //Left Side
        verticies[Vert++] = BottomCenter;
        verticies[Vert++] = BottomLeft;
        verticies[Vert++] = TopLeft;

        verticies[Vert++] = TopLeft;
        verticies[Vert++] = TopCentre;
        verticies[Vert++] = BottomCenter;

        //Right Side
        verticies[Vert++] = BottomCenter;
        verticies[Vert++] = TopCentre;
        verticies[Vert++] = TopRight;

        verticies[Vert++] = TopRight;
        verticies[Vert++] = Bottomright;
        verticies[Vert++] = BottomCenter;

        float CurrentAngle = -angle;
        float DeltaAngle = (angle * 2) / Segments;

        for (int i = 0; i < Segments; i++)
        {

           
             BottomLeft = Quaternion.Euler(0, CurrentAngle, 0) * Vector3.forward * distance;
             Bottomright = Quaternion.Euler(0, CurrentAngle + DeltaAngle, 0) * Vector3.forward * distance;

           
             TopLeft = BottomLeft + Vector3.up * height;
             TopRight = Bottomright + Vector3.up * height;

            //Far Side
            verticies[Vert++] = BottomLeft;
            verticies[Vert++] = Bottomright;
            verticies[Vert++] = TopRight;

            verticies[Vert++] = TopRight;
            verticies[Vert++] = TopLeft;
            verticies[Vert++] = BottomLeft;
            //Top
            verticies[Vert++] = TopCentre;
            verticies[Vert++] = TopLeft;
            verticies[Vert++] = TopRight;
            //Bottom
            verticies[Vert++] = BottomCenter;
            verticies[Vert++] = Bottomright;
            verticies[Vert++] = BottomLeft;

            CurrentAngle += DeltaAngle;
        }

        for (int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
        ScanInterval = 1.0f / ScanFrequency;
    }

    private void OnDrawGizmos()
    {
        if (mesh) 
        {
            Gizmos.color = meshcolour;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
    }

}
