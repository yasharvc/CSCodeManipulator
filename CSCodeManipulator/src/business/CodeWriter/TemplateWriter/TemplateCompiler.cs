using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWriter.TemplateWriter
{
    public class TemplateCompiler
    {
        List<TemplateVariable> Variables {  get; set; } = new List<TemplateVariable>();

        public TemplateCompiler()
        {

        }

        public TemplateCompiler(params TemplateVariable[] variables) => Variables.AddRange(variables);


    }
}
