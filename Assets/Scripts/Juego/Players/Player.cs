using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player 
{
    public Heroe heroe;

    public abstract void Init();

    public abstract void Jugar(bool tocaPoder);

    public List<Pieza> fichas = new List<Pieza>();



}
