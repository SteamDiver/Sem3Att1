using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using libTask3.Annotations;

namespace libTask3
{
    public abstract class DomesticMovie : IMovie
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; }

        private int _currentPosition;
        public int CurrentPosition
        {
            get => _currentPosition;
            set
            {  
                if (value >= Duration)
                {
                    _currentPosition = Duration;
                    Stop();
                }
                else
                {
                    _currentPosition = value < 0 ? 0 : value;
                }

                OnPropertyChanged(nameof(CurrentPosition));
            }
        }

        public bool IsStarted => T.Enabled;
        public Timer T { get; } = new Timer(1000);

        protected DomesticMovie(string name, string director, int duration)
        {
            Name = name;
            Duration = duration;
            Director = director;

            T.Elapsed += (sender, args) =>
            {
                CurrentPosition++;
                if (CurrentPosition == Duration)
                    Stop();
            };
        }

        public void Start()
        {
            if (CurrentPosition == Duration)
                CurrentPosition = 0;
            T.Start();
            OnPropertyChanged(nameof(IsStarted));
        }

        public void Stop()
        {
            T.Stop();
            OnPropertyChanged(nameof(IsStarted));
        }

        public abstract void Forward(int seconds);

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}