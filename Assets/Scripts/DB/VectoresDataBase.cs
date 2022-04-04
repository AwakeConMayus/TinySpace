﻿using System.Collections;
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

            KEY_VECTOR + " TEXT, " +
            KEY_ACIERTO + " INT (10), " +
            KEY_FALLO + " INT (10), " +
            KEY_WINRATIO + " TEXT )";
        Debug.Log(dbcmd2.CommandText);
        dbcmd2.ExecuteNonQuery();

    }

    public void addData(string vector, bool victorioso)
    {
        int aciertos = 0;
        int fallos = 0;
        float winratio = 0;
        bool error = false;

        try
        {
            IDataReader reader = base.GetRowUsingKeyStringByString(TABLE_NAME2, vector, KEY_VECTOR);

            aciertos = (int)reader[1];
            fallos = (int)reader[2];
        }
        catch
        {
            error = true;
        }


        IDbCommand dbcmd = getDbCommand();
        if (!error)
        {


            if (victorioso)
            {
                ++aciertos;
            }
            else
            {
                ++fallos;
            }

            winratio = (aciertos / (fallos + aciertos)) * 100;

            dbcmd.CommandText =
                "UPDATE " + TABLE_NAME2
                + " SET "
                + KEY_ACIERTO
                + " = " + aciertos
                + KEY_FALLO
                + " = " + fallos
                + KEY_WINRATIO
                + " = '" + winratio
                + "' WHERE " + KEY_VECTOR
                + " = " + vector + " ;";
        }
        else
        {

            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME2
                + " ( "
                + KEY_VECTOR + ", "
                + KEY_ACIERTO + ", "
                + KEY_FALLO + ", "
                + KEY_WINRATIO + " ) "

                + "VALUES ( '"
                + vector + "', " +
                + aciertos + ", "
                + fallos + ", '"
                + winratio.ToString("F2") + "' )";

        }

        dbcmd.ExecuteNonQuery();

        /*IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText =

        dbcmd.ExecuteNonQuery();

        IDataReader r = base.getAllData(TABLE_NAME1);
        int partidas =(int)r[0];


        IDbCommand dbcmd2 = getDbCommand();
        dbcmd2.CommandText = */

        

    }

}
