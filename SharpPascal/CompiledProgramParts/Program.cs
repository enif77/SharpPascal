/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.CompiledProgramParts
{
    using System;
    using System.Text;


    /// <summary>
    /// program :: program-heading ';' program-block '.' .
    /// </summary>
    public class Program : ICompiledProgramPart
    {
        public string Name => ProgramHeading.ProgramIdentifier;
        public ProgramHeading ProgramHeading { get; }
        public ICompiledProgramPart Block { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="programHeading">A parsed program heading.</param>
        public Program(ProgramHeading programHeading)
        {
            ProgramHeading = programHeading ?? throw new ArgumentNullException(nameof(programHeading));
        }


        public string GenerateOutput()
        {
            var sb = new StringBuilder();

            sb.AppendLine(ProgramHeading.GenerateOutput());
            sb.AppendLine();
            sb.AppendLine(ProgramSourceTemplate);
            sb.Replace("${PROGRAM-BODY}", (Block == null)
                ? string.Empty
                : Block.GenerateOutput());

            return sb.ToString();
        }


        private const string ProgramSourceTemplate = """
using System;
   
static class Program
{
${PROGRAM-BODY}  

    #region runtime-lib

    private static void _WriteLn(string text = "")
    {
        Console.WriteLine(text);
    }

    #endregion
}
""";
    }
}
