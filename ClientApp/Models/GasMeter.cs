using System;

namespace ClientApp.Models
{
    [Serializable]
    public class GasMeter
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public double Hydrogen { get; set; }
        public double Oxygen { get; set; }

        public GasMeter(int number, DateTime date, double hydrogen, double oxygen)
        {
            Number = number;
            Date = date;
            Hydrogen = hydrogen;
            Oxygen = oxygen;
        }

        public GasMeter() //для сериализации
        {
        }
    }
}