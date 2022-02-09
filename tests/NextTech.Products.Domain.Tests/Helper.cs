using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Domain.Tests
{
  public static class Helper
    {
        public static string GetStringOfLength(this IFixture fixture, int length)
        {
            return string.Join("", fixture.CreateMany<char>(length));
        }
    }
}
