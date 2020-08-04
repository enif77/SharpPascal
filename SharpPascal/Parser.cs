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
    using System.Collections.Generic;
    using System.Text;

    using SharpPascal.CompiledProgramParts;
    using SharpPascal.Tokens;


    public class Parser
    {
        public Tokenizer Tokenizer { get; }


        public Parser(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException(nameof(tokenizer));

            Tokenizer = tokenizer;
        }


        public ICompiledProgramPart Parse()
        {
            return ParseProgram();
        }


        /// <summary>
        /// program :: "program" identifier [ external-file-descriptors-list ] ';' blok '.' .
        /// </summary>
        /// <returns>A compiled program.</returns>
        private ICompiledProgramPart ParseProgram()
        {
            // program.
            var t = Tokenizer.NextToken();
            if (t.TokenCode != TokenCode.TOK_KEY_PROGRAM)
            {
                throw new CompilerException("The 'PROGRAM' key word expected.");
            }

            // identifier.
            t = Tokenizer.NextToken();
            if (t.TokenCode != TokenCode.TOK_IDENT)
            {
                throw new CompilerException("A program name expected.");
            }

            var programName = t.StringValue;

            // Eat identifier.
            t = Tokenizer.NextToken();

            var generateStdOutputCode = false;

            // external-file-descriptors-list?
            if (t.TokenCode == TokenCode.TOK_LBRA)
            {
                ParseExternalFileDescriptorsList(out generateStdOutputCode);

                // Eat ')'.
                t = Tokenizer.NextToken();
            }
            
            // ';'.
            if (t.TokenCode != TokenCode.TOK_SEP)
            {
                throw new CompilerException("The program name separator ';' expected.");
            }

            var program = new CompiledProgramParts.Program(programName, generateStdOutputCode);
            program.Block = ParseProgramBlock(new ProgramBlock(null));

            // Eat "end".
            t = Tokenizer.NextToken();
            if (t.TokenCode != TokenCode.TOK_PROG_END)
            {
                throw new CompilerException("The program end '.' expected.");
            }

            return program;
        }

        /// <summary>
        /// external-file-descriptors-list :: '(' "output" ')' .
        /// </summary>
        private void ParseExternalFileDescriptorsList(out bool generateStdOutputCode)
        {
            var t = Tokenizer.NextToken();
            if (t.TokenCode != TokenCode.TOK_IDENT)
            {
                throw new CompilerException("An external file descriptor name expected.");
            }

            if (t.StringValue == "OUTPUT")
            {
                generateStdOutputCode = true;
            }
            else
            {
                throw new CompilerException($"Unsupported external file descriptor name '{t.StringValue}' found.");
            }

            t = Tokenizer.NextToken();
            if (t.TokenCode != TokenCode.TOK_RBRA)
            {
                throw new CompilerException("The end of external file descriptors list ')' expected.");
            }
        }

        /// <summary>
        /// blok :: "begin" [ command { ';' command } ] "end" .
        /// </summary>
        /// <param name="parentBlock">A parent program block.</param>
        /// <returns>An ICompiledProgramPart instance representing this compiled program part.</returns>
        private ICompiledProgramPart ParseProgramBlock(IProgramBlock parentBlock)
        {
            var t = Tokenizer.NextToken();
            if (t.TokenCode != TokenCode.TOK_KEY_BEGIN)
            {
                throw new CompilerException("The 'BEGIN' key word expected.");
            }

            var block = new ProgramBlock(parentBlock);

            t = Tokenizer.NextToken();
            while (t.TokenCode != TokenCode.TOK_EOF)
            {
                block.AddCompiledProgramPart(ParseCommand(block, t));

                t = Tokenizer.CurrentToken;
                if (t.TokenCode == TokenCode.TOK_KEY_END)
                {
                    break;
                }

                if (t.TokenCode == TokenCode.TOK_SEP)
                {
                    t = Tokenizer.NextToken();

                    continue;
                }

                throw new CompilerException("The ';' command separator expected.");
            }

            if (t.TokenCode != TokenCode.TOK_KEY_END)
            {
                throw new CompilerException("The 'END' key word expected.");
            }

            return block;
        }


        /// <summary>
        /// command :: empty-command | procedure-identifier list-of-parameters-writeln .
        /// procedure-identifier:: "writeln" .
        /// list-of-parameters-writeln:: ['(' parameter - write { ',' parameter-write } ')' ] .
        /// parameter-write:: string .
        /// </summary>
        /// <param name="parentBlock">A parent program block.</param>
        /// <returns>An ICompiledProgramPart instance representing this compiled program part</returns>
        private ICompiledProgramPart ParseCommand(IProgramBlock parentBlock, IToken currentToken)
        {
            // procedure-ident paramaters
            if (currentToken.TokenCode == TokenCode.TOK_IDENT)
            {
                var procedureIdentifier = currentToken.StringValue.ToLowerInvariant();
                if (procedureIdentifier == "writeln")
                {
                    // Eat "writeln".
                    var t = Tokenizer.NextToken();
                    if (t.TokenCode == TokenCode.TOK_LBRA)
                    {
                        // Eat "(";
                        t = Tokenizer.NextToken();
                        if (t.TokenCode == TokenCode.TOK_STR)
                        {
                            var str = t.StringValue;

                            // Eat string.
                            t = Tokenizer.NextToken();
                            if (t.TokenCode == TokenCode.TOK_RBRA)
                            {
                                // Eat ")".
                                _ = Tokenizer.NextToken();

                                return new WritelnCommand(parentBlock, str);
                            }
                            else
                            {
                                throw new CompilerException("The end of formal parameters (')') expected.");
                            }
                        }
                        else
                        {
                            throw new CompilerException("A writeln parameter expected.");
                        }
                    }                 

                    return new WritelnCommand(parentBlock);
                }
            }
            else if (currentToken.TokenCode == TokenCode.TOK_SEP || currentToken.TokenCode == TokenCode.TOK_KEY_END)
            {
                return new EmptyCommand(parentBlock);
            }

            throw new CompilerException($"Unexpected token: {currentToken}");
        }
    }
}
