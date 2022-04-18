using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PropulsorCambioOrbital : Efecto
{
    bool preaparado_para_mover = false;

    List<Casilla> posibles_destinos = new List<Casilla>();

    public GameObject restos;

    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        //Planetas aliados
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(faccion, referencia);
        casillasDisponibles = FiltroCasillas.CasillasDeUnTipo(new List<Clase> { Clase.planeta}, casillasDisponibles);

        List<Casilla> casillasAEliminar = new List<Casilla>();
        //Discriminacion planetas
        foreach(Casilla c in casillasDisponibles)
        {
            posibles_destinos = FiltroCasillas.CasillasAdyacentes(c, true);
            List<Casilla> mis_cosas = FiltroCasillas.CasillasDeUnJugador(faccion, posibles_destinos);
            posibles_destinos = FiltroCasillas.RestaLista(posibles_destinos, mis_cosas);
            List<Casilla> planetas = FiltroCasillas.CasillasDeUnTipo(Clase.planeta, referencia);
            if(planetas.Contains(c)) planetas.Remove(c);
            planetas = FiltroCasillas.CasillasAdyacentes(planetas, true);
            posibles_destinos = FiltroCasillas.RestaLista(posibles_destinos, planetas);
            if (posibles_destinos.Count == 0) casillasAEliminar.Add(c);
        }

        casillasDisponibles = FiltroCasillas.RestaLista(casillasDisponibles, casillasAEliminar);

        return casillasDisponibles;
    }


    public override void Accion()
    {
        if (PhotonNetwork.InRoom && !gameObject.GetPhotonView().IsMine && PhotonNetwork.InRoom) return;

        posibles_destinos = FiltroCasillas.CasillasAdyacentes(casilla, true);
        List<Casilla> mis_cosas = FiltroCasillas.CasillasDeUnJugador(faccion, posibles_destinos);
        posibles_destinos = FiltroCasillas.RestaLista(posibles_destinos, mis_cosas);
        List<Casilla> planetas = FiltroCasillas.CasillasDeUnTipo(Clase.planeta);
        if (planetas.Contains(casilla)) planetas.Remove(casilla);
        planetas = FiltroCasillas.CasillasAdyacentes(planetas, true);
        posibles_destinos = FiltroCasillas.RestaLista(posibles_destinos, planetas);

        if (posibles_destinos.Count == 0)
        {
            Debug.Log("termino al mover planeta cuando no puedo mover");
            EventManager.TriggerEvent("AccionTerminadaConjunta");
            preaparado_para_mover = false;
        }
        else
        {
            EventManager.StartListening("ClickCasilla", Mover);
            Tablero.instance.ResetCasillasEfects();
            foreach (Casilla casilla in posibles_destinos) casilla.SetState(States.select);

            preaparado_para_mover = true; 
        }
    }

    public void Mover()
    {
        if (!preaparado_para_mover) return;

        Casilla c = ClickCasillas.casillaClick;

        if (posibles_destinos.Contains(c))
        {
            if (c.pieza)
            {
                OnlineManager.instance.Destroy_This_Pieza(c.pieza);
            }

            // Comprobacion de si el game se esta realizando online u offline
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2 && GetComponent<PhotonView>().IsMine)
            {
                OnlineManager.instance.Destroy_This_Pieza(casilla.pieza);
                // Instanciacion que utiliza photon
                GameObject thisPieza = PhotonNetwork.Instantiate(restos.name, c.transform.position, Quaternion.identity);
            }
            else if (!PhotonNetwork.InRoom)
            {
                casilla.Clear();
                // Instanciacion de piezas en el offline
                GameObject thisPieza = Instantiate(restos);
                thisPieza.transform.position = c.transform.position;
            }

            Tablero.instance.ResetCasillasEfects();
            EventManager.TriggerEvent("AccionTerminadaConjunta");
            Destroy(this.gameObject);
        }
    }
    

    public override void Colocar(Casilla c)
    {
        casilla = c;
        Accion();
    }
}
