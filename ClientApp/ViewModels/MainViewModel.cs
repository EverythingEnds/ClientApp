using ClientApp.Models;
using Extensions;
using System;
using System.Threading.Tasks;

namespace ClientApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Props and fields
        public Logger AppLogger { get; private set; } = new Logger();
        public ServerDispatcher ServerConnection { get; private set; }

        private GasMeter _currentGasValues;

        public GasMeter CurrentGasValues
        {
            get => _currentGasValues;
            private set
            {
                if (value != _currentGasValues)
                {
                    SetProperty(ref _currentGasValues, value);
                    if (IsSending)
                        if (CanDisconnect)
                            if (ServerConnection.Send(CurrentGasValues))
                                AppLogger.AddInfo("Данные отправлены на сервер.");
                            else
                                AppLogger.AddInfo("Ошибка подключения к серверу.");
                        else
                            AppLogger.AddInfo("Нет подключения к серверу.");
                }
            }
        }

        private bool _canDisconnect;

        public bool CanDisconnect
        {
            get => _canDisconnect;
            set
            {
                if (value != _canDisconnect)
                {
                    CanConnect = false;
                    SetProperty(ref _canDisconnect, value);
                }
            }
        }

        private bool _canConnect;

        public bool CanConnect
        {
            get => _canConnect;
            set
            {
                if (value != _canConnect)
                {
                    CanDisconnect = false;
                    SetProperty(ref _canConnect, value);
                }
            }
        }

        private bool _isGenerating;

        public bool IsGenerating
        {
            get => _isGenerating;
            set
            {
                if (value)
                    AppLogger.AddInfo("Симуляция изменения значений счётчика(ов).");
                SetProperty(ref _isGenerating, value);
            }
        }

        private bool _isSending;

        public bool IsSending
        {
            get => _isSending;
            set => SetProperty(ref _isSending, value);
        }

        public RelayCommand Connect => new RelayCommand
            (
            () => { CanDisconnect = true; },
            () => (CanConnect)
            );

        public RelayCommand Disconnect => new RelayCommand
           (
           () => { CanConnect = true; },
           () => (CanDisconnect)
           );

        #endregion propsAndFields

        #region ctor

        public MainViewModel()
        {
            CanConnect = true;
            IsSending = true;
            AppLogger.AddInfo("Начало работы программы.");
            ServerConnection = new ServerDispatcher(AppLogger);
            ValuesSimulating(AppLogger);
        }

        #endregion ctor

        private void ValuesSimulating(Logger log)
        {

            Task.Run(async () =>
            {
                Random rnd = new Random();
                int counter = 1;
                while (true)
                {
                    if (IsGenerating)
                    {
                        CurrentGasValues = new GasMeter(
                        counter,
                        DateTime.Now,
                        rnd.NextDouble() * 100,
                        rnd.NextDouble() * 100
                        );
                        await Task.Delay(200);
                    }
                }
            });
        }
    }
}