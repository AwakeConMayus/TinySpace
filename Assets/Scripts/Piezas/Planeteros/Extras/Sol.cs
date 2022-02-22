using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sol : Planetas
{
    [SerializeField] int[] puntos_6Casillas;
    [SerializeField] int[] puntos_5Casillas;
    [SerializeField] int[] puntos_4Casillas;
    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillas = FiltroCasillas.CasillasDeUnJugador(faccion, referencia);
        casillas = FiltroCasillas.CasillasDeUnTipo(Clase.planeta, casillas);

        return casillas;
    }


    public override int Puntos()
    {
        int puntos = base.Puntos();
        int nPiezas = 0;
        int nCasillas = 0;

        foreach (Casilla c in casilla.adyacentes)
        {
            if (c)
            {
                ++nCasillas;
                if (c.pieza)
                {
                    ++nPiezas;
                }
            }
        }

        switch (nCasillas)
        {
            case 6:
                puntos = puntos_6Casillas[nPiezas];
                break;
            case 5:
                puntos = puntos_5Casillas[nPiezas];
                break;
            case 4:
                puntos = puntos_4Casillas[nCasillas];
                break;
            default:
                Debug.LogError("En el sol no se estan contando bien las casillas adyacentes");
                break;

        }


        return puntos;
    }
}
