using System;
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
            Id = Guid.NewGuid().GetHashCode();
             
            Marks = new List<Mark>(); 
            ImgSrc = new List<projectX.domain.Img>();
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
         
        public List<Mark> Marks { get; set; }  
        public List<projectX.domain.Img> ImgSrc { get; set; }
        public List<CaseResult> CaseResults { get; set; }

        #endregion

        #region notify
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region clone
        public object Clone()
        {
            var newCase = new Case
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description
            };

            var arM = new Mark[this.Marks.Count];
            this.Marks.CopyTo(arM);
            newCase.Marks = arM.ToList();

            var arI = new Img[this.ImgSrc.Count];
            this.ImgSrc.CopyTo(arI);
            newCase.ImgSrc = arI.ToList();

            return newCase;
        }
        #endregion
    }
}
