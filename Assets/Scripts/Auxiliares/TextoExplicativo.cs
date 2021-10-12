using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TextoEsplicativo", order = 1)]
public class TextoExplicativo : ScriptableObject
{
    [SerializeField]
    string  mineral,
            heroeMaquinista,
            heroeColono,
            explorador, combate, laboratorio, estratega,
            exploradorMejora, combateMejora, laboratorioMejora, estrategaMejora,
            combateColonizador,
            comodin,
            planeta;


    public string GetTexto(GameObject prefab)
    {
        string texto = "";

        if (prefab.GetComponent<Explorador>())
        {
            texto = explorador;

            if (prefab.GetComponent<ExploradorMineroMejorado>())
            {
                texto = exploradorMejora;
            }
        }

        else if(prefab.GetComponent<NaveCombate>())
        {
            texto = combate;

            if (prefab.GetComponent<NaveCombateMinerosMejorada>())
            {
                texto = combateMejora;
            }
            else if (prefab.GetComponent<NaveCombatePlanetasColonizadores>())
            {
                texto = combateColonizador;
            }
        }

        else if (prefab.GetComponent<Investigador>())
        {
            texto = laboratorio;

            if (prefab.GetComponent<InvestigadorMinerosMejorado>())
            {
                texto = laboratorioMejora;
            }
        }

        else if (prefab.GetComponent<Estratega>())
        {
            texto = estratega;

            if (prefab.GetComponent<EstrategaMinerosMejorada>())
            {
                texto = estrategaMejora;
            }
        }

        else if (prefab.GetComponent<Comodin>())
        {
            texto = comodin;
        }

        else if (prefab.GetComponent<Planetas>())
        {
            texto = planeta;
        }

        else if (prefab.GetComponent<Poder>())
        {
            if (prefab.GetComponent<PoderMaquinista>())
            {
                texto = heroeMaquinista;
            }
            else if (prefab.GetComponent<PoderColono>())
            {
                texto = heroeColono;
            }
        }

        else if (prefab.GetComponent<TextMineral>())
        {
            texto = mineral;
        }

        return texto;
    }
            
}
