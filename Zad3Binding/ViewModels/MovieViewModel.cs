using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zad3Binding.Commands;
using Zad3Binding.Models;


namespace Zad3Binding.ViewModels
{

    public class MovieViewModel : INotifyPropertyChanged
    {
        private Movie selectedMovie;

        public ObservableCollection<Movie> Movies { get; set; }

        public Movie SelectedMovie
        {
            get => selectedMovie;
            set { selectedMovie = value; OnPropertyChanged(nameof(SelectedMovie)); }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ImportCommand { get; }
        public ICommand ExportCommand { get; }

        public MovieViewModel()
        {
            Movies = new ObservableCollection<Movie>();
            AddCommand = new RelayCommand(AddMovie);
            EditCommand = new RelayCommand(EditMovie, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteMovie, CanEditOrDelete);
            ImportCommand = new RelayCommand(ImportMovies);
            ExportCommand = new RelayCommand(ExportMovies);
        }
        public IEnumerable<Medium> Mediums
        {
            get { return Enum.GetValues(typeof(Medium)).Cast<Medium>(); }
        }

        private void AddMovie(object parameter)
        {
            var newMovie = new Movie();
            Movies.Add(newMovie);
            SelectedMovie = newMovie;
            var editWindow = new EditMovieWindow { DataContext = new MovieViewModel { SelectedMovie = newMovie } };
            editWindow.ShowDialog();
        }

        private void EditMovie(object parameter)
        {
            if (SelectedMovie != null)
            {
                var editWindow = new EditMovieWindow { DataContext = new MovieViewModel { SelectedMovie = SelectedMovie } };
                editWindow.ShowDialog();
            }
        }

        private void DeleteMovie(object parameter)
        {
            if (SelectedMovie != null)
            {
                Movies.Remove(SelectedMovie);
            }
        }

        private bool CanEditOrDelete(object parameter)
        {
            return SelectedMovie != null;
        }

        private void ImportMovies(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var jsonData = File.ReadAllText(openFileDialog.FileName);
                var importedMovies = JsonConvert.DeserializeObject<List<Movie>>(jsonData);
                foreach (var movie in importedMovies)
                {
                    Movies.Add(movie);
                }
            }
        }

        private void ExportMovies(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                var jsonData = JsonConvert.SerializeObject(Movies.ToList());
                File.WriteAllText(saveFileDialog.FileName, jsonData);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
