﻿/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.CompiledProgramParts
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
                sb.Append(variableDeclaration.GenerateOutput());
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