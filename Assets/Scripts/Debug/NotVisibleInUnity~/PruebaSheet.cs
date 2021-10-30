using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;


[CustomEditor(typeof(MyScriptableObjectScript))]
public class MySOInspector : Editor
{

    const string URL = "https://script.google.com/macros/s/AKfycbzTIH-Y3tg6R4QHJuv4KitgzIElbgpS1nPSuZjOFSZhZbKcHBMTFX7hlPiQRmpZop4u/exec";

    public string[] resultUpload = new string[30] {
            "aaa", "aaa", "aaa", "aaa", "aaa", "aaa", "aaa", "aaa", "aaa", "aaa",
            "bbb", "bbb", "bbb", "bbb", "bbb", "bbb", "bbb", "bbb", "bbb", "bbb",
            "ccc", "ccc", "ccc", "ccc", "ccc", "ccc", "ccc", "ccc", "ccc", "ccc",
        };

    string nula = null;

    private UnityWebRequest PeticionWeb;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Download from GDrive"))
        {
            PeticionWeb = UnityWebRequest.Get(URL);
            PeticionWeb.SendWebRequest();

            EditorApplication.update += CheckDownloadFinished; // the only way to wait for a process to finish is with this
        }

        if (GUILayout.Button("Upload to GDrive"))
        {
            PeticionWeb = UnityWebRequest.Post(URL, nula);
            PeticionWeb.SendWebRequest();
        
            //EditorApplication.update += CheckUploadFinished; // the only way to wait for a process to finish is with this
        }
    }

    private void CheckDownloadFinished()
    {
        if (PeticionWeb != null && PeticionWeb.isDone)
        {
            var result = JsonUtility.FromJson<GDocResponse>(PeticionWeb.downloadHandler.text);
            MyScriptableObjectScript myTarget = (MyScriptableObjectScript)target;
            myTarget.Value = result.result;
            EditorApplication.update -= CheckDownloadFinished;
            Repaint();
        }
    }

    private void CheckUploadFinished()
    {
        int i = 0;
        if (PeticionWeb != null && PeticionWeb.isDone)
        {
            ++i;
            Debug.Log(i);
            EditorApplication.update -= CheckUploadFinished;
            Repaint();
        }
    }


    class GDocResponse // this class is used to parse the JSON
     {
        public string[] result;
     }

    [CreateAssetMenu(fileName = "TestUploadDownload", menuName = "ScriptableSheet")]
    public class MyScriptableObjectScript : ScriptableObject
    {
        public string[] Value;
    }
}
