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

    [SerializeField]
    GameObject menuHeroes;
    [SerializeField]
    GameObject menuEspeciales;
    [SerializeField]
    GameObject menuMejoradas;
    [SerializeField]
    Button enterMatch;

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

    OpcionesFaccion faccion_Seleccionada;

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
    }

    public void Preparar( )
    {
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
        }

        for(int j = 0; j < faccion_Seleccionada.piezas_Basicas.Length; ++j)
        {
            mi_Seleccion.mis_opciones[j] = faccion_Seleccionada.piezas_Basicas[j];
        }
        preparado = true;
        base.photonView.RPC("RPC_Te_Toca_Preparar", RpcTarget.Others, faccion_Seleccionada.faccion);
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
        base.photonView.RPC("RPC_TerminarSeleccion", RpcTarget.MasterClient);
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
    public void RPC_Te_Toca_Preparar(Faccion mi_Faccion)
    {
        faccion_del_Rival = mi_Faccion;
        Preparar();
    }
}
