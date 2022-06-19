using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class Profesori
    {
        public int ProfesoriId { get; set; }
        public string EmriProfesorit { get; set; }
        public string Mbiemri { get; set; }
        public string Lenda { get; set; }
        public string Kontakti { get; set; }
        public string Kualifikimi { get; set; }
    }
}
