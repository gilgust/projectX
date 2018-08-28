using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectX.domain;

namespace projectX.models
{
    public class DomainColections
    {
        public ObservableCollection<Case> Cases { get; set; }
        public ObservableCollection<Proect> Proects { get; set; }

        public DomainColections()
        {
            Cases = new ObservableCollection<Case>
            {
                new Case {Proects = new List<Proect>(),
                    Description = "1111 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                    Marks = new ObservableCollection<string> { "#Case#1", "#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf"},
                    ImgSrc = new List<string>{ @"resources/Chrysanthemum.jpg" },
                    Name = " First Case"},
                new Case {Proects = new List<Proect>(),
                    Description = "22222 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                    Marks = new ObservableCollection<string>{ "#Case#2", "#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf2222222222222222222222222222222222222222222"},

                    Name = " Second Case"},
                new Case {Proects = new List<Proect>(),
                    Description = "33333 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                    Marks = new ObservableCollection<string>{ "#Case#3", "#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf111111111111111111111111111111"},

                    Name = " Therd Case"},
                new Case {Proects = new List<Proect>(),
                    Description = "44444 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                    Marks = new ObservableCollection<string>{ "#Case#4", "#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf"},

                    Name = " 4th Case"},
                new Case {Proects = new List<Proect>(),
                    Description = "555555 Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                    Marks = new ObservableCollection<string>{"#Case#5","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf","#dfsdfsdf"},
                   
                    Name = " 5th Case"},
            };
            Proects = new ObservableCollection<Proect>();
        }
    }
}
