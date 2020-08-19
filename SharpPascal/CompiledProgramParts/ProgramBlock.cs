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
    using System.Text;


    public class ProgramBlock : AProgramBlockBase
    {
        public ProgramBlock(IProgramBlock parentBlock)
            : base(parentBlock)
        {
        }


        public override string GenerateOutput()
        {
            var sb = new StringBuilder();

            foreach (var variableDeclaration in VariableDeclarations.Values)
            {
                sb.Append("static ");
                sb.Append(variableDeclaration.OutputTypeName);
                sb.Append(" ");
                sb.Append(variableDeclaration.Name);
                sb.AppendLine(";");
            }

            sb.AppendLine();

            sb.AppendLine("static void Main(string[] args)");
            sb.AppendLine("{");

            foreach (var child in Children)
            {
                // Do not add an empty output from a compiled program part.
                var childOutput = child.GenerateOutput();
                if (string.IsNullOrWhiteSpace(childOutput))
                {
                    continue;
                }
                
                sb.AppendLine(childOutput);
            }

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}

/*
 
static int a;

static void Main(string[] args)
{
    // Code...
}
 
 */