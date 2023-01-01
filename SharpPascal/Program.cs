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
