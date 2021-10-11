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

        form.AddField("entry.435469167", 69);
        form.AddField("entry.1370920118", 69);
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
