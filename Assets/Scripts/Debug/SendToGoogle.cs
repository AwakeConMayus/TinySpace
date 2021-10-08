using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://ninest.vercel.app/html/google-forms-embed

public class SendToGoogle : MonoBehaviour
{

    string BASE_URL = "https://docs.google.com/forms/d/e/1FAIpQLScwY7iChsmn_QTGxKEIYSI5Ytp0y4j_vGgZI35HpSsV7JQzdQ/formResponse";

    private IEnumerator Upload(int Score1, int Score2)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.435469167", Score1);
        form.AddField("entry.1370920118", Score2);

        byte[] rawData = form.data;
        WWW url = new WWW(BASE_URL, rawData);

        yield return url;
    }

    public void SendOnline()
    {
        InstancePiezas.instance.RecuentoPuntosTest();
        StartCoroutine(Upload(InstancePiezas.instance.puntuaciones[0], InstancePiezas.instance.puntuaciones[1]));
    }


}
