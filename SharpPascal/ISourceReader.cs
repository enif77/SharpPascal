/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal;

public interface ISourceReader
{
    int CurrentChar { get; }
    int NextChar();
}
