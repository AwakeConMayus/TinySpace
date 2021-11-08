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
        List<int> jugadasSimples = new List<int> { 2, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23 };
        List<int> jugadasCombinada = new List<int> {4, 6, 8, 10, 14, 16, 18, 20, 22 };
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
            jugada = JugadaSimple();
        }
        else if (jugadasCombinada.Contains(turno))
        {
            jugada = JugadaCombinada(rival);
        }
        else if (poderesSimple.Contains(turno))
        {
            jugada = PoderSimple(fase);
        }
        else if (poderesCombinado.Contains(turno))
        {
            jugada = PoderCombinado(rival, fase);
        }
        else if (poderesCombinadoPoder.Contains(turno))
        {
            jugada = PoderCombinadoPoder(rival, fase);
        }
        EjecutarJugada(jugada);
    }

    public abstract List<InfoTablero> JugadaSimpleOpciones();

    public InfoTablero JugadaSimple()
    {
        InfoTablero bestJugada = new InfoTablero();
        int bestPuntos = int.MinValue;

        foreach(InfoTablero newTab in JugadaSimpleOpciones())
        {
            IATablero.instance.PrintInfoTablero(newTab);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);

            if(puntos > bestPuntos)
            {
                bestPuntos = puntos;
                bestJugada = newTab;
            }
        }

        return bestJugada;
    }


    public InfoTablero JugadaCombinada(Opciones enemigo)
    {
        InfoTablero bestJugada = new InfoTablero();
        int bestPuntos = int.MinValue;

        foreach (InfoTablero newTab in JugadaSimpleOpciones())
        {
            int puntos = SimulacionEnemiga(enemigo, newTab);

            if (puntos > bestPuntos)
            {
                bestPuntos = puntos;
                bestJugada = newTab;
            }

        }       


        return bestJugada;
    }


    public InfoTablero JugadaCombinadaPoder(Opciones enemigo, int fase)
    {
        InfoTablero bestJugada = new InfoTablero();
        int bestPuntos = int.MinValue;

        foreach (InfoTablero newTab in JugadaSimpleOpciones())
        {
            int puntos = SimulacionPoderEnemigo(enemigo, newTab, fase);

            if (puntos > bestPuntos)
            {
                bestPuntos = puntos;
                bestJugada = newTab;
            }

        }


        return bestJugada;
    }


    public List<InfoTablero> PoderSimpleOpciones(int i)        
    {
        Debug.Log("IA PODR");
        List<InfoTablero> jugadas = new List<InfoTablero>();
        foreach(InfoTablero jugada in poder.GetComponent<PoderIA>().Fases[i].Opcionificador(new InfoTablero(Tablero.instance.mapa)))
        {
            jugadas.Add(jugada);
        }
        return jugadas;
    }

    public InfoTablero PoderSimple(int fase)
    {
        InfoTablero bestJugada = new InfoTablero();
        int bestPuntos = int.MinValue;

        foreach (InfoTablero newTab in PoderSimpleOpciones(fase))
        {
            IATablero.instance.PrintInfoTablero(newTab);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);

            if (puntos > bestPuntos)
            {
                bestPuntos = puntos;
                bestJugada = newTab;
            }
        }

        return bestJugada;
    }

    public InfoTablero PoderCombinado(Opciones enemigo, int fase)
    {
        InfoTablero bestJugada = new InfoTablero();
        int bestPuntos = int.MinValue;

        foreach (InfoTablero newTab in PoderSimpleOpciones(fase))
        {
            int puntos = SimulacionEnemiga(enemigo, newTab);

            if (puntos > bestPuntos)
            {
                bestPuntos = puntos;
                bestJugada = newTab;
            }

        }


        return bestJugada;
    }

    public InfoTablero PoderCombinadoPoder(Opciones enemigo, int fase)
    {
        InfoTablero bestJugada = new InfoTablero();
        int bestPuntos = int.MinValue;

        foreach (InfoTablero newTab in PoderSimpleOpciones(fase))
        {
            int puntos = SimulacionPoderEnemigo(enemigo, newTab, fase);

            if (puntos > bestPuntos)
            {
                bestPuntos = puntos;
                bestJugada = newTab;
            }

        }


        return bestJugada;
    }

    public void ActualizarTablero(InfoTablero newTab)
    {
        Tablero.instance.PrintInfoTablero(newTab);
    }

    public int SimulacionEnemiga(Opciones enemigo, InfoTablero tabBase)
    {
        int worstPuntos = int.MaxValue;

        foreach (GameObject pieza in enemigo.opcionesIniciales)
        {
            PiezaIA iaPieza = pieza.GetComponent<PiezaIA>();

            InfoTablero newTab = iaPieza.BestInmediateOpcion(tabBase);
            IATablero.instance.PrintInfoTablero(newTab);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
            worstPuntos = worstPuntos < puntos ? worstPuntos : puntos;
        }

        if (enemigo.gameObject.GetComponent<OpcionesMineros>() && enemigo.gameObject.GetComponent<OpcionesMineros>().mineral >= 3)
        {
            foreach (GameObject pieza in enemigo.gameObject.GetComponent<OpcionesMineros>().mejoras)
            {
                PiezaIA iaPieza = pieza.GetComponent<PiezaIA>();

                InfoTablero newTab = iaPieza.BestInmediateOpcion(tabBase);
                IATablero.instance.PrintInfoTablero(newTab);
                int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
                worstPuntos = worstPuntos < puntos ? worstPuntos : puntos;
            }
        }

        else if (enemigo.gameObject.GetComponent<IAOpcionesMineros>() && enemigo.gameObject.GetComponent<IAOpcionesMineros>().mineral >= 3)
        {
            foreach (GameObject pieza in enemigo.gameObject.GetComponent<IAOpcionesMineros>().mejoras)
            {
                PiezaIA iaPieza = pieza.GetComponent<PiezaIA>();

                InfoTablero newTab = iaPieza.BestInmediateOpcion(tabBase);
                IATablero.instance.PrintInfoTablero(newTab);
                int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
                worstPuntos = worstPuntos < puntos ? worstPuntos : puntos;
            }
        }

        

        

        return worstPuntos;
    }

    public int SimulacionPoderEnemigo(Opciones enemigo, InfoTablero tabBase, int fase)
    {
        int worstPuntos = int.MaxValue;

        PoderIA iaPoder = enemigo.poder.GetComponent<PoderIA>();

        PiezaIA iaPieza = iaPoder.Fases[fase];


        InfoTablero newTab = iaPieza.BestInmediateOpcion(tabBase);
        IATablero.instance.PrintInfoTablero(newTab);
        int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
        worstPuntos = worstPuntos < puntos ? worstPuntos : puntos;

        return worstPuntos;
    }

   

    

    public virtual void EjecutarJugada(InfoTablero newTab)
    {
        Tablero.instance.PrintInfoTablero(newTab);
    }
}


