using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class PoderMineros : Poder
{
    //AVISO IMPORTANTE: el objeto en el que vaya este script debe tener un componente photonView
    GameObject meteorito;

    

    public override void InitialAction(bool pasar_turno, int[] vector = null)
    {
        Debug.Log("INICIALIZACION MINEROS");
        meteorito = Resources.Load("Meteorito", typeof(GameObject)) as GameObject;

        if (vector == null)
        {


            for (int i = 0; i < 10; i++)
            {
                int rnd;
                do
                {
                    rnd = Random.Range(0, Tablero.instance.mapa.Count);
                } while (!meteorito.GetComponent<Pieza>().CasillasDisponibles().Contains(Tablero.instance.mapa[rnd]));


                if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
                {
                    PhotonNetwork.Instantiate(meteorito.name, Tablero.instance.mapa[rnd].transform.position, Quaternion.identity);
                    base.photonView.RPC("RPC_LlenarCasillaConMeteorito", RpcTarget.All, rnd);
                }
                else
                {
                    GameObject thisPieza = Instantiate(meteorito);
                    thisPieza.transform.position = Tablero.instance.mapa[rnd].transform.position;
                    Tablero.instance.mapa[rnd].meteorito = true;

                }

            }
        }

        else
        {
            List<Casilla> mineralesYaColocados = new List<Casilla>();
            if(vector[1] == 0) //no puede haber grupos de mas de dos minerales 
            {
                for(int i = 0; i < 10; ++i)
                {
                    List<Casilla> posibles = new List<Casilla>();

                    posibles = FiltroCasillas.CasillasLibres();
                    posibles = FiltroCasillas.CasillasSinMeteorito();
                    if (mineralesYaColocados.Count > 0) posibles = FiltroCasillas.RestaLista(posibles, FiltroCasillas.CasillasAdyacentes(mineralesYaColocados, false));
                    
                    int rnd = Random.Range(0, posibles.Count);

                    List<Casilla> aux = FiltroCasillas.CasillasAdyacentes(posibles[rnd], true);
                    int number = aux.Count;
                    if(number != FiltroCasillas.CasillasSinMeteorito(aux).Count)
                    {
                        mineralesYaColocados.Add(posibles[rnd]);

                        aux = FiltroCasillas.RestaLista(aux, FiltroCasillas.CasillasSinMeteorito(aux));

                        mineralesYaColocados.Add(aux[0]);
                    }

                    
                    Debug.Log("HOLA " + posibles.Count);

                    if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
                    {
                        PhotonNetwork.Instantiate(meteorito.name, posibles[rnd].transform.position, Quaternion.identity);
                        base.photonView.RPC("RPC_LlenarCasillaConMeteorito", RpcTarget.All, rnd);
                    }
                    else
                    {
                        GameObject thisPieza = Instantiate(meteorito);
                        thisPieza.transform.position = posibles[rnd].transform.position;
                        posibles[rnd].meteorito = true;

                    }
                }

            }
            else 
            {

                List<Casilla> mineralesJuntos = new List<Casilla>();

                int maximum;
                if (vector[1] == 1) maximum = Random.Range(3, 5);
                else maximum = Random.Range(5, 9);
                for (int i = 0; i < 10; ++i)
                {
                    List<Casilla> posibles = new List<Casilla>();
                    int rnd;
                    if(i < maximum)
                    {
                        posibles = FiltroCasillas.CasillasLibres();
                        posibles = FiltroCasillas.CasillasSinMeteorito();
                        if (mineralesJuntos.Count > 0) posibles = FiltroCasillas.Interseccion(posibles, FiltroCasillas.CasillasAdyacentes(mineralesJuntos, false));
                        posibles = FiltroCasillas.CasillasLibres(posibles); //esto deberia de sobrar pero no lo entiendo
                        rnd = Random.Range(0, posibles.Count);
                        mineralesJuntos.Add(posibles[rnd]);
                        mineralesYaColocados.Add(posibles[rnd]);
                    }
                    else
                    {
                        posibles = FiltroCasillas.CasillasLibres();
                        posibles = FiltroCasillas.CasillasSinMeteorito();
                        if (mineralesYaColocados.Count > 0) posibles = FiltroCasillas.RestaLista(posibles, FiltroCasillas.CasillasAdyacentes(mineralesYaColocados, false));
                        posibles = FiltroCasillas.CasillasLibres(posibles); // esto deberia de sobrar pero no se 
                         rnd = Random.Range(0, posibles.Count);

                        List<Casilla> aux = FiltroCasillas.CasillasAdyacentes(posibles[rnd], true);
                        int number = aux.Count;
                        if (number != FiltroCasillas.CasillasSinMeteorito(aux).Count)
                        {
                            mineralesYaColocados.Add(posibles[rnd]);

                            aux = FiltroCasillas.RestaLista(aux, FiltroCasillas.CasillasSinMeteorito(aux));

                            mineralesYaColocados.Add(aux[0]);
                        }
                    }


                    if (posibles[rnd].pieza) Debug.Log("HAY PIEZA WTF " + posibles[rnd].pieza.name);
                    

                    if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
                    {
                        PhotonNetwork.Instantiate(meteorito.name, posibles[rnd].transform.position, Quaternion.identity);
                        base.photonView.RPC("RPC_LlenarCasillaConMeteorito", RpcTarget.All, rnd);
                    }
                    else
                    {
                        GameObject thisPieza = Instantiate(meteorito);
                        thisPieza.transform.position = posibles[rnd].transform.position;
                        posibles[rnd].meteorito = true;

                    }

                }
            }
        }

        if (!pasar_turno)
        {
            Debug.Log("minero no he pasado turno paso");
            EventManager.TriggerEvent("AccionTerminadaConjunta");
        }
    }


    [PunRPC]
    public void RPC_LlenarCasillaConMeteorito(int i)
    {
        Tablero.instance.mapa[i].meteorito = true;
    }
}
