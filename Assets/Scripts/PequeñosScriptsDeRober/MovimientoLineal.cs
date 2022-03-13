using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoLineal : MonoBehaviour
{
    [SerializeField] float PosicionInicial;
    [SerializeField] float PosicionFinal;
    [SerializeField] float FillRellenoInicial;
    [SerializeField] float FillRellenoFinal;
    [SerializeField] GameObject Fill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Fill.transform.position = new Vector3(Fill.transform.position.x, (PosicionFinal + (PosicionFinal - PosicionInicial) / (FillRellenoFinal - FillRellenoInicial) * (FillRellenoInicial - FillRellenoFinal)), Fill.transform.position.z);
       
    }
}
 