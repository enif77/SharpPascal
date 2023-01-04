/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests
{
    using Xunit;


    public sealed class TokenizerTests
    {
        [Fact]
        public void CurrentChar_is_C_EOF_when_Tokenizer_is_just_created()
        {
            var t = new Tokenizer(new StringSourceReader(string.Empty));
            
            Assert.Equal(Tokenizer.C_EOF, t.CurrentChar);
        }

        [Fact]
        public void CurrentToken_is_TOK_EOF_when_Tokenizer_is_just_created()
        {
            var t = new Tokenizer(new StringSourceReader(string.Empty));
            
            Assert.Equal(TokenCode.TOK_EOF, t.CurrentToken.Code);
        }

        // [Fact]
        // public void SourcePosition_is_0_when_Tokenizer_is_just_created()
        // {
        //     var t = new Tokenizer(new StringSourceReader(string.Empty));
        //     
        //     Assert.Equal(0, t.SourcePosition);
        // }
        
        [Theory]
        [InlineData("")]
        public void NextToken_Returns_TOK_EOF_when_source_is_empty(string source)
        {
            var t = new Tokenizer(new StringSourceReader(source));

            Assert.Equal(TokenCode.TOK_EOF, t.NextToken().Code);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("-1", -1)]
        [InlineData("+123", 123)]
        public void NextToken_Returns_TOK_IDENTIFIER_NUMBER_and_value_when_source_is_integer_literal(string source, int expectedValue)
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
        public void NextToken_Returns_TOK_REAL_NUMBER_and_value_when_source_is_real_literal(string source, double expectedValue)
        {
            var t = new Tokenizer(new StringSourceReader(source));
            var tok = t.NextToken();

            Assert.Equal(TokenCode.TOK_REAL_NUMBER, tok.Code);
            Assert.Equal(expectedValue, tok.RealValue);
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
        [InlineData("]", TokenCode.TOK_RIGHT_BRACKET)]
        [InlineData("^", TokenCode.TOK_POINTER)]
        [InlineData(".", TokenCode.TOK_PROG_END)]
        public void NextToken_returns_expected_token(string source, TokenCode expectedTokenCode)
        {
            var t = new Tokenizer(new StringSourceReader(source));
            var tok = t.NextToken();

            Assert.Equal(expectedTokenCode, tok.Code);
        }
    }
}
