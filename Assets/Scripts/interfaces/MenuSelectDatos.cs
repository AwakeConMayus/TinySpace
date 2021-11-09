using System.Collections;
using System.Collections.Generic;  //Clanta: debo empezar a acostumbrarme que DobleC comenta siempre las librerias
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class MenuSelectDatos : MonoBehaviourPunCallbacks
{
    //Clanta: AVISO IMPORTANTE a este script le faltan todas las rpc necesarias. (Esperar al otro jugador para empezar)(Esperar a comprobar la faccion del rival para seleccionar aleatoriamente)(etc)
    //Clanta: Tenia un dolor de cabeza bastante loko cuando hice esto deberia revisarlo mas tarde

    //2C: dies uwus pa' ti mi pana

    [SerializeField]
    GameObject menuHeroes;
    [SerializeField]
    GameObject menuEspeciales;
    [SerializeField]
    GameObject menuMejoradas;
    [SerializeField]
    Button enterMatch;
    [SerializeField]
    Text faccion_rival_text;
    [SerializeField]
    DatosIA datosIA;

    Button[] btnHeroes;
    Button[] btnEspeciales;
    Button[] btnMejoradas;

    int heroeSel    = -1;
    int especialSel = -1;
    int mejoradaSel = -1;

    int jugadores_listos;

    [SerializeField]
    List<OpcionesFaccion> opciones = new List<OpcionesFaccion>();
    [SerializeField]
    TuSeleccion mi_Seleccion;
    [SerializeField]
    TuSeleccion seleccion_rival;


    OpcionesFaccion faccion_Seleccionada;
    OpcionesFaccion opciones_rival;

    bool preparado = false;

    Faccion faccion_del_Rival = Faccion.none;

    private void Start()
    {
        jugadores_listos = 0;
        btnHeroes     = new Button[3];
        btnHeroes     = menuHeroes.GetComponentsInChildren<Button>();

        btnEspeciales = new Button[3];
        btnEspeciales = menuEspeciales.GetComponentsInChildren<Button>();

        btnMejoradas  = new Button[3];
        btnMejoradas  = menuMejoradas.GetComponentsInChildren<Button>();

        if (PhotonNetwork.IsMasterClient && mi_Seleccion.faccion != Faccion.none)
        {
            Preparar();
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            base.photonView.RPC("RPC_Te_Toca_Preparar", RpcTarget.Others, Faccion.none);
        }
        else if(!PhotonNetwork.InRoom)
        {
            Preparar();
        }
    }

    public void Preparar( )
    {
        for(int i = 0; i < opciones.Count; ++i)
        {
            if(opciones[i].faccion == faccion_del_Rival)
            {
                opciones_rival = opciones[i];
            }
        }

        if (preparado) return;
        //Eleccion de las opciones correctas basadas en la faccion selecionado previamente
        for (int i = 0; i < opciones.Count; ++i)
        {
            if (mi_Seleccion.faccion == opciones[i].faccion)
            {
                faccion_Seleccionada = opciones[i];
            }
        }
        //Sistema de acceso aleatorio a la faccion 
        if (!faccion_Seleccionada)
        {
            for(int i = 0; i < opciones.Count; ++i)
            {
                if(opciones[i].faccion == faccion_del_Rival)
                {
                    opciones.Remove(opciones[i]);
                }
            }

            Debug.Log("Selecciono faccion de maneras aleatoria");

            int rnd = Random.Range(0, opciones.Count);

            faccion_Seleccionada = opciones[rnd];

            mi_Seleccion.faccion = faccion_Seleccionada.faccion;
        }
        // Desactivacion del menu de mejoras del minero
        if(faccion_Seleccionada.faccion == Faccion.minero)
        {
            menuMejoradas.SetActive(false);
            mejoradaSel = 11;
        }

        for(int j = 0; j < faccion_Seleccionada.piezas_Basicas.Length; ++j)
        {
            mi_Seleccion.mis_opciones[j] = faccion_Seleccionada.piezas_Basicas[j];
        }
        preparado = true;
        Poner_Nombres();
        if(PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2) base.photonView.RPC("RPC_Te_Toca_Preparar", RpcTarget.Others, faccion_Seleccionada.faccion);
    }

    public void selectHeroe(int heroe)
    {
        for (int i = 0; i < 3; i++)
        {
            btnHeroes[i].interactable = true;
        }

        heroeSel = heroe;
        btnHeroes[heroeSel].interactable = false;

        mi_Seleccion.mi_poder = faccion_Seleccionada.posibles_Poders[heroe];

        if (!enterMatch.interactable && heroeSel != -1 && especialSel != -1 && mejoradaSel != -1) enterMatch.interactable = true;
    }

    public void selectEspecial(int especial)
    {
        for (int i = 0; i < 3; i++)
        {
            btnEspeciales[i].interactable = true;
        }

        especialSel = especial;
        btnEspeciales[especialSel].interactable = false;

        mi_Seleccion.mis_opciones[4] = faccion_Seleccionada.posibles_Piezas_Especiales[especial];

        if (!enterMatch.interactable && heroeSel != -1 && especialSel != -1 && mejoradaSel != -1) enterMatch.interactable = true;
    }

    public void selectMejorada(int mejorada)
    {
        for (int i = 0; i < 3; i++)
        {
            btnMejoradas[i].interactable = true;
        }

        mejoradaSel = mejorada;
        btnMejoradas[mejoradaSel].interactable = false;

        for (int z = 0; z < faccion_Seleccionada.piezas_Basicas.Length; ++z)
        {
            mi_Seleccion.mis_opciones[z] = faccion_Seleccionada.piezas_Basicas[z];
        }

        mi_Seleccion.mis_opciones[faccion_Seleccionada.huecos_Especializadas[mejorada]] = faccion_Seleccionada.posibles_Piezas_Especializadas[mejorada];
        if (!enterMatch.interactable && heroeSel != -1 && especialSel != -1 && mejoradaSel != -1) enterMatch.interactable = true;
    }

    public void EnterGame()
    {
        SceneManager.LoadScene(3);
    }

    public void goBack()
    {
        SceneManager.LoadScene(0);
    }

    public void Terminar()
    {
        if (PhotonNetwork.InRoom)
        {
            base.photonView.RPC("RPC_TerminarSeleccion", RpcTarget.MasterClient);
            base.photonView.RPC("Eleccion_Enemiga", RpcTarget.Others, heroeSel, especialSel, mejoradaSel);
        }
        else
        {
            SeleccionIA();
            SceneManager.LoadScene(4);
        }
        enterMatch.gameObject.SetActive(false);
        menuHeroes.gameObject.SetActive(false);
        menuMejoradas.gameObject.SetActive(false);
        menuEspeciales.gameObject.SetActive(false);
        //Clanta: Aqui hay que poner la funcion de que en caso de que no estes online te lleve a jugar con la maquina
    }

    void SeleccionIA()
    {
        float bestwinrate = float.MinValue;
        List<int> bestOpciones = new List<int>();
        int bestOption = 0;
        float winrat = 0;

        switch (mi_Seleccion.faccion)
        {
            case Faccion.minero:
                //Faccion
                seleccion_rival.faccion = Faccion.oyente;                

                //Heroe
                winrat = datosIA.Oyentes.vsMineros.heroe1WinRate();
                if(winrat >= bestwinrate)
                {
                    if(winrat > bestwinrate) 
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(0);
                }
                winrat = datosIA.Oyentes.vsMineros.heroe2WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(1);
                }
                winrat = datosIA.Oyentes.vsMineros.heroe3WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(2);
                }
                bestOption = bestOpciones[Random.Range(0, bestOpciones.Count)];

                seleccion_rival.mi_poder = opciones[0].posibles_Poders[bestOption];

                //Mejora
                winrat = datosIA.Oyentes.vsMineros.mejora1WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(0);
                }
                winrat = datosIA.Oyentes.vsMineros.mejora2WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(1);
                }
                winrat = datosIA.Oyentes.vsMineros.mejora3WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(2);
                }
                bestOption = bestOpciones[Random.Range(0, bestOpciones.Count)];

                seleccion_rival.mis_opciones[opciones[0].huecos_Especializadas[bestOption]] = opciones[0].posibles_Piezas_Especializadas[bestOption];

                //Especial
                winrat = datosIA.Oyentes.vsMineros.especial1WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(0);
                }
                winrat = datosIA.Oyentes.vsMineros.especial2WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(1);
                }
                winrat = datosIA.Oyentes.vsMineros.especial3WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(2);
                }
                bestOption = bestOpciones[Random.Range(0, bestOpciones.Count)];

                seleccion_rival.mis_opciones[4] = opciones[0].posibles_Piezas_Especiales[bestOption];
                break;

            case Faccion.oyente:
                //Faccion
                seleccion_rival.faccion = Faccion.minero;

                //Heroe
                winrat = datosIA.Mineros.vsOyentes.heroe1WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(0);
                }
                winrat = datosIA.Mineros.vsOyentes.heroe2WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(1);
                }
                winrat = datosIA.Mineros.vsOyentes.heroe3WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(2);
                }

                bestOption = bestOpciones[Random.Range(0, bestOpciones.Count)];

                seleccion_rival.mi_poder = opciones[1].posibles_Poders[bestOption];

                

                //Especial
                winrat = datosIA.Mineros.vsOyentes.especial1WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(0);
                }
                winrat = datosIA.Mineros.vsOyentes.especial2WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(1);
                }
                winrat = datosIA.Mineros.vsOyentes.especial3WinRate();
                if (winrat >= bestwinrate)
                {
                    if (winrat > bestwinrate)
                    {
                        bestOpciones = new List<int>();
                        bestwinrate = winrat;
                    }
                    bestOpciones.Add(2);
                }
                bestOption = bestOpciones[Random.Range(0, bestOpciones.Count)];
                seleccion_rival.mis_opciones[4] = opciones[1].posibles_Piezas_Especiales[bestOption];
                break;
        }
    }

    [PunRPC]
    public void RPC_TerminarSeleccion()
    {
        ++jugadores_listos;
        if(jugadores_listos == 2)
        {
            EnterGame();
        }
    }

    [PunRPC]
    public void Eleccion_Enemiga(int heroe, int especial, int espcializacion)
    {
        seleccion_rival.faccion = opciones_rival.faccion;
        for (int j = 0; j < faccion_Seleccionada.piezas_Basicas.Length; ++j)
        {
            seleccion_rival.mis_opciones[j] = opciones_rival.piezas_Basicas[j];
        }
        seleccion_rival.mi_poder = opciones_rival.posibles_Poders[heroe];

        seleccion_rival.mis_opciones[4] = opciones_rival.posibles_Piezas_Especiales[especial];
        //Clanta: Esto es para dejar fuera el caso del minero
        if (espcializacion > 10) return;
        seleccion_rival.mis_opciones[opciones_rival.huecos_Especializadas[espcializacion]] = opciones_rival.posibles_Piezas_Especializadas[espcializacion];
    }

    [PunRPC]
    public void RPC_Te_Toca_Preparar(Faccion mi_Faccion)
    {
        faccion_del_Rival = mi_Faccion;
        faccion_rival_text.text = "Faccion del rival: " + mi_Faccion.ToString();
        Preparar();
    }

    public void Poner_Nombres()
    {
        for(int i = 0; i < 3; ++i)
        {
            btnHeroes[i].GetComponentInChildren<Text>().text = faccion_Seleccionada.posibles_Poders[i].name;
            btnEspeciales[i].GetComponentInChildren<Text>().text = faccion_Seleccionada.posibles_Piezas_Especiales[i].name;
            if (faccion_Seleccionada.faccion != Faccion.minero)
            {
                btnMejoradas[i].GetComponentInChildren<Text>().text = faccion_Seleccionada.posibles_Piezas_Especializadas[i].name;
            }
        }
    }
}
