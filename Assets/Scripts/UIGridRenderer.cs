using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGridRenderer : Graphic
{

    public Vector2Int gridSize = new Vector2Int(1, 1);
    float cellWidth;
    float cellHeight;
    float width;
    float height;


    public float thickness = 10;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        //SetData
        vh.Clear();
        height = rectTransform.rect.height;
        width = rectTransform.rect.width;

        cellWidth = width / gridSize.x;
        cellHeight = height / gridSize.y;

        //Iterate
        int count = 0;
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                DrawCell(x, y, count, vh);
                count++;
            }
        }
    }

    void DrawCell(int x, int y, int index, VertexHelper vh)
    {
        float xPosition = cellWidth * x;
        float yPosition = cellHeight * y;

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = new Vector3(xPosition, yPosition);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPosition, yPosition + cellHeight);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPosition + cellWidth, yPosition + cellHeight);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPosition + cellWidth, yPosition);
        vh.AddVert(vertex);

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 0);

        float widthSqr = thickness * thickness;
        float distSqr = widthSqr / 2f;
        float distance = Mathf.Sqrt(distSqr);

        vertex.position = new Vector3(xPosition + distance, yPosition + distance);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPosition + distance, yPosition + (cellHeight - distance));
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPosition + (cellWidth - distance), yPosition + (cellHeight - distance));
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPosition + (cellWidth - distance), yPosition + distance);
        vh.AddVert(vertex);

        int offset = index * 8;


        //Draw Triangles
        //Left Edge
        vh.AddTriangle(offset + 0, offset + 1, offset + 5);
        vh.AddTriangle(offset + 5, offset + 4, offset + 0);

        //Top Edge
        vh.AddTriangle(offset + 1, offset + 2, offset + 6);
        vh.AddTriangle(offset + 6, offset + 5, offset + 1);

        //RightEdge
        vh.AddTriangle(offset + 2, offset + 3, offset + 7);
        vh.AddTriangle(offset + 7, offset + 6, offset + 2);

        //BottomEdge
        vh.AddTriangle(offset + 3, offset + 0, offset + 4);
        vh.AddTriangle(offset + 4, offset + 7, offset + 3);
    }
}
