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
    }
}
