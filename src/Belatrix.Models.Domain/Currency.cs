namespace Belatrix.Models.Domain
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Value { get; set; }
    }
}
