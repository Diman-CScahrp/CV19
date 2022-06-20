using CV19.Infrastructure.Commands;
using CV19.Models.CV19;
using CV19.Services;
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

        private DataCountriesService _DataService;
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
        public CountriesStatisticViewModel() : this(null)
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException("Вызов конструктора, непредназначенного для использования пользователем");

            //_Contries = Enumerable.Range(1, 10)
            //    .Select(i => new CountryInfo
            //    {
            //        Name=$"Country {i}",
            //        ProvincesCounts = Enumerable.Range(1, 10).Select(j => new PlaceInfo
            //        {
            //            Name=$"Province {i}",
            //            Location = new System.Windows.Point(i, j),
            //            Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount 
            //            { 
            //                Date=DateTime.Now.Subtract(TimeSpan.FromDays(100-j)),
            //                Count = k
            //            }).ToArray()
            //        }).ToArray()
            //    }).ToArray();
        }
        public CountriesStatisticViewModel(MainWindowViewModel MainModel)
        {
            this.MainModel = MainModel;
            _DataService = new DataCountriesService();

            #region Commands

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);

            #endregion
        }
    }
}
