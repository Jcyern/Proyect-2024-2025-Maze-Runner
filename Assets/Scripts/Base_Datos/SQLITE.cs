using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using TMPro;
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






}