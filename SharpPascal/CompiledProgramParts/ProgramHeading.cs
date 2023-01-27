/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.CompiledProgramParts
{
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// program-heading :: "program" identifier [ '(' program-parameter-list ')' ] .
    /// </summary>
    public class ProgramHeading : ICompiledProgramPart
    {
        public string ProgramIdentifier { get; }
        

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="programIdentifier">A program identifier.</param>
        public ProgramHeading(string programIdentifier)
        {
            if (string.IsNullOrWhiteSpace(programIdentifier)) throw new ArgumentException("A program identifier expected.");

            ProgramIdentifier = programIdentifier;
            ExternalFileDescriptors = new Dictionary<string, string>();
        }


        public bool HasExternalFileDescriptor(string name)
        {
            return string.IsNullOrWhiteSpace(name) == false && ExternalFileDescriptors.ContainsKey(name);
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
            return $"namespace {ProgramIdentifier};";
        }


        private Dictionary<string, string> ExternalFileDescriptors { get; }
    }
}
