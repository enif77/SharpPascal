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
        /// A string literal.
        /// </summary>
        TOK_STRING_LITERAL,

        /// <summary>
        /// An identifier.
        /// </summary>
        TOK_IDENTIFIER,

        /// <summary>
        /// An integer number or a label.
        /// </summary>
        TOK_INTEGER_NUMBER,

        /// <summary>
        /// A real number.
        /// </summary>
        TOK_REAL_NUMBER,

        // Special symbols:
        
        TOK_ADD_OP,   // '+'
        TOK_SUB_OP,   // '-'
        TOK_MUL_OP,   // '*'
        TOK_DIV_OP,   // '/'
        TOK_EQ_OP,    // '='
        TOK_LT_OP,    // '<'
        TOK_GT_OP,    // '>'
        TOK_LEFT_BRACKET, // '['
        TOK_RIGHT_BRACKET, // ']'
        TOK_PROG_END, // '.'
        TOK_LIST_SEP, // ','
        TOK_DDOT,     // ':'
        TOK_SEP,      // ';'
        TOK_POINTER,  // '^'
        TOK_LEFT_PAREN,  // '('
        TOK_RIGHT_PAREN, // ')'
        TOK_NEQ_OP,   // '<>'
        TOK_LE_OP,    // '<='
        TOK_GE_OP,    // '>='
        TOK_ASGN_OP,  // ':='
        
        // Word symbols:
        
        TOK_KEY_AND,
        TOK_KEY_ARRAY,
        TOK_KEY_BEGIN,
        TOK_KEY_CASE,
        TOK_KEY_CONST,
        TOK_KEY_DIV,
        TOK_KEY_DO,
        TOK_KEY_DOWNTO,
        TOK_KEY_ELSE,
        TOK_KEY_END,
        TOK_KEY_FILE,
        TOK_KEY_FOR,
        TOK_KEY_FUNCTION,
        TOK_KEY_GOTO,
        TOK_KEY_IF,
        TOK_KEY_IN,
        TOK_KEY_LABEL,
        TOK_KEY_MOD,
        TOK_KEY_NIL,
        TOK_KEY_NOT,
        TOK_KEY_OF,
        TOK_KEY_OR,
        TOK_KEY_PACKED,
        TOK_KEY_PROCEDURE,
        TOK_KEY_PROGRAM,
        TOK_KEY_RECORD,
        TOK_KEY_REPEAT,
        TOK_KEY_SET,
        TOK_KEY_THEN,
        TOK_KEY_TO,
        TOK_KEY_TYPE,
        TOK_KEY_UNTIL,
        TOK_KEY_VAR,
        TOK_KEY_WHILE,
        TOK_KEY_WITH
    }


    public interface IToken
    {
        TokenCode Code { get; }
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
