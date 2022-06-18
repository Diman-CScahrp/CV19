using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        #region Properties

        private DataService _DataService;
        private MainWindowViewModel MainModel { get; }

        #region Contries

        private IEnumerable<CountryInfo> _Contries;
        /// <summary>
        /// Статистика по странам
        /// </summary>
        public IEnumerable<CountryInfo> Contries
        {
            get => _Contries;
            private set => Set(ref _Contries, value);
        }

        #endregion

        #endregion

        #region Commands

        public ICommand RefreshDataCommand { get; }
        private void OnRefreshDataCommandExecuted(object p)
        {
            Contries = _DataService.GetData();
        }

        #endregion
        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            this.MainModel = MainModel;
            _DataService = new DataService();

            #region Commands

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);

            #endregion
        }
    }
}
