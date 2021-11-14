using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PiezasDataBase", order = 1)]
public class PiezasDataBase : ScriptableObject
{
    [SerializeField]
    GameObject
    //Mineros
    ExploradorMinero,
    CombateMinero,
    LaboratorioMinero,
    EstrategaMinero,

    //Mejoras
    ExploradorMineroMejorado,
    CombateMineroMejorado,
    LaboratorioMineroAstro,
    EstrategaMineroAstro,

    //Especiales
    Supernave,

    //Poderes
    Garrapata,

    //Oyentes
    Planetas,
    ExploradorOyentes,
    CombateOyentes,
    LaboratorioOyentes,
    EstrategaOyentes,

    //Mejoras
    CombateOyentesMejorado,
    LaboratorioOyentesMejorado,
    EstrategaOyentesMejorado,

    //Especiales
    Satelite,

    //Poderes
    PlanetaSagrado,
    Luna,
    AgujeroNegro;


    public GameObject GetPieza(IDPieza ID)
    {
        switch (ID)
        {
            case IDPieza.None:
                return null;  

            //Mineros
            case IDPieza.ExploradorMinero:
                return ExploradorMinero;
            case IDPieza.CombateMinero:
                return CombateMinero;
            case IDPieza.LaboratorioMinero:
                return LaboratorioMinero;
            case IDPieza.EstrategaMinero:
                return EstrategaMinero;

            //Mejoras
            case IDPieza.ExploradorMineroMejorado:
                return ExploradorMineroMejorado;
            case IDPieza.CombateMineroMejorado:
                return CombateMineroMejorado;
            case IDPieza.LaboratorioMineroAstro:
                return LaboratorioMineroAstro;
            case IDPieza.EstrategaMineroAstro:
                return EstrategaMineroAstro;

            //Especiales
            case IDPieza.Supernave:
                return Supernave;

            //Poderes
            case IDPieza.Garrapata:
                return Garrapata;

            //Oyentes
            case IDPieza.Planetas:
                return Planetas;
            case IDPieza.ExploradorOyentes:
                return ExploradorOyentes;
            case IDPieza.CombateOyentes:
                return CombateOyentes;
            case IDPieza.LaboratorioOyentes:
                return LaboratorioOyentes;
            case IDPieza.EstrategaOyentes:
                return EstrategaOyentes;

            //Mejoras
            case IDPieza.CombateOyentesMejorado:
                return CombateOyentesMejorado;
            case IDPieza.LaboratorioOyentesMejorado:
                return LaboratorioOyentesMejorado;
            case IDPieza.EstrategaOyentesMejorado:
                return EstrategaOyentesMejorado;

            //Especiales
            case IDPieza.Satelite:
                return Satelite;

            //Poderes
            case IDPieza.PlanetaSagrado:
                return PlanetaSagrado;
            case IDPieza.Luna:
                return Luna;
            case IDPieza.AgujeroNegro:
                return AgujeroNegro;
            default:
                Debug.LogError("Esta pieza no la entiendo");
                break;
        }
        return null;
    }
    public IDPieza GetPieza(GameObject Pieza)
    {
        string piezaName = Pieza.name.Split('(')[0];

        //Mineros
        if (piezaName == ExploradorMinero.name) return IDPieza.ExploradorMinero;
        else if (piezaName == CombateMinero.name) return IDPieza.CombateMinero;
        else if (piezaName == LaboratorioMinero.name) return IDPieza.LaboratorioMinero;
        else if (piezaName == EstrategaMinero.name) return IDPieza.EstrategaMinero;

        //Mejoras
        else if (piezaName == ExploradorMineroMejorado.name) return IDPieza.ExploradorMineroMejorado;
        else if (piezaName == CombateMineroMejorado.name) return IDPieza.CombateMineroMejorado;
        else if (piezaName == LaboratorioMineroAstro.name) return IDPieza.LaboratorioMineroAstro;
        else if (piezaName == EstrategaMineroAstro.name) return IDPieza.EstrategaMineroAstro;

        //Especiales
        else if (piezaName == Supernave.name) return IDPieza.Supernave;

        //Poderes
        else if (piezaName == Garrapata.name) return IDPieza.Garrapata;

        //Oyentes
        else if (piezaName == Planetas.name) return IDPieza.Planetas;
        else if (piezaName == ExploradorOyentes.name) return IDPieza.ExploradorOyentes;
        else if (piezaName == CombateOyentes.name) return IDPieza.CombateOyentes;
        else if (piezaName == LaboratorioOyentes.name) return IDPieza.LaboratorioOyentes;
        else if (piezaName == EstrategaOyentes.name) return IDPieza.EstrategaOyentes;

        //Mejoras
        else if (piezaName == CombateOyentesMejorado.name) return IDPieza.CombateOyentesMejorado;
        else if (piezaName == LaboratorioOyentesMejorado.name) return IDPieza.LaboratorioOyentesMejorado;
        else if (piezaName == EstrategaOyentesMejorado.name) return IDPieza.EstrategaOyentesMejorado;

        //Especiales
        else if (piezaName == Satelite.name) return IDPieza.Satelite;

        //Poderes
        else if (piezaName == PlanetaSagrado.name) return IDPieza.PlanetaSagrado;
        else if (piezaName == Luna.name) return IDPieza.Luna;
        else if (piezaName == AgujeroNegro.name) return IDPieza.AgujeroNegro;


        else Debug.LogError("Esta pieza no la entiendo");


        return IDPieza.None;
    }
}

public enum IDPieza
{
    None,

    //Mineros
    ExploradorMinero,
    CombateMinero,
    LaboratorioMinero,
    EstrategaMinero,

    //Mejoras
    ExploradorMineroMejorado,
    CombateMineroMejorado,
    LaboratorioMineroAstro,
    EstrategaMineroAstro,

    //Especiales
    Supernave,

    //Poderes
    Garrapata,

    //Oyentes
    Planetas,
    ExploradorOyentes,
    CombateOyentes,
    LaboratorioOyentes,
    EstrategaOyentes,

    //Mejoras
    CombateOyentesMejorado,
    LaboratorioOyentesMejorado,
    EstrategaOyentesMejorado,

    //Especiales
    Satelite,

    //Poderes
    PlanetaSagrado,
    Luna,
    AgujeroNegro
}
