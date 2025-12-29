using System;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Vector3[] points;
    public float radius = 2f;

    public Vector3 GetPoint(int index)
    {
        return points[index];
    }
    
    void OnDrawGizmos()
    {
        if (points == null)
            return;

        for (int i = 0; i < points.Length; i++)
        {
            if (i + 1 < points.Length)
            {
                Gizmos.DrawLine(points[i], points[i + 1]);
            }
        }
    }
}