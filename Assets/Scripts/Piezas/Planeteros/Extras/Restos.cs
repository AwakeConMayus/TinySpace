using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restos : Pieza
{
    protected override void SetClase()
    {
        clase = Clase.restos;
    }
    public override int Puntos()
    {
        return 0;
    }
    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        return null;
    }
}
