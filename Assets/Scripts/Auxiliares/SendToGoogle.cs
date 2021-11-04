using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0618 //* Uso del método WWW en vez de UnityWebRequest puesto que este primero permite subir data binario a google forms

// En esta url se explica de forma menos específica que como obtengo las entries para el formulario https://ninest.vercel.app/html/google-forms-embed

public class SendToGoogle : MonoBehaviour
{
    //Variables

    //Generales
    string Ganador;

    //Facciones Escogida
    string Faccion1, Faccion2;



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

    public bool SendingDataOnline = false;

    //* Dirección del formulario al que se suben los datos, para obtenerla lo previsualizamos y copiamos su url, cambiando el final de "viewform" a "formResponse"
    const string BASE_URL = "https://docs.google.com/forms/d/e/1FAIpQLScGjJts-KwsMlpdMi-FbrFI6edqkbDnIenlQdc2VHlc6cPIkw/formResponse";
    WWWForm form;

    //* Función que es llamada al finalizar el juego aquí se inicia la corrutina que subirá los datos
    public void SendOnline()
    {
        if(SendingDataOnline) StartCoroutine(Upload());
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

        form.AddField("entry.744986798",  Ganador);                 // Ganador 
        form.AddField("entry.1898422490", Ganador);        // GanaQuienEmpieza

        form.AddField("entry.1823031720", Ganador);               // FaccionP1

        form.AddField("entry.1396022188", Ganador);                 // Score1
        form.AddField("entry.2084878629", Ganador);                 // Score2

        form.AddField("entry.743563174", Ganador);            // ExploradorP1
        form.AddField("entry.1680791580", Ganador);               // CombateP1
        form.AddField("entry.2031779512", Ganador);           // LaboratorioP1
        form.AddField("entry.2087586458", Ganador);             // EstrategaP1

        form.AddField("entry.1592083320", Ganador);         // ExploradorMejP1
        form.AddField("entry.57859528", Ganador);            // CombateMejP1
        form.AddField("entry.1568221146", Ganador);        // LaboratorioMejP1
        form.AddField("entry.1623494950", Ganador);          // EstrategaMejP1

        form.AddField("entry.2143636952", Ganador);            // ExploradorP2
        form.AddField("entry.1510679630", Ganador);               // CombateP2
        form.AddField("entry.724512350", Ganador);           // LaboratorioP2
        form.AddField("entry.82857284", Ganador);             // EstrategaP2
        form.AddField("entry.5292467", Ganador);               // PlanetaP2
        form.AddField("entry.364573089", Ganador);        // PlanetaSagradoP2
        form.AddField("entry.743639779", Ganador);            // CombateEspP2

        form.AddField("entry.2006495554", Ganador);       // scoreExploradorP1
        form.AddField("entry.2022311863", Ganador);          // scoreCombateP1
        form.AddField("entry.1012944182", Ganador);      // scoreLaboratorioP1
        form.AddField("entry.848013273", Ganador);        // scoreEstrategaP1

        form.AddField("entry.2081159488", Ganador);     // scoreExploradorMejP1
        form.AddField("entry.827129007", Ganador);        // scoreCombateMejP1
        form.AddField("entry.157811916", Ganador);    // scoreLaboratorioMejP1
        form.AddField("entry.1361294743", Ganador);      // scoreEstrategaMejP1

        form.AddField("entry.1327141632", Ganador);       // scoreExploradorP2
        form.AddField("entry.1722183929", Ganador);          // scoreCombateP2
        form.AddField("entry.1655902867", Ganador);      // scoreLaboratorioP2
        form.AddField("entry.409777714", Ganador);        // scoreEstrategaP2
        form.AddField("entry.388807094", Ganador);          // scorePlanetaP2
        form.AddField("entry.218373057", Ganador);   // scorePlanetaSagradoP2
        form.AddField("entry.488067953", Ganador);       // scoreCombateEspP2       
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
