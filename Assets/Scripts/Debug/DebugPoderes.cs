using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPoderes : MonoBehaviour
{
    [SerializeField]
    List<GameObject> poderes;

    Poder poderActivo;

    private void Awake()
    {
        SetPoder(0);
    }

    public void SetPoder(int i)
    {
        poderActivo = poderes[i].GetComponent<Poder>();
    }

    public void Action0()
    {
        poderActivo.InitialAction();
    }
    public void Action1()
    {
        poderActivo.FirstAction();
    }
    public void Action2()
    {
        poderActivo.SecondAction();
    }
}
