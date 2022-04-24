using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAOpciones : Opciones
{
    int PuntosOrigen = 0;

    public bool mejorIA;

    

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
        if (mejorIA) yield return JugarMejor(rival, turno);
        else yield return JugarPeor(rival, turno);
    }

    public IEnumerator JugarPeor(Opciones rival, int turno) 
    {
        print("empiezo a pensar: " + faccion);
        PuntosOrigen = PiezaIA.Evaluar(Tablero.instance.mapa, faccion);
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
            yield return StartCoroutine( PoderSimple(turno));
        }
        else if (poderesCombinado.Contains(turno))
        {
            yield return StartCoroutine( PoderCombinado(rival, turno));
        }
        else if (poderesCombinadoPoder.Contains(turno))
        {
             yield return StartCoroutine( PoderCombinadoPoder(rival, turno));
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

    public List<InfoTablero> PoderSimpleOpciones(int turno)        
    {
        List<InfoTablero> jugadas = new List<InfoTablero>();
        int fases = 0;
        if (turno > 15) fases = 1;
        foreach (InfoTablero jugada in mypoder.Fases[fases].Opcionificador(new InfoTablero(Tablero.instance.mapa)))
        {
            jugadas.Add(jugada);
        }
        return jugadas;
    }

    public IEnumerator PoderSimple(int turno)
    {

        int bestPuntos = int.MinValue;

        foreach (InfoTablero newTab in PoderSimpleOpciones(turno))
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

    public IEnumerator PoderCombinado(Opciones enemigo, int turno)
    {



        
        int bestPuntos = int.MinValue;

        foreach (InfoTablero newTab in PoderSimpleOpciones(turno))
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


    public IEnumerator PoderCombinadoPoder(Opciones enemigo, int turno)
    {



        int bestPuntos = int.MinValue;

        foreach (InfoTablero newTab in PoderSimpleOpciones(turno))
        {
            IATablero.instance.PrintInfoTablero(newTab);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
            //if (puntos <= PuntosOrigen) continue;

            puntos = SimulacionPoderEnemigo(enemigo, newTab, turno);
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


    List<int> turnosPoder = new List<int> { 12, 13, 24, 25 };
    int jugadasValoradas = int.MaxValue;

    public IEnumerator JugarMejor(Opciones rival, int turno)
    {
        List<InfoTablero> posibilidades;
        if (!turnosPoder.Contains(turno)) posibilidades = JugadaSimpleOpciones();
        else posibilidades = PoderSimpleOpciones(turno);
        foreach (InfoTablero it in posibilidades) it.SetFaccion(faccion);
        if (posibilidades.Count > 0) posibilidades.Sort(posibilidades[0]);
        List<InfoTablero> posibilidadesReales = new List<InfoTablero>();
        if (posibilidades.Count > jugadasValoradas)
        {
            for (int i = 0; i < jugadasValoradas; i++)
            {
                posibilidadesReales.Add(posibilidades[i]);
            }
        }
        else posibilidadesReales = posibilidades;
        

        List<int> valoracionesPosibilidades = new List<int>();
        int[] ponderacion;
        ponderaciones.TryGetValue(turno, out ponderacion);
        yield return null;

        List<InfoTablero> posibilidadesAnalizadas = new List<InfoTablero>();

        

        foreach(InfoTablero it in posibilidadesReales)
        {
            InfoTablero posibilidad = it;

            foreach(int i in ponderacion)
            {
                switch (i)
                {
                    case 1:
                        posibilidad = BestRespuesta(posibilidad);
                        break;
                    case 2:
                        posibilidad = rival.BestRespuesta(posibilidad);
                        break;
                    case 3:
                        posibilidad = BestRespuestaPoder(posibilidad,turno);
                        break;
                    case 4:
                        posibilidad = rival.BestRespuestaPoder(posibilidad,turno);
                        break;
                }
                yield return null;
            }

            posibilidadesAnalizadas.Add(posibilidad);
            yield return null;
        }

        int best = 0;
        int bestValue = int.MinValue;
        int bestValueActual = int.MinValue;

        for (int i = 0; i < posibilidadesAnalizadas.Count; i++)
        {
            int puntos = PiezaIA.Evaluar(posibilidadesAnalizadas[i], faccion);
            int puntosActuales = PiezaIA.Evaluar(posibilidadesReales[i], faccion);
            if (puntos > bestValue || (puntos == bestValue && puntosActuales > bestValueActual))
            {
                bestValue = puntos;
                bestValueActual = puntosActuales;
                best = i;
            }
        }

        if (posibilidadesReales.Count > 0) EjecutarJugada(posibilidadesReales[best]);
        else NoEjecutarJugada();
    }

    

    public virtual void EjecutarJugada(InfoTablero newTab)
    {
        Debug.Log("Se dibuja el tablero Tablero");
        Tablero.instance.PrintInfoTablero(newTab);
        EventManager.TriggerEvent("AccionTerminadaConjunta");
    }

    public virtual void NoEjecutarJugada()
    {
        Debug.Log("No se dibuja el tablero Tablero");
        EventManager.TriggerEvent("AccionTerminadaConjunta");
    }

    Dictionary<int, int[]> ponderaciones = new Dictionary<int, int[]>
    {
        {2,new int[]{2,1,4,3} },
        {3,new int[]{1,2,3,4} },
        {4,new int[]{2,1,3,4} },
        {5,new int[]{1,2,4,3} },
        {6,new int[]{2,1,4,3} },
        {7,new int[]{1,2,3,4} },
        {8,new int[]{2,1,3,4} },
        {9,new int[]{1,2,4,3} },
        {10,new int[]{2,4,3,0} },
        {11,new int[]{3,4,0,0} },
        {12,new int[]{4,1,0,0} },
        {13,new int[]{2,1,0,0} },
        {14,new int[]{2,1,4,3} },
        {15,new int[]{1,2,3,4} },
        {16,new int[]{2,1,3,4} },
        {17,new int[]{1,2,4,3} },
        {18,new int[]{2,1,4,3} },
        {19,new int[]{1,2,3,4} },
        {20,new int[]{2,1,3,4} },
        {21,new int[]{1,2,4,3} },
        {22,new int[]{2,4,3,0} },
        {23,new int[]{3,4,0,0} },
        {24,new int[]{4,0,0,0} },
        {25,new int[]{0,0,0,0} }
    };
}


