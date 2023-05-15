using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace Baza
{
    class modelSplata
    {
        public List<Splata> lista = new List<Splata>();
        SqliteConnection connection = new SqliteConnection("Data Source=lombard.db");
        public modelSplata()
        {
            connection.Open();
        }
        ~modelSplata()
        {
            connection.Close();
        }

        public int RepayedTotal(int idOsoby)
        {
            int total = 0;
            var command = connection.CreateCommand();
            command.CommandText = $@"SELECT SUM(kwota_splaty) FROM splaty WHERE pozyczki_id = {idOsoby}";
            using (var reader = command.ExecuteReader())
            {
                try
                {
                    while (reader.Read())
                    {
                        total = System.Convert.ToInt32(reader.GetString(0));
                    }
                    return total;
                }
                catch
                {
                    return 0;
                }
                
            }
        }

        public void Add(int pozyczka_id, int kwota, string data)
        {
            var command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"INSERT INTO splaty (pozyczki_id,kwota_splaty,data_splaty) VALUES ('" + pozyczka_id + "', '" + kwota + "', '" + data + "');";
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void Delete(int to_delete_id)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM splaty WHERE splaty_id='" + to_delete_id + "';";
            command.ExecuteNonQuery();
        }
        public void LoanPaid(int pozyczka_id, int stan_id)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"UPDATE pozyczki SET stan_id =  '" + stan_id + "' WHERE pozyczki_id ='" + pozyczka_id + "';";
            command.ExecuteNonQuery();
        }

        public List<Splata> Start(int idOsoby, int nrStrony)
        {
            //Generowanie listy
            lista.Clear();
            int offset = 6 * (nrStrony - 1);
            var command = connection.CreateCommand();
            command.CommandText = $@"SELECT * FROM splaty WHERE pozyczki_id = {idOsoby}";

            using (var reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    int id = System.Convert.ToInt32(reader.GetString(0));
                    var pozyczki_id = reader.GetString(1);
                    var kwota_splaty = reader.GetString(2);
                    var data_splaty = reader.GetString(3);
                    lista.Add(new Splata(id, pozyczki_id, kwota_splaty, data_splaty));
                }
                return lista;

            }
        }
    }
}
