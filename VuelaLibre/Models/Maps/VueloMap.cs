﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VuelaLibre.Models.Maps
{
    public class VueloMap: IEntityTypeConfiguration<Vuelos>
    {
        public void Configure(EntityTypeBuilder<Vuelos> builder)
        {
                builder.ToTable("CrearVuelo");
                builder.HasKey(o => o.Id);
        }
        
    }
}
