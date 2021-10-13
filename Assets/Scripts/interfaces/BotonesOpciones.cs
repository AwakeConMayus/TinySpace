using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BotonesOpciones : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextoOpciones textOpciones;
    public int index;

    
    private void Start()
    {
        textOpciones = GetComponentInParent<TextoOpciones>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textOpciones.In(index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textOpciones.Out(index);
    }
}
