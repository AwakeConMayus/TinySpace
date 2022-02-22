using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATerraformador : PiezaIA
{
    public int fase;

    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        Pieza nuevoPlaneta = null;

        switch (fase)
        {
            case 0:
                nuevoPlaneta = Resources.Load<Pieza>("Planeta Sol Planetarios");
                break;
            case 1:
                nuevoPlaneta = Resources.Load<Pieza>("Planeta Sol Planetarios");
                break;
            case 2:
                nuevoPlaneta = Resources.Load<Pieza>("Planeta Sol Planetarios");
                break;
            case 3:
                nuevoPlaneta = Resources.Load<Pieza>("Planeta Sol Planetarios");
                break;

        }

        return null;
    }
}
