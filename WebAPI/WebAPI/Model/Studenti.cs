using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class Studenti
    {
        public int StudentiId { get; set; }
        public string EmriStudentit { get; set; }
        public string Mbiemri { get; set; }
        public string Ditelindja { get; set; }
        public string Department { get; set; }
        public string Kontakti { get; set; }
        public string NrPersonal { get; set; }
        public string DataRegjistrimit { get; set; }
        public string LLojiRegjistrimit { get; set; }
        public String PhotoFileName { get; set; }
    }
}
