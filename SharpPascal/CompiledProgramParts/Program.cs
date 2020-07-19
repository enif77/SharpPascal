/* SharpPascal - (C) 2020 Premysl Fara 
 
SharpPascal is available under the zlib license:
This software is provided 'as-is', without any express or implied
warranty.  In no event will the authors be held liable for any damages
arising from the use of this software.
Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:
1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.
2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.
3. This notice may not be removed or altered from any source distribution.
 
 */

namespace SharpPascal.CompiledProgramParts
{
    using System;
    using System.Text;


    public class Program : ICompiledProgramPart
    {
        public string Name { get; }

        public ICompiledProgramPart Block { get; set; }


        public Program(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("A program name expected.");

            Name = name;
        }


        public string GenerateOutput()
        {
            var sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine();
            sb.AppendLine("namespace HelloWorld");
            sb.AppendLine("{");
            sb.AppendLine("  class Program");
            sb.AppendLine("  {");
            sb.AppendLine("    static void Main(string[] args)");
            sb.AppendLine("    {");

            if (Block != null)
            {
                sb.Append(Block.GenerateOutput());
            }

            sb.AppendLine("    }");
            sb.AppendLine("  }");

            // TODO: Program methods.

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
