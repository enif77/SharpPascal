/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests
{
    using System;

    using Xunit;

    using SharpPascal.SourceReaders;
    

    public sealed class StringSourceReaderTests
    {
        [Fact]
        public void Empty_string_is_valid_source()
        {
            Assert.NotNull(new StringSourceReader(string.Empty));
        }
        
        [Fact]
        public void Null_is_not_valid_source()
        {
            Assert.Throws<ArgumentNullException>(() => new StringSourceReader(null));
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("not an empty source")]
        public void CurrentChar_is_EOF_when_StringSourceReader_is_just_created(string src)
        {
            var r = new StringSourceReader(src);
            
            Assert.Equal(-1, r.CurrentChar);
        }
        
        [Fact]
        public void NextChar_returns_EOF_when_called_on_empty_source()
        {
            var r = new StringSourceReader(string.Empty);
            
            Assert.Equal(-1, r.NextChar());
        }
        
        [Fact]
        public void CurrentChar_is_EOF_when_NextChar_is_called_on_empty_source()
        {
            var r = new StringSourceReader(string.Empty);
            r.NextChar();
            
            Assert.Equal(-1, r.CurrentChar);
        }
        
        [Theory]
        [InlineData("1", '1')]
        [InlineData("abcd", 'a')]
        public void CurrentChar_is_the_first_source_char_when_NextChar_is_first_called(string src, char expectedChar)
        {
            var r = new StringSourceReader(src);
            r.NextChar();
            
            Assert.Equal(expectedChar, (char)r.CurrentChar);
        }
        
        [Theory]
        [InlineData("1", '1')]
        [InlineData("abcd", 'a')]
        public void NextChar_returns_the_first_source_char_when_NextChar_is_first_called(string src, char expectedChar)
        {
            var r = new StringSourceReader(src);
            
            Assert.Equal(expectedChar, (char)r.NextChar());
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("abcd")]
        public void NextChar_sets_CurrentChar_when_NextChar_is_called(string src)
        {
            var r = new StringSourceReader(src);

            var c = r.NextChar();
            
            Assert.Equal(c, r.CurrentChar);
        }
    }
}
