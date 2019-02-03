using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fakturisanje.Models
{
    public class Faktura
    {
        [DisplayName("Redni broj")]
        public int RedniBroj { get; set; }

        public string FK_DokumetFakture { get; set; }

        public Dictionary<int, Tuple<int, double, double>> S { get; set; }

        [DisplayName("Datum fakture")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DatumFakture { get; set; }

        [DisplayName("Broj fakture")]
        public string BrojFakture { get; set; }

        //public Faktura S { get; set; }
    }
}