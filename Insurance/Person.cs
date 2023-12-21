using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance
{
    public class Person
    {
        public string name;
        public string nember;
        public string passport;
        public string age;
        public string adres;
        public string nember_drive;
        public string insurance;
        public string insurance_nember;
        public string date_start;
        public string date_end;
        public string path;


        public Person(string name, string nember, string passport, string age, string adres,string nember_drive, string insurance, string insurance_nember, string date_start, string date_end, string path)
        {
            this.name = name;
            this.nember = nember;
            this.passport = passport;
            this.age = age;
            this.adres = adres;
            this.nember_drive = nember_drive;
            this.insurance = insurance;
            this.insurance_nember = insurance_nember;
            this.date_start = date_start;
            this.date_end = date_end;
            this.path = path;
        }
    }
}
