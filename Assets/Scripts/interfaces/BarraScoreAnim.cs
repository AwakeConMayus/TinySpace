using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraScoreAnim : MonoBehaviour
{
    RectTransform myTransform;
    [SerializeField] GameObject PerderPuntos;
    [SerializeField] GameObject GanarPuntos;
    float targetSize = 250;
    float error = 0.01f;
    public float time_toChange = 3;
    private float counter;
    bool semaforo = true;
    
   
    private void Awake()
    {
        myTransform = GetComponent<RectTransform>();
        Invoke("desactivar", 5f);

    }

    private void Update()
    {
       /* if(Mathf.Abs(myTransform.sizeDelta.y - targetSize) > error)
        {
            counter += Time.deltaTime;
            if(myTransform.sizeDelta.y >  Mathf.Lerp(myTransform.sizeDelta.y, targetSize, counter / time_toChange) && semaforo == false)
            {
                PerderPuntos.SetActive(true);
                semaforo = true;
                Invoke("desactivar", 2);
            }
            if(myTransform.sizeDelta.y < Mathf.Lerp(myTransform.sizeDelta.y, targetSize, counter / time_toChange) && semaforo == false)
            {
                GanarPuntos.SetActive(true);
                semaforo = true;
                Invoke("desactivar", 2);
            }
          
            myTransform.sizeDelta = new Vector2(myTransform.sizeDelta.x, Mathf.Lerp(myTransform.sizeDelta.y, targetSize, counter/time_toChange));
           
        }*/
    }
    public void desactivar()
    {
        PerderPuntos.SetActive(false);
        semaforo = false;
        GanarPuntos.SetActive(false);
    }

    public void SetTargetSize(float f)
    {
        targetSize = f;
        counter = 0;
    }
   
}
