using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
#pragma warning disable CS0618 

public class LoadFromGoogle : MonoBehaviour
{
    //* ID específico del Sheet de Google
    const string SHEET_ID = "17hEEH4I1xifis66MAXurrM5oK_DFNZ3N-aexMu_V6E4";
    
    //* Url de descarga del Sheet
    const string BASE_URL = "https://docs.google.com/spreadsheets/d/" + SHEET_ID + "/export?format=csv";
    
    //* Dónde se van a guardar los datos dentro del ordenador (se cambia en GetCSV())
    string filePath = null;


    void Start()
    {
        StartCoroutine(GetCSV());
    }

    //* Guarda en el ordenador del usuario los datos de la IA
    IEnumerator GetCSV()
    {
        filePath = string.Format("{0}/{1}.csv", Application.persistentDataPath, "PlayData");

        using (UnityWebRequest www = UnityWebRequest.Get(BASE_URL))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError) Debug.Log(www.error);
            else
            {
                Debug.Log("Datos para la IA guardados en: " + filePath);
                File.WriteAllText(filePath, www.downloadHandler.text + "\r\n2C");
                
                StartCoroutine(ParsearCSV()); // Una vez ha guardado el archivo, lo parsea
            }
        }
    }

    //* Parsea el archivo utilizado lógica de un guiri de youtube para eliminar las comas, y saltar de fila cuando es necesario
    public IEnumerator ParsearCSV()
    {
        StreamReader streamReader = new StreamReader(filePath);
        string sheetCSV = streamReader.ReadToEnd();

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        // Line level
        int currLineIndex = 0;
        bool inQuote = false;
        int linesSinceUpdate = 0;
        int kLinesBetweenUpdate = 15;

        // Entry level
        string currEntry = "";
        int currCharIndex = 0;
        bool currEntryContainedQuote = false;
        List<string> currLineEntries = new List<string>();

        // "\r" means end of line and should be only occurence of '\r'
        char lineEnding = '\r';
        int lineEndingLength = 1;

        while (currCharIndex < sheetCSV.Length)
        {
            if (!inQuote && (sheetCSV[currCharIndex] == lineEnding))
            {
                // Skip the line ending
                currCharIndex += lineEndingLength;

                // Wrap up the last entry
                // If we were in a quote, trim bordering quotation marks
                if (currEntryContainedQuote)
                {
                    currEntry = currEntry.Substring(1, currEntry.Length - 2);
                }

                currLineEntries.Add(currEntry);
                currEntry = "";
                currEntryContainedQuote = false;

                // Line ended
                ParsearFila(currLineEntries, currLineIndex);
                currLineIndex++;
                currLineEntries = new List<string>();


                linesSinceUpdate++;
                if (linesSinceUpdate > kLinesBetweenUpdate)
                {
                    linesSinceUpdate = 0;
                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                if (sheetCSV[currCharIndex] == '"')
                {
                    inQuote = !inQuote;
                    currEntryContainedQuote = true;
                }

                // Entry level stuff
                {
                    if (sheetCSV[currCharIndex] == ',')
                    {
                        if (inQuote)
                        {
                            currEntry += sheetCSV[currCharIndex];
                        }
                        else
                        {
                            // If we were in a quote, trim bordering quotation marks
                            if (currEntryContainedQuote)
                            {
                                currEntry = currEntry.Substring(1, currEntry.Length - 2);
                            }

                            currLineEntries.Add(currEntry);
                            currEntry = "";
                            currEntryContainedQuote = false;
                        }
                    }
                    else
                    {
                        currEntry += sheetCSV[currCharIndex];
                    }
                }
                currCharIndex++;
            }
            //progress = (int)((float)currCharIndex / sheetCSV.Length * 100.0f);
        }
    }

    //* Recorre los datos de cada una de las filas que va obteniendo de ParsearCSV();
    //* Dentro hay un debuglog que printea todos los datos parseados si lo descomentas.
    private void ParsearFila(List<string> strFila, int numFila)
    {
        if (strFila.Count > 0)
        {
            for (int numColumna = 1; numColumna < strFila.Count; numColumna++)
            {
                string datoIndividual = strFila[numColumna];
                
              // Debug.Log("(" + numFila.ToString() + ", " + numColumna.ToString() + ") = " + datoIndividual);
            }
        }
    }
}
