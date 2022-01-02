using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_API_Database.Database
{
    public class Personne
    {
        public int PersonneID { get; set; }
        public string nom { get; set; } = null!;
        public string prenom { get; set; } = null!;
        public DateTime dateDeNaissance { get; set; }
    }
}
