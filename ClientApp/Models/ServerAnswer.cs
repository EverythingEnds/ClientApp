using System;

namespace ClientApp.Models
{
    [Serializable]
    public class ServerAnswer
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }

        public ServerAnswer(int number, DateTime date)
        {
            Number = number;
            Date = date;
        }

        public ServerAnswer()//для сериализации
        {
        }
    }
}