/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.CompiledProgramParts
{
    using System.Text;


    public class ProgramBlock : AProgramBlockBase
    {
        public ProgramBlock()
            : base(null)
        {
        }


        public override string GenerateOutput()
        {
            var sb = new StringBuilder();

            foreach (var variableDeclaration in VariableDeclarations.Values)
            {
                sb.Append("static ");
                sb.Append(variableDeclaration.OutputTypeName);
                sb.Append(' ');
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