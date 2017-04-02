using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configurations
{
    class TeamConfiguration : EntityTypeConfiguration<Team>
    {
        public TeamConfiguration()
        {
            this.Property(c => c.Name).HasMaxLength(25).IsRequired();
            this.Property(c => c.Description).HasMaxLength(32);
            this.Property(c => c.Actronym).HasMaxLength(3).IsFixedLength();
        }
    }
}
