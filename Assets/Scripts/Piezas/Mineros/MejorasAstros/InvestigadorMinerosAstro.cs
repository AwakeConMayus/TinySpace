using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InvestigadorMinerosAstro : InvestigadorMineros
{
    private bool preparado_para_disparar = false;
    bool primera_vez = true;
    List<Casilla> objetivos = new List<Casilla>();

    int puntosDestruidos;

    protected override void SetClase()
    {
        clase = Clase.investigador;
    }

    public override void Colocar(Casilla c)
    {
        casilla = c;
        casilla.pieza = this;
        if ((this.gameObject.GetPhotonView().IsMine && primera_vez) || (!PhotonNetwork.InRoom && primera_vez)) Preparar();
    }

    public void Preparar()
    {
        objetivos = FiltroCasillas.CasillasAdyacentes(casilla, false);
        objetivos = FiltroCasillas.CasillasAdyacentes(objetivos, false);
        objetivos = FiltroCasillas.CasillasDeOtroJugador(faccion, objetivos);
        objetivos = FiltroCasillas.CasillasNoAstro(objetivos);
        if (objetivos.Count == 0)
        {
            EventManager.TriggerEvent("AccionTerminadaConjunta");
            primera_vez = false;
        }
        else
        {
            EventManager.StartListening("ClickCasilla", Disparo);
            Tablero.instance.ResetCasillasEfects();
            foreach (Casilla casilla in objetivos) casilla.SetState(States.select);

            preparado_para_disparar = true;
        }
    }

    public void Disparo()
    {
        if (!preparado_para_disparar) return;
        Casilla c = ClickCasillas.casillaClick;

        if (objetivos.Contains(c))
        {
            puntosDestruidos = c.pieza.Puntos();
            OnlineManager.instance.Destroy_This_Pieza(c.pieza);

            preparado_para_disparar = false;
            Tablero.instance.ResetCasillasEfects();
            EventManager.TriggerEvent("AccionTerminadaConjunta");
            primera_vez = false;
        }
    }

    public int GetPuntosDestruidos()
    {
        return puntosDestruidos;
    }
}
