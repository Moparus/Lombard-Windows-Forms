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
        public string gender { get; set; }
        public string voivodeship { get; set; }
        public string description { get; set; }

        public Osoba(int id, string name, string surname, string gender, string voivodeship, string description)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.gender = gender;
            this.voivodeship = voivodeship;
            this.description = description;
        }
    }
}
