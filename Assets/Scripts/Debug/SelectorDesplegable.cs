using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorDesplegable : MonoBehaviour
{
    [SerializeField]
    List<GameObject> prefabsNaves;
    [SerializeField]
    InstancePiezas Instanciador;

    GameObject naveSelect;

    public void SelectNave(int i)
    {
        naveSelect = prefabsNaves[i];
        SetNave();
    }

    public void SetNave()
    {
        Instanciador.SetPieza(naveSelect);
    }
}
