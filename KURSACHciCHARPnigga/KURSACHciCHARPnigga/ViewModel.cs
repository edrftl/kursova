using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KURSACHciCHARPnigga
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Room _ourPos;

        public Room OurPos
        {
            get { return _ourPos; }
            set
            {
                if (_ourPos != value)
                {
                    _ourPos = value;
                    OnPropertyChanged(nameof(OurPos));
                    OnPropertyChanged(nameof(PlaceName));
                }
            }
        }
        RelayCommand firstButtonCommand;
        RelayCommand secondButtonCommand;
        RelayCommand thirdButtonCommand;

        public ICommand firstCommand => firstButtonCommand;
        public ICommand secondCommand => secondButtonCommand;
        public ICommand thirdCommand => thirdButtonCommand;
        public ViewModel()
        {
            firstButtonCommand = new RelayCommand((o) => { MoveTo(1); },
                    (o) => firstCommand != null);
            secondButtonCommand = new RelayCommand((o) => { MoveTo(2); },
                    (o) => secondCommand != null);
            thirdButtonCommand = new RelayCommand((o) => { MoveTo(3); },
                   (o) => thirdCommand != null);
        }
        public void MoveTo(int num)
        {
            if (OurPos != null)
            {
                switch (num)
                {
                    //case 1:
                    //    OurPos = OurPos?.firstRoom;
                    //    break;
                    //case 2:
                    //    OurPos = OurPos?.secondRoom;
                    //    break;
                    //case 3:
                    //    OurPos = OurPos?.thirdRoom;
                    //    break;
                    default:
                        break;
                }
            }
        }



        public string PlaceName => OurPos?.NameOfRoom;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
