using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0618 //* Uso del método WWW en vez de UnityWebRequest puesto que este primero permite subir data binario a google forms

// En esta url se explica de forma menos específica que como obtengo las entries para el formulario https://ninest.vercel.app/html/google-forms-embed

public class SendToGoogle : MonoBehaviour
{
    //Variables

    //Auxiliares
    string Ganador;
    bool GanaQuienEmpieza;

    //Faccion Escogida
    string FaccionP1, FaccionP2;

    //Score
    int ScoreP1, ScoreP2;

    //Numero Naves Cada Tipo
    int ExploradorP1, CombateP1, LaboratorioP1, EstrategaP1, PlanetaP1,
        ExploradorMejP1, CombateMejP1, LaboratorioMejP1, EstrategaMejP1, CombateEspP1;
    int ExploradorP2, CombateP2, LaboratorioP2, EstrategaP2, PlanetaP2,
        ExploradorMejP2, CombateMejP2, LaboratorioMejP2, EstrategaMejP2, CombateEspP2;

    //Numero Puntos Cada Tipo de Nave
    int scoreExploradorP1, scoreCombateP1, scoreLaboratorioP1, scoreEstrategaP1,
        scorePlanetaP1, scoreExploradorMejP1, scoreCombateMejP1, scoreLaboratorioMejP1,
        scoreEstrategaMejP1, scoreCombateEspP1;
    int scoreExploradorP2, scoreCombateP2, scoreLaboratorioP2, scoreEstrategaP2,
        scorePlanetaP2, scoreExploradorMejP2, scoreCombateMejP2, scoreLaboratorioMejP2, 
        scoreEstrategaMejP2, scoreCombateEspP2;


    public static SendToGoogle instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);

        arbitroPartida = GetComponent<Arbitro>();
    }

    Arbitro arbitroPartida;

    //* Dirección del formulario al que se suben los datos, para obtenerla lo previsualizamos y copiamos su url, cambiando el final de "viewform" a "formResponse"
    string BASE_URL = "https://docs.google.com/forms/d/e/1FAIpQLScwY7iChsmn_QTGxKEIYSI5Ytp0y4j_vGgZI35HpSsV7JQzdQ/formResponse";
    WWWForm form;

    //* Función que es llamada por el botón Score (y que será llamada para subir los datos de los usuarios), aquí se obtienen/calculan los datos a subir
    public void SendOnline()
    {
        StartCoroutine(Upload());
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

    void rellenarFormulario()
    {
        BuscarDatos();

        form = new WWWForm();

        //* Crea un nuevo formulario al que le añade los fields correspondientes a los del formulario de google forms, para obtener el dato entry.XXXXXX debes de:
        //* Pulsar inspeccionar elemento sobre la visualización del formulario y en el código de la web buscar con ctrl f el siguiente término: FB_PUBLIC_LOAD_DATA_
        //* Ahí debes coger el número grande poco después de la variable del formulario a la que quieras asociar el field, por ejemplo: (...)"PuntosMineros",null,0,[[435469167,(...)"PuntosPlanetarios",null,0,[[1370920118,(...)
        //* Una vez tengas ese numero, debes ponerlo junto a entry. por lo tanto para asociar el field PuntosMineros a la Score1, el entry será "entry.435469167"

        form.AddField("entry.435469167",  Ganador);                         // Ganador 
        form.AddField("entry.1370920118", GanaQuienEmpieza.ToString());     // GanaQuienEmpieza

        form.AddField("entry.1874309661", FaccionP1);     // FaccionP1
        form.AddField("entry.1214466468", FaccionP2);     // FaccionP2

        form.AddField("entry.1020366196", ScoreP1);       // Score1
        form.AddField("entry.2024526894", ScoreP2);       // Score2

        form.AddField("entry.1040339605", ExploradorP1);    // ExploradorP1
        form.AddField("entry.1254542756", CombateP1);       // CombateP1
        form.AddField("entry.840516388",  LaboratorioP1);   // LaboratorioP1
        form.AddField("entry.1915383336", EstrategaP1);     // EstrategaP1
        form.AddField("entry.1910669193", PlanetaP1);       // PlanetaP1

        form.AddField("entry.306931683",  ExploradorMejP1);     // ExploradorMejP1
        form.AddField("entry.1935103977", CombateMejP1);        // CombateMejP1
        form.AddField("entry.1788277006", LaboratorioMejP1);    // LaboratorioMejP1
        form.AddField("entry.2144116206", EstrategaMejP1);      // EstrategaMejP1
        form.AddField("entry.397139689",  CombateEspP1);        // CombateEspP1

        form.AddField("entry.2010821728", ExploradorP2);    // ExploradorP2
        form.AddField("entry.1698841726", CombateP2);       // CombateP2
        form.AddField("entry.726993894",  LaboratorioP2);   // LaboratorioP2
        form.AddField("entry.2007228196", EstrategaP2);     // EstrategaP2
        form.AddField("entry.307780979",  PlanetaP2);       // PlanetaP2

        form.AddField("entry.496397925",  ExploradorMejP2);     // ExploradorMejP2
        form.AddField("entry.46395252",   CombateMejP2);        // CombateMejP2
        form.AddField("entry.1378000763", LaboratorioMejP2);    // LaboratorioMejP2
        form.AddField("entry.340695916",  EstrategaMejP2);      // EstrategaMejP2
        form.AddField("entry.831687966",  CombateEspP2);        // CombateEspP2

        form.AddField("entry.1761560571", scoreExploradorP1);   // scoreExploradorP1
        form.AddField("entry.1824586309", scoreCombateP1);      // scoreCombateP1
        form.AddField("entry.1134179669", scoreLaboratorioP1);  // scoreLaboratorioP1
        form.AddField("entry.2126107367", scoreEstrategaP1);    // scoreEstrategaP1
        form.AddField("entry.1579320810", scorePlanetaP1);      // scorePlanetaP1

        form.AddField("entry.839756224", scoreExploradorMejP1);     // scoreExploradorMejP1
        form.AddField("entry.627397951", scoreCombateMejP1);        // scoreCombateMejP1
        form.AddField("entry.8614594",   scoreLaboratorioMejP1);    // scoreLaboratorioMejP1
        form.AddField("entry.127181305", scoreEstrategaMejP1);      // scoreEstrategaMejP1
        form.AddField("entry.873678886", scoreCombateEspP1);        // scoreCombateEspP1

        form.AddField("entry.169886086",  scoreExploradorP2);   // scoreExploradorP2
        form.AddField("entry.487198462",  scoreCombateP2);      // scoreCombateP2
        form.AddField("entry.1810626703", scoreLaboratorioP2);  // scoreLaboratorioP2
        form.AddField("entry.1709659503", scoreEstrategaP2);    // scoreEstrategaP2
        form.AddField("entry.2024345642", scorePlanetaP2);      // scorePlanetaP2

        form.AddField("entry.205301163", scoreExploradorMejP2);     // scoreExploradorMejP2
        form.AddField("entry.280659382", scoreCombateMejP2);        // scoreCombateMejP2
        form.AddField("entry.552524032", scoreLaboratorioMejP2);    // scoreLaboratorioMejP2
        form.AddField("entry.489967046", scoreEstrategaMejP2);      // scoreEstrategaMejP2
        form.AddField("entry.786605642", scoreCombateEspP2);        // scoreCombateEspP2
    }


    void BuscarDatos()
    {
        //Variables Auxiliares
        int[] puntosFinal = Tablero.instance.RecuentoPuntos();

        Opciones jugador = arbitroPartida.player;

        string[] facciones = new string[2];
        if(arbitroPartida.opciones[0] == jugador)
        {
            if(jugador.jugador == 0)
            {
                facciones[0] = arbitroPartida.opciones[0].gameObject.name;
            }
            else
            {
                facciones[1] = arbitroPartida.opciones[0].gameObject.name;

            }
        }
        else
        {
            if (jugador.jugador == 0)
            {
                facciones[0] = arbitroPartida.opciones[1].gameObject.name;
            }
            else
            {
                facciones[1] = arbitroPartida.opciones[1].gameObject.name;

            }
        }

        int jugadorGanador = 2;
        if (puntosFinal[0] > puntosFinal[1]) jugadorGanador = 0;
        else if (puntosFinal[1] > puntosFinal[0]) jugadorGanador = 1;


        //Variables Formulario


        //Ganador
        Ganador = "empate";
        if (jugadorGanador < 2) Ganador = facciones[jugadorGanador];

        //GanaQuienEmpieza
        GanaQuienEmpieza = jugadorGanador == jugador.jugador;

        //FaccionP1, FaccionP2
        FaccionP1 = facciones[0];
        FaccionP2 = facciones[1];

        //ScoreP1, ScoreP2
        ScoreP1 = puntosFinal[0];
        ScoreP2 = puntosFinal[1];



        //Recuento de naves y puntos
        foreach(Casilla c in Tablero.instance.mapa)
        {
            if (c.pieza)
            {
                if(c.pieza.gameObject.GetComponent<Explorador>())
                {
                    if (c.pieza.gameObject.GetComponent<ExploradorMineroMejorado>())
                    {
                        if (c.pieza.Get_Jugador() == 0)
                        {
                            ++ExploradorMejP1;
                            scoreExploradorMejP1 += c.pieza.Puntos();
                        }
                        else
                        {
                            ++ExploradorMejP2;
                            scoreExploradorMejP2 += c.pieza.Puntos();
                        }
                    }

                    else
                    {
                        if (c.pieza.Get_Jugador() == 0)
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
                        if (c.pieza.Get_Jugador() == 0)
                        {
                            ++CombateEspP1;
                            scoreCombateEspP1 += c.pieza.Puntos();
                        }
                        else
                        {
                            ++CombateEspP2;
                            scoreCombateEspP2 += c.pieza.Puntos();
                        }
                    }

                    else if(c.pieza.gameObject.GetComponent<NaveCombateMinerosMejorada>())
                    {
                        if (c.pieza.Get_Jugador() == 0)
                        {
                            ++CombateMejP1;
                            scoreCombateMejP1 += c.pieza.Puntos();
                        }
                        else
                        {
                            ++CombateMejP2;
                            scoreCombateMejP2 += c.pieza.Puntos();
                        }
                    }

                    else
                    {
                        if (c.pieza.Get_Jugador() == 0)
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
                     if (c.pieza.gameObject.GetComponent<InvestigadorMinerosMejorado>())
                    {
                        if (c.pieza.Get_Jugador() == 0)
                        {
                            ++LaboratorioMejP1;
                            scoreLaboratorioMejP1 += c.pieza.Puntos();
                        }
                        else
                        {
                            ++LaboratorioMejP2;
                            scoreLaboratorioMejP2 += c.pieza.Puntos();
                        }
                    }

                    else
                    {
                        if (c.pieza.Get_Jugador() == 0)
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
                    if (c.pieza.gameObject.GetComponent<EstrategaMinerosMejorada>())
                    {
                        if (c.pieza.Get_Jugador() == 0)
                        {
                            ++EstrategaMejP1;
                            scoreEstrategaMejP1 += c.pieza.Puntos();
                        }
                        else
                        {
                            ++EstrategaMejP2;
                            scoreEstrategaMejP2 += c.pieza.Puntos();
                        }
                    }

                    else
                    {
                        if (c.pieza.Get_Jugador() == 0)
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
                    if (c.pieza.Get_Jugador() == 0)
                    {
                        ++PlanetaP1;
                        scorePlanetaP1 += c.pieza.Puntos();
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
