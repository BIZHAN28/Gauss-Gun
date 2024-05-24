using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite arrowHeadSprite; // Добавление спрайта для стрелки
    [SerializeField] private Color gridColor = Color.grey; // Цвет сетки
	public float wireLength = 1f;
	public float currency = 1f;
	public int coilTurns = 10;
    private RectTransform graphContainer;

    void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        List<float> valueList = new List<float>();
		valueList = GraphFunction(100);
        DrawGrid(10, 10);
        DrawAxes(); 
		ShowGraph(valueList);
    }
	
	private List<float> GraphFunction(int points) 
	{
		float y = 0;
		List<float> valueList = new List<float>();
		for (int x = 0; x < points; x++) {
			float F = 1.26f * MathF.Pow(10f, -6f) * coilTurns * MathF.Pow(currency, 2f) * wireLength / x;
			y = F;
			valueList.Add(y);
		}
		return valueList;
	}

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject circleObject = new GameObject("circle", typeof(Image));
        circleObject.transform.SetParent(graphContainer, false);
        circleObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = circleObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return circleObject;
    }

    private void ShowGraph(List<float> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float yMaximum = 10000;
        float xSize = graphWidth / (valueList.Count - 1); // Расстояние между точками по X

        GameObject lastCircleObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleObject != null)
            {
                CreateDotConnection(lastCircleObject.GetComponent<RectTransform>().anchoredPosition, circleObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleObject = circleObject;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject connectonObject = new GameObject("dotConnection", typeof(Image));
        connectonObject.transform.SetParent(graphContainer, false);
        connectonObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        RectTransform rectTransform = connectonObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;
        rectTransform.sizeDelta = new Vector2(distance, 1f);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

    private void DrawGrid(int xDivisions, int yDivisions) 
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;

        for (int i = 1; i < xDivisions; i++)
        {
            float xPosition = i * (graphWidth / xDivisions);
            CreateGridLine(new Vector2(xPosition, 0), new Vector2(xPosition, graphHeight));
        }

        for (int i = 1; i < yDivisions; i++)
        {
            float yPosition = i * (graphHeight / yDivisions);
            CreateGridLine(new Vector2(0, yPosition), new Vector2(graphWidth, yPosition));
        }
    }

    private void CreateGridLine(Vector2 startPosition, Vector2 endPosition)
    {
        GameObject lineObject = new GameObject("gridLine", typeof(Image));
        lineObject.transform.SetParent(graphContainer, false);
        lineObject.GetComponent<Image>().color = gridColor;
        RectTransform rectTransform = lineObject.GetComponent<RectTransform>();
        Vector2 direction = (endPosition - startPosition).normalized;
        float distance = Vector2.Distance(startPosition, endPosition);
        rectTransform.anchoredPosition = startPosition + direction * (distance / 2);
        rectTransform.sizeDelta = new Vector2(distance, 2);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(direction));
    }

    private void DrawAxes()
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;

        CreateGridLine(new Vector2(0, 0), new Vector2(0, graphHeight));
        CreateArrow(new Vector2(0, graphHeight), Vector2.up);

        CreateGridLine(new Vector2(0, 0), new Vector2(graphWidth, 0));
        CreateArrow(new Vector2(graphWidth, 0), Vector2.right);
    }

    private void CreateArrow(Vector2 position, Vector2 direction) 
    {
        GameObject arrowObject = new GameObject("arrow", typeof(Image));
        arrowObject.transform.SetParent(graphContainer, false);
        arrowObject.GetComponent<Image>().sprite = arrowHeadSprite;
        RectTransform rectTransform = arrowObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = new Vector2(25, 25);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        float angle = UtilsClass.GetAngleFromVectorFloat(direction);
        rectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
