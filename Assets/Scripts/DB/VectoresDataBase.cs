using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class VectoresDataBase : SQLite
{

    private const string TABLE_NAME1 = "Totalpartidas";
    private const string KEY_Partidas = "partidasJugadas";




    private const string TABLE_NAME2 = "VectoresDB";
    private const string KEY_VECTOR = "Vector";
    private const string KEY_ACIERTO = "Acierto";
    private const string KEY_FALLO = "Fallo";
    private const string KEY_WINRATIO = "Winratio";


    public VectoresDataBase() : base()
    {
        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME1 + " ( " +
            KEY_Partidas + " INT (10) )";
        dbcmd.ExecuteNonQuery();


        IDbCommand dbcmd2 = getDbCommand();
        dbcmd2.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME2 + " ( " +

            KEY_VECTOR + " TEXT " +
            KEY_ACIERTO + " INT (10) " +
            KEY_FALLO + " INT (10) " +
            KEY_WINRATIO + " FLOAT (10) )";
        dbcmd2.ExecuteNonQuery();

    }

    public void addData(string vector)
    {
        /*IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText =
            "INSERT INTO " + TABLE_NAME2
            + " ( "
            + KEY_VECTOR + ", "
            + KEY_ACIERTO + ", "
            + KEY_FALLO + ", "
            + KEY_WINRATIO + " ) "

            + "VALUES ( '"
            + vector+ "', " +
            ", '"
            + location._Lat + "', '"
            + location._Lng + "' )";
        dbcmd.ExecuteNonQuery();*/
    }

}
