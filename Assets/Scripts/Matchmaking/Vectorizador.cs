using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vectorizador
{
    public static List<int[]> Vectorizar(List<Casilla> tablero, Opciones player1, Opciones player2)
    {
        List<int[]> vectores = new List<int[]>();

        switch (player1.faccion)
        {
            case Faccion.none:
                break;
            case Faccion.minero:
                vectores.Add(VectorMineros(tablero, player1, true));
                break;
            case Faccion.oyente:
                vectores.Add(VectorColonos(tablero, player1, true));
                break;
            default:
                break;
        }

        switch (player2.faccion)
        {
            case Faccion.none:
                break;
            case Faccion.minero:
                vectores.Add(VectorMineros(tablero, player2, false));
                break;
            case Faccion.oyente:
                vectores.Add(VectorColonos(tablero, player2, false));
                break;
            default:
                break;
        }

        return vectores;
    }

    static int[] VectorColonos(List<Casilla> tablero, Opciones player, bool initial)
    {
        int[] vector = new int[5];

        //inicial
        if (initial) vector[0] = 1;


        //Nucleo Planetas C o I
        List<Casilla> casillasPlanetas = new List<Casilla>();
        foreach (Casilla casilla in tablero) if (casilla.pieza.clase == Clase.planeta) casillasPlanetas.Add(casilla);

        List<Casilla> planetasNucleo = new List<Casilla>();
        foreach(Casilla casilla in casillasPlanetas)
        {
            List<Casilla> casillas2Dist = new List<Casilla>();
            casillas2Dist = FiltroCasillas.CasillasAdyacentes(casilla, false);
            casillas2Dist = FiltroCasillas.CasillasAdyacentes(casillas2Dist, false);
            casillas2Dist.Remove(casilla);

            foreach(Casilla c in casillas2Dist)
            {
                if(c.pieza.clase == Clase.planeta)
                {
                    planetasNucleo.Add(casilla);
                    planetasNucleo.Add(c);
                    break;
                }
            }

            if (planetasNucleo.Count != 0) break;
        }
        List<Casilla> lista1 = FiltroCasillas.CasillasAdyacentes(planetasNucleo[0],true);
        List<Casilla> lista2 = FiltroCasillas.CasillasAdyacentes(planetasNucleo[1], true);
        int coincidencias = 0;
        foreach(Casilla c in lista1)
        {
            if (lista2.Contains(c)) ++coincidencias;
        }

        if (coincidencias == 1) vector[1] = 1;

        //Distancia 3 planeta
        Casilla planetaAislado = null;
        foreach (Casilla casilla in casillasPlanetas) if (!planetasNucleo.Contains(casilla)) planetaAislado = casilla;

        int radio = 0;
        List<Casilla> areabsuqueda = new List<Casilla>();
        areabsuqueda.Add(planetaAislado);
        bool buscando = true;
        do
        {
            ++radio;
            areabsuqueda = FiltroCasillas.CasillasAdyacentes(areabsuqueda, false);
            foreach(Casilla c in areabsuqueda)
            {
                if(c.pieza.clase == Clase.planeta)
                {
                    buscando = false;
                    break;
                }
            }
        } while (buscando);

        if (radio >= 5) vector[2] = 2;
        else if (radio >= 3) vector[2] = 1;

        //Rotacion Mejorada
        int piezaMejorada = 0;
        for (int i = 0; i < player.opcionesIniciales.Length; i++)        
        {
            if (new List<string>(player.opcionesIniciales[i].name.Split(' ')).Contains("+"))
            {
                piezaMejorada = i;
            }
        }

        if (player.opcionesDisponibles[4] == piezaMejorada) vector[3] = 2;
        else if (player.opcionesDisponibles[3] == piezaMejorada) vector[3] = 1;

        //Handicap Heroe
        if (player.poder.GetComponent<PoderColono>())
        {
            bool sinNavesBuenas = true;
            for (int i = 0; i < 3; i++)
            {
                if(player.opcionesDisponibles[i] == 0 || player.opcionesDisponibles[i] == 1)
                {
                    sinNavesBuenas = false;
                    break;
                }
            }
            if (sinNavesBuenas) vector[4] = 1;
        }
        else if (player.poder.GetComponent<PoderAstrofisico>())
        {
            foreach(Casilla casilla in tablero)
            {
                if(casilla.pieza.clase == Clase.investigador)
                    foreach(Casilla c in FiltroCasillas.CasillasAdyacentes(casilla, true))
                    {
                        if(c.pieza.clase == Clase.investigador)
                        {
                            vector[4] = 1;
                        }
                    }
            }
        }
        else if (player.poder.GetComponent<PoderLunatico>())
        {
            foreach(Casilla casilla in casillasPlanetas)
            {
                int lunas = 0;
                foreach(Casilla c in FiltroCasillas.CasillasAdyacentes(casilla, true))
                {
                    if (c.pieza.clase == Clase.luna) ++lunas;
                }
                if (lunas > 1) vector[4] = 1;
            }
        }

        return vector;
    }

    static int[] VectorMineros(List<Casilla> tablero, Opciones player, bool initial)
    {
        int[] vector = new int[3];

        //inicial
        if (initial) vector[0] = 1;

        //meteoritos
        List<List<Casilla>> gruposMeteoritos = new List<List<Casilla>>();
        List<Casilla> meteoritos = new List<Casilla>();

        foreach(Casilla casilla in tablero)
        {
            if(casilla.meteorito && !meteoritos.Contains(casilla))
            {
                List<Casilla> nuevoGrupo = new List<Casilla>();
                nuevoGrupo.Add(casilla);
                int size = 1;
                int oldSize = 0;
                while(oldSize != size)
                {
                    oldSize = size;
                    foreach(Casilla c in FiltroCasillas.CasillasAdyacentes(nuevoGrupo, true))
                    {
                        if (c.meteorito)
                        {
                            nuevoGrupo.Add(c);
                        }
                    }
                    size = nuevoGrupo.Count;
                }
                gruposMeteoritos.Add(nuevoGrupo);
            }
        }

        int mayorGrupo = 0;
        foreach(List<Casilla> grupo in gruposMeteoritos)
        {
            mayorGrupo = Mathf.Max(mayorGrupo, grupo.Count);
        }

        if (mayorGrupo >= 5) vector[1] = 2;
        else if (mayorGrupo >= 3) vector[1] = 1;

        //Mano inicial
        bool labInicial = false, estInicial = false;

        for (int i = 0; i < 3; i++)
        {
            int opcion = player.opcionesDisponibles[i];
            if (opcion == 2) labInicial = true;
            else if (opcion == 3) estInicial = true;
        }

        if (labInicial && estInicial) vector[2] = 1;



        return vector;
    }
}
