using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovimientoLineal : MonoBehaviour
{
    [SerializeField] float PosicionInicial;
    [SerializeField] float PosicionFinal;
    [SerializeField] float FillRellenoInicial;
    [SerializeField] float FillRellenoFinal;
    [SerializeField] Image Fill;
    RectTransform myTransform;
    private void Awake()
    {
        myTransform = Fill.GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       this.transform.localPosition = new Vector3(this.transform.localPosition.x, (PosicionFinal + (PosicionFinal - PosicionInicial) / (FillRellenoFinal - FillRellenoInicial) * (myTransform.sizeDelta.y - FillRellenoFinal)), this.transform.localPosition.z);
       
    }
}
 