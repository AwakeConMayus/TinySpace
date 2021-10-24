 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoderMecanico : PoderMineros
{
    [SerializeField] GameObject miComodin;

    bool preparado = false;

    List<Casilla> opciones = new List<Casilla>();

    private void Start()
    {
        EventManager.StartListening("ClickCasilla", Cambiar_Nave);
    }

    public override void FirstAction()
    {
        opciones = FiltroCasillas.CasillasDeUnJugador(jugador);

        Tablero.instance.ResetCasillasEfects();
        foreach (Casilla casilla in opciones) casilla.SetState(States.select);

        preparado = true;
    }

    public override void SecondAction()
    {
        FirstAction();
    }

    public void Cambiar_Nave()
    {
        if (!preparado) return;

        Casilla c = ClickCasillas.casillaClick;

        if (opciones.Contains(c))
        {
            OnlineManager.instance.Destroy_This_Pieza(c.pieza);

            GameObject thisPieza;
            if (PhotonNetwork.InRoom)
            {
                thisPieza = PhotonNetwork.Instantiate(miComodin.name, c.transform.position, Quaternion.identity);
            }
            else
            {
                thisPieza = Instantiate(miComodin, c.transform.position, Quaternion.identity);
            }
            //Clanta: Tengo dudas de si habria que colocarla manualmente, si es neceseria hay que hacer una caso limite en la funcion colocar
            //Clanta: Para que una pieza no se coloque dos veces
        }
    }
}
