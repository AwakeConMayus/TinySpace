using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroePlanetarioColonizador : HeroePlanetario
{
    bool primeraFaseRealizada = false;

    public override void Poder()
    {
        if(!primeraFaseRealizada)
        {

            primeraFaseRealizada = true;
        }
        else
        {

        }
    }

}
