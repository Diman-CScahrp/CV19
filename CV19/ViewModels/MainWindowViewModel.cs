using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties


        private readonly CountriesStatisticViewModel _ContriesStatistic;

        #region SelectedDirectiory

        private DirectoryViewModel _SelectedDirectory;
        /// <summary>
        /// Текущая папка
        /// </summary>
        public DirectoryViewModel SelectedDirectory
        {
            get => _SelectedDirectory;
            set => Set(ref _SelectedDirectory, value);
        }

        #endregion

        #region DiskRootDir
        public DirectoryViewModel DiskRootDir { get; } = new DirectoryViewModel("C:\\");
        #endregion

        #region TestStudent
        public IEnumerable<Student> TestStudent =>
            Enumerable.Range(1, App.IsDesignMode ? 10 : 100_000)
            .Select(i => new Student()
            {
                Name = $"Имя {i}",
                Surname= $"Фамилия {i}"
            });
        #endregion

        #region SelectedGroupStudent
        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();
        private void OnStudentFiltred(object sender, FilterEventArgs e)
        {
            Student student = e.Item as Student;

            if (student.Name is null || student.Surname is null || student.Patronymic is null)
            {
                e.Accepted = false;
                return;
            }

            string filter_text = _StudentFilterText;

            if (string.IsNullOrWhiteSpace(filter_text))
            {
                return;
            }
            if (student != null)
            {
                if(student.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase) ||
                        student.Surname.Contains(filter_text, StringComparison.OrdinalIgnoreCase) ||
                        student.Patronymic.Contains(filter_text, StringComparison.OrdinalIgnoreCase))
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }
        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View;
        #endregion

        #region StudentFilterText

        private string _StudentFilterText;
        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if (!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudents.View.Refresh();
            }
        }

        #endregion

        #region CompositeCollection
        public object[] CompositeCollection { get; }
        #endregion

        #region SelectedCompositeValue

        private object _SelectedCompositeValue;
        public object SelectedCompositeValue
        {
            get => _SelectedCompositeValue;
            set => Set(ref _SelectedCompositeValue, value);
        }

        #endregion

        #region SelectedGroup

        private Group _SelectedGroup;

        /// <summary>
        /// Выбранная группа
        /// </summary>
        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set {
                if (!Set(ref _SelectedGroup, value)) return;
                _SelectedGroupStudents.Source = value?.Students;
                OnPropertyChanged(nameof(SelectedGroupStudents));
            }
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

        #region ChangeTabIndexCommand

        public ICommand ChangeTabIndexCommand { get; }
        private bool OnChangeTabIndexCommandCanExecute(object p) => _SelectedPageIndex >= 0;
        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;

            SelectedPageIndex += Convert.ToInt32(p);
        }

        #endregion

        #region CreateGroup

        public ICommand CreateGroupCommand { get; }
        private bool CanCreateGroupCommandExecute(object p) => true;
        private void OnCreateGroupCommandExecuted(object p)
        {
            var group_max_index = Groups.Count + 1;
            var new_group = new Group()
            {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>()
            };

            Groups.Add(new_group);
        }

        #endregion

        #region DeleteGroup
        public ICommand DeleteGroupCommand { get; }
        private bool CanDeleteGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);
        private void OnDeleteGroupCommandExecuted(object p)
        {
            if (!(p is Group group)) return;
            var group_index = Groups.IndexOf(group);
            Groups.Remove(group);
            if (group_index < Groups.Count)
                SelectedGroup = Groups[group_index];
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            _ContriesStatistic = new CountriesStatisticViewModel(this);

            #region Commands

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, OnCloseApplicationCommandCanExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, OnChangeTabIndexCommandCanExecute);
            CreateGroupCommand = new LambdaCommand(OnCreateGroupCommandExecuted, CanCreateGroupCommandExecute);
            DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);

            #endregion

            #region other
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
            });
            Groups = new ObservableCollection<Group>(groups);

            var data_list = new List<object>();
            data_list.Add("Hello World!");
            data_list.Add(42);
            var group = Groups[1];
            data_list.Add(group);
            data_list.Add(group.Students[0]);

            CompositeCollection = data_list.ToArray();
            #endregion

            _SelectedGroupStudents.Filter += OnStudentFiltred;
        }
    }
}
