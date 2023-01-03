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

        // Special symbols:
        
        TOK_ADD_OP,   // '+'
        TOK_SUB_OP,   // '-'
        TOK_MUL_OP,   // '*'
        TOK_DIV_OP,   // '/'
        TOK_EQ_OP,    // '='
        TOK_LT_OP,    // '<'
        TOK_GT_OP,    // '>'
        // '['
        // ']'
        TOK_PROG_END, // '.'
        TOK_LIST_SEP, // ','
        TOK_DDOT,     // ':'
        TOK_SEP,      // ';'
        // '^'
        TOK_LBRA,     // '('
        TOK_RBRA,     // ')'
        TOK_NEQ_OP,   // '<>'
        TOK_LE_OP,    // '<='
        TOK_GE_OP,    // '>='
        TOK_ASGN_OP,  // ':='
        
        // Word symbols:
        
        TOK_AND_OP,   // AND
        // ARRAY
        TOK_KEY_BEGIN,
        // CASE
        // CONST
        TOK_DIVI_OP,  // DIV
        // DO
        // DOWNTO
        // ELSE
        TOK_KEY_END,  // END
        // FILE
        // FOR
        // FUNCTION
        // GOTO
        // IF
        TOK_IN_OP,    // IN
        // LABEL
        TOK_MOD_OP,   // MOD
        // NIL
        // NOT
        // OF
        TOK_OR_OP,    // OR
        // PACKED
        // PROCEDURE
        TOK_KEY_PROGRAM,
        // RECORD
        // REPEAT
        // SET
        // THEN
        // TO
        // TYPE
        // UNTIL
        TOK_KEY_VAR,
        // WHILE
        // WITH
    }


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
