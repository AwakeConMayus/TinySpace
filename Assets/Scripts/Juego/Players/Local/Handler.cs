using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandlerStep
{
    seleccionpieza,
    seleccioncasilla
}
public class Handler : MonoBehaviour
{
    public static Handler instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    Pieza piezaSeleccionada;
    List<Casilla> casillasDisponibles;
    Casilla casillaSeleccionada;

    List<Instruccion> instrucciones = new List<Instruccion>();
    int instruccion;
    List<HandlerStep> steps = new List<HandlerStep>();

    public void AddStep(HandlerStep step)
    {
        steps.Add(step);
    }

    public void Ejecutar(Casilla casilla)
    {
        if(steps[0] == HandlerStep.seleccioncasilla)
        {

        }
    }
}

public class Instruccion
{
    public bool add;
    public Pieza pieza;
    public Casilla casilla;
}
