using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorDesplegable : MonoBehaviour
{
    [SerializeField]
    int player;
    [SerializeField]
    List<GameObject> prefabsNaves;
    [SerializeField]
    InstancePiezas Instanciador;

    GameObject naveSelect;

    private void Start()
    {
        naveSelect = prefabsNaves[0];
    }

    public void SelectNave(int i)
    {
        naveSelect = prefabsNaves[i];
        SetNave();
    }

    public void SetNave()
    {
        Instanciador.SetJugador(player);
        Instanciador.SetPieza(naveSelect);
    }
}
