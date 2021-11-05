using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PiezasDataBase", order = 1)]
public class DatosIA : ScriptableObject
{
    DatosIAFaccion Mineros, Oyentes, Honorables, Simbionte;
}

public struct DatosIAFaccion
{
    public DatosVS vsMineros, vsOyentes, vsHonorables, vsSimbionte;
}

public struct DatosVS
{
    public int jugadas, ganadas;

    public int heroe1jugadas, heroe1ganadas;
    public int heroe2jugadas, heroes2ganadas;
    public int heroe3jugadas, heroe3ganadas;

    public int mejora1jugadas, mejora1ganadas;
    public int mejora2jugadas, mejora2ganadas;
    public int mejora3jugadas, mejora3ganadas;

    public int especial1jugadas, especial1ganadas;
    public int especial2jugadas, especial2ganadas;
    public int especial3jugadas, especial3ganadas;
}


