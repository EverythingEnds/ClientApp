namespace ClientApp.Models
{
    public class Logger : Extensions.BindableBase
    {
        private string _logText;

        public string LogText
        {
            get => _logText;
            set => SetProperty(ref _logText, value);
        }

        public void AddInfo(string information)
        {
            LogText += information + "\n";
        }
    }
}