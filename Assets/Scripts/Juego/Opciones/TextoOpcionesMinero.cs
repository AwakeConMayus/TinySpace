using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoOpcionesMinero : TextoOpciones
{
    public Sprite[] cartasMejoradas = new Sprite[4];
    public Sprite cartaBlancaMejorada;

    Sprite[] copiaCartas = new Sprite[5];
    Sprite copiaCartaBlanca;

    bool clantamaforo = true;

    public override void Actualizar()
    {
        if (opciones.gameObject.GetComponent<OpcionesMineros>().especial)
        {
            if (clantamaforo)
            {
                for (int i = 0; i < cartas.Length; i++)
                {
                    copiaCartas[i] = cartas[i];
                }
                copiaCartaBlanca = cartaBlanca;
                clantamaforo = false;
            }

            for (int i = 0; i < 4; i++)
            {
                cartas[i] = cartasMejoradas[i];
            }
            cartaBlanca = cartaBlancaMejorada;
        }
        else if (copiaCartas[0] != null)
        {
            for (int i = 0; i < cartas.Length; i++)
            {
                cartas[i] = copiaCartas[i];
            }
            cartaBlanca = copiaCartaBlanca;
        }

        base.Actualizar();
    }

    public override void In(int i)
    {
        if(opciones.opcionesDisponibles[i] == 4)
        {
            if(copiaCartaBlanca == null)
            {
                copiaCartaBlanca = cartaBlanca;
            }

            botones[i].GetComponent<Image>().sprite = copiaCartaBlanca;
        }

        else
        {
            botones[i].GetComponent<Image>().sprite = cartaBlanca;
        }

        botones[i].GetComponentInChildren<Text>().text = textos.GetTexto(prefabsOrdenados[i]);
    }
}
