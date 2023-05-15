using System;
using System.Collections.Generic;
using System.Text;

namespace Baza
{
    class Splata
    {
        public int id { get; set; }
        public string pozyczki_id { get; set; }
        public string kwota_splaty { get; set; }
        public string data_splaty { get; set; }
        public Splata(int id, string pozyczki_id, string kwota_splaty, string data_splaty)
        {
            this.id = id;
            this.pozyczki_id = pozyczki_id;
            this.kwota_splaty = kwota_splaty;
            this.data_splaty = data_splaty;
        }
    }
}
