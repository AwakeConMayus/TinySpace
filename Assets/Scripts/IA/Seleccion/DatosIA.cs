using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DatosIA", order = 1)]
public class DatosIA : ScriptableObject
{
    public DatosIAFaccion Mineros, Oyentes, Honorables, Simbionte;    
}

[System.Serializable]
public struct DatosIAFaccion
{
    public DatosVS vsMineros, vsOyentes, vsHonorables, vsSimbionte;
}

[System.Serializable]
public struct DatosVS
{
    public float jugadas, ganadas;
    
    public float heroe1jugadas, heroe1ganadas;
    public float heroe2jugadas, heroe2ganadas;
    public float heroe3jugadas, heroe3ganadas;

    public float mejora1jugadas, mejora1ganadas;
    public float mejora2jugadas, mejora2ganadas;
    public float mejora3jugadas, mejora3ganadas;

    public float especial1jugadas, especial1ganadas;
    public float especial2jugadas, especial2ganadas;
    public float especial3jugadas, especial3ganadas;

    public float heroe1WinRate()
    {
        if (heroe1jugadas == 0) return 0.5f;
        return (heroe1ganadas / heroe1jugadas);
    }
    public float heroe2WinRate()
    {
        if (heroe2jugadas == 0) return 0.5f;
        return (heroe2ganadas / heroe2jugadas);
    }
    public float heroe3WinRate()
    {
        if (heroe3jugadas == 0) return 0.5f;
        return (heroe3ganadas / heroe3jugadas);
    }

    public float mejora1WinRate()
    {
        if (mejora1jugadas == 0) return 0.5f;
        return (mejora1ganadas / mejora1jugadas);
    }
    public float mejora2WinRate()
    {
        if (mejora2jugadas == 0) return 0.5f;
        return (mejora2ganadas / mejora2jugadas);
    }
    public float mejora3WinRate()
    {
        if (mejora3jugadas == 0) return 0.5f;
        return (mejora3ganadas / mejora3jugadas);
    }

    public float especial1WinRate()
    {
        if (especial1jugadas == 0) return 0.5f;
        return (especial1ganadas / especial1jugadas);
    }
    public float especial2WinRate()
    {
        if (especial2jugadas == 0) return 0.5f;
        return (especial2ganadas / especial2jugadas);
    }
    public float especial3WinRate()
    {
        if (especial3jugadas == 0) return 0.5f;
        return (especial3ganadas / especial3jugadas);
    }
}


