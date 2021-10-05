using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Opciones : MonoBehaviour
{
    public GameObject poder;
    public GameObject[] opcionesIniciales = new GameObject[5];

    [HideInInspector]
    public List<GameObject> opcionesDisponibles = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> opcionesEnfriamiento = new List<GameObject>();

    public void Preparacion()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject g;
            do
            {
                g = opcionesIniciales[Random.Range(0, opcionesIniciales.Length)];
            } while (opcionesDisponibles.Contains(g));
            opcionesDisponibles.Add(g);
        }
    }

    public int[] Shuffle<T>(int length)
    {
        int[] array = new int[length];
        int n = array.Length;
        while (n > 1)
        {
            int k = 0; //Random.Next(n--);
            int temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
        return array;
    }
}
