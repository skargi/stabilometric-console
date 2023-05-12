using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class ScoresGraph : MonoBehaviour
{
    [SerializeField]private Sprite circleSprite;
    private RectTransform GraphContainer;

    private void Awake(){
        GraphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();

        List<int> scoresList = new List<int>() {5, 98, 56, 45, 30, 22};
        ShowGraph(scoresList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition){
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(GraphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);  
        return gameObject;      
    }

    private void ShowGraph(List<int> valuesList) {
        float graphHeight = GraphContainer.sizeDelta.y;
        float xSize = 100f;
        float yMax = 100f;

        GameObject lastCircleGameObject = null;
        for(int i = 0; i < valuesList.Count; i++){
            float xPosition = 10 + i * xSize;
            float yPosition = (valuesList[i] / yMax) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null){
                ConnectDots(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    } 

    private void ConnectDots(Vector2 dotPositionA, Vector2 dotPositionB){
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(GraphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);   
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0,0,UtilsClass.GetAngleFromVectorFloat(dir));
    }
}
