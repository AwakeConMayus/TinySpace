using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DatosIA", order = 1)]
public class DatosIA : ScriptableObject
{
    [SerializeField] TuSeleccion IASeleccion, PlayerSelection;
    public DatosIAFaccion Mineros, Oyentes, Honorables, Simbionte;

    public void AddData(bool win)
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
                        if (IASeleccion.mi_poder == Resources.Load<GameObject>("Maquinista"))
                        {
                            Mineros.vsOyentes.heroe1jugadas++;
                            if (win) Mineros.vsOyentes.heroe1ganadas++;
                        }
                        else if (IASeleccion.mi_poder == Resources.Load<GameObject>("Mecánico"))
                        {
                            Mineros.vsOyentes.heroe2jugadas++;
                            if (win) Mineros.vsOyentes.heroe2ganadas++;
                        }
                        else if (IASeleccion.mi_poder == Resources.Load<GameObject>("Chantajista"))
                        {
                            Mineros.vsOyentes.heroe3jugadas++;
                            if (win) Mineros.vsOyentes.heroe3ganadas++;
                        }

                        //Especiales
                        if (IASeleccion.mis_opciones[4] == Resources.Load<GameObject>("Comodin"))
                        {
                            Mineros.vsOyentes.especial1jugadas++;
                            if (win) Mineros.vsOyentes.especial1ganadas++;
                        }
                        else if (IASeleccion.mis_opciones[4] == Resources.Load<GameObject>("Modelo Perfeccionado"))
                        {
                            Mineros.vsOyentes.especial2jugadas++;
                            if (win) Mineros.vsOyentes.especial2ganadas++;
                        }
                        else if (IASeleccion.mis_opciones[4] == Resources.Load<GameObject>("Supernave"))
                        {
                            Mineros.vsOyentes.especial3jugadas++;
                            if (win) Mineros.vsOyentes.especial3ganadas++;
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
                        if (IASeleccion.mi_poder == Resources.Load<GameObject>("Colono"))
                        {
                            Oyentes.vsMineros.heroe1jugadas++;
                            if (win) Oyentes.vsMineros.heroe1ganadas++;
                        }
                        else if (IASeleccion.mi_poder == Resources.Load<GameObject>("Astrofísico"))
                        {
                            Oyentes.vsMineros.heroe2jugadas++;
                            if (win) Oyentes.vsMineros.heroe2ganadas++;
                        }
                        else if (IASeleccion.mi_poder == Resources.Load<GameObject>("Lunático"))
                        {
                            Oyentes.vsMineros.heroe3jugadas++;
                            if (win) Oyentes.vsMineros.heroe3ganadas++;
                        }

                        //Especiales
                        if (IASeleccion.mis_opciones[4] == Resources.Load<GameObject>("Planeta"))
                        {
                            Oyentes.vsMineros.heroe1jugadas++;
                            if (win) Oyentes.vsMineros.heroe1ganadas++;
                        }
                        else if (IASeleccion.mis_opciones[4] == Resources.Load<GameObject>("Propulsor de Cambio Orbital"))
                        {
                            Oyentes.vsMineros.heroe2jugadas++;
                            if (win) Oyentes.vsMineros.heroe2ganadas++;
                        }
                        else if (IASeleccion.mis_opciones[4] == Resources.Load<GameObject>("Terraformador"))
                        {
                            Oyentes.vsMineros.heroe3jugadas++;
                            if (win) Oyentes.vsMineros.heroe3ganadas++;
                        }

                        //Mejoras
                        if (IASeleccion.mis_opciones[1] == Resources.Load<GameObject>("Colonizadores de combate +"))
                        {
                            Oyentes.vsMineros.heroe1jugadas++;
                            if (win) Oyentes.vsMineros.heroe1ganadas++;
                        }
                        else if (IASeleccion.mis_opciones[2] == Resources.Load<GameObject>("Laboratorio de Terraformacion +"))
                        {
                            Oyentes.vsMineros.heroe2jugadas++;
                            if (win) Oyentes.vsMineros.heroe2ganadas++;
                        }
                        else if (IASeleccion.mis_opciones[3] == Resources.Load<GameObject>("Cuartel estratega orbital +"))
                        {
                            Oyentes.vsMineros.heroe3jugadas++;
                            if (win) Oyentes.vsMineros.heroe3ganadas++;
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
}



/*[CustomEditor(typeof(DatosIA))]
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
}*/


