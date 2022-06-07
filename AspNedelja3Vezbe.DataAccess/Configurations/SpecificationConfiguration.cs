using ASPNedelja3Vezbe.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNedelja3Vezbe.DataAccess.Configurations
{
    public class SpecificationConfiguration : EntityConfiguration<Specification>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Specification> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.CategorySpecifications)
                   .WithOne(x => x.Specification)
                   .HasForeignKey(x => x.SpecificationId)
                   .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(x => x.SpecificationValues)
                .WithOne(x => x.Specification)
                .HasForeignKey(x => x.SpecificationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class SpecificationValueCOnfiguration : EntityConfiguration<SpecificationValue>
    {
        protected override void ConfigureRules(EntityTypeBuilder<SpecificationValue> builder)
        {
            builder.Property(x => x.Value).IsRequired();
        }
    }
}
