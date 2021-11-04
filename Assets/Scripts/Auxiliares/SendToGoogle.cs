﻿using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
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
    string PartidaConIA = "NO";

    //Elecciones
    int Maquinista, Mecanico, Chantajista, Comodin, ModelPerfeccionado, Supernave;
    int Colono, Lunatico, Astrofisico, Combate, Laboratorio, Estratega, Planeta, Satelite, CambioOrbital;





    public static SendToGoogle instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);

        arbitroPartida = GetComponent<Arbitro>();
    }

    Arbitro arbitroPartida;

    //* Dirección del formulario al que se suben los datos, para obtenerla lo previsualizamos y copiamos su url, cambiando el final de "viewform" a "formResponse"
    const string BASE_URL = "https://docs.google.com/forms/d/e/1FAIpQLSf2Ht626ha_IM_aG0_wbf6DPJNyruJ82Hi0im_LdYgfjP-RvA/formResponse";
    WWWForm form;

    //* Función que es llamada al finalizar el juego aquí se inicia la corrutina que subirá los datos
    public void SendOnline()
    {
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

        form.AddField("entry.952161928",  Ganador);
        form.AddField("entry.917130770",  Faccion1);
        form.AddField("entry.1479737980", Faccion2);
        form.AddField("entry.940617184",  PartidaConIA);

        form.AddField("entry.724103590",  Maquinista);   
        form.AddField("entry.1738738836", Mecanico);  
        form.AddField("entry.1171462077", Chantajista);  
        form.AddField("entry.2125969092", Comodin);  
        form.AddField("entry.1391571527", ModelPerfeccionado);  
        form.AddField("entry.144225273",  Supernave);

        form.AddField("entry.675215660", Colono);  
        form.AddField("entry.396095084", Lunatico);  
        form.AddField("entry.154332309", Astrofisico);

        form.AddField("entry.205489418",  Combate);  
        form.AddField("entry.1471576595", Laboratorio);   
        form.AddField("entry.535514923",  Estratega);    
        form.AddField("entry.273910366",  Planeta);     
        form.AddField("entry.672572753",  Satelite);   
        form.AddField("entry.633594529",  CambioOrbital);

    }

    
    //* Obtiene la información que introducirá en el formulario
    void BuscarDatos()
    {
        //Variables Auxiliares
        int[] puntosFinal = Tablero.instance.RecuentoPuntos();

        Opciones jugador = arbitroPartida.player;

        string[] facciones = new string[2];
        if(arbitroPartida.opciones[0] == jugador)
        {
            facciones[0] = arbitroPartida.opciones[0].gameObject.name;
            facciones[1] = arbitroPartida.opciones[1].gameObject.name;
        }
        else
        {
            facciones[0] = arbitroPartida.opciones[1].gameObject.name;
            facciones[1] = arbitroPartida.opciones[0].gameObject.name;
        }

        int jugadorGanador = 2;
        if (puntosFinal[0] > puntosFinal[1]) jugadorGanador = 0;
        else if (puntosFinal[1] > puntosFinal[0]) jugadorGanador = 1;


        //Variables Formulario


        //Ganador
        Ganador = "empate";
        if (jugadorGanador < 2) Ganador = facciones[jugadorGanador];


      

        

       
    }
}
