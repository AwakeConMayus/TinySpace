﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoderAstrofisico : PoderPlanetas
{

    [SerializeField] GameObject blackHole;

    List<GameObject> mis_BalckHoles = new List<GameObject>();

    bool preparado_para_instanciar = false;

    private void Awake()
    {
        EventManager.StartListening("ClickCasilla", Crear_BlackHole);
    }

    public override void InitialAction(bool sin_pasar_turno = false)
    {
        base.InitialAction(true);

        List<Casilla> planetas = FiltroCasillas.CasillasDeUnJugador(faccion);

        foreach (Casilla c in planetas)
        {
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
                thisPieza = PhotonNetwork.Instantiate(padre.opcionesIniciales[2].name, posibles[rnd].transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("instanciacion");
                thisPieza = Instantiate(padre.opcionesIniciales[2], posibles[rnd].transform.position, Quaternion.identity);
            }
            thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
            thisPieza.GetComponent<Pieza>().Colocar(posibles[rnd]);
            posibles[rnd].pieza = thisPieza.GetComponent<Pieza>();
        }

        if (!sin_pasar_turno) EventManager.TriggerEvent("AccionTerminadaConjunta");
    }

    public override void FirstActionPersonal()
    {
        if (!gameObject.GetPhotonView().IsMine) return;
        List<Casilla> posibles_lugares = blackHole.GetComponent<Pieza>().CasillasDisponibles();
        Debug.Log(posibles_lugares.Count);

        Tablero.instance.ResetCasillasEfects();
        foreach (Casilla casilla in posibles_lugares) casilla.SetState(States.select);

        preparado_para_instanciar = true;
    }

    public void Crear_BlackHole()
    {
        if (!preparado_para_instanciar) return;

        Casilla c = ClickCasillas.casillaClick;

        List<Casilla> posibles_lugares = blackHole.GetComponent<Pieza>().CasillasDisponibles();

        if (posibles_lugares.Contains(c))
        {
            GameObject this_pieza;
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                this_pieza = PhotonNetwork.Instantiate(blackHole.name, c.transform.position, Quaternion.identity);
            }
            else
            {
                this_pieza = Instantiate(blackHole, c.transform.position, Quaternion.identity);
            }
            this_pieza.GetComponent<Pieza>().Set_Pieza_Extra();
            this_pieza.GetComponent<Pieza>().Colocar(c);

            mis_BalckHoles.Add(this_pieza);

            preparado_para_instanciar = false;
            Tablero.instance.ResetCasillasEfects();

           StartCoroutine( Activar());
        }
    }

    IEnumerator Activar()
    {
        for (int i = 0; i < mis_BalckHoles.Count; ++i)
        {
            Casilla origen = mis_BalckHoles[i].GetComponent<Pieza>().casilla;

            for (int j = 0; j < origen.adyacentes.Length; ++j)
            {
                if (origen.adyacentes[j] && origen.adyacentes[j].pieza && origen.adyacentes[j].pieza.clase != Clase.astros)
                {
                    Debug.Log("destruyo en esta direccon: " + j);
                    Debug.Log("destruyo esta pieza: " + origen.adyacentes[j].pieza);
                    OnlineManager.instance.Destroy_This_Pieza(origen.adyacentes[j].pieza);
                }
                Atraer_Todo_En_Una_Direccion(origen.adyacentes[j], j);
            }
            yield return new WaitForSeconds(1.5f);
        }


        EventManager.TriggerEvent("AccionTerminadaConjunta");
    }
    public override void SecondAction()
    {
        FirstActionPersonal();
    }

    public void Atraer_Todo_En_Una_Direccion(Casilla c, int direccion)
    {
        if (!c) return;
        if (c.pieza)
        {
            Debug.Log("Pieza a mover: " + c.pieza);
            if (c.pieza.clase == Clase.astros) return;
            int aux_reverseDirection;
            if (direccion < 3) aux_reverseDirection = direccion + 3;
            else aux_reverseDirection = direccion - 3;

            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                if (c.pieza.gameObject.GetPhotonView().IsMine)
                {
                    c.pieza.Set_Pieza_Extra();
                    c.pieza.transform.position = c.adyacentes[aux_reverseDirection].transform.position;
                    c.pieza.Colocar(c.adyacentes[aux_reverseDirection]);
                    c.Limpiar_Pieza(c.pieza);
                }
                else
                {
                    int i = Tablero.instance.Get_Numero_Casilla(c.gameObject);
                    int j = Tablero.instance.Get_Numero_Casilla(c.adyacentes[aux_reverseDirection].gameObject);
                    base.photonView.RPC("RPC_Move_FromC_ToC2", RpcTarget.Others, i, j, true);

                }
            }
            else
            {
                c.pieza.Set_Pieza_Extra();

                c.pieza.transform.position = c.adyacentes[aux_reverseDirection].transform.position;
            }
        }
        if (c.adyacentes[direccion])
        {
            Atraer_Todo_En_Una_Direccion(c.adyacentes[direccion], direccion);
        }
    }
}
