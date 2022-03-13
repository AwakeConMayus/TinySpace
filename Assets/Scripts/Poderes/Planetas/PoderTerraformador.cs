using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PoderTerraformador : PoderPlanetas
{
    GameObject comodinTerraformar;

    bool terraformando = false;
    bool setPLaneta = false;
    int planetasPuestos = 0;
    [SerializeField] int numeroPlanetasFase;
    private void Awake()
    {
        EventManager.StartListening("ClickCasilla", ColocarMenuTerraformador);
        EventManager.StartListening("ClickCasilla", PlacePlaneta);
    }


    public override void FirstActionPersonal()
    {
        List<Casilla> casillasPosibles = planeta.GetComponent<Pieza>().CasillasDisponibles();
        if (casillasPosibles.Count == 0)
        {
            EventManager.TriggerEvent("AccionTerminadaConjunta");
            return;
        }
        Debug.Log(casillasPosibles.Count);
        Tablero.instance.ResetCasillasEfects();
        foreach (Casilla casilla in casillasPosibles) casilla.SetState(States.select);

        setPLaneta = true;
    }

    public override void SecondAction()
    {
        print("secondAction");
        comodinTerraformar = Resources.Load<GameObject>("Comodin Terraformador");
        List<Casilla> casillasPosibles = new List<Casilla>();
        casillasPosibles = comodinTerraformar.GetComponent<Pieza>().CasillasDisponibles();
        print(casillasPosibles.Count);


        Tablero.instance.ResetCasillasEfects();
        foreach (Casilla casilla in casillasPosibles) casilla.SetState(States.select);

        terraformando = true;

    }



    void ColocarMenuTerraformador()
    {
        if (!terraformando) return;
        Casilla c = ClickCasillas.casillaClick;
        List<Casilla> casillasPosibles = new List<Casilla>();
        casillasPosibles = comodinTerraformar.GetComponent<Pieza>().CasillasDisponibles();
        if (casillasPosibles.Contains(c))
        {
            
            if (PhotonNetwork.InRoom)
            {
                 PhotonNetwork.Instantiate(comodinTerraformar.name, c.transform.position, Quaternion.identity);
            }
            else
            {
                 Instantiate(comodinTerraformar, c.transform.position, Quaternion.identity);
            }


            terraformando = false;
            Tablero.instance.ResetCasillasEfects();
        }

    }

    void PlacePlaneta()
    {
        if (!setPLaneta) return;
        Casilla c = ClickCasillas.casillaClick;
        List<Casilla> casillasPosibles = planeta.GetComponent<Pieza>().CasillasDisponibles();
        if (casillasPosibles.Count == 0)
        {
            EventManager.TriggerEvent("AccionTerminadaConjunta");
            return;
        }
        if (casillasPosibles.Contains(c))
        {
            GameObject thisPieza;
            if (PhotonNetwork.InRoom)
            {
                thisPieza = PhotonNetwork.Instantiate(planeta.name, c.transform.position, Quaternion.identity);
            }
            else
            {
                thisPieza = Instantiate(planeta, c.transform.position, Quaternion.identity);
            }
            thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
            thisPieza.GetComponent<Pieza>().casilla = c;
            c.pieza = thisPieza.GetComponent<Pieza>();
            setPLaneta = false;
            if (planetasPuestos < numeroPlanetasFase-2)
            {
                ++planetasPuestos;
                FirstActionPersonal();
                Debug.Log("lunas puestas: " + planetasPuestos);
                Debug.Log("LUNas maximas: " + numeroPlanetasFase);
            }
            else
            {
                planetasPuestos = 0;
                Tablero.instance.ResetCasillasEfects();
                EventManager.TriggerEvent("AccionTerminadaConjunta");
            }
        }
    }

}
