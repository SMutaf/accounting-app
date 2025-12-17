using AccountingApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Data.Configuration
{
    public class TransactionLineConfiguration : IEntityTypeConfiguration<TransactionLine>
    {
        public void Configure(EntityTypeBuilder<TransactionLine> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Notes).HasMaxLength(500);

            builder.Property(x => x.DebitAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.CreditAmount)
                .HasColumnType("decimal(18,2)");
        }
    }
}
