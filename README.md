# SharpPascal

A ISO 7185 Pascal to C# compiler.

## BNF

This needs to be cleaned up to match the actual compiler state...

```
program :: program-heading ';' program-block '.' .
program-heading :: "program" identifier [ '(' program-parameter-list ')' ] .
program-parameter-list :: identifier-list .
identifier-list :: identifier { ',' identifier } .
identifier :: letter { letter | digit } .
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
string-terminator :: ''' .
string-terminator-escape :: "''" .
string-character :: any char except string-terminator .
comment :: comment-start { any char except comment-start or comment-end } comment-end .
comment-start :: '{' | "(*" .
comment-end :: '}' | "*)" .
```

## Examples

```
(* Empty program. *)
program HelloWorld; begin end.
```

```
(* Hello world in Pascal. *)
program HelloWorld;
begin
  writeln ('Hello, world!')
end.
```

The Hello world program will be translated into the following C# code:

```
using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            _WriteLn("Hello, world!");
        }

        private static _WriteLn(string text = null)
        {
            Console.WriteLine(text);
        }
    }
}
```
