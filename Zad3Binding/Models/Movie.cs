using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad3Binding.Models
{
    public class Movie : INotifyPropertyChanged
    {
        private string title;
        private string director;
        private string studio;
        private Medium medium;
        private TimeSpan duration;
        private DateTime releaseDate;

        public string Title
        {
            get => title;
            set { title = value; OnPropertyChanged(nameof(Title)); }
        }

        public string Director
        {
            get => director;
            set { director = value; OnPropertyChanged(nameof(Director)); }
        }

        public string Studio
        {
            get => studio;
            set { studio = value; OnPropertyChanged(nameof(Studio)); }
        }

        public Medium Medium
        {
            get => medium;
            set { medium = value; OnPropertyChanged(nameof(Medium)); }
        }

        public TimeSpan Duration
        {
            get => duration;
            set { duration = value; OnPropertyChanged(nameof(Duration)); }
        }

        public DateTime ReleaseDate
        {
            get => releaseDate;
            set { releaseDate = value; OnPropertyChanged(nameof(ReleaseDate)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
