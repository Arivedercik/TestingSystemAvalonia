using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystemAvalonia.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdQuestion { get; set; }
        public bool IsTrue {  get; set; }

    }
}
