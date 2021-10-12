using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExplicacionHeroe : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextoExplicativo explicaciones;

    [SerializeField]
    GameObject interfazExplicativa;

    private void Start()
    {
        explicaciones = Resources.Load<TextoExplicativo>("Textos");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        interfazExplicativa.SetActive(true);
        interfazExplicativa.GetComponentInChildren<Text>().text = explicaciones.GetTexto(GetComponentInParent<Opciones>().poder);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        interfazExplicativa.SetActive(false);
    }
}
