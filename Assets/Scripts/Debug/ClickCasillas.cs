﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCasillas : MonoBehaviour
{
    Camera MainCamera;
    Casilla CasillaHit;
    InstancePiezas instanciator;
    ColorearCasillas coloreador;

    private void Start()
    {
        MainCamera = Camera.main;
        instanciator = GetComponent<InstancePiezas>();
        coloreador = GetComponent<ColorearCasillas>();

    }

    void Update()
    {
       //* Al pulsar click izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            //* Genera un rayo a donde clickes con el mouse
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            ///Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);

            //* Si el rayo choca con un objeto, obtiene su componente casilla e instancia la ficha que hayamos seleccionado en los botones en su posición
            if (Physics.Raycast(ray, out hit))
            {
                ///Debug.Log(hit.collider);
                CasillaHit = hit.collider.GetComponent<Casilla>();
                instanciator.CrearPieza(CasillaHit);

                //coloreador.reColor("yellow", CasillaHit);

            }
        } 
    }


    

}
