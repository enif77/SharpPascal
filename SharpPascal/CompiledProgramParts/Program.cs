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


    public class Program : ICompiledProgramPart, IParentBlock
    {
        public string Name { get; }
        public bool GenerateStdOutputCode { get; }

        /// <summary>
        /// Program has no parent block.
        /// </summary>
        public IParentBlock ParentBlock => this;

        public ICompiledProgramPart Block { get; set; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A program name.</param>
        /// <param name="generateStdOutputCode">If true, standard output code will be generated (writeln etc.)</param>
        public Program(string name, bool generateStdOutputCode)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("A program name expected.");

            Name = name;
            GenerateStdOutputCode = generateStdOutputCode;
        }


        public string GenerateOutput()
        {
            var sb = new StringBuilder(_sourceTemplate);

            sb.Replace("${namespace-name}", Name);
            sb.Replace("${program-body}", (Block == null) ? string.Empty : Block.GenerateOutput());
            sb.Replace("${std-output-code}", GenerateStdOutputCode ? _stdOutputSourceTemplate : string.Empty);

            // TODO: Program methods.

            return sb.ToString();
        }


        private static readonly string _sourceTemplate = @"using System;

namespace ${namespace-name}
{
    static class Program
    {
        static void Main(string[] args)
        {
            ${program-body}
        }

        ${std-output-code}
    }
}";

        private static readonly string _stdOutputSourceTemplate = @"private static _WriteLn(string text = null)
        {
            Console.WriteLine(text);
        }
";

    }
}
