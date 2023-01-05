# SharpPascal

A ISO 7185:1090 Pascal to C# compiler.

## 6.1 Lexical tokens

The syntax given in this subclause describes the formation of lexical tokens
from characters and the separation of these tokens.

### 6.1.1 General

The lexical tokens used to construct Pascal programs are classified into special-symbols,
identifiers, directives, unsigned-numbers, labels, and character-strings . The representation
of any letter (upper case or lower case, differences of font, etc.) occurring anywhere outside
of a character-string (see 6.1.7) shall be insignificant in that occurrence to the meaning
of the program.

```
letter :: 
  'a' | 'b' | 'c' | 'd' | 'e' | 'f' | 'g' | 'h' | 'i' | 'j' |
  'k' | 'l' | 'm' | 'n' | 'o' | 'p' | 'q' | 'r' | 's' | 't' |
  'u' | 'v' | 'w' | 'x' | 'y' | 'z' .

digit ::
  '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' .
```

### 6.1.2 Special-symbols

The special-symbols are tokens having special meanings and are used to delimit the syntactic
units of the language.

```
special-symbol :: 
  '+' | '~' | '*' | '/' | '=' | '<' | '>' | '[' | ']' |
  '.' | ',' | ':' | ';' | '"' | '(' | ')' |
  '<>' | '<=' | '>=' | ':=' | '..' | word-symbol .
  
word-symbol ::
  'and' | 'array' | 'begin' | 'case' | 'const' | 'div' | 
  'do' | 'downto' | 'else' | 'end' | 'file' | 'for' |
  'function' | 'goto' | 'if' | 'in' | 'label' | 'mod' |
  'nil' | 'not' | 'of' | 'or' | 'packed' | 'procedure' |
  'program' | 'record' | 'repeat' | 'set' | 'then' |
  'to' | 'type' | 'until' | 'var' | 'while' | 'with' .  
```

### 6.1.3 Identifiers

Identifiers can be of any length . The spelling of an identifier shall be composed from all
its constituent characters taken in textual order, without regard for the case of letters.
No identifier shall have the same spelling as any word-symbol .

```
identifier :: letter { letter | digit } .

Examples:
  X
  time
  readinteger
  WG4
  AlterHeatSetting
  InquireWorkstationTransformation
  InquireWorkstationIdentification
```

### 6.1.4 Directives

TODO: Parsed as identifiers. The only defined directive - "forward" - will be recognized and used 
by the compiler later.

### 6.1.5 Numbers

An unsigned-integer shall denote in decimal notation a value of integer-type (see 6.4.2.2).
An unsigned-real shall denote in decimal notation a value of real-type (see 6.4.2.2).
The letter `e' preceding a scale-factor shall mean times ten to the power of. The value denoted
by an unsigned-integer shall be in the closed interval 0 to maxint (see 6.4.2.2 and 6.7.2.2).

```
signed-number :: signed-integer | signed-real .
signed-real :: [ sign ] unsigned-real .
signed-integer :: [ sign ] unsigned-integer .
unsigned-number :: unsigned-integer | unsigned-real .
sign = `+' | `-' .
unsigned-real ::
  digit-sequence '.' fractional-part [ 'e' scale-factor ] |
  digit-sequence 'e' scale-factor .
unsigned-integer :: digit-sequence .
fractional-part :: digit-sequence .
scale-factor = [ sign ] digit-sequence .
digit-sequence = digit { digit } .

Examples:
  1e10
  1
  +100
  -0.1
  5e-3
  87.35E+8
```

### 6.1.6 Labels

Labels shall be digit-sequences and shall be distinguished by their apparent integral values and shall
be in the closed interval 0 to 9999 . The spelling of a label shall be its apparent integral value. 

```
label :: digit-sequence .
```

They are recognized as integers now.

### 6.1.7 Character-strings

A character-string containing a single string-element shall denote a value of the required char-type (see 6 .4 .2 .2).
A character-string containing more than one string-element shall denote a value of a string-type (see 6 .4.3.2)
with the same number of components as the character-string contains string-elements.

```
character-string :: ''' string-element { string-element } ''' .
string-element :: apostrophe-image | string-character .
apostrophe-image :: '''' .
string-character :: one-of-a-set-of-implementation-defined-characters .

Examples:
  'A'
  ';'
  ''''
  'Pascal'
  'THIS IS A STRING '
```

### 6.1.8 Token separators

Tokens can be separated by white characters or a comment.
Comments can start with '{' or '(*' and end with '}' or '*)'. 

```
white-character :: ' ' | '\t' | '\r' | '\n' .
comment :: ( '{' | '(*' ) commentary ( '*)' | '}' ) .
```

### 6.1.9 Lexical alternatives

Some tokens given in 6.1.1 to 6.1.8 have defined alternatives:

```
Reference token  Alternative token
  ^                @
  [                (.
  ]                .)
  {                (*
  }                *)
```

## Parser and compiler

### Program

The top level structure of a program:

```pascal
program Hello;
begin 
end.
```

is compiled to:

```csharp
namespace EMPTYPROG
{
    using System;
    
    static class Program
    {
        static void Main()
        {
        }
        
        private static void _WriteLn(string text = "")
        {
            Console.WriteLine(text);
        }
    }
}
```


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
