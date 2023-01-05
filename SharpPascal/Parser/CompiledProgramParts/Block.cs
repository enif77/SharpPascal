/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Parser.CompiledProgramParts
{
    using System.Text;


    public class Block : AProgramBlockBase
    {
        public Block(IProgramBlock parentBlock)
            : base(parentBlock)
        {
        }


        public override string GenerateOutput()
        {
            var sb = new StringBuilder();

            sb.Append("{");

            foreach (var variableDeclaration in VariableDeclarations.Values)
            {
                sb.Append(variableDeclaration.OutputTypeName);
                sb.Append(" ");
                sb.Append(variableDeclaration.Name);
                sb.AppendLine(";");
            }

            sb.AppendLine();

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
 
{
    int a, b, c;

    // Code...
}
 
 */