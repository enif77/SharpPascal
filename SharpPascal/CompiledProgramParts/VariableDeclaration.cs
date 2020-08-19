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


    public class VariableDeclaration
    {
        public string Name { get; private set; }
        public string TypeName { get; private set; }
        public string OutputTypeName { get; private set; }


        private VariableDeclaration()
        { 
        }


        public static VariableDeclaration CreateIntegerVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeName = "integer",
                OutputTypeName = "int"
            };
        }


        public static VariableDeclaration CreateRealVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeName = "real",
                OutputTypeName = "double"
            };
        }


        public static VariableDeclaration CreateCharVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeName = "char",
                OutputTypeName = "char"
            };
        }


        public static VariableDeclaration CreateBooleanVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeName = "boolean",
                OutputTypeName = "bool"
            };
        }


        public static VariableDeclaration CreateStringVariableDeclaration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A variable name expected.");

            return new VariableDeclaration()
            {
                Name = name,
                TypeName = "string",
                OutputTypeName = "string"
            };
        }
    }
}
