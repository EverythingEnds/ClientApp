using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ClientApp.Models
{
    public class ServerDispatcher : Extensions.BindableBase
    {
        public string HostName { get; private set; }
        public int Port { get; private set; }
        private Logger _logger;

        public ServerDispatcher(Logger logger)
        {
            HostName = "127.0.0.1";
            Port = 8070;
            _logger = logger;
        }
        private ServerAnswer _message;

        public ServerAnswer Message
        {
            get => _message;
            set
            {
                _logger.AddInfo("Посылка от счётчика принята, его № - " + value.Number + ", дата принятия -" + value.Date);
                SetProperty(ref _message, value);
            }
        }
        public bool Send(GasMeter gas)
        {
            try
            {
                TcpClient clientSocket = new TcpClient(HostName, Port);
                NetworkStream dataStream = clientSocket.GetStream();

                var json = JsonSerializer.Serialize(gas);
                var sendMessage = Encoding.ASCII.GetBytes(json);

                dataStream.Write(sendMessage, 0, sendMessage.Length);
                dataStream.Flush();

                byte[] bytes = new byte[256];
                var bytesCount = dataStream.Read(bytes, 0, bytes.Length);
                var serverAnswer = Encoding.ASCII.GetString(bytes, 0, bytesCount);
                Message = JsonSerializer.Deserialize<ServerAnswer>(serverAnswer);
                clientSocket.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}