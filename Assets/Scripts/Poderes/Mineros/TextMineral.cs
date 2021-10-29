using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextMineral : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    OpcionesMineros poder;
    [SerializeField]
    OpcionesRivalMineros rival_opciones;

    [SerializeField]
    GameObject interfazExplicativa;

    TextoExplicativo explicaciones;
    Text texto;

    private void Awake()
    {
        texto = GetComponent<Text>();
    }

    private void Start()
    {
        ActualizarTextoMineral();
        EventManager.StartListening("CambioMineral", ActualizarTextoMineral);
        explicaciones = Resources.Load<TextoExplicativo>("Textos");
    }

    public void ActualizarTextoMineral()
    {
        Debug.Log(!rival_opciones);
        if (rival_opciones) texto.text = rival_opciones.mineral.ToString();
        else texto.text = poder.mineral.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!interfazExplicativa) return;
        interfazExplicativa.SetActive(true);
       
        interfazExplicativa.GetComponentInChildren<Text>().text = explicaciones.GetTexto(this.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!interfazExplicativa) return;
        interfazExplicativa.SetActive(false);
    }
}
