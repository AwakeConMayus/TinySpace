using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoOpciones : MonoBehaviour
{
    public Text b1, b2, b3;
    public Text c1, c2;
    [HideInInspector]
    public Opciones opciones;

    private void Awake()
    {
        opciones = GetComponent<Opciones>();
        EventManager.StartListening("RotacionOpciones", Actualizar);
    }

    public void Actualizar()
    {
        if (opciones.opcionesDisponibles.Count == 0) return;
        b1.text = opciones.opcionesIniciales[opciones.opcionesDisponibles[0]].name;
        b2.text = opciones.opcionesIniciales[opciones.opcionesDisponibles[1]].name;
        b3.text = opciones.opcionesIniciales[opciones.opcionesDisponibles[2]].name;

        c1.text = opciones.opcionesIniciales[opciones.opcionesEnfriamiento[0]].name;
        c2.text = opciones.opcionesIniciales[opciones.opcionesEnfriamiento[1]].name;
    }
}
