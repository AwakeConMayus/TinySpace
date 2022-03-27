using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TextoEsplicativo", order = 1)]
public class TextoExplicativo : ScriptableObject
{
    [SerializeField]
    string  mineral,
            heroeMaquinista, heroeMecanico, heroeChantajista,
            heroeColono, heroeAstrofisico, heroeLunatico,
            explorador, combate, laboratorio, estratega,
            exploradorMejora, combateMejora, laboratorioMejora, estrategaMejora,
            combateColonizador, laboratorioTerraformador, estrategaOrbital,
            comodin, modeloPerfeccionado, supernave,
            planeta, propulsorOrbital, terraformacion,
            sol, planetaHelado, planetaVolcanico, planetaSagrdo,
            luna, agujeroNegro;


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

            if (prefab.GetComponent<InvestigadorPlanetasTerraformador>())
            {
                texto = laboratorioTerraformador;
            }
        }

        else if (prefab.GetComponent<InvestigadorMinerosMejorado>())
        {
            texto = laboratorioMejora;
        }

        else if (prefab.GetComponent<Estratega>())
        {
            texto = estratega;

            if (prefab.GetComponent<EstrategaPlanetasCuartelesOrbitales>())
            {
                texto = estrategaOrbital;
            }
        }

        else if (prefab.GetComponent<EstrategaMinerosMejorada>())
        {
            texto = estrategaMejora;
        }

        else if (prefab.GetComponent<Comodin>())
        {
            texto = comodin;
        }

        else if (prefab.GetComponent<ModeloPerfecionadoMineros>())
        {
            texto = modeloPerfeccionado;
        }

        else if (prefab.GetComponent<SuperNave>())
        {
            texto = supernave;
        }

        else if (prefab.GetComponent<Planetas>())
        {
            texto = planeta;

            if (prefab.GetComponent<Sol>())
            {
                texto = sol;
            }
            else if (prefab.GetComponent<PlanetaHelado>())
            {
                texto = planetaHelado;
            }
            else if (prefab.GetComponent<PlanetaVolcanico>())
            {
                texto = planetaVolcanico;
            }
            else if (prefab.GetComponent<PlanetaSagrado>())
            {
                texto = planetaSagrdo;
            }
        }

        else if (prefab.GetComponent<PropulsorCambioOrbital>())
        {
            texto = propulsorOrbital;
        }

        else if (prefab.GetComponent<Terraformar>())
        {
            texto = terraformacion;
        }

        else if (prefab.GetComponent<AgujeroNegro>())
        {
            texto = agujeroNegro;
        }

        else if (prefab.GetComponent<Luna>())
        {
            texto = luna;
        }

        else if (prefab.GetComponent<Poder>())
        {
            if (prefab.GetComponent<PoderMaquinista>())
            {
                texto = heroeMaquinista;
            }
            else if (prefab.GetComponent<PoderMecanico>())
            {
                texto = heroeMecanico;
            }
            else if (prefab.GetComponent<PoderChantajista2>())
            {
                texto = heroeChantajista;
            }
            else if (prefab.GetComponent<PoderColono>())
            {
                texto = heroeColono;
            }
            else if (prefab.GetComponent<PoderAstrofisico>())
            {
                texto = heroeAstrofisico;
            }
            else if (prefab.GetComponent<PoderLunatico>())
            {
                texto = heroeLunatico;
            }
        }

        else if (prefab.GetComponent<TextMineral>())
        {
            texto = mineral;
        }

        texto = EditTextos(texto);
        return texto;
    }
            
    string EditTextos(string s)
    {
        return s.Replace("<n>", "\n");
    }
}
