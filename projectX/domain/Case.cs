﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using projectX.Annotations;

namespace projectX.domain
{
    public class Case : INotifyPropertyChanged, ICloneable
    {
        private string _name;
        private string _description;


        //ctor
        public Case()
        {
            //Id = Guid.NewGuid().GetHashCode();

            //Proects = new ObservableCollection<Proect>();
            //Marks = new ObservableCollection<string>();
            Marks = new List<string>();
            ImgSrc = new List<string>();
        }


        #region property

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description == value) return;
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        //public ObservableCollection<Proect> Proects { get; set; } 
        //public ObservableCollection<string> Marks { get; set; }  
        //public ObservableCollection<string> ImgSrc { get; set; }
        public List<string> Marks { get; set; }  
        public List<string> ImgSrc { get; set; }

        #endregion



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
