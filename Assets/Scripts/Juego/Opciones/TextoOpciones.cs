using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Este script hay que refactorizarlo :S
/// </summary>

public class TextoOpciones : MonoBehaviour
{
    public Text b1, b2, b3;
    public Text c1, c2;
    Button buton1, buton2, buton3;
    [HideInInspector]
    public Opciones opciones;

    GameObject g1, g2, g3;

    TextoExplicativo textos;

    private void Awake()
    {
        opciones = GetComponent<Opciones>();
        EventManager.StartListening("RotacionOpciones", Actualizar);

        textos = Resources.Load<TextoExplicativo>("Textos");

        buton1 = b1.GetComponentInParent<Button>();
        buton2 = b2.GetComponentInParent<Button>();
        buton3 = b3.GetComponentInParent<Button>();

    }

    public void Actualizar()
    {
        if (opciones.opcionesDisponibles.Count == 0) return;

        g1 = opciones.opcionesIniciales[opciones.opcionesDisponibles[0]];
        g2 = opciones.opcionesIniciales[opciones.opcionesDisponibles[1]];
        g3 = opciones.opcionesIniciales[opciones.opcionesDisponibles[2]];

        b1.text = g1.name;
        b2.text = g2.name;
        b3.text = g3.name;

        c1.text = opciones.opcionesIniciales[opciones.opcionesEnfriamiento[0]].name;
        c2.text = opciones.opcionesIniciales[opciones.opcionesEnfriamiento[1]].name;


        // Esto se borrará cuando sustituyamos los placeholders
        buton1 = b1.GetComponentInParent<Button>();
        buton2 = b2.GetComponentInParent<Button>();
        buton3 = b3.GetComponentInParent<Button>();

        changeColor(buton1);
        changeColor(buton2);
        changeColor(buton3);   
    }

    /// <summary>
    /// Esta funcion hay que eliminarla y sustituirla por una mejor cuando tengamos sprites y botones personalizados
    /// </summary>
    private void changeColor(Button b)
    {
        switch (b.GetComponentInChildren<Text>().text)
        {
            case "Estratega Mineros":     b.image.color = Color.magenta; break;
            case "Estratega Planetarios": b.image.color = Color.magenta; break;

            case "Investigador Mineros":     b.image.color = Color.green; break;
            case "Investigador Planetarios": b.image.color = Color.green; break;

            case "Combate Mineros":                   b.image.color = Color.red; break;
            case "Combate Planetarios Colonizadores": b.image.color = Color.red; break;

            case "Explorador Mineros":     b.image.color = Color.cyan; break;
            case "Explorador Planetarios": b.image.color = Color.cyan; break;

            case "Comodin Mineros":     b.image.color = Color.grey; break;
            case "Planeta Planetarios": b.image.color = Color.grey; break;
        }
    }

    #region explicaciones
    public void B1In()
    {
        b1.text = textos.GetTexto(g1);
    }
    public void B1Out()
    {
        b1.text = g1.name;
    }
    public void B2In()
    {
        b2.text = textos.GetTexto(g2);
    }
    public void B2Out()
    {
        b2.text = g2.name;
    }
    public void B3In()
    {
        b3.text = textos.GetTexto(g3);
    }
    public void B3Out()
    {
        b3.text = g3.name;
    }
    #endregion
}
