using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0618 //* Uso del método WWW en vez de UnityWebRequest puesto que este primero permite subir data binario a google forms

// En esta url se explica de forma menos específica que como obtengo las entries para el formulario https://ninest.vercel.app/html/google-forms-embed

public class SendToGoogle : MonoBehaviour
{
    //* Dirección del formulario al que se suben los datos, para obtenerla lo previsualizamos y copiamos su url, cambiando el final de "viewform" a "formResponse"
    string BASE_URL = "https://docs.google.com/forms/d/e/1FAIpQLScwY7iChsmn_QTGxKEIYSI5Ytp0y4j_vGgZI35HpSsV7JQzdQ/formResponse";

    //* Función que es llamada por el botón Score (y que será llamada para subir los datos de los usuarios), aquí se obtienen/calculan los datos a subir
    public void SendOnline()
    {
        InstancePiezas.instance.RecuentoPuntosTest();

        StartCoroutine(Upload(InstancePiezas.instance.getPuntuacion(0), InstancePiezas.instance.getPuntuacion(1)));
    }

    //* Corrutina que sube los datos a la red, recibe por parametro todos los datos que vaya a subir
    private IEnumerator Upload(int Score1, int Score2)
    {
        //* Crea un nuevo formulario al que le añade los fields correspondientes a los del formulario de google forms, para obtener el dato entry.XXXXXX debes de:
        //* Pulsar inspeccionar elemento sobre la visualización del formulario y en el código de la web buscar con ctrl f el siguiente término: FB_PUBLIC_LOAD_DATA_
        //* Ahí debes coger el número grande poco después de la variable del formulario a la que quieras asociar el field, por ejemplo: (...)"PuntosMineros",null,0,[[435469167,(...)"PuntosPlanetarios",null,0,[[1370920118,(...)
        //* Una vez tengas ese numero, debes ponerlo junto a entry. por lo tanto para asociar el field PuntosMineros a la Score1, el entry será "entry.435469167"
        WWWForm form = new WWWForm();
        form.AddField("entry.435469167", Score1);
        form.AddField("entry.1370920118", Score2);

        //* Guarda de forma binaria los datos del formulario y los adjunta a una url que sirve para responder el formulario, que después genera el excel en base a sus respuestas
        byte[] rawData = form.data;
        WWW url = new WWW(BASE_URL, rawData);

        yield return url;
    }

}
