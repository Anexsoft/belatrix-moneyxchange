using System;

namespace Belatrix.Models.DTOs
{
    public class CurrencyDto
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Value { get; set; }
    }
}
