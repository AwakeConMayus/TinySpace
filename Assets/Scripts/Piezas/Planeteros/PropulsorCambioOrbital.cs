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
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(jugador, referencia);
        return FiltroCasillas.CasillasDeUnTipo(new List<Clase> { Clase.astros }, casillasDisponibles);
    }


    public override void Accion()
    {
        if (!gameObject.GetPhotonView().IsMine) return;

        posibles_destinos = FiltroCasillas.CasillasAdyacentes(casilla, true);
        List<Casilla> mis_cosas = FiltroCasillas.CasillasDeUnJugador(jugador, posibles_destinos);
        posibles_destinos = FiltroCasillas.RestaLista(posibles_destinos, mis_cosas);
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
        }

        Destroy(this.gameObject);
    }

    public override void Colocar(Casilla c)
    {
        casilla = c;
        Accion();
    }
}
