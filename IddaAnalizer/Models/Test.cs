using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IddaAnalyser
{
    public class Test
    {
        public Test()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public void SetName(string name)
        {
            this.Name = name;
        }

    }
}
