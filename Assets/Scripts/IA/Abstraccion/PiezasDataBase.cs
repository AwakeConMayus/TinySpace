using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PiezasDataBase", order = 1)]
public class PiezasDataBase : ScriptableObject
{
    [SerializeField]
    GameObject
    ExploradorMinero,
    CombateMinero,
    LaboratorioMinero,
    EstrategaMinero,

    //Mejoras
    ExploradorMineroMejorado,
    CombateMineroMejorado,
    LaboratorioMineroAstro,
    EstrategaMineroAstro,

    //Oyentes
    Planetas,
    ExploradorOyentes,
    CombateOyentes,
    LaboratorioOyentes,
    EstrategaOyentes,

    //Mejoras
    CombateOyentesMejorado,

    //Poderes
    PlanetaSagrado;


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

            //Poderes
            case IDPieza.PlanetaSagrado:
                return PlanetaSagrado;
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

        //Oyentes
        else if (piezaName == Planetas.name) return IDPieza.Planetas;
        else if (piezaName == ExploradorOyentes.name) return IDPieza.ExploradorOyentes;
        else if (piezaName == CombateOyentes.name) return IDPieza.CombateOyentes;
        else if (piezaName == LaboratorioOyentes.name) return IDPieza.LaboratorioOyentes;
        else if (piezaName == EstrategaOyentes.name) return IDPieza.EstrategaOyentes;

        //Mejoras
        else if (piezaName == CombateOyentesMejorado.name) return IDPieza.CombateOyentesMejorado;

        //Poderes
        else if (piezaName == PlanetaSagrado.name) return IDPieza.PlanetaSagrado;

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

    //Oyentes
    Planetas,
    ExploradorOyentes,
    CombateOyentes,
    LaboratorioOyentes,
    EstrategaOyentes,

    //Mejoras
    CombateOyentesMejorado,

    //Poderes
    PlanetaSagrado
}
