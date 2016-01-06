using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadlyChallengeConsole
{
    // classes acquired via PasteSpecial->Paste JSON as classes
    // then, renamed.

    public class ChallengePage
    {
        public string note { get; set; }
        public string url { get; set; }
    }


    public class ClassList
    {
        public string note { get; set; }

        //public string[] classes { get; set; }
        // modifying to List<string>
        public List<string> classes { get; set; }

    }


    public class Class
    {
        public string note { get; set; }
        public string room { get; set; }
        public Student[] students { get; set; }
        public string next { get; set; }
    }

    public class Student
    {
        public string name { get; set; }
        public int age { get; set; }
        public string id { get; set; }
    }

}
