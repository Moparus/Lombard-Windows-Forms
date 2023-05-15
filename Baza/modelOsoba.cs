using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace Baza
{

    public class modelOsoba
    {
        public int offSetNumber = 6;
        public List<Osoba> lista = new List<Osoba>();
        SqliteConnection connection = new SqliteConnection("Data Source=lombard.db");
        public modelOsoba()
        {
            connection.Open();
        }
        ~modelOsoba()
        {
            connection.Close();
        }
        public void Add(string name, string surname, int gender_id, int province_id, string description)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO osoba (imie,nazwisko,plec_id,wojewodztwo_id,opis) VALUES ('" + name + "', '" + surname + "', '" + gender_id + "', '" + province_id + "', '" + description + "' );";
            command.ExecuteNonQuery();
        }

        public void Delete(int to_delete_id)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM osoba WHERE osoba_id='" + to_delete_id + "';";
            command.ExecuteNonQuery();
        }
        public void Modify(int id, string name, string surname, int gender_id, int province_id, string description)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"UPDATE osoba SET imie ='" + name + "',nazwisko = '" + surname + "',plec_id = '" + gender_id + "',wojewodztwo_id = '" + province_id + "',opis = '" + description + "' WHERE osoba_id = '" + id + "'";
            command.ExecuteNonQuery();
        }

        public void Start(string osobaDoWyszukania, int nrStrony)
        {
            //Generowanie listy
            lista.Clear();
            int offset = offSetNumber * (nrStrony - 1);
            var command = connection.CreateCommand();
            command.CommandText = $@"SELECT * FROM osoba WHERE imie LIKE '%{osobaDoWyszukania}%' OR nazwisko LIKE '%{osobaDoWyszukania}%' LIMIT {offSetNumber} OFFSET {offset}";

            //OKreślenie długości życia "reader"
            //Instrukcja using C# definiuje granicę dla obiektu, poza którą obiekt jest automatycznie niszczony.
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = System.Convert.ToInt32(reader.GetString(0));
                    var name = reader.GetString(1);
                    var surname = reader.GetString(2);
                    var gender = reader.GetString(3);
                    var voivodeship = reader.GetString(4);
                    var description = reader.GetString(5);
                    lista.Add(new Osoba(id, name, surname, gender, voivodeship, description));
                }
                reader.Close();
            }
        }
    }
}
