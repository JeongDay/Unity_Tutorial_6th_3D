using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    private LineRenderer line;

    private int lineCount;

    public Color color;
    public float lineWidth = 0.05f;
    
    public List<GameObject> lineObjs = new List<GameObject>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 드래그 시작
        {
            GameObject lineObj = new GameObject("Line");
            line = lineObj.AddComponent<LineRenderer>();
            line.useWorldSpace = false;
            
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.startColor = color;
            line.endColor = color;

            line.material = new Material(Shader.Find("Universal Render Pipeline/Particles/Unlit"));

            lineObjs.Add(lineObj);
        }
        else if (Input.GetMouseButton(0)) // 마우스 드래그
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            line.positionCount = ++lineCount;
            line.SetPosition(lineCount - 1, worldPos);
        }
        else if (Input.GetMouseButtonUp(0)) // 마우스 드래그 종료
        {
            lineCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (var lineObj in lineObjs)
                Destroy(lineObj);

            lineObjs.Clear();
        }
    }
}