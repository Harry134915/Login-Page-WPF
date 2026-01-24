using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Login_Pape.Commands;

namespace Login_Pape.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _email;
        private string _password;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }

        public MainViewModel()
        {
            LoginCommand = new RelayCommand(Login, CanLogin);
            CloseCommand = new RelayCommand(() => Application.Current.Shutdown());
            MinimizeCommand = new RelayCommand(() =>
                Application.Current.MainWindow.WindowState = WindowState.Minimized);
        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(Email)
                && !string.IsNullOrWhiteSpace(Password);
        }

        private void Login()
        {
            MessageBox.Show("Successfully Signed In");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }

        private bool _isEmailFocused = true; // 默认打开窗口时 Email 获取焦点
        public bool IsEmailFocused
        {
            get => _isEmailFocused;
            set
            {
                _isEmailFocused = value;
                OnPropertyChanged();
            }
        }

        private bool _isPasswordFocused;
        public bool IsPasswordFocused
        {
            get => _isPasswordFocused;
            set
            {
                _isPasswordFocused = value;
                OnPropertyChanged();
            }
        }


    }
}

