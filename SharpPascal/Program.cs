using System;

namespace SharpPascal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sharp Pascal v1.0");

            var tokenizer = new Tokenizer();

            tokenizer.Source = "program HelloWorld; begin writeln ('Hello, world!'); writeln end.";

            var t = tokenizer.NextToken();
            while (t.TokenCode != TokenCode.TOK_EOF)
            {
                Console.WriteLine("  > " + t.ToString());

                t = tokenizer.NextToken();
            }

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
