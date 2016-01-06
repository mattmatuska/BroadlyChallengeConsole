using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadlyChallengeConsole
{
    // classes acquired via PasteSpecial->Paste JSON as classes
    // then, renamed and changed arrays to Lists. 

    public class ChallengePage
    {
        public string Note { get; set; }
        public string Url { get; set; }
    }


    public class ClassList
    {
        public string Note { get; set; }

        //public string[] classes { get; set; }
        // modifying to List<string>
        public string[] Classes { get; set; }

    }


    public class Class
    {
        public string Note { get; set; }
        public string Room { get; set; }
        public Student[] Students { get; set; }
        public string Next { get; set; }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
    }

}
