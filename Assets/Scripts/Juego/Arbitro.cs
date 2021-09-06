using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Arbitro : MonoBehaviour
{
    public static Arbitro instance;
    int turno = 0;


    public Player[] jugadores = new Player[2];

    public int[] jugadas = { 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 2, 2, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 2, 2, };


    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);

    }
    public void Init()
    {

        //TODO: Falta sistema de quien va primero en el online//
        //TODO: Sistema de seleccion de naves
        //TODO: Sistema de eleccion de heores y obtencion online de naves y heroes rivales

        //TEMPORAL------------------------------------------
        jugadores[0] = new LocalPlayer();
        jugadores[0].heroe = new HeroePlanetario();
        jugadores[0].fichas.Add(new ExploradorPlanetas());
        jugadores[0].fichas.Add(new NaveCombatePlanetasColonizadores());
        jugadores[0].fichas.Add(new InvestigadorPlanetas());
        jugadores[0].fichas.Add(new EstrategaPlanetas());
        jugadores[1] = jugadores[0];
        //TEMPORAL------------------------------------------

        jugadores[0].Init();
        //jugadores[1].Init();



        /*if(jugadores[0].heroe.turno > jugadores[i].heroe.turno)
         * {
         *      jugadas[11] = 0;
         *      jugadas[12] = 1;
         *      jugadas[23] = 0;
         *      jugadas[24] = 1;
         * }
         * 
         * else
         * {
         *     jugadas[11] = 1;
         *     jugadas[12] = 0;
         *     jugadas[23] = 1;
         *     jugadas[24] = 0;
         * 
         * }
        */

        Juega(jugadas[turno]);
        ++turno;
    }

    public void Juega(int i)
    {
        if(turno == 11 || turno == 12 || turno == 23 || turno == 24)
        {
            jugadores[i].Jugar(true);
        }
        else
        {
            jugadores[i].Jugar(false);
        }
    }

    public void Siguiente_Jugada()
    {
        Juega(jugadas[turno]);
        ++turno;
    }
}
