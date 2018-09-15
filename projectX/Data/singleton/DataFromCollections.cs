using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectX.domain;
using projectX.Data.interfaces;

namespace projectX.Data
{
    public sealed class DataFromCollections : ICaseCrud, IProectCrud
    {
        #region singleton
        private static readonly Lazy<DataFromCollections> Lazy =
            new Lazy<DataFromCollections>(() => new DataFromCollections());

        public static DataFromCollections Instance => Lazy.Value;
        #endregion

        private DataFromCollections()
        {
            Cases = new ObservableCollection<Case>
            {
                new Case {///Proects = new ObservableCollection<Proect>(),
                    Description = "1111 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                    Marks = new List<Mark>{new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"}},
                    ImgSrc = new List<projectX.domain.Img>{ new Img{src = @"/resources/Chrysanthemum.jpg" } },
                    Name = " First Case"},
                new Case {//Proects = new ObservableCollection<Proect>(),
                    Description = "22222 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                    Marks = new List<Mark>{new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"}},
                    Name = " Second Case"},
                new Case {//Proects = new ObservableCollection<Proect>(),
                    Description = "33333 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                    Marks = new List<Mark>{new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"}},
                    Name = " Therd Case"},
                new Case {//Proects = new ObservableCollection<Proect>(),
                    Description = "44444 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                    Marks = new List<Mark>{new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"}},
                    Name = " 4th Case"},
                new Case {//Proects = new ObservableCollection<Proect>(),
                    Description = "555555 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                    Marks = new List<Mark>{new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"},new Mark{Text = "11111"}},
                    Name = " 5th Case"},
            };
            Proects = new ObservableCollection<Proect>()
            {
                new Proect(){Name = "Proect_1",
                    Description = "555555 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    Marks = new ObservableCollection<string>{"#Case#5","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf"}}, 
                new Proect(){Name = "Proect_2",
                    Description = "555555 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    Marks = new ObservableCollection<string>{"#Case#5","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf"}},
                new Proect(){Name = "Proect_3",
                    Description = "555555 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    Marks = new ObservableCollection<string>{"#Case#5","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf"}},
                new Proect(){Name = "Proect_4",
                    Description = "555555 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    Marks = new ObservableCollection<string>{"#Case#5","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf"}},
                new Proect(){Name = "Proect_5",
                    Description = "555555 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    Marks = new ObservableCollection<string>{"#Case#5","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf"}},
                new Proect(){Name = "Proect_6",
                    Description = "555555 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    Marks = new ObservableCollection<string>{"#Case#5","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf"}}
            };
            Marks = new ObservableCollection<string>();
        } 

        public ObservableCollection<Case> Cases { get; set; }
        public ObservableCollection<Proect> Proects { get; set; }
        public ObservableCollection<string> Marks { get; set; }

        #region proectCrud
        public void AddProect(Proect newProect)
        {
            Proects.Add(newProect);
        }

        public void RemoveProect(Proect remProect)
        {
            Proects.Remove(remProect);
        }

        public void EditProect(Proect newProect)
        {
            var oldProect = Proects.FirstOrDefault((item) => item.Id == newProect.Id);
            var index = Proects.IndexOf(oldProect);
            Proects.Remove(oldProect);
            Proects.Insert(index, newProect);
        }
        #endregion

        #region CaseCrud
        public Case AddCase(Case newCase)
        {
             Cases.Add(newCase);
            return null;
        }

        public void RemoveCace(Case remCase)
        {
            Cases.Remove(remCase);
        }

        public void EditCase(Case newCase)
        {
            var oldCase = Cases.FirstOrDefault((item) => item.Id == newCase.Id);
            var index = Cases.IndexOf(oldCase);
            Cases.Remove(oldCase);
            Cases.Insert(index, newCase);
        }
        #endregion
    }
}
