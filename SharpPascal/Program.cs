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


namespace SharpPascal
{
    using System;
    using System.IO;


    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sharp Pascal v1.0");

            if (args.Length < 1)
            {
                Console.WriteLine("USAGE: ptocs.exe source.pas");

                return;
            }

            var parser = new Parser(
                new Tokenizer(File.ReadAllText(args[0])));

            var p = parser.Parse();

            File.WriteAllText(args[0] + ".cs", p.GenerateOutput());

            Console.WriteLine("DONE");
        }
    }
}

/*
 
SharpPascal
===========

program :: "program" identifier [ '(' program-parameter-list ')' ] ';' program-block '.' .
program-parameter-list :: "output" .
program-block :: block .
block :: variable-declaration-part "begin" [ command { ';' command } ] "end" .
variable-declaration-part :: [ "var" variable-declaration ';' { variable-declaration ';' } ] .
variable-declaration :: identifier-list ':' type-denoter .
identifier-list :: identifier { ',' identifier } .
type-denoter :: "integer" | "real" | "char" | "boolean" | "string" .
command :: procedure-identifier list-of-parameters-writeln .
procedure-identifier :: "writeln" .
list-of-parameters-writeln :: [ '(' parameter-write { ',' parameter-write } ')' ] .
parameter-write :: string .
string :: string-terminator [ string-character | string-terminator-escape ] string-terminator .
string-terminator :: ''';
string-terminator-escape :: "''" .
string-character :: any char except string-terminator .
comment :: comment-start { any char except comment-start or comment-end } comment-end .
comment-start :: '{' | "(*" .
comment-end :: '}' | "*)" .


program HelloWorld; begin end.


program HelloWorld;
begin
  writeln ('Hello, world!');
  writeln
end.


using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            _WriteLn("Hello, world!");
            _WriteLn();
        }


        private static _WriteLn(string text = null)
        {
            Console.WriteLine(text);
        }
    }
}
 
 
 */
