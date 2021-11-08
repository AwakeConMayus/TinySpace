using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgujeroNegro : Pieza
{

    protected override void SetClase()
    {
        clase = Clase.astros;
    }
    public override int Puntos()
    {
        return 0;
    }
    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> posibles_lugares = FiltroCasillas.CasillasDeUnTipo(Clase.investigador);
        posibles_lugares = FiltroCasillas.CasillasDeUnJugador(faccion, posibles_lugares);
        posibles_lugares = FiltroCasillas.CasillasAdyacentes(posibles_lugares, true);
        posibles_lugares = FiltroCasillas.CasillasLibres(posibles_lugares);
        return posibles_lugares;
    }

    public static void ActivarAgujeroNegro(Casilla casilla)
    {
        for (int  i = 0;  i < casilla.adyacentes.Length;  i++)
        {
            if (casilla.adyacentes[i])
            {
                casilla.adyacentes[i].pieza = null;
                Atraer_Todo_En_Una_Direccion(casilla.adyacentes[i], i);
            }
        }        
    }

    public static void ActivarAgujerosNegros(List<Casilla> casillas)
    {
        foreach(Casilla c in casillas)
        {
            if (c.GetComponent<AgujeroNegro>())
            {
                ActivarAgujeroNegro(c);
            }
        }
    }



    static void Atraer_Todo_En_Una_Direccion(Casilla c, int direccion)
    {
        if (!c) return;
        if (c.pieza)
        {
            if (c.pieza.clase == Clase.astros) return;
            int aux_reverseDirection;
            if (direccion < 3) aux_reverseDirection = direccion + 3;
            else aux_reverseDirection = direccion - 3;

            c.adyacentes[aux_reverseDirection].pieza = c.pieza;
            c.pieza = null;

        }
        if (c.adyacentes[direccion])
        {
            Atraer_Todo_En_Una_Direccion(c.adyacentes[direccion], direccion);
        }
    }
}
