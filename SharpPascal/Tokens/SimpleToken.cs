/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    using System;
    
    
    /// <summary>
    /// Represents a token without a value.
    /// NOTE: The StringValue contains the string representation of this token.
    /// </summary>
    public class SimpleToken : AToken
    {
        public SimpleToken(TokenCode tokenCode, string stringRepresentation, int linePosition, int line)
            : base(linePosition, line)
        {
            if (string.IsNullOrEmpty(stringRepresentation))
            {
                throw new ArgumentException("A token string representation expected.");
            }

            Code = tokenCode;
            StringValue = stringRepresentation;
        }
        
        
        public override string ToString()
        {
            return StringValue;
        }
    }
}
