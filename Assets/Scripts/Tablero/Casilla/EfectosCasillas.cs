using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectosCasillas : MonoBehaviour
{
    public GameObject holy;
    public GameObject tpOut;
    public GameObject tpIn;

    List<GameObject> efects = new List<GameObject>();

    public void Holy()
    {
        GameObject g = Instantiate(holy, transform);
        efects.Add(g);
    }
    public void TPOut()
    {
        GameObject g = Instantiate(tpOut, transform);
        efects.Add(g);
    }
    public void TPIn()
    {
        GameObject g = Instantiate(tpIn, transform);
        efects.Add(g);
    }


    public void ResetEfects()
    {
        int counter = efects.Count;
        for (int i = 0; i < counter; i++)
        {
            Destroy(efects[0]);
        }
        efects = new List<GameObject>();
    }
}
