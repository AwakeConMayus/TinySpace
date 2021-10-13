using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoOpcionesMinero : TextoOpciones
{
    public Sprite[] cartasMejoradas = new Sprite[4];
    public Sprite cartaBlancaMejorada;

    Sprite[] copiaCartas = new Sprite[5];
    Sprite copiaCartaBlanca;

   

    public override void Actualizar()
    {
        if (opciones.gameObject.GetComponent<OpcionesMineros>().especial)
        {
            for (int i = 0; i < cartas.Length; i++)
            {
                copiaCartas[i] = cartas[i];
            }
            copiaCartaBlanca = cartaBlanca;

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
}
