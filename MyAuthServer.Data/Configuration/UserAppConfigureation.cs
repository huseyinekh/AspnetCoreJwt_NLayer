using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyAuthServer.Core.Models;


namespace MyAuthServer.Data.Configuration
{
    public class UserAppConfigureation : IEntityTypeConfiguration<UserApp>
    {
        public void Configure(EntityTypeBuilder<UserApp> builder)
        {


            builder.Property(x => x.City).HasMaxLength(100);
        }
        }
    }
