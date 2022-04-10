using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Opciones_Faccion")]
public class OpcionesFaccion : ScriptableObject
{


    public Faccion faccion = Faccion.none;


    public GameObject[] posibles_Poders = new GameObject[3];


    public List<GameObject> posibles_Piezas_Especializadas = new List<GameObject>();


    public List<int> huecos_Especializadas = new List<int>();

    public List<GameObject> posibles_Piezas_Especiales = new List<GameObject>();

    public GameObject[] piezas_Basicas = new GameObject[4];

}
