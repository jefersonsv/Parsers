using System;
using Xunit;

namespace Parsers.Test
{
    public class BasicTests
    {
        [Fact]
        public void CpfValido()
        {
            Assert.True(CPF.IsValid("562.832.510-48"));
        }

        [Fact]
        public void CpfInvalido()
        {
            Assert.False(CPF.IsValid("562.832.510-00"));
        }

        [Fact]
        public void Telefone()
        {
            var phone = Internacional.PhoneNumber.Parse("55119878784758");
            Assert.True(phone.HasValues);
        }
    }
}
