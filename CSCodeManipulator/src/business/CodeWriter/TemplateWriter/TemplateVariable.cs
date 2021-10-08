using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWriter.TemplateWriter
{
    public class TemplateVariable
    {
        public string Name { get; set; }
        public string Value {  get; set; }

        public string Render() 
        {  
            return Value; 
        }
    }
}
