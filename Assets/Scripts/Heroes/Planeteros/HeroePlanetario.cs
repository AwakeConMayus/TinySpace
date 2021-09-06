using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroePlanetario : Heroe
{
    public override void Praparación()
    {
        List<Casilla> casillasLibres = new List<Casilla>();
        for (int i = 0; i < 3; i++)
        {
            casillasLibres = FiltroCasillas.CasillasLibres();
            int elegida = Random.Range(0, casillasLibres.Count);

            Tablero.instance.Colocar(new Planetas(), casillasLibres[elegida], -1);
        }
    }

    public override void Poder()
    {
        throw new System.NotImplementedException();
    }
}
