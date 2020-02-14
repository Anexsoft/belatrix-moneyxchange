using System;

namespace Belatrix.Models.Domain
{
    public class CurrencyAudit
    {
        public int CurrencyAuditId { get; set; }
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
