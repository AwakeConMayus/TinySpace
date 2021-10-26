using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Mi_Seleccion")]
public class TuSeleccion : ScriptableObject
{


    public Faccion faccion = Faccion.none;

    public GameObject mi_poder;

    public GameObject[] mis_opciones = new GameObject[5];
}
