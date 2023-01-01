/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal
{
    public enum TokenCode
    {
        /// <summary>
        /// The end of the file token.
        /// </summary>
        TOK_EOF,

        /// <summary>
        /// The end of the line token.
        /// </summary>
        TOK_EOLN,

        /// <summary>
        /// The program end mark '.'.
        /// </summary>
        TOK_PROG_END,

        /// <summary>
        /// The statement separator ';'.
        /// </summary>
        TOK_SEP,

        /// <summary>
        /// The list separator ','.
        /// </summary>
        TOK_LIST_SEP,

        /// <summary>
        /// The ':'.
        /// </summary>
        TOK_DDOT,

        /// <summary>
        /// '('.
        /// </summary>
        TOK_LBRA,
        
        /// <summary>
        /// ')'.
        /// </summary>
        TOK_RBRA,

        /// <summary>
        /// A string literal.
        /// </summary>
        TOK_STR,

        /// <summary>
        /// An identifier.
        /// </summary>
        TOK_IDENT,

        /// <summary>
        /// An integer number or a label.
        /// </summary>
        TOK_INTEGER_NUMBER,

        /// <summary>
        /// A real number.
        /// </summary>
        TOK_REAL_NUMBER,

        // Operators.
        TOK_ADD_OP,  // '+'
        TOK_AND_OP,  // AND
        TOK_ASGN_OP, // ':='
        TOK_DIV_OP,  // '/'
        TOK_EQ_OP,   // '='
        TOK_DIVI_OP, // DIV
        TOK_GE_OP,   // '>='
        TOK_GT_OP,   // '>'
        TOK_IN_OP,   // IN
        TOK_LE_OP,   // '<='
        TOK_LT_OP,   // '<'
        TOK_MUL_OP,  // '*'
        TOK_MOD_OP,  // MOD
        TOK_OR_OP,   // OR
        TOK_NEQ_OP,  // '<>'
        TOK_SUB_OP,  // '-'

        // Keywords.
        TOK_KEY_BEGIN,
        TOK_KEY_END,
        TOK_KEY_PROGRAM,
        TOK_KEY_VAR,
    };


    public interface IToken
    {
        TokenCode TokenCode { get; }
        bool BooleanValue { get; }
        int IntegerValue { get; }
        double RealValue { get; }
        string StringValue { get; }
        
        /// <summary>
        /// The line position (1 .. N).
        /// </summary>
        int LinePosition { get; }

        /// <summary>
        /// The line (1 .. N).
        /// </summary>
        public int Line { get; }
    }
}
