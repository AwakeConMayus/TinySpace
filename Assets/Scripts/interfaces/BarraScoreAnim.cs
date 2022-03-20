using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraScoreAnim : MonoBehaviour
{
    RectTransform myTransform;

    float targetSize = 250;
    float error = 0.01f;
    public float time_toChange = 3;
    private float counter;
    
    int semaforo = 0;
    private void Awake()
    {
        myTransform = GetComponent<RectTransform>();
        
    }

    private void Update()
    {
        if(Mathf.Abs(myTransform.sizeDelta.y - targetSize) > error)
        {
            counter += Time.deltaTime;
            
          
            myTransform.sizeDelta = new Vector2(myTransform.sizeDelta.x, Mathf.Lerp(myTransform.sizeDelta.y, targetSize, counter/time_toChange));
           
        }
    }

    public void SetTargetSize(float f)
    {
        targetSize = f;
        counter = 0;
    }
   
}
