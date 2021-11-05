using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAOpciones : Opciones
{
    public override void PrepararPreparacion() { }

    public override void Preparacion()
    {
        opcionesDisponibles = new List<int>();
        foreach(PoderIABase fase in poder.GetComponent<PoderIA>().Fases)
        {
            fase.padre = this;
            fase.padre = this;
        }

        


        List<int> disponibles = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            disponibles.Add(i);
        }
        int numero;
        for (int i = 0; i < opcionesIniciales.Length; i++)
        {
            numero = disponibles[Random.Range(0, disponibles.Count)];
            opcionesDisponibles.Add(numero);
            disponibles.Remove(numero);
        }
    }

    public void IARotarOpcion(int i)
    {
        opcionActual = i;
        int aux = opcionesDisponibles[opcionActual];
        opcionesDisponibles.Remove(aux);
        opcionesDisponibles.Add(aux);
    }

    public void Jugar(Opciones rival, int turno) 
    {
        print("Empieza el show");
        List<int> jugadasSimples = new List<int> { 3, 5, 7, 9, 11, 15, 17, 19, 21, 23 };
        List<int> jugadasCombinada = new List<int> { 2, 4, 6, 8, 10, 14, 16, 18, 20, 22 };
        List<int> poderesSimple = new List<int> { 25 };
        List<int> poderesCombinado = new List<int> { 13 };
        List<int> poderesCombinadoPoder = new List<int> { 12, 24 };

        #region Esquema Explicativo
        /*
                0	Nada
	            1	Nada

            2		JugadaCombinada

            3		JugadaSimple
            4		JugadaCombinada

            5		JugadaSimple
            6		JugadaCombinada

            7		JugadaSimple
            8		JugadaCombinada

            9		JugadaSimple
            10		JugadaCombinada

            11		JugadaSimple

	            12	PoderCombinadoPoder
	            13	PoderCombinado

            14		JugadaCombinada

            15		JugadaSimple
            16		JugadaCombinada

            17		JugadaSimple
            18		JugadaCombinada

            19		JugadaSimple
            20		JugadaCombinada

            21		JugadaSimple
            22		JugadaCombinada

            23		JugadaSimple

	            24	PoderCombinadoPoder
	            25	PoderSimple
         */
        #endregion

        int fase = 0;
        if (turno > 15) fase = 1;

        InfoTablero jugada = new InfoTablero();

        if (jugadasSimples.Contains(turno))
        {
            List<InfoTablero> jugadas = JugadaSimple();
            jugada = DecidirJugada(jugadas);
        }
        else if (jugadasCombinada.Contains(turno))
        {
            List<InfoTablero> jugadas = JugadaCombinada(rival);
            jugada = DecidirJugada(jugadas);
        }
        else if (poderesSimple.Contains(turno))
        {
            List<InfoTablero> jugadas = PoderSimple(fase);
            jugada = DecidirJugada(jugadas);
        }
        else if (poderesCombinado.Contains(turno))
        {
            List<InfoTablero> jugadas = PoderCombinado(rival, fase);
            jugada = DecidirJugada(jugadas);
        }
        else if (poderesCombinadoPoder.Contains(turno))
        {
            List<InfoTablero> jugadas = PoderCombinadoPoder(rival, fase);
            jugada = DecidirJugada(jugadas);
        }
        EjecutarJugada(jugada);
    }

    public abstract List<InfoTablero> JugadaSimple();

    public List<InfoTablero> JugadaCombinada(Opciones enemigo)
    {
        List<InfoTablero> jugadas = new List<InfoTablero>();

        foreach(InfoTablero tabBase in JugadaSimple())
        {
            jugadas.Add(SimulacionEnemiga(enemigo, tabBase));

        }

        return jugadas;
    }


    public List<InfoTablero> JugadaCombinadaPoder(Opciones enemigo, int fase)
    {
        List<InfoTablero> jugadas = new List<InfoTablero>();

        foreach (InfoTablero tabBase in JugadaSimple())
        {
            jugadas.Add(SimulacionPoderEnemigo(enemigo, tabBase, fase));
        }

        return jugadas;
    }


    public List<InfoTablero> PoderSimple(int i)        
    {
        Debug.Log("IA PODR");
        List<InfoTablero> jugadas = new List<InfoTablero>();
        foreach(InfoTablero jugada in poder.GetComponent<PoderIA>().Fases[i].Opcionificador(new InfoTablero(Tablero.instance.mapa)))
        {
            jugadas.Add(jugada);
        }
        return jugadas;
    }

    public List<InfoTablero> PoderCombinado(Opciones rival, int fase)
    {
        List<InfoTablero> jugadas = new List<InfoTablero>();

        foreach (InfoTablero tabBase in PoderSimple(fase))
        {
            jugadas.Add(SimulacionEnemiga(rival, tabBase));

        }

        return jugadas;
    }

    public List<InfoTablero> PoderCombinadoPoder(Opciones enemigo, int fase)
    {
        List<InfoTablero> jugadas = new List<InfoTablero>();

        foreach (InfoTablero tabBase in PoderSimple(fase))
        {
            jugadas.Add(SimulacionPoderEnemigo(enemigo, tabBase, fase));            
        }

        return jugadas;
    }

    public void ActualizarTablero(InfoTablero newTab)
    {
        Tablero.instance.PrintInfoTablero(newTab);
    }

    public InfoTablero SimulacionEnemiga(Opciones enemigo, InfoTablero tabBase)
    {
        InfoTablero worstJugada = new InfoTablero();
        int worstPuntos = int.MaxValue;

        foreach (GameObject pieza in enemigo.opcionesIniciales)
        {
            PiezaIA iaPieza = pieza.GetComponent<PiezaIA>();
            foreach(InfoTablero newTab in iaPieza.Opcionificador(tabBase))
            {
                IATablero.instance.PrintInfoTablero(newTab);
                int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
                worstPuntos = worstPuntos < puntos ? worstPuntos : puntos;
                if (worstPuntos == puntos) worstJugada = newTab;
            }
        }

        if (enemigo.gameObject.GetComponent<OpcionesMineros>())
        {
            foreach (GameObject pieza in enemigo.gameObject.GetComponent<OpcionesMineros>().mejoras)
            {
                PiezaIA iaPieza = pieza.GetComponent<PiezaIA>();
                foreach (InfoTablero newTab in iaPieza.Opcionificador(tabBase))
                {
                    IATablero.instance.PrintInfoTablero(newTab);
                    int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
                    worstPuntos = worstPuntos < puntos ? worstPuntos : puntos;
                    if (worstPuntos == puntos) worstJugada = newTab;
                }
            }
        }

        else if (enemigo.gameObject.GetComponent<IAOpcionesMineros>())
        {
            foreach (GameObject pieza in enemigo.gameObject.GetComponent<IAOpcionesMineros>().mejoras)
            {
                PiezaIA iaPieza = pieza.GetComponent<PiezaIA>();
                foreach (InfoTablero newTab in iaPieza.Opcionificador(tabBase))
                {
                    IATablero.instance.PrintInfoTablero(newTab);
                    int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
                    worstPuntos = worstPuntos < puntos ? worstPuntos : puntos;
                    if (worstPuntos == puntos) worstJugada = newTab;
                }
            }
        }

        

        

        return worstJugada;
    }

    public InfoTablero SimulacionPoderEnemigo(Opciones enemigo, InfoTablero tabBase, int fase)
    {
        InfoTablero worstJugada = new InfoTablero();

        int worstPuntos = int.MaxValue;

        PoderIA iaPoder = enemigo.poder.GetComponent<PoderIA>();

        foreach(InfoTablero newTab in iaPoder.Fases[fase].Opcionificador(tabBase))
        {
            IATablero.instance.PrintInfoTablero(newTab);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
            worstPuntos = worstPuntos < puntos ? worstPuntos : puntos;
            if (worstPuntos == puntos) worstJugada = newTab;
        }

        return worstJugada;
    }

    public InfoTablero DecidirJugada(List<InfoTablero> posibilidades)
    {
        int bestPuntos = int.MinValue;
        InfoTablero bestJugada = new InfoTablero();

        foreach(InfoTablero jugada in posibilidades)
        {
            IATablero.instance.PrintInfoTablero(jugada);
            if(PiezaIA.Evaluar(IATablero.instance.mapa,faccion) > bestPuntos)
            {
                bestJugada = jugada;
            }
        }

        return bestJugada;
    }

    

    public virtual void EjecutarJugada(InfoTablero newTab)
    {
        Tablero.instance.PrintInfoTablero(newTab);
    }
}


