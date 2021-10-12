using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraScoreAnim : MonoBehaviour
{
    RectTransform myTransform;

    float targetSize;
    float error = 1f;
    public float speed = 1;

    private void Awake()
    {
        myTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(myTransform.sizeDelta.y - targetSize > error)
        {
            myTransform.sizeDelta = new Vector2(myTransform.sizeDelta.x, Mathf.Lerp(myTransform.sizeDelta.y, targetSize, speed * Time.deltaTime));
        }
    }

    public void SetTargetSize(float f)
    {
        targetSize = f;
    }
}
