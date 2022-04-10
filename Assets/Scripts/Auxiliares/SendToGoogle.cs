﻿using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
#pragma warning disable CS0618 //* Uso del método WWW en vez de UnityWebRequest puesto que este primero permite subir data binario a google forms

// En esta url se explica de forma menos específica que como obtengo las entries para el formulario https://ninest.vercel.app/html/google-forms-embed

public class SendToGoogle : MonoBehaviour
{
    //* Tiene que ser true para que se envíen los datos
    private bool SendingDataOnline = true;


    //Variables

    //Generales
    string Ganador;

    //Facciones Escogida
    string Faccion1, Faccion2;

    //Partida con IA
    string PartidaConIA;

    //Elecciones
    int Maquinista, Mecanico, Chantajista, Comodin, ModelPerfeccionado, Supernave;
    int Colono, Lunatico, Astrofisico, Combate, Laboratorio, Estratega, Planeta, Terraformador, CambioOrbital;


    int VectorMineros, VectorOyentes;



    public static SendToGoogle instance;

    bool IA = false;
    Faccion inicial = Faccion.none;
    [SerializeField] TuSeleccion miSeleccion, SeleccionRival;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        
    }    

    //* Dirección del formulario al que se suben los datos, para obtenerla lo previsualizamos y copiamos su url, cambiando el final de "viewform" a "formResponse"
    const string BASE_URL = "https://docs.google.com/forms/d/e/1FAIpQLSdmH1u9Py0CUHifpeVFgVl-14fxbuQTHJLQiXwpDzSehkgYJw/formResponse";
    WWWForm form;

    //* Función que es llamada al finalizar el juego aquí se inicia la corrutina que subirá los datos
    public void SendOnline(Faccion _inicial, bool _IA = false, List<int[]> vectores = null)
    {
        inicial = _inicial;
        IA = _IA;
        if (vectores != null)
        {
            foreach (int[] a in vectores)
            {
                if (a.Length == 3)
                {
                    VectorMineros += a[2];
                    VectorMineros += a[1] * 10;
                    VectorMineros += a[0] * 100;
                    VectorMineros += 1000000;

                }
                else if (a.Length == 5)
                {
                    VectorOyentes += a[4];
                    VectorOyentes += a[3] * 10;
                    VectorOyentes += a[2] * 100;
                    VectorOyentes += a[1] * 1000;
                    VectorOyentes += a[0] * 10000;
                    VectorOyentes += 1000000;
                }
                else
                {
                    Debug.Log("HAY UN VECTOR QUE SE HA COJIDO MAL");
                }
            }
        }
        if (SendingDataOnline) StartCoroutine(Upload());
    }

    //* Corrutina que sube los datos a la red
    private IEnumerator Upload()
    {
        rellenarFormulario();

        //* Guarda de forma binaria los datos del formulario y los adjunta a una url que sirve para responder el formulario, que después genera el excel en base a sus respuestas
        byte[] rawData = form.data;
        WWW url = new WWW(BASE_URL, rawData);

        yield return url;
    }

    //* Introduce la información en el formulario que crea
    void rellenarFormulario()
    {
        BuscarDatos();
       
        form = new WWWForm();

        //* Crea un nuevo formulario al que le añade los fields correspondientes a los del formulario de google forms, para obtener el dato entry.XXXXXX debes de:
        //* Pulsar inspeccionar elemento sobre la visualización del formulario y en el código de la web buscar con ctrl f el siguiente término: FB_PUBLIC_LOAD_DATA_
        //* Ahí debes coger el número grande poco después de la variable del formulario a la que quieras asociar el field, por ejemplo: (...)"PuntosMineros",null,0,[[435469167,(...)"PuntosPlanetarios",null,0,[[1370920118,(...)
        //* Una vez tengas ese numero, debes ponerlo junto a entry. por lo tanto para asociar el field PuntosMineros a la Score1, el entry será "entry.435469167"

        form.AddField("entry.1671880236", Ganador);
        form.AddField("entry.1747506782", Faccion1);
        form.AddField("entry.14402550", Faccion2);
        form.AddField("entry.572210542", PartidaConIA);

        form.AddField("entry.864094340", Maquinista);
        form.AddField("entry.1172698563", Mecanico);
        form.AddField("entry.1792845175", Chantajista);
        form.AddField("entry.725644111", Comodin);

        form.AddField("entry.1313404459", ModelPerfeccionado);
        form.AddField("entry.241310762", Supernave);

        form.AddField("entry.1367708030", Colono);
        form.AddField("entry.1263929892", Lunatico);
        form.AddField("entry.550311028", Astrofisico);

        form.AddField("entry.1474848063", Combate);
        form.AddField("entry.758557112", Laboratorio);
        form.AddField("entry.747600312", Estratega);
        form.AddField("entry.901872699", Planeta);
        form.AddField("entry.1834529338", Terraformador);

        form.AddField("entry.1328728453", CambioOrbital);

        form.AddField("entry.2078574632", VectorMineros);
        form.AddField("entry.24745043", VectorOyentes);





    }


    //* Obtiene la información que introducirá en el formulario
    void BuscarDatos()
    {
        //Variables Auxiliares

        //Partida IA
        if (IA) PartidaConIA = "SI";
        else PartidaConIA = "NO";

        //Ganador
        int[] puntosFinal = Tablero.instance.RecuentoPuntos();

        int bestPuntos = int.MinValue;
        int winner = -1;

        {
            for (int i = 0; i < puntosFinal.Length; i++)
            {
                if (puntosFinal[i] == bestPuntos) winner = -1;
                else if(puntosFinal[i] > bestPuntos)
                {
                    bestPuntos = puntosFinal[i];
                    winner = i;
                }
            }
        }

        
        Ganador = ((Faccion)(winner + 1)).ToString();

        //Facciones
        Faccion1 = inicial.ToString();

        Faccion noInicial = Faccion.none;

        if(miSeleccion.faccion == inicial)
        {
            Faccion2 = SeleccionRival.faccion.ToString();
        }
        else
        {
            Faccion2 = miSeleccion.faccion.ToString();
        }
        /*
        for (int i = 0; i < 2; i++)
        {
            
            //Clanta: el parseo ese del if no tiene sentido, si me equivoco que alguien me lo diga.
            Debug.Log(puntosFinal[i]);
            Debug.Log((Faccion)(puntosFinal[i] + 1));
            if(puntosFinal[i] != 0 && inicial != (Faccion)(puntosFinal[i] + 1))
            {
                noInicial = (Faccion)(i + 1);
                Faccion2 = noInicial.ToString();
            }


        }*/


        //Elecciones       

        //Heroes

        //Mineros
        if (miSeleccion.mi_poder == Resources.Load<GameObject>("Maquinista") || SeleccionRival.mi_poder == Resources.Load<GameObject>("Maquinista")) Maquinista = 1;
        else if (miSeleccion.mi_poder == Resources.Load<GameObject>("Mecánico") || SeleccionRival.mi_poder == Resources.Load<GameObject>("Mecánico")) Mecanico = 1;
        else if (miSeleccion.mi_poder == Resources.Load<GameObject>("Chantajista") || SeleccionRival.mi_poder == Resources.Load<GameObject>("Chantajista")) Chantajista = 1;

        //Oyentes
        if (miSeleccion.mi_poder == Resources.Load<GameObject>("Colono") || SeleccionRival.mi_poder == Resources.Load<GameObject>("Colono")) Colono = 1;
        else if (miSeleccion.mi_poder == Resources.Load<GameObject>("Lunático") || SeleccionRival.mi_poder == Resources.Load<GameObject>("Lunático")) Lunatico = 1;
        else if (miSeleccion.mi_poder == Resources.Load<GameObject>("Astrofísico") || SeleccionRival.mi_poder == Resources.Load<GameObject>("Astrofísico")) Astrofisico = 1;


        //Especiales

        //Mineros
        if (miSeleccion.mis_opciones[4] == Resources.Load<GameObject>("Comodin") || SeleccionRival.mis_opciones[4] == Resources.Load<GameObject>("Comodin")) Comodin = 1;
        else if (miSeleccion.mis_opciones[4] == Resources.Load<GameObject>("Modelo Perfeccionado") || SeleccionRival.mis_opciones[4] == Resources.Load<GameObject>("Modelo Perfeccionado")) ModelPerfeccionado = 1;
        else if (miSeleccion.mis_opciones[4] == Resources.Load<GameObject>("Supernave") || SeleccionRival.mis_opciones[4] == Resources.Load<GameObject>("Supernave")) Supernave = 1;

        //Oyentes
        if (miSeleccion.mis_opciones[4] == Resources.Load<GameObject>("Planeta") || SeleccionRival.mis_opciones[4] == Resources.Load<GameObject>("Planeta")) Comodin = 1;
        else if (miSeleccion.mis_opciones[4] == Resources.Load<GameObject>("Terraformador") || SeleccionRival.mis_opciones[4] == Resources.Load<GameObject>("Terraformador")) Terraformador = 1;
        else if (miSeleccion.mis_opciones[4] == Resources.Load<GameObject>("Propulsor de Cambio Orbital") || SeleccionRival.mis_opciones[4] == Resources.Load<GameObject>("Propulsor de Cambio Orbital")) CambioOrbital = 1;


        //Mejoras

        //Oyentes
        if (miSeleccion.mis_opciones[1] == Resources.Load<GameObject>("Colonizadores de combate +") || SeleccionRival.mis_opciones[1] == Resources.Load<GameObject>("Colonizadores de combate +")) Combate = 1;
        else if (miSeleccion.mis_opciones[2] == Resources.Load<GameObject>("Laboratorio de Terraformación +") || SeleccionRival.mis_opciones[2] == Resources.Load<GameObject>("Laboratorio de Terraformación +")) Laboratorio = 1;
        else if (miSeleccion.mis_opciones[3] == Resources.Load<GameObject>("Cuartel estratega orbital +") || SeleccionRival.mis_opciones[3] == Resources.Load<GameObject>("Cuartel estratega orbital +")) Estratega = 1;
    }
}
