using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties

        #region SelectedGroup

        private Group _SelectedGroup;

        /// <summary>
        /// Выбранная группа
        /// </summary>
        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set => Set(ref _SelectedGroup, value);
        }

        #endregion

        #region Groups

        public ObservableCollection<Group> Groups { get; }

        #endregion

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

        #region TestDataPoints
        /// <summary>
        /// Тестовый набор данных
        /// </summary>
        private IEnumerable<DataPoint> _TestDataPoints;
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        }
        #endregion

        #region SelectedPageIndex
        private int _SelectedPageIndex;
        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
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

        #region

        public ICommand ChangeTabIndexCommand { get; }
        private bool OnChangeTabIndexCommandCanExecute(object p) => _SelectedPageIndex >= 0;
        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;

            SelectedPageIndex += Convert.ToInt32(p);
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Commands

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, OnCloseApplicationCommandCanExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, OnChangeTabIndexCommandCanExecute);
            
            #endregion

            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x < 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(2 * Math.PI * x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }
            TestDataPoints = data_points;

            var student_index = 1;
            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {student_index}",
                Surname = $"Surname {student_index}",
                Patronymic = $"Patronymic {student_index++}",
                Birthday = DateTime.Now,
                Rating = 0
            });
            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)
            }); ;
            Groups = new ObservableCollection<Group>(groups);
        }
    }
}
