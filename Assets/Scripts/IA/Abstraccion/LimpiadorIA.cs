using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimpiadorIA : MonoBehaviour
{
    static List<GameObject> piezas = new List<GameObject>();

    [SerializeField] int maxValue;

    static bool cleneable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pieza>() && !piezas.Contains(other.gameObject)) piezas.Add(other.gameObject);
    }

    private void Update()
    {
        if (piezas.Count > maxValue) cleneable = true;
    }

    public static void Clean()
    {
        if (!cleneable) return;
        cleneable = false;
        int piezasLimpiar = piezas.Count;
        for (int i = piezasLimpiar-1; i >= 0; i--)
        {
            Destroy(piezas[i]);
        }
        piezas = new List<GameObject>();
        foreach (Casilla c in IATablero.instance.mapa) c.pieza = null;
    }
}
