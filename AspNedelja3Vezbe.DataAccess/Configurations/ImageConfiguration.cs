﻿using ASPNedelja3Vezbe.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNedelja3Vezbe.DataAccess.Configurations
{
    public class ImageConfiguration : EntityConfiguration<Image>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Path).HasMaxLength(200).IsRequired(true);
        }
    }
}
