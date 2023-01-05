/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Parser.CompiledProgramParts
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class Program : ICompiledProgramPart
    {
        public string Name { get; }
        public ICompiledProgramPart Block { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A program name.</param>
        public Program(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("A program name expected.");

            Name = name;
            ExternalFileDescriptors = new Dictionary<string, string>();
        }


        public bool HasExternalFileDescriptor(string name)
        {
            return string.IsNullOrEmpty(name) == false && ExternalFileDescriptors.ContainsKey(name);
        }


        public string GetExternalFileDescriptor(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("An external file descriptor name expected.");

            if (ExternalFileDescriptors.ContainsKey(name))
            {
                throw new CompilerException($"The '{name}' external file descriptor is not defined.");
            }

            return ExternalFileDescriptors[name];
        }


        public void AddExternalFileDescriptor(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("An external file descriptor name expected.");

            if (ExternalFileDescriptors.ContainsKey(name))
            {
                throw new CompilerException($"The '{name}' external file descriptor is already defined.");
            }

            ExternalFileDescriptors.Add(name, name);
        }


        public string GenerateOutput()
        {
            var sb = new StringBuilder(ProgramSourceTemplate);

            sb.Replace("${PROGRAM-NAME}", Name);
            sb.Replace("${PROGRAM-BODY}", (Block == null)
                ? string.Empty
                : Block.GenerateOutput());

            return sb.ToString();
        }


        private Dictionary<string, string> ExternalFileDescriptors { get; }
        
        private const string ProgramSourceTemplate = @"namespace ${PROGRAM-NAME}
{
    using System;
   
    static class Program
    {
${PROGRAM-BODY}  

        private static void _WriteLn(string text = """")
        {
            Console.WriteLine(text);
        }
    }     
}";
    }
}
