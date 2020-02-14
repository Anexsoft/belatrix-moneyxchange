using System;

namespace Belatrix.Query.Service.DTOs
{
    public class CurrencyDto
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Value { get; set; }
    }
}
