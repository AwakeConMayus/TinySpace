using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FiltroCasillas 
{
    public static List<Casilla> CasillasNoAstro(List<Casilla> listaBase = null)
    {
        if (listaBase == null) listaBase = Tablero.instance.mapa;
        List<Casilla> resultado = new List<Casilla>();
        foreach(Casilla c in listaBase)
        {
            if (!c.pieza || !c.pieza.astro) resultado.Add(c);            
        }
        return resultado;
    }

    public static List<Casilla> CasillasConPiezaAdyacente(List<Casilla> listaBase = null)
    {
        if (listaBase == null) listaBase = Tablero.instance.mapa;
        List<Casilla> resultado = new List<Casilla>();
        foreach(Casilla c in listaBase)
        {
            foreach(Casilla cc in c.adyacentes)
            {
                if(c && c.pieza)
                {
                    resultado.Add(c);
                    break;
                }
            }
        }
        return resultado;
    }

    //Busca todas las casillas que esten en el rango desde X casilla
    public static List<Casilla> CasillasEnRango(int rango, Casilla origen, bool incluirOrigen = true, List<Casilla> listaBase = null)
    {
        if (listaBase == null) listaBase = Tablero.instance.mapa;

        List<Casilla> resultado = new List<Casilla>();
        List<Casilla> nuevas = new List<Casilla>();
        List<Casilla> proximas = new List<Casilla>();

        nuevas.Add(origen);

        for (int i = 0; i < rango; i++)
        {
            foreach (Casilla inexplorada in nuevas)
            {
                foreach (Casilla adyacente in inexplorada.adyacentes)
                {
                    if (!resultado.Contains(adyacente) && !nuevas.Contains(adyacente) && !proximas.Contains(adyacente))
                    {
                        if (listaBase.Contains(adyacente)) proximas.Add(adyacente);
                    }
                }
            }
            foreach (Casilla explorada in nuevas)
            {
                resultado.Add(explorada);
            }
            nuevas = new List<Casilla>(proximas);
            proximas = new List<Casilla>();
        }
        foreach (Casilla explorada in nuevas)
        {
            resultado.Add(explorada);
        }
        if (!incluirOrigen) resultado.Remove(origen);
        return resultado;
    }

    
    //Busca casillas sin ocupar
    public static List<Casilla> CasillasLibres(List<Casilla> listaBase = null)
    {
        if (listaBase == null) listaBase = Tablero.instance.mapa;
        List<Casilla> resultado = new List<Casilla>();
        foreach (Casilla candidata in listaBase)
        {
            if (!candidata.pieza) resultado.Add(candidata);
        }
        return resultado;
    }

    public static List<Casilla> CasillasSinMeteorito(List<Casilla> listaBase = null)
    {
        if (listaBase == null) listaBase = Tablero.instance.mapa;
        List<Casilla> resultado = new List<Casilla>();
        foreach (Casilla candidata in listaBase)
        {
            if (!candidata.meteorito) resultado.Add(candidata);
        }
        return resultado;
    }

    public static List<Casilla> CasillasDeUnTipo(Clase clase, List<Casilla> listaBase = null)
    {
        if (listaBase == null) listaBase = Tablero.instance.mapa;
        List<Casilla> resultado = new List<Casilla>();

        foreach (Casilla candidata in listaBase)
        {
            if (candidata.pieza && clase == candidata.pieza.clase)
            {
                resultado.Add(candidata);
            }
        }
        return resultado;
    }

    public static List<Casilla> CasillasPlaneta(List<Casilla> listaBase = null)
    {
        if (listaBase == null) listaBase = Tablero.instance.mapa;
        List<Casilla> resultado = new List<Casilla>();

        foreach(Casilla candidata in listaBase)
        {
            if(candidata.pieza && candidata.pieza.GetComponent<Planetas>())
            {
                resultado.Add(candidata);
            }
        }
        return resultado;
    }
    public static List<Casilla> CasillasDeUnTipo(List<Clase> clases = null, List<Casilla> listaBase = null)
    {
        if (clases == null) return new List<Casilla>();
        if (listaBase == null) listaBase = Tablero.instance.mapa;
        List<Casilla> resultado = new List<Casilla>();

        foreach (Casilla candidata in listaBase)
        {
            if (candidata.pieza && clases.Contains(candidata.pieza.clase))
            {
                resultado.Add(candidata);
            }
        }
        return resultado;
    }


    public static List<Casilla> CasillasDeUnJugador(Faccion jugador, List<Casilla> listaBase = null)
    {
        if (listaBase == null) listaBase = Tablero.instance.mapa;
        List<Casilla> resultado = new List<Casilla>();
        foreach (Casilla candidata in listaBase)
        {
            if (candidata.pieza && candidata.pieza.faccion == jugador) resultado.Add(candidata);
        }
        return resultado;
    }
    public static List<Casilla> CasillasDeOtroJugador(Faccion jugador, List<Casilla> listaBase = null)
    {
        if (listaBase == null) listaBase = Tablero.instance.mapa;
        List<Casilla> resultado = new List<Casilla>();
        foreach (Casilla candidata in listaBase)
        {
            if (candidata.pieza && candidata.pieza.faccion != jugador) resultado.Add(candidata);
        }
        return resultado;
    }

    public static List<Casilla> CasillasAdyacentes(List<Casilla> listaBase, bool excluirOriginales)
    {
        List<Casilla> resultado = new List<Casilla>();

        foreach (Casilla candidata in listaBase)
        {
            foreach (Casilla adyacente in candidata.adyacentes)
            {
                if (adyacente && !resultado.Contains(adyacente)) resultado.Add(adyacente);

            }
        }
        if (excluirOriginales)
        {
            foreach (Casilla original in listaBase)
            {

                resultado.Remove(original);
            }
        }
        return resultado;
    }

    public static List<Casilla> CasillasAdyacentes(Casilla casillaBase, bool excluirOriginal)
    {
        List<Casilla> resultado = new List<Casilla>();
        List<Casilla> listaBase = new List<Casilla>();

        listaBase.Add(casillaBase);

        foreach (Casilla candidata in listaBase)
        {
            foreach (Casilla adyacente in candidata.adyacentes)
            {
                if (adyacente && !resultado.Contains(adyacente)) resultado.Add(adyacente);

            }
        }
        if (excluirOriginal)
        {
            foreach (Casilla original in listaBase)
            {

                resultado.Remove(original);
            }
        }
        return resultado;
    }

    public static List<Casilla> RestaLista (List<Casilla> original, List<Casilla> resta)
    {
        List<Casilla> result = original;

        foreach(Casilla c in resta)
        {
            if (result.Contains(c)) result.Remove(c);
        }

        return result;
    }

    public static List<Casilla> Suma(List<Casilla> original, List<Casilla> suma)
    {
        List<Casilla> result = original;

        foreach (Casilla c in suma)
        {
            if (!result.Contains(c)) result.Add(c);
        }

        return result;
    }

    public static List<Casilla> Interseccion(List<Casilla> a, List<Casilla> b)
    {
        List<Casilla> result = new List<Casilla>();

        foreach(Casilla c in a)
        {
            if (b.Contains(c)) result.Add(c); 
        }
        return result;
    }

    public static List<Casilla> EliminarBordes(List<Casilla> listaBase)
    {
        List<Casilla> result = new List<Casilla>();

        foreach(Casilla c in listaBase)
        {
            bool valida = true;
            for (int i = 0; i < c.adyacentes.Length; i++)
            {
                if (c.adyacentes[i] == null) valida = false;
            }
            if (valida) result.Add(c);
        }

        return result;
    }
}
