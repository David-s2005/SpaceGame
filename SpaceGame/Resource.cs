using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    class Resource
    {
        private string name;

        public Resource(string _name) 
        {
            if (name != null)
            {
                name = _name;
            }
            else 
            {
                name = "Null";
            }
        }

        public string Name 
        {
            get 
            {
                return name; 
            }
            set 
            {
                name = value;
            }
        }
    }
}
