/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    using System;
    
    
    public abstract class AToken : IToken
    {
        public TokenCode TokenCode { get; protected init; }

        public bool BooleanValue { get; protected init; }
        public int IntegerValue { get; protected init; }
        public double RealValue { get; protected init; }
        public string StringValue { get; protected init; }
        public int Line { get; }
        public int LinePosition { get; }


        protected AToken(int linePosition, int line)
        {
            if (linePosition <= 0) throw new ArgumentOutOfRangeException(
                nameof(linePosition),
                "The linePosition parameter should be greater than 0.");
            
            if (line <= 0) throw new ArgumentOutOfRangeException(
                nameof(line),
                "The line parameter should be greater than 0.");

            LinePosition = linePosition;
            Line = line;
        }

        public override string ToString()
        {
            switch (TokenCode)
            {
                case TokenCode.TOK_ADD_OP: return "+";
                case TokenCode.TOK_SUB_OP: return "-";
                case TokenCode.TOK_MUL_OP: return "*";
                case TokenCode.TOK_DIV_OP: return "/";
                case TokenCode.TOK_EQ_OP: return "=";
                case TokenCode.TOK_LT_OP: return "<";
                case TokenCode.TOK_GT_OP: return ">";
                // [
                // ]
                // .
                case TokenCode.TOK_LIST_SEP: return ",";
                case TokenCode.TOK_DDOT: return ":";
                case TokenCode.TOK_SEP: return ";";
                // ^
                case TokenCode.TOK_LBRA: return "(";
                case TokenCode.TOK_RBRA: return ")";
                case TokenCode.TOK_NEQ_OP: return "<>";
                case TokenCode.TOK_LE_OP: return "<=";
                case TokenCode.TOK_GE_OP: return ">=";
                case TokenCode.TOK_ASGN_OP: return ":=";
                // ..
            }

            throw new CompilerException("Unknown token " + TokenCode + ".");
        }
    }
}
