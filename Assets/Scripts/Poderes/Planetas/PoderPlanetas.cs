using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class PoderPlanetas : Poder
{
    protected GameObject planeta;

    bool SetPlaneta = false;

    

    public override void InitialAction(bool sin_pasar_turno = false, int[] vector = null)
    {
        Debug.Log("INICIALIZACION PLANETAS");
        EventManager.StartListening("ClickCasilla", CrearPieza);
        planeta = Resources.Load<GameObject>("Planeta");//SEGUN CLANTA ESTO ES UNA CACA HAY QUE CHANGEARLO


        if (vector == null || vector.Length != 5)
        {
            Debug.LogWarning(" Sin vector introducido o vector erroneo para la faciion de los planetas");
            Casilla planetaReferencia = null;

            for (int i = 0; i < 3; i++)
            {
                List<Casilla> casillasPosibles = FiltroCasillas.CasillasSinMeteorito(planeta.GetComponent<Pieza>().CasillasDisponibles());
                casillasPosibles = FiltroCasillas.EliminarBordes(casillasPosibles);
                if (i == 1)
                {
                    casillasPosibles = FiltroCasillas.CasillasAdyacentes(planetaReferencia, true);
                    casillasPosibles = FiltroCasillas.CasillasAdyacentes(casillasPosibles, true);
                    casillasPosibles = FiltroCasillas.CasillasSinMeteorito(casillasPosibles);
                    casillasPosibles = FiltroCasillas.Interseccion(casillasPosibles, planeta.GetComponent<Pieza>().CasillasDisponibles());
                    casillasPosibles = FiltroCasillas.EliminarBordes(casillasPosibles);
                }
                int rnd;
                do
                {
                    rnd = Random.Range(0, Tablero.instance.mapa.Count);
                } while (!casillasPosibles.Contains(Tablero.instance.mapa[rnd]));

                if (i == 0) planetaReferencia = Tablero.instance.mapa[rnd];

                if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
                {
                    if (!planeta) planeta = Resources.Load<GameObject>("Planeta Planetarios");
                    GameObject thisPieza = PhotonNetwork.Instantiate(planeta.name, Tablero.instance.mapa[rnd].transform.position, Quaternion.identity);
                    thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                    thisPieza.GetComponent<Pieza>().casilla = Tablero.instance.mapa[rnd];
                    Tablero.instance.mapa[rnd].pieza = thisPieza.GetComponent<Pieza>();
                }
                else
                {
                    GameObject thisPieza = Instantiate(planeta);
                    thisPieza.transform.position = Tablero.instance.mapa[rnd].transform.position;
                    thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                    thisPieza.GetComponent<Pieza>().Colocar(Tablero.instance.mapa[rnd]);
                }
            }
            Tablero.instance.ResetCasillasEfects();
            if (!sin_pasar_turno)
            {
                Debug.Log("pasar turno no pasado planetas");
                EventManager.TriggerEvent("AccionTerminadaConjunta");
            }
        }
        else
        {
            Casilla planetaReferencia = null;

            for (int i = 0; i < 3; i++)
            {
                List<Casilla> casillasPosibles = FiltroCasillas.CasillasLibres();
                
                int rnd;
                switch (i)
                {
                    case 0://La colocacion del primer planeta puede ser aleatoria
                        if( vector[2] >= 1)
                        {
                            casillasPosibles = FiltroCasillas.CasillasEsquineras();
                            casillasPosibles = FiltroCasillas.CasillasAdyacentes(casillasPosibles, true);
                            if(vector[2] == 1)
                            {
                                casillasPosibles = FiltroCasillas.CasillasAdyacentes(casillasPosibles, false);
                            }
                        }
                        break;

                    case 1://colocacion del segundo planeta
                        casillasPosibles = FiltroCasillas.CasillasAdyacentes(planetaReferencia, true);
                        casillasPosibles = FiltroCasillas.CasillasAdyacentes(casillasPosibles, true);
                        casillasPosibles = FiltroCasillas.Interseccion(casillasPosibles, planeta.GetComponent<Pieza>().CasillasDisponibles());

                        List<Casilla> alfaCasillas = new List<Casilla>();
                        alfaCasillas = FiltroCasillas.CasillasEnLineaACasillaDada(planetaReferencia, casillasPosibles);

                        if (vector[1] == 1)//si tienen que estar en linea
                        {
                            casillasPosibles = alfaCasillas;
                        }
                        else //si tienen que estar en C
                        {
                            casillasPosibles = FiltroCasillas.RestaLista(casillasPosibles, alfaCasillas);
                        }

                        break;
                    case 2:
                        List<Casilla> auxCasillas = FiltroCasillas.CasillasPlaneta();
                        List<Casilla> aux2Casillas = FiltroCasillas.CasillasPlaneta();
                        //esto se queda fuera porque como es el minimo se utiliza en todos
                        auxCasillas = FiltroCasillas.CasillasAdyacentes(auxCasillas, false); //1a
                        auxCasillas = FiltroCasillas.CasillasAdyacentes(auxCasillas, false);//2a


                        aux2Casillas = FiltroCasillas.CasillasAdyacentes(aux2Casillas, false);//1b


                        if (vector[2] > 0)
                        {


                            auxCasillas = FiltroCasillas.CasillasAdyacentes(auxCasillas, false);//3a
       

                            aux2Casillas = FiltroCasillas.CasillasAdyacentes(aux2Casillas, false);//2b

                        }
                        if(vector[2] > 1)
                        {
                            auxCasillas = FiltroCasillas.CasillasLibres();

                            aux2Casillas = FiltroCasillas.CasillasAdyacentes(aux2Casillas, false);//3b

                        }

                        casillasPosibles = FiltroCasillas.RestaLista(auxCasillas, aux2Casillas);
                        break;
                }


                casillasPosibles = FiltroCasillas.EliminarBordes(casillasPosibles);
                rnd =  Random.Range(0, casillasPosibles.Count);
                int patatas = Tablero.instance.mapa.IndexOf(casillasPosibles[rnd]);
                planetaReferencia = casillasPosibles[rnd];

                if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
                {
                    if (!planeta) planeta = Resources.Load<GameObject>("Planeta Planetarios");
                    GameObject thisPieza = PhotonNetwork.Instantiate(planeta.name,  Tablero.instance.mapa[patatas].transform.position, Quaternion.identity);
                    thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                    thisPieza.GetComponent<Pieza>().casilla = Tablero.instance.mapa[patatas];
                    Tablero.instance.mapa[patatas].pieza = thisPieza.GetComponent<Pieza>();
                }
                else
                {
                    GameObject thisPieza = Instantiate(planeta);
                    thisPieza.transform.position = Tablero.instance.mapa[patatas].transform.position;
                    thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                    thisPieza.GetComponent<Pieza>().casilla = Tablero.instance.mapa[patatas];
                    Tablero.instance.mapa[patatas].pieza = thisPieza.GetComponent<Pieza>();

                    
                }
            }


            Tablero.instance.ResetCasillasEfects();
            if (!sin_pasar_turno)
            {
                Debug.Log("pasar turno no pasado planetas");
                EventManager.TriggerEvent("AccionTerminadaConjunta");
            }

        }
    }

    public override void FirstAction()
    {
        List<Casilla> casillasPosibles = new List<Casilla>();
        casillasPosibles = planeta.GetComponent<Pieza>().CasillasDisponibles();

        Tablero.instance.ResetCasillasEfects();
        foreach (Casilla casilla in casillasPosibles) casilla.SetState(States.select);

        SetPlaneta = true;
    }

    void CrearPieza()
    {
        if (!SetPlaneta) return;
        Casilla c = ClickCasillas.casillaClick;
        List<Casilla> casillasPosibles = new List<Casilla>();
        casillasPosibles = planeta.GetComponent<Pieza>().CasillasDisponibles();
        if (casillasPosibles.Contains(c))
        {
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                if (!planeta) planeta = Resources.Load<GameObject>("Planeta Planetarios");
                GameObject thisPieza = PhotonNetwork.Instantiate(planeta.name, c.transform.position, Quaternion.identity);
                thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                thisPieza.GetComponent<Pieza>().casilla = c;
                c.pieza = thisPieza.GetComponent<Pieza>();
            }
            else
            {
                GameObject thisPieza = Instantiate(planeta);
                thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                thisPieza.transform.position = c.transform.position;
                thisPieza.GetComponent<Pieza>().Colocar(c);
            }


            SetPlaneta = false;

            Tablero.instance.ResetCasillasEfects();

            FirstActionPersonal();
        }

    }

    public abstract void FirstActionPersonal();

    [PunRPC]
    public void RPC_InstanciarPlaneta(int i, int _jugador)
    {
        if(!planeta) planeta = Resources.Load<GameObject>("Planeta Planetarios");
        GameObject thisPieza = Instantiate(planeta);
        thisPieza.transform.position = Tablero.instance.mapa[i].transform.position;
        thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
        thisPieza.GetComponent<Pieza>().casilla = Tablero.instance.mapa[i];
        Tablero.instance.mapa[i].pieza = thisPieza.GetComponent<Pieza>();
    }
}
