using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BotonesOpciones : MonoBehaviour
{
    TextoOpciones textOpciones;
    public int index;

    
    private void Start()
    {
        textOpciones = GetComponentInParent<TextoOpciones>();
    }


    private void OnMouseOver()
    {
        print("Entro en el boton");
        switch (index)
        {
            case 1:
                textOpciones.B1In();
                break;
            case 2:
                textOpciones.B2In();
                break;
            case 3:
                textOpciones.B3In();
                break;
        }
    }

    private void OnMouseExit()
    {
        print("Salgo del boton");
        switch (index)
        {
            case 1:
                textOpciones.B1Out();
                break;
            case 2:
                textOpciones.B2Out();
                break;
            case 3:
                textOpciones.B3Out();
                break;
        }
    }
}
