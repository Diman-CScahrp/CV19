using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties
        #region Title
        private string _Title = "Анализ статистики CV19";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion
        #region Status
        private string _Status = "Готов!";
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }
        #endregion
        #endregion
        #region Commands
        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; } 
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool OnCloseApplicationCommandCanExecute(object p) => true;
        #endregion
        #endregion

        public MainWindowViewModel()
        {
            #region Commands
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, OnCloseApplicationCommandCanExecute);
            #endregion
        }
    }
}
