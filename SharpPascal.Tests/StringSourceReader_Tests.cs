/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tests
{
    using System;

    using Xunit;


    public class StringSourceReader_Tests
    {
        [Fact]
        public void Null_Source_Is_Not_Valid_Source()
        {
            Assert.Throws<ArgumentNullException>(() => new StringSourceReader(null));
        }
    }
}
