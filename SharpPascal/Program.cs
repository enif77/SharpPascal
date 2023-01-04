/* Copyright (C) Premysl Fara and Contributors */

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
                TokenizeProgram(args[0]);
                CompileProgram(args[0]);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("E: {0}", ex.Message);

                return 1;
            }
            
            Console.WriteLine("DONE");

            return 0;
        }

        
        private static void TokenizeProgram(string path)
        {
            var tokenizer = new Tokenizer(
                new StringSourceReader(
                    File.ReadAllText(path)
                )
            );

            var tok = tokenizer.NextToken();
            while (tok.Code != TokenCode.TOK_EOF)
            {
                switch (tok.Code)
                {
                    case TokenCode.TOK_IDENTIFIER:
                        Console.WriteLine("[{0:0000}:{1:0000}] identifier({2})", tok.Line, tok.LinePosition, tok);
                        break;

                    default:
                        Console.WriteLine("[{0:0000}:{1:0000}] {2}", tok.Line, tok.LinePosition, tok);
                        break;
                }

                tok = tokenizer.NextToken();
            }
            
            Console.WriteLine("[{0:0000}:{1:0000}] {2}", tok.Line, tok.LinePosition, tok);
            Console.WriteLine();
        }
        
        
        private static void CompileProgram(string path)
        {
            Console.WriteLine("Compiling: {0}", path);
            
            var parser = new Parser(
                new Tokenizer(
                    new StringSourceReader(
                        File.ReadAllText(path)
                    )
                )
            );

            var p = parser.Parse();

            File.WriteAllText(path + ".cs", p.GenerateOutput());
            
            Console.WriteLine("Compilation OK.");
        }
    }
}
