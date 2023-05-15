using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace Baza
{

    public class modelOsoba
    {
        public List<Osoba> lista = new List<Osoba>();
        SqliteConnection connection = new SqliteConnection("Data Source=bazaDanych.db");
        public void Add(string name, string surname)
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Osoby (imie,nazwisko) VALUES ('" + name + "', '" + surname + "' );";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(int to_delete_id)
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM Osoby WHERE id='" + to_delete_id + "';";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Modify(int id, string name, string surname)
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"UPDATE Osoby SET imie ='" + name + "',nazwisko = '" + surname + "' WHERE id = '" + id + "'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Start(string osobaDoWyszukania, int nrStrony)
        {
            //Generowanie listy
            lista.Clear();
            int offset = 4 * (nrStrony - 1);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $@"SELECT * FROM Osoby WHERE imie LIKE '%{osobaDoWyszukania}%' OR nazwisko LIKE '%{osobaDoWyszukania}%' LIMIT 4 OFFSET {offset}";

            using (var reader = command.ExecuteReader())
            {

                while (reader.Read())
                {

                    int id = System.Convert.ToInt32(reader.GetString(0));
                    var name = reader.GetString(1);
                    var surname = reader.GetString(2);

                    lista.Add(new Osoba(id, name, surname));
                }

            }
            connection.Close();
        }
    }
}
