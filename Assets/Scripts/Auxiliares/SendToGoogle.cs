using System.Collections;
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
    string HeroeMinero, EspecialMinero;
    string HeroeOyente, MejoraOyente, EspecialOyente;


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
    const string BASE_URL = "https://docs.google.com/forms/d/e/1FAIpQLSf2Ht626ha_IM_aG0_wbf6DPJNyruJ82Hi0im_LdYgfjP-RvA/formResponse";
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

        form.AddField("entry.952161928", Ganador);
        form.AddField("entry.917130770", Faccion1);
        form.AddField("entry.1479737980", Faccion2);
        form.AddField("entry.940617184", PartidaConIA);

        form.AddField("entry.724103590", HeroeMinero);
        print(EspecialMinero);
        form.AddField("entry.1738738836", EspecialMinero);

        form.AddField("entry.1171462077", HeroeOyente);
        form.AddField("entry.2125969092", MejoraOyente);
        print(EspecialOyente);
        form.AddField("entry.1391571527", EspecialOyente);

        form.AddField("entry.144225273", VectorMineros);
        form.AddField("entry.675215660", VectorOyentes);
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
        if (miSeleccion.mi_poder == Resources.Load<GameObject>("Maquinista") || SeleccionRival.mi_poder == Resources.Load<GameObject>("Maquinista")) HeroeMinero = "Maquinista";
        else if (miSeleccion.mi_poder == Resources.Load<GameObject>("Mecánico") || SeleccionRival.mi_poder == Resources.Load<GameObject>("Mecánico")) HeroeMinero = "Mecanico";
        else if (miSeleccion.mi_poder == Resources.Load<GameObject>("Chantajista") || SeleccionRival.mi_poder == Resources.Load<GameObject>("Chantajista")) HeroeMinero = "Chantajista";

        //Oyentes
        if (miSeleccion.mi_poder == Resources.Load<GameObject>("Colono") || SeleccionRival.mi_poder == Resources.Load<GameObject>("Colono")) HeroeOyente = "Terraformador";
        else if (miSeleccion.mi_poder == Resources.Load<GameObject>("Lunático") || SeleccionRival.mi_poder == Resources.Load<GameObject>("Lunático")) HeroeOyente = "Lunatico";
        else if (miSeleccion.mi_poder == Resources.Load<GameObject>("Astrofísico") || SeleccionRival.mi_poder == Resources.Load<GameObject>("Astrofísico")) HeroeOyente = "Astrofisico";

        //Especiales

        //Mineros
        if (miSeleccion.mis_opciones[4] == Resources.Load<GameObject>("Comodin") || SeleccionRival.mis_opciones[4] == Resources.Load<GameObject>("Comodin")) EspecialMinero = "Comodin";
        else if (miSeleccion.mis_opciones[4] == Resources.Load<GameObject>("Modelo Perfeccionado") || SeleccionRival.mis_opciones[4] == Resources.Load<GameObject>("Modelo Perfeccionado")) EspecialMinero = "Modelo Perfeccionado";
        else if (miSeleccion.mis_opciones[4] == Resources.Load<GameObject>("Supernave") || SeleccionRival.mis_opciones[4] == Resources.Load<GameObject>("Supernave")) EspecialMinero = "Supernave";

        //Oyentes       
        print(miSeleccion.mis_opciones[4].name + " // " + SeleccionRival.mis_opciones[4].name);
        if (miSeleccion.mis_opciones[4] == Resources.Load<GameObject>("Planeta") || SeleccionRival.mis_opciones[4] == Resources.Load<GameObject>("Planeta")) EspecialOyente = "Planeta";
        else if (miSeleccion.mis_opciones[4] == Resources.Load<GameObject>("Terraformador") || SeleccionRival.mis_opciones[4] == Resources.Load<GameObject>("Terraformador")) EspecialOyente = "Terraformar";
        else if (miSeleccion.mis_opciones[4] == Resources.Load<GameObject>("Propulsor de Cambio Orbital") || SeleccionRival.mis_opciones[4] == Resources.Load<GameObject>("Propulsor de Cambio Orbital")) EspecialOyente = "Propulsor";
        else print("no encrontre nada");

        //Mejoras

        //Oyentes
        if (miSeleccion.mis_opciones[1] == Resources.Load<GameObject>("Colonizadores de combate +") || SeleccionRival.mis_opciones[1] == Resources.Load<GameObject>("Colonizadores de combate +")) MejoraOyente = "Combate";
        else if (miSeleccion.mis_opciones[2] == Resources.Load<GameObject>("Laboratorio de Terraformación +") || SeleccionRival.mis_opciones[2] == Resources.Load<GameObject>("Laboratorio de Terraformación +")) MejoraOyente = "Laboratorio";
        else if (miSeleccion.mis_opciones[3] == Resources.Load<GameObject>("Cuartel estratega orbital +") || SeleccionRival.mis_opciones[3] == Resources.Load<GameObject>("Cuartel estratega orbital +")) MejoraOyente = "Estratega";
    }
}
