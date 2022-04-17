using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DatosIA", order = 1)]
public class DatosIA : ScriptableObject
{
    [SerializeField] TuSeleccion IASeleccion, PlayerSelection;
    [SerializeField] OpcionesFaccion opcionesMinero, opcionesOyentes;
    public DatosIAFaccion Mineros, Oyentes, Honorables, Simbionte;

    public void AddData(bool win, bool doble = false)
    {
        switch (IASeleccion.faccion)
        {
            //Mineros
            case Faccion.minero:
                switch (PlayerSelection.faccion)
                {
                    //VS Oyentes
                    case Faccion.oyente:

                        Mineros.vsOyentes.jugadas++;
                        if (win) Mineros.vsOyentes.ganadas++;

                        //Heroes
                        if (IASeleccion.mi_poder == opcionesMinero.posibles_Poders[0])
                        {
                            Mineros.vsOyentes.heroe1jugadas++;
                            if (win) Mineros.vsOyentes.heroe1ganadas++;
                        }
                        else if (IASeleccion.mi_poder == opcionesMinero.posibles_Poders[1])
                        {
                            Mineros.vsOyentes.heroe2jugadas++;
                            if (win) Mineros.vsOyentes.heroe2ganadas++;
                        }
                        else if (IASeleccion.mi_poder == opcionesMinero.posibles_Poders[2])
                        {
                            Mineros.vsOyentes.heroe3jugadas++;
                            if (win) Mineros.vsOyentes.heroe3ganadas++;
                        }

                        //Especiales
                        if (IASeleccion.mis_opciones[4] == opcionesMinero.posibles_Piezas_Especiales[0])
                        {
                            Mineros.vsOyentes.especial1jugadas++;
                            if (win) Mineros.vsOyentes.especial1ganadas++;
                        }
                        else if (IASeleccion.mis_opciones[4] == opcionesMinero.posibles_Piezas_Especiales[1])
                        {
                            Mineros.vsOyentes.especial2jugadas++;
                            if (win) Mineros.vsOyentes.especial2ganadas++;
                        }
                        else if (IASeleccion.mis_opciones[4] == opcionesMinero.posibles_Piezas_Especiales[2])
                        {
                            Mineros.vsOyentes.especial3jugadas++;
                            if (win) Mineros.vsOyentes.especial3ganadas++;
                        }

                        if (doble)
                        {
                            Oyentes.vsMineros.jugadas++;
                            if (!win) Oyentes.vsMineros.ganadas++;

                            //Heroes
                            if (PlayerSelection.mi_poder == opcionesOyentes.posibles_Poders[0])
                            {
                                Oyentes.vsMineros.heroe1jugadas++;
                                if (!win) Oyentes.vsMineros.heroe1ganadas++;
                            }
                            else if (PlayerSelection.mi_poder == opcionesOyentes.posibles_Poders[1])
                            {
                                Oyentes.vsMineros.heroe2jugadas++;
                                if (!win) Oyentes.vsMineros.heroe2ganadas++;
                            }
                            else if (PlayerSelection.mi_poder == opcionesOyentes.posibles_Poders[2])
                            {
                                Oyentes.vsMineros.heroe3jugadas++;
                                if (!win) Oyentes.vsMineros.heroe3ganadas++;
                            }

                            //Especiales
                            if (PlayerSelection.mis_opciones[4] == opcionesOyentes.posibles_Piezas_Especiales[0])
                            {
                                Oyentes.vsMineros.especial1jugadas++;
                                if (!win) Oyentes.vsMineros.especial1ganadas++;
                            }
                            else if (PlayerSelection.mis_opciones[4] == opcionesOyentes.posibles_Piezas_Especiales[1])
                            {
                                Oyentes.vsMineros.especial2jugadas++;
                                if (!win) Oyentes.vsMineros.especial2ganadas++;
                            }
                            else if (PlayerSelection.mis_opciones[4] == opcionesOyentes.posibles_Piezas_Especiales[2])
                            {
                                Oyentes.vsMineros.especial3jugadas++;
                                if (!win) Oyentes.vsMineros.especial3ganadas++;
                            }

                            //Mejoras
                            if (PlayerSelection.mis_opciones[opcionesOyentes.huecos_Especializadas[0]] == opcionesOyentes.posibles_Piezas_Especializadas[0])
                            {
                                Oyentes.vsMineros.mejora1jugadas++;
                                if (!win) Oyentes.vsMineros.mejora1ganadas++;
                            }
                            else if (PlayerSelection.mis_opciones[opcionesOyentes.huecos_Especializadas[1]] == opcionesOyentes.posibles_Piezas_Especializadas[1])
                            {
                                Oyentes.vsMineros.mejora2jugadas++;
                                if (!win) Oyentes.vsMineros.mejora2ganadas++;
                            }
                            else if (PlayerSelection.mis_opciones[opcionesOyentes.huecos_Especializadas[2]] == opcionesOyentes.posibles_Piezas_Especializadas[2])
                            {
                                Oyentes.vsMineros.mejora3jugadas++;
                                if (!win) Oyentes.vsMineros.mejora3ganadas++;
                            }
                            break;
                        }

                        break;
                }
                break;

            //Oyentes
            case Faccion.oyente:
                switch (PlayerSelection.faccion)
                {
                    //VS Mineros
                    case Faccion.minero:
                        Oyentes.vsMineros.jugadas++;
                        if (win) Oyentes.vsMineros.ganadas++;

                        //Heroes
                        if (IASeleccion.mi_poder == opcionesOyentes.posibles_Poders[0])
                        {
                            Oyentes.vsMineros.heroe1jugadas++;
                            if (win) Oyentes.vsMineros.heroe1ganadas++;
                        }
                        else if (IASeleccion.mi_poder == opcionesOyentes.posibles_Poders[1])
                        {
                            Oyentes.vsMineros.heroe2jugadas++;
                            if (win) Oyentes.vsMineros.heroe2ganadas++;
                        }
                        else if (IASeleccion.mi_poder == opcionesOyentes.posibles_Poders[2])
                        {
                            Oyentes.vsMineros.heroe3jugadas++;
                            if (win) Oyentes.vsMineros.heroe3ganadas++;
                        }

                        //Especiales
                        if (IASeleccion.mis_opciones[4] == opcionesOyentes.posibles_Piezas_Especiales[0])
                        {
                            Oyentes.vsMineros.especial1jugadas++;
                            if (win) Oyentes.vsMineros.especial1ganadas++;
                        }
                        else if (IASeleccion.mis_opciones[4] == opcionesOyentes.posibles_Piezas_Especiales[1])
                        {
                            Oyentes.vsMineros.especial2jugadas++;
                            if (win) Oyentes.vsMineros.especial2ganadas++;
                        }
                        else if (IASeleccion.mis_opciones[4] == opcionesOyentes.posibles_Piezas_Especiales[2])
                        {
                            Oyentes.vsMineros.especial3jugadas++;
                            if (win) Oyentes.vsMineros.especial3ganadas++;
                        }

                        //Mejoras
                        if (IASeleccion.mis_opciones[opcionesOyentes.huecos_Especializadas[0]] == opcionesOyentes.posibles_Piezas_Especializadas[0])
                        {
                            Oyentes.vsMineros.mejora1jugadas++;
                            if (win) Oyentes.vsMineros.mejora1ganadas++;
                        }
                        else if (IASeleccion.mis_opciones[opcionesOyentes.huecos_Especializadas[1]] == opcionesOyentes.posibles_Piezas_Especializadas[1])
                        {
                            Oyentes.vsMineros.mejora2jugadas++;
                            if (win) Oyentes.vsMineros.mejora2ganadas++;
                        }
                        else if (IASeleccion.mis_opciones[opcionesOyentes.huecos_Especializadas[2]] == opcionesOyentes.posibles_Piezas_Especializadas[2])
                        {
                            Oyentes.vsMineros.mejora3jugadas++;
                            if (win) Oyentes.vsMineros.mejora3ganadas++;
                        }

                        if (doble)
                        {
                            Mineros.vsOyentes.jugadas++;
                            if (!win) Mineros.vsOyentes.ganadas++;

                            //Heroes
                            if (PlayerSelection.mi_poder == opcionesMinero.posibles_Poders[0])
                            {
                                Mineros.vsOyentes.heroe1jugadas++;
                                if (!win) Mineros.vsOyentes.heroe1ganadas++;
                            }
                            else if (PlayerSelection.mi_poder == opcionesMinero.posibles_Poders[1])
                            {
                                Mineros.vsOyentes.heroe2jugadas++;
                                if (!win) Mineros.vsOyentes.heroe2ganadas++;
                            }
                            else if (PlayerSelection.mi_poder == opcionesMinero.posibles_Poders[2])
                            {
                                Mineros.vsOyentes.heroe3jugadas++;
                                if (!win) Mineros.vsOyentes.heroe3ganadas++;
                            }

                            //Especiales
                            if (PlayerSelection.mis_opciones[4] == opcionesMinero.posibles_Piezas_Especiales[0])
                            {
                                Mineros.vsOyentes.especial1jugadas++;
                                if (!win) Mineros.vsOyentes.especial1ganadas++;
                            }
                            else if (PlayerSelection.mis_opciones[4] == opcionesMinero.posibles_Piezas_Especiales[1])
                            {
                                Mineros.vsOyentes.especial2jugadas++;
                                if (!win) Mineros.vsOyentes.especial2ganadas++;
                            }
                            else if (PlayerSelection.mis_opciones[4] == opcionesMinero.posibles_Piezas_Especiales[2])
                            {
                                Mineros.vsOyentes.especial3jugadas++;
                                if (!win) Mineros.vsOyentes.especial3ganadas++;
                            }
                        }
                        break;

                }
                break;
        }
    }

    public void ResetData()
    {
        Mineros = new DatosIAFaccion();
        Oyentes = new DatosIAFaccion();
        Honorables = new DatosIAFaccion();
        Simbionte = new DatosIAFaccion();
    }
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

    public int BestHeroe(int posibleRandom = 0)
    {
        float bestWR = 0;
        int bestHeroe = -1;
        bestWR = heroe1WinRate();
        bestHeroe = 0;

        if(heroe2WinRate() >= bestWR)
        {
            bestWR = heroe2WinRate();
            if (heroe2WinRate() > bestWR || Random.Range(0, 2) == 0) bestHeroe = 1;
        }

        if (heroe3WinRate() >= bestWR)
        {
            bestWR = heroe3WinRate();
            if (heroe3WinRate() > bestWR || Random.Range(0, 2) == 0) bestHeroe = 2;
        }

        if (posibleRandom != 0 && Random.Range(0, 100) < posibleRandom) bestHeroe = Random.Range(0, 3);
        return bestHeroe;
    }

    public int BestEspecial(int posibleRandom = 0)
    {
        float bestWR = 0;
        int bestEspecial = -1;
        bestWR = especial1WinRate();
        bestEspecial = 0;

        if (especial2WinRate() >= bestWR)
        {
            bestWR = especial2WinRate();
            if (especial2WinRate() > bestWR || Random.Range(0, 2) == 0) bestEspecial = 1;
        }

        if (especial3WinRate() >= bestWR)
        {
            bestWR = especial3WinRate();
            if (especial3WinRate() > bestWR || Random.Range(0, 2) == 0) bestEspecial = 2;
        }

        if (posibleRandom != 0 && Random.Range(0, 100) < posibleRandom) bestEspecial = Random.Range(0, 3);
        return bestEspecial;
    }

    public int BestMejora(int posibleRandom = 0)
    {
        float bestWR = 0;
        int bestMejora = -1;
        bestWR = mejora1WinRate();
        bestMejora = 0;

        if (mejora2WinRate() >= bestWR)
        {
            bestWR = mejora2WinRate();
            if (mejora2WinRate() > bestWR || Random.Range(0, 2) == 0) bestMejora = 1;
        }

        if (mejora2WinRate() >= bestWR)
        {
            bestWR = mejora2WinRate();
            if (mejora2WinRate() > bestWR || Random.Range(0, 2) == 0) bestMejora = 2;
        }

        if (posibleRandom != 0 && Random.Range(0, 100) < posibleRandom) bestMejora = Random.Range(0, 3);
        return bestMejora;
    }
}


/*
[CustomEditor(typeof(DatosIA))]
public class TestScriptableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (DatosIA)target;

        if (GUILayout.Button("Reset Values", GUILayout.Height(20)))
        {
            script.ResetData();
        }

    }
}
*/


