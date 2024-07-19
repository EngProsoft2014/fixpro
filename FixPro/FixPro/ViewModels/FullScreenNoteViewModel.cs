using FixPro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FixPro.ViewModels
{
    public class FullScreenNoteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        bool _IsBusy;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsBusy"));
                }
            }
        }

        string _Note;
        public string Note
        {
            get
            {
                return _Note;
            }
            set
            {
                _Note = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Note"));
                }
            }
        }

        public delegate void NoteDelegte(string Note);
        public event NoteDelegte NoteClose;

        public ICommand ApplyNote { get; set; }

        public FullScreenNoteViewModel()
        {
            
        }

        public FullScreenNoteViewModel(string note)
        {
            Note = note;  
            ApplyNote = new Command<string>(OnApplyNote);
        }

        async void OnApplyNote(string note)
        {
            IsBusy = true;

            NoteClose.Invoke(note);
           
            await App.Current.MainPage.Navigation.PopAsync();
            IsBusy = false;
        }


    }
}
