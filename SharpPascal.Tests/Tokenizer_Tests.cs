/* SharpPascal - (C) 2020 Premysl Fara 
 
SharpPascal is available under the zlib license:
This software is provided 'as-is', without any express or implied
warranty.  In no event will the authors be held liable for any damages
arising from the use of this software.
Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:
1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.
2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.
3. This notice may not be removed or altered from any source distribution.
 
 */

namespace SharpPascal.Tests
{
    using System;

    using Xunit;

    using SharpPascal.Tokens;


    public class Tokenizer_Tests
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
            
            Assert.Equal(TokenCode.TOK_EOF, t.CurrentToken.TokenCode);
        }

        [Fact]
        public void SourcePosition_is_0_when_Tokenizer_is_just_created()
        {
            var t = new Tokenizer(new StringSourceReader(string.Empty));
            
            Assert.Equal(0, t.SourcePosition);
        }
        
        [Theory]
        [InlineData("")]
        public void NextToken_Returns_TOK_EOF_when_source_is_empty(string source)
        {
            var t = new Tokenizer(new StringSourceReader(source));

            Assert.Equal(TokenCode.TOK_EOF, t.NextToken().TokenCode);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("-1", -1)]
        [InlineData("+123", 123)]
        public void NextToken_Returns_TOK_IDENTIFIER_NUMBER_and_value_when_source_is_integer_literal(string source, int expectedValue)
        {
            var t = new Tokenizer(new StringSourceReader(source));
            var tok = t.NextToken();

            Assert.Equal(TokenCode.TOK_INTEGER_NUMBER, tok.TokenCode);
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

            Assert.Equal(TokenCode.TOK_REAL_NUMBER, tok.TokenCode);
            Assert.Equal(expectedValue, tok.RealValue);
        }
    }
}
