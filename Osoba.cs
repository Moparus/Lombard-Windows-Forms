using System;
using System.Collections.Generic;
using System.Text;

namespace Baza
{
    public class Osoba
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }

        public Osoba(int id, string name, string surname)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
        }
    }
}
