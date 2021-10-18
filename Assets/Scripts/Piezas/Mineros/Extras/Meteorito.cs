using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorito : Pieza
{
    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        return FiltroCasillas.CasillasSinMeteorito(FiltroCasillas.CasillasLibres(referencia));
    }

    public override int Puntos()
    {
        return 0;
    }

    protected override void SetClase()
    {
        clase = Clase.none;
    }

    

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Casilla>())
        {
            Colocar(other.gameObject.GetComponent<Casilla>());
        }

        if (other.gameObject.GetComponent<Pieza>() && !other.CompareTag("Meteorito"))
        {
            Pieza p = other.gameObject.GetComponent<Pieza>();
            if (p.Get_Jugador() == jugador) EventManager.TriggerEvent("RecogerMineral");
            Destroy(this.gameObject);
        }
    }


    public override void Colocar(Casilla c)
    {
        casilla = c;
    }

    private void OnDestroy()
    {
        casilla.meteorito = false;
    }
}
