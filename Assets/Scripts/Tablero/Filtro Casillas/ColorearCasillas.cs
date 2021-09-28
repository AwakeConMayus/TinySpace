using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorearCasillas : MonoBehaviour
{
    MeshRenderer MeshCasilla;

    [SerializeField]
    Material ShaderAzul;
    [SerializeField]
    Material ShaderRojo;
    [SerializeField]
    Material ShaderVerde;
    [SerializeField]
    Material ShaderAmarillo;

    //* Colorea la casilla del color dado
    public void reColor(string colorName, Casilla c)
    {
        colorName.ToLower(); //* Convertir la string en minúsculas por si acaso
        MeshRenderer MeshCasilla = c.GetComponentInChildren<MeshRenderer>(); //* Obtener el material de la casilla

        switch (colorName)
        {
            case "blue"  : MeshCasilla.material = ShaderAzul;     break;
            case "red"   : MeshCasilla.material = ShaderRojo;     break;
            case "green" : MeshCasilla.material = ShaderVerde;    break;
            case "yellow": MeshCasilla.material = ShaderAmarillo; break;
        }
    }

    public void reColor(string colorName, List <Casilla> listC)
    {
        foreach (Casilla c in listC) reColor(colorName, c);
    }

    //* Restaura el color azul
    public void initialColor(Casilla c)
    {
        MeshRenderer MeshCasilla = c.GetComponentInChildren<MeshRenderer>();
        MeshCasilla.material = ShaderAzul;
    }

    public void initialColor(List <Casilla> listC = null)
    {
        if (listC == null) listC = Tablero.instance.mapa;

        foreach (Casilla c in listC) initialColor(c);
    }

    //public void initialColor(string colorName, Casilla c)
    //{
    //    MeshRenderer MeshCasilla = c.GetComponentInChildren<MeshRenderer>();
    //    Material materialALimpiar = ShaderVerde;
    //    
    //    switch (colorName)
    //    {
    //        case "blue":   materialALimpiar = ShaderAzul;     break;
    //        case "red":    materialALimpiar = ShaderRojo;     break;
    //        case "green":  materialALimpiar = ShaderVerde;    break;
    //        case "yellow": materialALimpiar = ShaderAmarillo; break;
    //    }
    //
    //    if(MeshCasilla.material == materialALimpiar) MeshCasilla.material = ShaderAzul;
    //}

}
