using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoderLunatico : PoderPlanetas
{
    public GameObject luna;

    bool setLuna = false;
    int numeroLunasPorFase = 1;
    int lunasPuestas = 0;

    private void Awake()
    {
        EventManager.StartListening("ClickCasilla", PlaceLuna);        
    }

    public override void InitialAction(bool sin_pasar_turno = false, int[] vector = null)
    {
        base.InitialAction(true, vector);
        List<Casilla> planetas = FiltroCasillas.CasillasDeUnJugador(faccion);

        if (vector == null) // si no existe vector hacemos lo de siempre
        {


            foreach (Casilla c in planetas)
            {
                Debug.Log("casilla del tablero " + Tablero.instance.mapa.IndexOf(c));
                List<Casilla> posibles = new List<Casilla>();
                foreach (Casilla cc in c.adyacentes)
                {
                    if (cc) posibles.Add(cc);
                }
                posibles = FiltroCasillas.CasillasSinMeteorito(FiltroCasillas.CasillasLibres(posibles));
                int rnd = Random.Range(0, posibles.Count);
                GameObject thisPieza;
                if (PhotonNetwork.InRoom)
                {
                    thisPieza = PhotonNetwork.Instantiate(luna.name, posibles[rnd].transform.position, Quaternion.identity);
                }
                else
                {
                    thisPieza = Instantiate(luna, posibles[rnd].transform.position, Quaternion.identity);
                }
                thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                thisPieza.GetComponent<Pieza>().Colocar(posibles[rnd]);
                posibles[rnd].pieza = thisPieza.GetComponent<Pieza>();
            }
        }
        else
        {
            List<Casilla> posibles = new List<Casilla>();
            if (vector[4] == 1) // cuando el herore tiene handicaop
            {
                for (int i = 0; i < planetas.Count; ++i) 
                {
                    int memoria = 0;//esto evita que pongamos dos lunas en el mismo planeta
                                    //si la intersecion es entre 1 y 2 o 1 y 3 no colocamos en el primer 
                                    //si es entre 2 y 3 es menos uno y asi no colocamos en el tercero 

                    if (i == 0) // nos basta con que la primera luna sea contigua
                    {
                        List<Casilla> planeta1 = new List<Casilla>();
                        List<Casilla> planeta2 = new List<Casilla>();
                        List<Casilla> planeta3 = new List<Casilla>();

                        planeta1.Add(planetas[0]);
                        planeta2.Add(planetas[1]);
                        planeta3.Add(planetas[2]);

                        planeta1 = FiltroCasillas.CasillasAdyacentes(planeta1, true);
                        planeta2 = FiltroCasillas.CasillasAdyacentes(planeta2, true);
                        planeta3 = FiltroCasillas.CasillasAdyacentes(planeta3, true);

                        //comprobacion de intersecciones
                        posibles = FiltroCasillas.Interseccion(planeta1, planeta2);
                        if (posibles.Count == 0)
                        {
                            posibles = FiltroCasillas.Interseccion(planeta1, planeta3);
                            if (posibles.Count == 0)
                            {
                                posibles = FiltroCasillas.Interseccion(planeta2, planeta3);
                                memoria = 1;
                            }
                        }

                    }
                    else // el resto pueden salir como quieran 
                    {
                        posibles = FiltroCasillas.CasillasAdyacentes(planetas[i - memoria], true);
                        posibles = FiltroCasillas.CasillasLibres(posibles);
                    }

                    int rnd = Random.Range(0, posibles.Count);
                    GameObject thisPieza;
                    if (PhotonNetwork.InRoom)
                    {
                        thisPieza = PhotonNetwork.Instantiate(luna.name, posibles[rnd].transform.position, Quaternion.identity);
                    }
                    else
                    {
                        thisPieza = Instantiate(luna, posibles[rnd].transform.position, Quaternion.identity);
                    }
                    thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                    thisPieza.GetComponent<Pieza>().Colocar(posibles[rnd]);
                    posibles[rnd].pieza = thisPieza.GetComponent<Pieza>();
                }
            }
            else //cuando el herore no tiene handicap y queremos que la luna aparezca separada
            {
                
          
                for(int i = 0; i < planetas.Count; ++i) // ninguna luna tiene que ser adyacente a ninguno de los planetas
                {
                    posibles = FiltroCasillas.CasillasAdyacentes(planetas[i], true);
                   
                    for(int j = 0; j < planetas.Count; ++j) // restamos las adyacentes del planeta que queremos rodear contra las adyacentes de todos los demas planetas
                    {
                       if(j != i)     posibles = FiltroCasillas.RestaLista(posibles, FiltroCasillas.CasillasAdyacentes(planetas[j], true));
                    }

                    int rnd = Random.Range(0, posibles.Count);
                    GameObject thisPieza;
                    if (PhotonNetwork.InRoom)
                    {
                        thisPieza = PhotonNetwork.Instantiate(luna.name, posibles[rnd].transform.position, Quaternion.identity);
                    }
                    else
                    {
                        thisPieza = Instantiate(luna, posibles[rnd].transform.position, Quaternion.identity);
                    }
                    thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                    thisPieza.GetComponent<Pieza>().Colocar(posibles[rnd]);
                    posibles[rnd].pieza = thisPieza.GetComponent<Pieza>();
                }
            }



        }
        if (!sin_pasar_turno)
        {
            Debug.Log("sin pasar turno lunatico paso turno");
            EventManager.TriggerEvent("AccionTerminadaConjunta");
        }
    }

    public override void FirstActionPersonal()
    {
        List<Casilla> casillasPosibles = luna.GetComponent<Pieza>().CasillasDisponibles();
        if(casillasPosibles.Count == 0)
        {
            Debug.Log("no hay hueco en el poder lunatico");
            EventManager.TriggerEvent("AccionTerminadaConjunta");
            return;
        }
        Debug.Log(casillasPosibles.Count);
        Tablero.instance.ResetCasillasEfects();
        foreach (Casilla casilla in casillasPosibles) casilla.SetState(States.select);

        setLuna = true;
    }
    public override void SecondAction()
    {
        lunasPuestas = 0;
        FirstActionPersonal();
    }

    void PlaceLuna()
    {
        if (!setLuna) return;
        Casilla c = ClickCasillas.casillaClick;
        List<Casilla> casillasPosibles = luna.GetComponent<Pieza>().CasillasDisponibles();
        if(casillasPosibles.Count == 0)
        {
            Debug.Log("no hay hueco para poner liuna");
            EventManager.TriggerEvent("AccionTerminadaConjunta");
            return;
        }
        if (casillasPosibles.Contains(c))
        {
            GameObject thisPieza;
            if (PhotonNetwork.InRoom)
            {
                thisPieza = PhotonNetwork.Instantiate(luna.name, c.transform.position, Quaternion.identity);
            }
            else
            {
                thisPieza = Instantiate(luna, c.transform.position, Quaternion.identity);
            }
            thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
            thisPieza.GetComponent<Pieza>().casilla = c;
            c.pieza = thisPieza.GetComponent<Pieza>();
            setLuna = false;
            if (lunasPuestas < numeroLunasPorFase)
            {
                ++lunasPuestas;
                FirstActionPersonal();
                Debug.Log("lunas puestas: " + lunasPuestas);
                Debug.Log("LUNas maximas: " + numeroLunasPorFase);
            }
            else
            {
                lunasPuestas = 0;
                Tablero.instance.ResetCasillasEfects();
                Debug.Log("se acabo poner lunas");
                EventManager.TriggerEvent("AccionTerminadaConjunta");
            }
        }
    }

}
