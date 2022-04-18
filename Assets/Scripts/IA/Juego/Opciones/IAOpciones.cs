using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAOpciones : Opciones
{
    int PuntosOrigen = 0;

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

    public IEnumerator Jugar(Opciones rival, int turno) 
    {
        print("empiezo a pensar: " + faccion);
        PuntosOrigen = PiezaIA.Evaluar(Tablero.instance.mapa, faccion);
        Debug.Log(turno);
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

        bestJugada = new InfoTablero();
        if (jugadasSimples.Contains(turno))
        {
            yield return StartCoroutine(JugadaSimple());
        }
        else if (jugadasCombinada.Contains(turno))
        {
            yield return StartCoroutine( JugadaCombinada(rival));
        }
        else if (poderesSimple.Contains(turno))
        {
            yield return StartCoroutine( PoderSimple(fase));
        }
        else if (poderesCombinado.Contains(turno))
        {
            yield return StartCoroutine( PoderCombinado(rival, fase));
        }
        else if (poderesCombinadoPoder.Contains(turno))
        {
             yield return StartCoroutine( PoderCombinadoPoder(rival, fase));
        }
        EjecutarJugada(bestJugada);
    }

    public abstract List<InfoTablero> JugadaSimpleOpciones();

    public IEnumerator JugadaSimple()
    {
        int bestPuntos = int.MinValue;

        foreach(InfoTablero newTab in JugadaSimpleOpciones())
        {
            IATablero.instance.PrintInfoTablero(newTab);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
            if (puntos > bestPuntos)
            {
                bestPuntos = puntos;
                bestJugada = newTab;
            }
            yield return null;
        }


    }

    InfoTablero bestJugada = new InfoTablero();

    public IEnumerator JugadaCombinada(Opciones enemigo)
    {

        int bestPuntos = int.MinValue;

        foreach (InfoTablero newTab in JugadaSimpleOpciones())
        {
            IATablero.instance.PrintInfoTablero(newTab);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
            if (puntos <= PuntosOrigen) continue;
                
            yield return StartCoroutine( SimulacionEnemiga(enemigo, newTab));
            puntos = worstPuntos;
            if (puntos > bestPuntos)
            {
                bestPuntos = puntos;
                bestJugada = newTab;
            }
            yield return null;
        }       
    }
  

    public IEnumerator JugadaCombinadaPoder(Opciones enemigo, int fase)
    {

        int bestPuntos = int.MinValue;

        foreach (InfoTablero newTab in JugadaSimpleOpciones())
        {
            IATablero.instance.PrintInfoTablero(newTab);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
            if (puntos <= PuntosOrigen) continue;

            puntos = SimulacionPoderEnemigo(enemigo, newTab, fase);
            Debug.Log(puntos + " puntos ia");
            if (puntos > bestPuntos)
            {
                bestPuntos = puntos;
                bestJugada = newTab;
            }
            yield return null;
        }


    }

  



    public List<InfoTablero> PoderSimpleOpciones(int i)        
    {
        List<InfoTablero> jugadas = new List<InfoTablero>();
        foreach(InfoTablero jugada in poder.GetComponent<PoderIA>().Fases[i].Opcionificador(new InfoTablero(Tablero.instance.mapa)))
        {
            jugadas.Add(jugada);
        }
        return jugadas;
    }

    public IEnumerator PoderSimple(int fase)
    {

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
            yield return null;
        }

    }

    public IEnumerator PoderCombinado(Opciones enemigo, int fase)
    {



        
        int bestPuntos = int.MinValue;

        foreach (InfoTablero newTab in PoderSimpleOpciones(fase))
        {
            IATablero.instance.PrintInfoTablero(newTab);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
            //if (puntos <= PuntosOrigen) continue;

            yield return StartCoroutine(SimulacionEnemiga(enemigo, newTab));
            puntos = worstPuntos;

            if (puntos > bestPuntos)
            {
                bestPuntos = puntos;
                bestJugada = newTab;
            }
            yield return null;
        }
    }


    public IEnumerator PoderCombinadoPoder(Opciones enemigo, int fase)
    {



        int bestPuntos = int.MinValue;

        foreach (InfoTablero newTab in PoderSimpleOpciones(fase))
        {
            IATablero.instance.PrintInfoTablero(newTab);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
            //if (puntos <= PuntosOrigen) continue;

            puntos = SimulacionPoderEnemigo(enemigo, newTab, fase);
            if (puntos > bestPuntos)
            {
                bestPuntos = puntos;
                bestJugada = newTab;
            }
            yield return null;
        }

    }


    public void ActualizarTablero(InfoTablero newTab)
    {
        Tablero.instance.PrintInfoTablero(newTab);
    }

    int worstPuntos;
    public IEnumerator SimulacionEnemiga(Opciones enemigo, InfoTablero tabBase)
    {
        worstPuntos = int.MaxValue;


        
        foreach (GameObject pieza in enemigo.opcionesIniciales)
        {
            PiezaIA iaPieza = pieza.GetComponent<PiezaIA>();

            InfoTablero newTab = iaPieza.BestInmediateOpcion(tabBase);
            IATablero.instance.PrintInfoTablero(newTab);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
            worstPuntos = worstPuntos < puntos ? worstPuntos : puntos;
            yield return null;
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
                yield return null;
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
                yield return null;
            }
        }
        
        
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
        Debug.Log(" se dibuja el tablero Tablero");
        Tablero.instance.PrintInfoTablero(newTab);
        EventManager.TriggerEvent("AccionTerminadaConjunta");
    }
}


