using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationPW
{
    class Plik
    {
        public double weight { get; set; }
        public String name { get; set; }
        public DateTime timeUploadCreate { get; set; }
        public Double queryFactor {get; set;}

        public Plik(double weight, string name, DateTime datetime)
        {
            this.weight = weight;
            this.name = name;
            this.timeUploadCreate = datetime;
        }

        private double generateRandomDouble(int range)
        {
            Random r = new Random();
            double rDouble = r.NextDouble() * range;
            return rDouble;
        }

        public Plik()
        {
        }
    }
}
