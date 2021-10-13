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

    

    #region explicaciones
    public void B1In()
    {
        botones[0].GetComponent<Image>().sprite = cartaBlanca;
        botones[0].GetComponentInChildren<Text>().text = textos.GetTexto(prefabsOrdenados[0]);
    }
    public void B1Out()
    {
        botones[0].GetComponent<Image>().sprite = cartas[opciones.opcionesDisponibles[0]];
        botones[0].GetComponentInChildren<Text>().text = "";
    }
    public void B2In()
    {
        botones[1].GetComponent<Image>().sprite = cartaBlanca;
        botones[1].GetComponentInChildren<Text>().text = textos.GetTexto(prefabsOrdenados[1]);
    }
    public void B2Out()
    {
        botones[1].GetComponent<Image>().sprite = cartas[opciones.opcionesDisponibles[1]];
        botones[1].GetComponentInChildren<Text>().text = "";
    }
    public void B3In()
    {
        botones[2].GetComponent<Image>().sprite = cartaBlanca;
        botones[2].GetComponentInChildren<Text>().text = textos.GetTexto(prefabsOrdenados[2]);
    }
    public void B3Out()
    {
        botones[2].GetComponent<Image>().sprite = cartas[opciones.opcionesDisponibles[2]];
        botones[2].GetComponentInChildren<Text>().text = "";
    }
    #endregion
}
