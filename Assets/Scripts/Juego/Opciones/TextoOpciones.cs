using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Este script hay que refactorizarlo :S
/// </summary>

public class TextoOpciones : MonoBehaviour
{
    public GameObject[] botones = new GameObject[5];

    public Sprite[] cartas = new Sprite[5];

    public Sprite cartaBlanca;

    protected Opciones opciones;

    protected GameObject[] prefabsOrdenados = new GameObject[5];

    protected TextoExplicativo textos;

    private void Awake()
    {
        opciones = GetComponent<Opciones>();

        EventManager.StartListening("RotacionOpciones", Actualizar);

        textos = Resources.Load<TextoExplicativo>("Textos");   
    }   

    public virtual void Actualizar()
    {
        for (int i = 0; i < 5; i++)
        {
            prefabsOrdenados[i] = opciones.opcionesIniciales[opciones.opcionesDisponibles[i]];
        }
        for (int i = 0; i < 3; i++)
        {
            botones[i].GetComponent<Image>().sprite = cartas[opciones.opcionesDisponibles[i]];
        }
        for (int i = 3; i < 5; i++)
        {
            botones[i].GetComponentInChildren<Text>().text = prefabsOrdenados[i].name;
        }
    }


    public virtual void In(int i)
    {
        botones[i].GetComponent<Image>().sprite = cartaBlanca;
        botones[i].GetComponentInChildren<Text>().text = textos.GetTexto(prefabsOrdenados[i]);
    }
    public void Out(int i)
    {
        botones[i].GetComponent<Image>().sprite = cartas[opciones.opcionesDisponibles[i]];
        botones[i].GetComponentInChildren<Text>().text = "";
    }
}
