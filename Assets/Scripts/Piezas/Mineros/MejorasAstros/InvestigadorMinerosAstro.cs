﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InvestigadorMinerosAstro : InvestigadorMineros
{
    private bool preparado_para_disparar = false;

    List<Casilla> objetivos = new List<Casilla>();

    protected override void SetClase()
    {
        clase = Clase.astros;
    }

    public override void Colocar(Casilla c)
    {
        casilla = c;
        casilla.pieza = this;
        if (this.gameObject.GetPhotonView().IsMine) Preparar();
    }

    public void Preparar()
    {
        objetivos = FiltroCasillas.CasillasAdyacentes(casilla, false);
        objetivos = FiltroCasillas.CasillasAdyacentes(objetivos, false);
        objetivos = FiltroCasillas.CasillasDeUnJugador(InstancePiezas.instance.jugadorEnemigo, objetivos);
        objetivos = FiltroCasillas.RestaLista(objetivos, FiltroCasillas.CasillasDeUnTipo(Clase.astros, objetivos));

        if(objetivos.Count == 0)
        {
            EventManager.TriggerEvent("AccionTerminadaConjunta");
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
            c.Clear();
            preparado_para_disparar = false;
            Tablero.instance.ResetCasillasEfects();
            EventManager.TriggerEvent("AccionTerminadaConjunta");
        }
    }


}