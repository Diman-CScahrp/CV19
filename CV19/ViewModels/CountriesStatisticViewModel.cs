using CV19.Infrastructure.Commands;
using CV19.Models.CV19;
using CV19.Services;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class CountriesStatisticViewModel : ViewModel
    {
        #region Properties

        private IDataService _DataService;
        public MainWindowViewModel MainModel { get; internal set; }

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

        #region SelectedCountry

        private CountryInfo _SelectedCountry;
        public CountryInfo SelectedCountry
        {
            get => _SelectedCountry;
            set => Set(ref _SelectedCountry, value);
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
        public CountriesStatisticViewModel(IDataService service)
        {
            _DataService = service;

            #region Commands

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);

            #endregion
        }
    }
}
