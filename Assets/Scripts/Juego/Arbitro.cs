using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbitro : MonoBehaviour
{
    public Opciones player;
    bool active;
    bool specialActive;

    bool specialPhase = true;
    int turno0 = 0;
    int turno1 = 0;

    bool inputActive = false;

    private void Start()
    {
        EventManager.StartListening("AccionTerminada", NextTurn);
    }

    public void SetInitial(bool initialPlayer)
    {
        active = specialActive = initialPlayer;
        if (initialPlayer) player.jugador = 0;
        else player.jugador = 1;
    }

    public void NextTurn()
    {
        if (specialPhase) SpecialTurn();
        else Turn();
    }

    void SpecialTurn()
    {
        if (!specialActive)
        {
            if (inputActive) SwitchActive();
            return;
        }
        if(turno0 % 2 != 0)
        {
            specialPhase = false;
        }
        if(++turno0 % 2 != 0)
        {
            specialActive = !specialActive;
        }
    }

    void Turn()
    {
        if(turno1 >= 19)
        {
            EndGame();
        }
        if (!active)
        {
            if (inputActive) SwitchActive();
            return;
        }
        if((turno1+1) % 10 == 0)
        {
            specialPhase = true;
        }
        if (++turno1 % 2 == 0)
        {
            active = !active;
        }
    }

    void SwitchActive()
    {
        inputActive = player.active = !inputActive;
    }

    void EndGame()
    {
        Debug.Log("NO HAY FINAL, EMPIEZAN LOS ERRORES :)");
    }
}
