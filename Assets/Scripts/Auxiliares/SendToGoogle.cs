using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0618 //* Uso del método WWW en vez de UnityWebRequest puesto que este primero permite subir data binario a google forms

// En esta url se explica de forma menos específica que como obtengo las entries para el formulario https://ninest.vercel.app/html/google-forms-embed

public class SendToGoogle : MonoBehaviour
{
    //Variables

    //Auxiliares
    string Ganador;
    string GanaQuienEmpieza;

    //Faccion Escogida
    string FaccionP1;

    //Score
    int ScoreP1, ScoreP2;

    //Numero Naves Cada Tipo
    int ExploradorP1, CombateP1, LaboratorioP1, EstrategaP1,
        ExploradorMejP1, CombateMejP1, LaboratorioMejP1, EstrategaMejP1;
    int ExploradorP2, CombateP2, LaboratorioP2, EstrategaP2, 
        PlanetaP2, PlanetaSagradoP2, CombateEspP2;

    //Numero Puntos Cada Tipo de Nave
    int scoreExploradorP1, scoreCombateP1, scoreLaboratorioP1, scoreEstrategaP1,
        scoreExploradorMejP1, scoreCombateMejP1, scoreLaboratorioMejP1, scoreEstrategaMejP1;
    int scoreExploradorP2, scoreCombateP2, scoreLaboratorioP2, scoreEstrategaP2,
        scorePlanetaP2, scorePlanetaSagradoP2, scoreCombateEspP2;



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
        form.AddField("entry.1898422490", GanaQuienEmpieza);        // GanaQuienEmpieza

        form.AddField("entry.1823031720", FaccionP1);               // FaccionP1

        form.AddField("entry.1396022188", ScoreP1);                 // Score1
        form.AddField("entry.2084878629", ScoreP2);                 // Score2

        form.AddField("entry.743563174",  ExploradorP1);            // ExploradorP1
        form.AddField("entry.1680791580", CombateP1);               // CombateP1
        form.AddField("entry.2031779512", LaboratorioP1);           // LaboratorioP1
        form.AddField("entry.2087586458", EstrategaP1);             // EstrategaP1

        form.AddField("entry.1592083320", ExploradorMejP1);         // ExploradorMejP1
        form.AddField("entry.57859528",   CombateMejP1);            // CombateMejP1
        form.AddField("entry.1568221146", LaboratorioMejP1);        // LaboratorioMejP1
        form.AddField("entry.1623494950", EstrategaMejP1);          // EstrategaMejP1

        form.AddField("entry.2143636952", ExploradorP2);            // ExploradorP2
        form.AddField("entry.1510679630", CombateP2);               // CombateP2
        form.AddField("entry.724512350",  LaboratorioP2);           // LaboratorioP2
        form.AddField("entry.82857284",   EstrategaP2);             // EstrategaP2
        form.AddField("entry.5292467",    PlanetaP2);               // PlanetaP2
        form.AddField("entry.364573089",  PlanetaSagradoP2);        // PlanetaSagradoP2
        form.AddField("entry.743639779",  CombateEspP2);            // CombateEspP2

        form.AddField("entry.2006495554", scoreExploradorP1);       // scoreExploradorP1
        form.AddField("entry.2022311863", scoreCombateP1);          // scoreCombateP1
        form.AddField("entry.1012944182", scoreLaboratorioP1);      // scoreLaboratorioP1
        form.AddField("entry.848013273",  scoreEstrategaP1);        // scoreEstrategaP1

        form.AddField("entry.2081159488", scoreExploradorMejP1);     // scoreExploradorMejP1
        form.AddField("entry.827129007",  scoreCombateMejP1);        // scoreCombateMejP1
        form.AddField("entry.157811916",  scoreLaboratorioMejP1);    // scoreLaboratorioMejP1
        form.AddField("entry.1361294743", scoreEstrategaMejP1);      // scoreEstrategaMejP1

        form.AddField("entry.1327141632", scoreExploradorP2);       // scoreExploradorP2
        form.AddField("entry.1722183929", scoreCombateP2);          // scoreCombateP2
        form.AddField("entry.1655902867", scoreLaboratorioP2);      // scoreLaboratorioP2
        form.AddField("entry.409777714",  scoreEstrategaP2);        // scoreEstrategaP2
        form.AddField("entry.388807094",  scorePlanetaP2);          // scorePlanetaP2
        form.AddField("entry.218373057",  scorePlanetaSagradoP2);   // scorePlanetaSagradoP2
        form.AddField("entry.488067953",  scoreCombateEspP2);       // scoreCombateEspP2       
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

        //GanaQuienEmpieza
        if (jugadorGanador == 2) GanaQuienEmpieza = "empate";

        //else if (jugadorGanador == jugador.jugador) GanaQuienEmpieza = "SI";
        else GanaQuienEmpieza = "NO";

        //FaccionP1, FaccionP2
        FaccionP1 = arbitroPartida.player.gameObject.name;

        if(FaccionP1 == "Mineros")
        {
            ScoreP1 = puntosFinal[0];
            ScoreP2 = puntosFinal[1];
        }
        else
        {
            ScoreP1 = puntosFinal[1];
            ScoreP2 = puntosFinal[0];
        }


        //Recuento de naves y puntos
        foreach(Casilla c in Tablero.instance.mapa)
        {
            if (c.pieza)
            {
                if(c.pieza.gameObject.GetComponent<Explorador>())
                {
                    if (c.pieza.gameObject.GetComponent<ExploradorMineroMejorado>())
                    {
                        ++ExploradorMejP1;
                        scoreExploradorMejP1 += c.pieza.Puntos();
                    }

                    else
                    {
                        if (c.pieza.faccion == Faccion.minero)
                        {
                            ++ExploradorP1;
                            scoreExploradorP1 += c.pieza.Puntos();
                        }
                        else
                        {
                            ++ExploradorP2;
                            scoreExploradorP2 += c.pieza.Puntos();
                        }
                    }
                }

                else if (c.pieza.gameObject.GetComponent<NaveCombate>())
                {
                    if (c.pieza.gameObject.GetComponent<NaveCombatePlanetasColonizadores>())
                    {
                        ++CombateEspP2;
                        scoreCombateEspP2 += c.pieza.Puntos();
                    }

                    else if(c.pieza.gameObject.GetComponent<NaveCombateMinerosMejorada>())
                    {
                        ++CombateMejP1;
                        scoreCombateMejP1 += c.pieza.Puntos();
                    }

                    else
                    {
                        if (c.pieza.faccion == Faccion.minero)
                        {
                            ++CombateP1;
                            scoreCombateP1 += c.pieza.Puntos();
                        }
                        else
                        {
                            ++CombateP2;
                            scoreCombateP2 += c.pieza.Puntos();
                        }
                    }
                }

                else if (c.pieza.gameObject.GetComponent<Investigador>())
                {
                    if (c.pieza.gameObject.GetComponent<InvestigadorMinerosAstro>())
                    {
                        ++LaboratorioMejP1;
                        ++LaboratorioP1;
                        scoreLaboratorioP1 += c.pieza.Puntos();
                    }

                    else
                    {
                        if (c.pieza.faccion == Faccion.minero)
                        {
                            ++LaboratorioP1;
                            scoreLaboratorioP1 += c.pieza.Puntos();
                        }
                        else
                        {
                            ++LaboratorioP2;
                            scoreLaboratorioP2 += c.pieza.Puntos();
                        }
                    }
                }

                else if (c.pieza.gameObject.GetComponent<Estratega>())
                {
                    if (c.pieza.gameObject.GetComponent<EstrategaMinerosAstro>())
                    {
                        ++EstrategaMejP1;
                        ++EstrategaP1;
                        scoreEstrategaP1 += c.pieza.Puntos();
                    }

                    else
                    {
                        if (c.pieza.faccion == Faccion.minero)
                        {
                            ++EstrategaP1;
                            scoreEstrategaP1 += c.pieza.Puntos();
                        }
                        else
                        {
                            ++EstrategaP2;
                            scoreEstrategaP2 += c.pieza.Puntos();
                        }
                    }
                }

                else if (c.pieza.gameObject.GetComponent<Planetas>())
                {
                    if (c.pieza.gameObject.GetComponent<PlanetaSagrado>())
                    {
                        ++PlanetaP2;
                        if (c.pieza.Puntos() > 0)
                        {
                            scorePlanetaP2 += 3;
                            scorePlanetaSagradoP2 -= 3;
                        }
                        ++PlanetaSagradoP2;
                        scorePlanetaSagradoP2 += c.pieza.Puntos();
                    }

                    else
                    {
                        ++PlanetaP2;
                        scorePlanetaP2 += c.pieza.Puntos();
                    }                    
                }
            }
        }
    }
}
