using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GarrapataMinero : Pieza
{


    public int nivel = 1;
    int acumulacion = 0;

    private void Start()
    {
        GameObject g = Tablero.instance.Crear_Casilla_Vacia();
        transform.position = g.transform.position;
    }
    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        return null;
    }
    protected override void SetClase()
    {
        clase = Clase.none;
    }
    public override int Puntos()
    {
        return acumulacion * nivel;
    }
    
    public void Chantaje()
    {
        ++acumulacion;
    }
}
