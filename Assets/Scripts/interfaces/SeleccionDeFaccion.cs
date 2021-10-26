using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionDeFaccion : MonoBehaviour
{


    [SerializeField]
    TuSeleccion mi_Seleccion;
    [SerializeField]
    TuSeleccion eleccion_rival;

    private void Start()
    {
        mi_Seleccion.faccion = Faccion.none;
        mi_Seleccion.mi_poder = null;
        eleccion_rival.faccion = Faccion.none;
        eleccion_rival.mi_poder = null;

        for(int i = 0; i < mi_Seleccion.mis_opciones.Length; ++i)
        {
            mi_Seleccion.mis_opciones[i] = null;
            eleccion_rival.mis_opciones[i] = null;
        }
    }


    public void Seleccionar_Faccion(int i)
    {
        mi_Seleccion.faccion = (Faccion)i;
        SceneManager.LoadScene(1);
    }
}
