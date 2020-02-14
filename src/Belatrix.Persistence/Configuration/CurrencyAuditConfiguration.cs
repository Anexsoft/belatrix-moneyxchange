using Belatrix.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Belatrix.Persistence.Configuration
{
    public class CurrencyAuditConfiguration
    {
        public CurrencyAuditConfiguration(EntityTypeBuilder<CurrencyAudit> entityBuilder)
        {
            entityBuilder.HasKey(x => x.CurrencyAuditId);

            entityBuilder.HasData(
                new CurrencyAudit
                {
                    CurrencyAuditId = 1,
                    CurrencyId = 1,
                    Value = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new CurrencyAudit
                {
                    CurrencyAuditId = 2,
                    CurrencyId = 2,
                    Value = 0.92m,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
