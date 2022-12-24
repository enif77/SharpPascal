/* SharpPascal - (C) 2020 - 2022 Premysl Fara 
 
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
        static int Main(string[] args)
        {
            Console.WriteLine("Sharp Pascal v1.0");

            if (args.Length < 1)
            {
                Console.WriteLine("USAGE: ptocs.exe source.pas");

                return 0;
            }

            try
            {
                var parser = new Parser(
                new Tokenizer(
                    new StringSourceReader(
                        File.ReadAllText(args[0])
                        )
                    )
                );

                var p = parser.Parse();

                File.WriteAllText(args[0] + ".cs", p.GenerateOutput());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("E: {0}", ex.Message);

                return 1;
            }
            
            Console.WriteLine("DONE");

            return 0;
        }
    }
}

/*
 
SharpPascal
===========

program :: program-heading ';' program-block '.' .
program-heading :: "program" identifier [ '(' program-parameter-list ')' ] .
program-parameter-list :: identifier-list .
identifier-list :: identifier { ',' identifier } .
program-block :: block .
block :: variable-declaration-part statement-part .
variable-declaration-part :: [ "var" variable-declaration ';' { variable-declaration ';' } ] .
variable-declaration :: identifier-list ':' type-denoter .
type-denoter :: "integer" | "real" | "char" | "boolean" | "string" .
statement-part :: compound-statement .
compound-statement :: "begin" statement-sequence "end" .
statement-sequence :: statement { ';' statement } .

statement :: [ label ':' ] ( simple-statement | structured-statement ) .
simple-statement :: empty-statement | assignment-statement | procedure-statement | goto-statement .
empty-statement :: .
assignment-statement :: ( variable-access | function-identifier ) ":=" expression .
procedure-statement :: procedure-identifier [ ( actual-parameter-list | read-parameter-list | readln-parameter-list | write-parameter-list | writeln-parameter-list ) ] .
goto-statement :: "goto" label .
structured-statement :: compound-statement | conditional-statement | repetitive-statement | with-statement .
actual-parameter-list :: '(' actual-parameter { ',' actual-parameter } `)' .
actual-parameter :: expression | variable-access | procedure-identifier | function-identifier .
writeln-parameter-list :: '(' ( file-variable | write-parameter ) { ',' write-parameter } ')' .
write-parameter :: expression [ ':' expression [ ':' expression ] ] .


adding-operator :: '+' | '-' | "or" .
multiplying-operator :: '*' | '/' | "div" | "mod" | "and" .
relational-operator :: '=' | '<>' | '<' | '>' | '<=' | '>=' | "in" .
digit :: '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' .
digit-sequence :: digit { digit } .

sign :: '+' | '-' .
signed-integer :: [ sign ] unsigned-integer .
signed-number :: signed-integer | signed-real .
signed-real :: [ sign ] unsigned-real .
unsigned-integer :: digit-sequence .
unsigned-number :: unsigned-integer | unsigned-real .
unsigned-real :: ( digit-sequence '.' fractional-part [ 'e' scale-factor ] ) | ( digit-sequence 'e' scale-factor ) .
scale-factor :: [ sign ] digit-sequence .
fractional-part :: digit-sequence .

label :: digit-sequence .
label-declaration-part :: [ "label" label {  ',' label } ';' ] .
goto-statement :: "goto" label .


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
