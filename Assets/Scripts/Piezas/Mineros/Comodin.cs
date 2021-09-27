using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comodin : Efecto
{
    public override void Accion()
    {
        MenuComodin.instance.Convocar(casilla);
    }

    public override List<Casilla> CasillasDisponibles()
    {
        return FiltroCasillas.CasillasLibres();
    }
}
