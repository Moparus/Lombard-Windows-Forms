using System;
using System.Collections.Generic;
using System.Text;

namespace Baza
{
    class Pozyczka
    {
        public int id { get; set; }
        public string osoba_id { get; set; }
        public string data_pozyczki { get; set; }
        public string kwota { get; set; }
        public string termin_zwrotu { get; set; }
        public string zabezpieczenie { get; set; }
        public string stan { get; set; }

        public Pozyczka(int id, string osoba_id, string data_pozyczki, string kwota, string termin_zwrotu, string zabezpieczenie, string stan)
        {
            this.id = id;
            this.osoba_id = osoba_id;
            this.data_pozyczki = data_pozyczki;
            this.kwota = kwota;
            this.termin_zwrotu = termin_zwrotu;
            this.zabezpieczenie = zabezpieczenie;
            this.stan = stan;
        }
    }
}
