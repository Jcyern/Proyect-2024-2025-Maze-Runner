using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SQLITE : MonoBehaviour
{
    public static SQLITE instance;
    private string dbName = "URI=file:DataBase.db";
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        CreateTable();


    }

    private void CreateTable()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                string sqlcreation = "";


                sqlcreation += "CREATE TABLE IF NOT EXISTS cards(";
                sqlcreation += "id INTEGER NOT NULL ";
                sqlcreation += "PRIMARY KEY AUTOINCREMENT,";
                sqlcreation += "name     VARCHAR(50) NOT NULL,";
                sqlcreation += "faction VARCHAR(50) NOT NULL,";
                sqlcreation += "enfriamiento   VARCHAR(50) NOT NULL,";
                sqlcreation += "hability   VARCHAR(50) NOT NULL";
                sqlcreation += ");";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
    private void AddColumnsToCardsTable()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {    //ingresa en la tabla card las columnas con valor determinado 0 
                command.CommandText = "ALTER TABLE cards ADD COLUMN velocidad INTEGER NOT NULL DEFAULT 0;";
                command.ExecuteNonQuery();


            }
            connection.Close();
        }
    }




    //para pasarle  comandos a la base de datos 
    public void Query(string q)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = q;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Debug.Log("name: " + reader["name"] + " password: " + reader["password"]);

                    }
                }
            }

            connection.Close();
        }
    }




    public void AddElementToBase()
    {
        string Name = GameObject.Find("Name").GetComponent<TMP_InputField>().text;
        string Faction = GameObject.Find("Faction").GetComponent<TMP_InputField>().text;
        string Hability = GameObject.Find("Habilidad").GetComponent<TMP_InputField>().text;
        string Enfriamiento = GameObject.Find("Enfriamiento").GetComponent<TMP_InputField>().text;

        string command = $"INSERT INTO cards (name, faction, enfriamiento, hability) VALUES ('" + Name + "','" + Faction + "','" + Enfriamiento + "','" + Hability + "') ";

        Query(command);

        Debug.Log($"Se anadio ficha :  name{Name} , faction{Faction} , hability {Hability} , enfriamiento {Enfriamiento}");
        LimpiarInPut(GameObject.Find("Name"));
        LimpiarInPut(GameObject.Find("Faction"));
        LimpiarInPut(GameObject.Find("Habilidad"));
        LimpiarInPut(GameObject.Find("Enfriamiento"));

        void LimpiarInPut(GameObject go)
        {
            go.GetComponent<TMP_InputField>().text = "";
        }
    }


    //metodo para bucar las Fichas de la base de dato dependiendo de la casa seleccionada 
    public void AddFiles()
    {

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            string q = "SELECT*FROM cards where faction = '" + Manager.house.Item2.ToString() + "' ";
            using (var command = connection.CreateCommand())
            {
                command.CommandText = q;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        File.fichas.Add(new File(reader["name"].ToString(), reader["hability"].ToString(), Convert.ToInt32(reader["enfriamiento"].ToString()), Convert.ToInt32(reader["velocidad"].ToString())));
                        //lideres[pos]=  new Lider(reader["name"].ToString(),reader["effect"].ToString() );                        

                        // Debug.Log("nombre " + reader["name"] + " efecto: " + reader["effect"]);
                        // pos += 1;
                    }
                }
            }

            connection.Close();
        }

        Debug.Log(File.fichas.Count);
        foreach (var file in File.fichas)
        {
            Debug.Log($"ficha : {file.Name}  , habillity {file.Habilidad}   , enfriamiento {file.Enfriamieto} , velocidad {file.Velocidad},  ");
        }



    }

}