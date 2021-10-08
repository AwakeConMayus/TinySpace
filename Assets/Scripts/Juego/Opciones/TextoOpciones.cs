using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoOpciones : MonoBehaviour
{
    public Text b1, b2, b3;
    public Text c1, c2;
    public Button bColor1, bColor2, bColor3;
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


        // Esto se borrará cuando sustituyamos los placeholders
        bColor1 = b1.GetComponentInParent<Button>();
        bColor2 = b2.GetComponentInParent<Button>();
        bColor3 = b3.GetComponentInParent<Button>();

        changeColor(bColor1);
        changeColor(bColor2);
        changeColor(bColor3);   
    }

    //* Función totalmente debug, cambia los colores de los botones solo para que, mientras haya botones placeholders,
    //* sea más sencillo para la gente diferenciar que carta es cual. Los nombres que comprueba son los del Prefab (nombradlos bien cabrones!)
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
}
