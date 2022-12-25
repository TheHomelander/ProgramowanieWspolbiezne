using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationPW
{
    class Klient
    {
        public string name { get; set; }
        public List<Plik> plikiList = new List<Plik>();
        public long equationCalcValue { get; set; }
        public DateTime timeCreate { get; set; }


        public void addPlik(Plik plik)
        {
            this.plikiList.Add(plik);
        }
    }
}
