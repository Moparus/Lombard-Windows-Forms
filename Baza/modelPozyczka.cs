using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace Baza
{
    class modelPozyczka
    {
        public int offSetNumber = 6;
        public List<Pozyczka> lista = new List<Pozyczka>();
        SqliteConnection connection = new SqliteConnection("Data Source=lombard.db");
        public modelPozyczka()
        {
            connection.Open();
        }
        ~modelPozyczka()
        {
            connection.Close();
        }
        public void Add(int idOsoby, string loan_date, int amount, string loan_return_date, string protection)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO pozyczki (osoba_id,data_pozyczki,kwota,termin_zwrotu,zabezpieczenie,stan_id) VALUES ('" + idOsoby + "', '" + loan_date + "', '" + amount + "', '" + loan_return_date + "', '" + protection + "', '" + 1 + "' );";
            command.ExecuteNonQuery();
        }

        public void Delete(int to_delete_id)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM splaty WHERE pozyczki_id='" + to_delete_id + "';";
            command.ExecuteNonQuery();
            command.CommandText = @"DELETE FROM pozyczki WHERE pozyczki_id='" + to_delete_id + "';";
            command.ExecuteNonQuery();
        }

        public void Modify(int idPozyczki,int idOsoby, string loan_date, int amount, string loan_return_date, string protection)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"UPDATE pozyczki SET osoba_id ='" + idOsoby + "',data_pozyczki = '" + loan_date + "',kwota = '" + amount + "',termin_zwrotu = '" + loan_return_date + "',zabezpieczenie = '" + protection + "' WHERE pozyczki_id = '" + idPozyczki + "'";
            command.ExecuteNonQuery();
        }

        public void Start(int idOsoby, int nrStrony)
        {
            //Generowanie listy
            lista.Clear();
            int offset = offSetNumber * (nrStrony - 1);
            var command = connection.CreateCommand();
            command.CommandText = $@"SELECT * FROM pozyczki INNER JOIN stan USING(stan_id) WHERE osoba_id = {idOsoby} LIMIT {offSetNumber} OFFSET {offset}";

            using (var reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    int id = System.Convert.ToInt32(reader.GetString(0));
                    var osoba_id = reader.GetString(1);
                    var data_pozyczki = reader.GetString(2);
                    var kwota = reader.GetString(3);
                    var termin_zwrotu = reader.GetString(4);
                    var zabezpieczenie = reader.GetString(5);
                    var stan = reader.GetString(7);
                    lista.Add(new Pozyczka(id,osoba_id,data_pozyczki,kwota,termin_zwrotu,zabezpieczenie,stan));
                }
                reader.Close();
            }
        }
    }
}
