﻿/* SharpPascal - (C) 2020 Premysl Fara 
 
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
        /// program :: program-heading ';' program-block '.' EOF .
        /// </summary>
        /// <returns>A compiled program.</returns>
        private ICompiledProgramPart ParseProgram()
        {
            // Read the firs token.
            Tokenizer.NextToken();

            var program = ParseProgramHeading();

            // ';'.
            ExpectAndEat(TokenCode.TOK_SEP, "The program name separator ';' expected.");

            ((CompiledProgramParts.Program)program).Block = ParseProgramBlock();

            // '.'.
            ExpectAndEat(TokenCode.TOK_PROG_END, "The program end '.' expected.");

            // EOF.
            Expect(TokenCode.TOK_EOF, "No more tokens expected.");

            return program;
        }

        /// <summary>
        /// program-heading :: "program" identifier [ '(' program-parameter-list ')' ] .
        /// program-block :: block .
        /// </summary>
        /// <returns>A compiled program.</returns>
        private ICompiledProgramPart ParseProgramHeading()
        {
            // "PROGRAM".
            ExpectAndEat(TokenCode.TOK_KEY_PROGRAM, "The 'PROGRAM' key word expected.");

            // An identifier.
            Expect(TokenCode.TOK_IDENT, "A program name expected.");
            var programName = Tokenizer.CurrentToken.StringValue;

            // program-parameter-list?
            var t = Tokenizer.NextToken();
            if (t.TokenCode == TokenCode.TOK_LBRA)
            {
                // Eat '('.
                Tokenizer.NextToken();

                ParseProgramParameterList();

                // ')'.
                ExpectAndEat(TokenCode.TOK_RBRA, "The end of program parameter list ')' expected.");
            }

            return new CompiledProgramParts.Program(programName);
        }

        /// <summary>
        /// program-parameter-list :: identifier-list .
        /// identifier-list :: identifier { ',' identifier } .
        /// </summary>
        private void ParseProgramParameterList()
        {
            Expect(TokenCode.TOK_IDENT, "An external file descriptor identifier expected.");

            if (Tokenizer.CurrentToken.StringValue != "OUTPUT")
            {
                throw new CompilerException($"Unsupported external file descriptor name '{Tokenizer.CurrentToken.StringValue}' found.");
            }

            // Eat "OUTPUT".
            Tokenizer.NextToken();
        }

        /// <summary>
        /// program-block :: block .
        /// </summary>
        /// <param name="parentBlock"></param>
        /// <returns></returns>
        private ICompiledProgramPart ParseProgramBlock()
        { 
            return ParseBlock(null);
        }

        /// <summary>
        /// --block :: variable-declaration-part "begin" [ command { ';' command } ] "end" .
        /// 
        /// block :: variable-declaration-part statement-part .
        /// variable-declaration-part :: [ "var" variable-declaration ';' { variable-declaration ';' } ] .
        /// statement-part :: compound-statement .
        /// compound-statement :: "begin" statement-sequence "end" .
        /// statement-sequence :: statement { ';' statement } .
        /// </summary>
        /// <param name="parentBlock">A parent program block.</param>
        /// <returns>An ICompiledProgramPart instance representing this compiled program part.</returns>
        private ICompiledProgramPart ParseBlock(IProgramBlock parentBlock)
        {
            var block = (parentBlock == null) 
                ? (IProgramBlock)new ProgramBlock(parentBlock)
                : (IProgramBlock)new Block(parentBlock);

            ParseVariableDeclarationPart(block);
            ParseStatementPart(block);

            return block;
        }

        /// <summary>
        /// variable-declaration-part :: [ "var" variable-declaration ';' { variable-declaration ';' } ] .
        /// variable-declaration :: identifier-list ':' type-denoter .
        /// identifier-list :: identifier { ',' identifier } .
        /// type-denoter :: "integer" | "real" | "char" | "boolean" | "string" .
        /// </summary>
        /// <param name="programBlock">A program block containing this variables declaration list.</param>
        private void ParseVariableDeclarationPart(IProgramBlock programBlock)
        {
            if (Tokenizer.CurrentToken.TokenCode != TokenCode.TOK_KEY_VAR)
            {
                return;
            }

            // Eat "var".
            Tokenizer.NextToken();

            while (true)
            {
                ParseVariableDeclaration(programBlock);

                // Get the token behind the last variable-declaration.
                var t = Tokenizer.CurrentToken;

                // ';' ?
                if (t.TokenCode != TokenCode.TOK_SEP)
                {
                    throw new CompilerException("The end of variable declaration list (';') expected.");
                }

                // Eat ';'.
                t = Tokenizer.NextToken();

                if (t.TokenCode != TokenCode.TOK_IDENT)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// variable-declaration :: identifier-list ':' type-denoter .
        /// identifier-list :: identifier { ',' identifier } .
        /// type-denoter :: "integer" | "real" | "char" | "boolean" | "string" .
        /// </summary>
        /// <param name="programBlock"></param>
        /// <param name="currentToken"></param>
        private void ParseVariableDeclaration(IProgramBlock programBlock)
        {
            var t = Tokenizer.CurrentToken;
            if (t.TokenCode != TokenCode.TOK_IDENT)
            {
                throw new CompilerException("An identifier in the variable declaration list expected.");
            }

            var variablesList = new List<string>();
            while (t.TokenCode == TokenCode.TOK_IDENT)
            {
                variablesList.Add(t.StringValue);

                // Eat the identifier.
                t = Tokenizer.NextToken();

                // ','.
                if (t.TokenCode == TokenCode.TOK_LIST_SEP)
                {
                    // Eat the ','.
                    t = Tokenizer.NextToken();

                    if (t.TokenCode == TokenCode.TOK_IDENT)
                    {
                        continue;
                    }

                    throw new CompilerException("An identifier in the variable declaration list expected.");
                }

                // At the end of the variables list?
                break;
            }

            // ':'?
            if (t.TokenCode != TokenCode.TOK_DDOT)
            {
                throw new CompilerException("A variable type specification part expected.");
            }

            // Eat ':'.
            t = Tokenizer.NextToken();

            // A type identifier.
            if (t.TokenCode != TokenCode.TOK_IDENT)
            {
                throw new CompilerException("A type denoter in variable declaration expected.");
            }

            var typeName = t.StringValue;

            // Eat identifier.
            Tokenizer.NextToken();

            // Declare all found variables.
            foreach (var variableName in variablesList)
            {
                programBlock.AddVariableDeclaration(variableName, typeName);
            }
        }

        /// <summary>
        /// statement-part :: compound-statement .
        /// </summary>
        /// <param name="parentBlock"></param>
        /// <returns></returns>
        private void ParseStatementPart(IProgramBlock parentBlock)
        {
            ParseCompoundStatement(parentBlock);
        }

        /// <summary>
        /// compound-statement :: "begin" statement-sequence "end" .
        /// </summary>
        /// <param name="currentBlock">The currently parsed program block.</param>
        private void ParseCompoundStatement(IProgramBlock currentBlock)
        {
            // "BEGIN".
            ExpectAndEat(TokenCode.TOK_KEY_BEGIN, "The 'BEGIN' key word expected.");

            ParseStatementSequence(currentBlock);

            // "END".
            ExpectAndEat(TokenCode.TOK_KEY_END, "The 'END' key word expected.");
        }

        /// <summary>
        /// statement-sequence :: statement { ';' statement } .
        /// </summary>
        /// <param name="currentBlock">The currently parsed program block.</param>
        private void ParseStatementSequence(IProgramBlock currentBlock)
        {
            var t = Tokenizer.CurrentToken;
            while (t.TokenCode != TokenCode.TOK_EOF)
            {
                currentBlock.AddCompiledProgramPart(ParseCommand(currentBlock, t));

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
        }

        /// <summary>
        /// command :: empty-command | procedure-identifier list-of-parameters-writeln .
        /// procedure-identifier:: "writeln" .
        /// list-of-parameters-writeln:: [ '(' parameter-write ')' ] .
        /// parameter-write:: string .
        /// </summary>
        /// <param name="parentBlock">A parent program block.</param>
        /// <returns>An ICompiledProgramPart instance representing this compiled program part.</returns>
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
                        return ParseWriteLnParams(parentBlock);
                    }                 

                    return new WritelnCommand(parentBlock);
                }

                throw new CompilerException($"Undefined identifier: {currentToken.StringValue}");
            }
            else if (currentToken.TokenCode == TokenCode.TOK_SEP || currentToken.TokenCode == TokenCode.TOK_KEY_END)
            {
                return new EmptyCommand(parentBlock);
            }

            throw new CompilerException($"Unexpected token: {currentToken}");
        }

        /// <summary>
        /// list-of-parameters-write:: [ '(' parameter-write ')' ] .
        /// parameter-write:: string .
        /// </summary>
        /// <param name="parentBlock">A parent program block.</param>
        /// <returns>An ICompiledProgramPart instance representing this compiled program part.</returns>
        private ICompiledProgramPart ParseWriteLnParams(IProgramBlock parentBlock)
        {
            // Eat "(";
            var t = Tokenizer.NextToken();
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
           
                throw new CompilerException("The end of formal parameters (')') expected.");
            }

            throw new CompilerException("A formal parameter expected.");
        }


        /// <summary>
        /// Checks, if the last extracted token is the expected one.
        /// Throws exception, if not.
        /// </summary>
        /// <param name="tokenCode">An expected token code.</param>
        /// <param name="errorMessage">An error mesage thrown as an exception, if the expected token was not found.</param>
        private void Expect(TokenCode tokenCode, string errorMessage)
        {
            if (Tokenizer.CurrentToken.TokenCode != tokenCode)
            {
                throw new CompilerException(errorMessage);
            }
        }

        /// <summary>
        /// Moves to the next token.
        /// </summary>
        private void Eat()
        {
            Tokenizer.NextToken();
        }

        /// <summary>
        /// Checks, if the last extracted token is the expected one.
        /// Throws an exception, if not, or moves to the next token, if it is.
        /// </summary>
        /// <param name="tokenCode">An expected token code.</param>
        /// <param name="errorMessage">An error mesage thrown as an exception, if the expected token was not found.</param>
        private void ExpectAndEat(TokenCode tokenCode, string errorMessage)
        {
            Expect(tokenCode, errorMessage);
            Eat();
        }

    }
}
