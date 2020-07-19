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
    

    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sharp Pascal v1.0");

            var parser = new Parser(
                //new Tokenizer("program HelloWorld; begin writeln ('Hello, world!'); writeln end."));
                new Tokenizer("program HelloWorld; begin end."));

            var p = parser.Parse();

            Console.WriteLine();
            Console.WriteLine(p.GenerateOutput());
            Console.WriteLine();

            Console.WriteLine("DONE");
        }
    }
}

/*
 
SharpPascal
===========

program :: “program” identifier ‘;’ blok ‘.’ .
blok :: “begin” [ command { ‘;’ command } ] “end” .
command :: procedure-identifier list-of-parameters-writeln .
procedure-identifier :: “writeln” .
list-of-parameters-writeln :: [ ‘(’ parameter-write { ‘,’ parameter-write } ‘)’ ] .
parameter-write :: string .
string :: ‘’’ [ string-character | string-terminator-escape ] ‘’’ .
string-terminator-escape :: “‘’” .
string-character :: any-char-except ‘’’ .


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
