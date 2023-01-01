/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.CompiledProgramParts
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
            _externalFileDescriptors = new Dictionary<string, string>();
        }


        public bool HasExternalFileDescriptor(string name)
        {
            return string.IsNullOrEmpty(name) == false && _externalFileDescriptors.ContainsKey(name);
        }


        public string GetExternalFileDescriptor(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("An external file descriptor name expected.");

            if (_externalFileDescriptors.ContainsKey(name))
            {
                throw new CompilerException($"The '{name}' external file descriptor is not defined.");
            }

            return _externalFileDescriptors[name];
        }


        public void AddExternalFileDescriptor(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("An external file descriptor name expected.");

            if (_externalFileDescriptors.ContainsKey(name))
            {
                throw new CompilerException($"The '{name}' external file descriptor is already defined.");
            }

            _externalFileDescriptors.Add(name, name);
        }


        public string GenerateOutput()
        {
            var sb = new StringBuilder();

            sb.Append("namespace ");
            sb.AppendLine(Name);
            sb.AppendLine("{");
            sb.AppendLine("static class Program");
            sb.AppendLine("{");
            sb.AppendLine((Block == null) ? string.Empty : Block.GenerateOutput());

            // TODO: Program methods.

            sb.Append(_stdOutputSourceTemplate);
            sb.AppendLine("}");
            sb.AppendLine("}");

            return sb.ToString();
        }


        private Dictionary<string, string> _externalFileDescriptors { get; }


        private static readonly string _stdOutputSourceTemplate = @"private static void _WriteLn(string text = """")
        {
            Console.WriteLine(text);
        }
";

    }
}
