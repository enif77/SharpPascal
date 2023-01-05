/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests
{
    using Xunit;
    
    using SharpPascal.SourceReaders;


    public sealed class TokenizerTests
    {
        [Fact]
        public void CurrentChar_is_C_EOF_when_Tokenizer_is_just_created()
        {
            var t = new Tokenizer(new StringSourceReader(string.Empty));
            
            Assert.True(t.CurrentChar < 0);
        }

        [Fact]
        public void CurrentToken_is_TOK_EOF_when_Tokenizer_is_just_created()
        {
            var t = new Tokenizer(new StringSourceReader(string.Empty));
            
            Assert.Equal(TokenCode.TOK_EOF, t.CurrentToken.Code);
        }

        [Theory]
        [InlineData("")]
        public void NextToken_Returns_TOK_EOF_when_source_is_empty(string source)
        {
            var t = new Tokenizer(new StringSourceReader(source));

            Assert.Equal(TokenCode.TOK_EOF, t.NextToken().Code);
        }

        [Theory]
        [InlineData("", TokenCode.TOK_EOF)]
        [InlineData("abcd", TokenCode.TOK_IDENTIFIER)]
        [InlineData("+123", TokenCode.TOK_INTEGER_NUMBER)]
        [InlineData("-1.5", TokenCode.TOK_REAL_NUMBER)]
        [InlineData("'abcd'", TokenCode.TOK_STRING_LITERAL)]
        
        [InlineData("+", TokenCode.TOK_ADD_OP)]
        [InlineData("+abc", TokenCode.TOK_ADD_OP)]
        [InlineData("-", TokenCode.TOK_SUB_OP)]
        [InlineData("-abc", TokenCode.TOK_SUB_OP)]
        [InlineData("*", TokenCode.TOK_MUL_OP)]
        [InlineData("/", TokenCode.TOK_DIV_OP)]
        [InlineData("=", TokenCode.TOK_EQ_OP)]
        [InlineData("<", TokenCode.TOK_LT_OP)]
        [InlineData("<>", TokenCode.TOK_NEQ_OP)]
        [InlineData("<=", TokenCode.TOK_LE_OP)]
        [InlineData(">", TokenCode.TOK_GT_OP)]
        [InlineData(">=", TokenCode.TOK_GE_OP)]
        [InlineData(";", TokenCode.TOK_SEP)]
        [InlineData(",", TokenCode.TOK_LIST_SEP)]
        [InlineData(":", TokenCode.TOK_DDOT)]
        [InlineData(":=", TokenCode.TOK_ASGN_OP)]
        [InlineData("(", TokenCode.TOK_LEFT_PAREN)]
        [InlineData(")", TokenCode.TOK_RIGHT_PAREN)]
        [InlineData("[", TokenCode.TOK_LEFT_BRACKET)]
        [InlineData("(.", TokenCode.TOK_LEFT_BRACKET)]
        [InlineData("]", TokenCode.TOK_RIGHT_BRACKET)]
        [InlineData(".)", TokenCode.TOK_RIGHT_BRACKET)]
        [InlineData("^", TokenCode.TOK_POINTER)]
        [InlineData("@", TokenCode.TOK_POINTER)]
        [InlineData(".", TokenCode.TOK_PROG_END)]
        
        [InlineData("and", TokenCode.TOK_KEY_AND)]
        [InlineData("array", TokenCode.TOK_KEY_ARRAY)]
        [InlineData("begin", TokenCode.TOK_KEY_BEGIN)]
        [InlineData("case", TokenCode.TOK_KEY_CASE)]
        [InlineData("const", TokenCode.TOK_KEY_CONST)]
        [InlineData("div", TokenCode.TOK_KEY_DIV)]
        [InlineData("do", TokenCode.TOK_KEY_DO)]
        [InlineData("downto", TokenCode.TOK_KEY_DOWNTO)]
        [InlineData("else", TokenCode.TOK_KEY_ELSE)]
        [InlineData("end", TokenCode.TOK_KEY_END)]
        [InlineData("file", TokenCode.TOK_KEY_FILE)]
        [InlineData("for", TokenCode.TOK_KEY_FOR)]
        [InlineData("function", TokenCode.TOK_KEY_FUNCTION)]
        [InlineData("goto", TokenCode.TOK_KEY_GOTO)]
        [InlineData("if", TokenCode.TOK_KEY_IF)]
        [InlineData("in", TokenCode.TOK_KEY_IN)]
        [InlineData("label", TokenCode.TOK_KEY_LABEL)]
        [InlineData("mod", TokenCode.TOK_KEY_MOD)]
        [InlineData("nil", TokenCode.TOK_KEY_NIL)]
        [InlineData("not", TokenCode.TOK_KEY_NOT)]
        [InlineData("of", TokenCode.TOK_KEY_OF)]
        [InlineData("or", TokenCode.TOK_KEY_OR)]
        [InlineData("packed", TokenCode.TOK_KEY_PACKED)]
        [InlineData("procedure", TokenCode.TOK_KEY_PROCEDURE)]
        [InlineData("program", TokenCode.TOK_KEY_PROGRAM)]
        [InlineData("record", TokenCode.TOK_KEY_RECORD)]
        [InlineData("repeat", TokenCode.TOK_KEY_REPEAT)]
        [InlineData("set", TokenCode.TOK_KEY_SET)]
        [InlineData("then", TokenCode.TOK_KEY_THEN)]
        [InlineData("to", TokenCode.TOK_KEY_TO)]
        [InlineData("type", TokenCode.TOK_KEY_TYPE)]
        [InlineData("until", TokenCode.TOK_KEY_UNTIL)]
        [InlineData("var", TokenCode.TOK_KEY_VAR)]
        [InlineData("while", TokenCode.TOK_KEY_WHILE)]
        [InlineData("with", TokenCode.TOK_KEY_WITH)]
        public void NextToken_returns_expected_token(string source, TokenCode expectedTokenCode)
        {
            var t = new Tokenizer(new StringSourceReader(source));
            var tok = t.NextToken();

            Assert.Equal(expectedTokenCode, tok.Code);
        }
        
        [Theory]
        [InlineData("X", "X")]
        [InlineData("time", "TIME")]
        [InlineData("readinteger", "READINTEGER")]
        [InlineData("WG4", "WG4")]
        [InlineData("AlterHeatSetting", "ALTERHEATSETTING")]
        [InlineData("InquireWorkstationTransformation", "INQUIREWORKSTATIONTRANSFORMATION")]
        [InlineData("InquireWorkstationIdentification", "INQUIREWORKSTATIONIDENTIFICATION")]
        public void NextToken_Returns_TOK_IDENTIFIER_and_value_when_source_is_identifier(string source, string expectedValue)
        {
            var t = new Tokenizer(new StringSourceReader(source));
            var tok = t.NextToken();

            Assert.Equal(TokenCode.TOK_IDENTIFIER, tok.Code);
            Assert.Equal(expectedValue, tok.StringValue);
        }
        
        [Theory]
        [InlineData("'abcd'", "abcd")]
        [InlineData("'ab''cd'", "ab'cd")]
        [InlineData("'''abcd'", "'abcd")]
        [InlineData("'abcd'''", "abcd'")]
        public void NextToken_Returns_TOK_STRING_LITERAL_and_value_when_source_is_string_literal(string source, string expectedValue)
        {
            var t = new Tokenizer(new StringSourceReader(source));
            var tok = t.NextToken();

            Assert.Equal(TokenCode.TOK_STRING_LITERAL, tok.Code);
            Assert.Equal(expectedValue, tok.StringValue);
        }
        
        [Fact]
        public void String_literal_must_be_terminated()
        {
            var t = new Tokenizer(new StringSourceReader("'abcd"));
            
            Assert.Throws<CompilerException>(() => t.NextToken());
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("-1", -1)]
        [InlineData("+123", 123)]
        public void NextToken_Returns_TOK_INTEGER_NUMBER_and_value_when_source_is_integer_literal(string source, int expectedValue)
        {
            var t = new Tokenizer(new StringSourceReader(source));
            var tok = t.NextToken();

            Assert.Equal(TokenCode.TOK_INTEGER_NUMBER, tok.Code);
            Assert.Equal(expectedValue, tok.IntegerValue);
        }

        [Theory]
        [InlineData("0.0", 0.0)]
        [InlineData("-1.5", -1.5)]
        [InlineData("+123.45", 123.45)]
        [InlineData("12e3", 12000)]
        [InlineData("12.3e3", 12300)]
        [InlineData("5e-3", 0.005)]
        [InlineData("5e+8", 500000000)]
        public void NextToken_Returns_TOK_REAL_NUMBER_and_value_when_source_is_real_literal(string source, double expectedValue)
        {
            var t = new Tokenizer(new StringSourceReader(source));
            var tok = t.NextToken();

            Assert.Equal(TokenCode.TOK_REAL_NUMBER, tok.Code);
            Assert.Equal(expectedValue, tok.RealValue);
        }
        
        [Fact]
        public void Fractional_part_of_real_number_is_required()
        {
            var t = new Tokenizer(new StringSourceReader("3."));
            
            Assert.Throws<CompilerException>(() => t.NextToken());
        }
        
        [Fact]
        public void Scale_factor_of_real_number_is_required()
        {
            var t = new Tokenizer(new StringSourceReader("3e"));
            
            Assert.Throws<CompilerException>(() => t.NextToken());
        }
    }
}
