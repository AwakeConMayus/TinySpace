using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PropulsorCambioOrbital : Efecto
{
    bool preaparado_para_mover = false;

    List<Casilla> posibles_destinos = new List<Casilla>();

    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(faccion, referencia);
        return FiltroCasillas.CasillasDeUnTipo(new List<Clase> { Clase.astros }, casillasDisponibles);
    }


    public override void Accion()
    {
        if (PhotonNetwork.InRoom && !gameObject.GetPhotonView().IsMine && PhotonNetwork.InRoom) return;

        posibles_destinos = FiltroCasillas.CasillasAdyacentes(casilla, true);
        List<Casilla> mis_cosas = FiltroCasillas.CasillasDeUnJugador(faccion, posibles_destinos);
        mis_cosas = FiltroCasillas.CasillasPlaneta(mis_cosas);
        posibles_destinos = FiltroCasillas.RestaLista(posibles_destinos, mis_cosas);

        if (posibles_destinos.Count == 0)
        {
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
            
            casilla.pieza.transform.position = c.transform.position;
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
