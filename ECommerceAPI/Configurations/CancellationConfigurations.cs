using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceAPI.Configurations
{
    public class CancellationConfigurations : IEntityTypeConfiguration<Cancellation>
    {
        public void Configure(EntityTypeBuilder<Cancellation> builder)
        {
            //throw new NotImplementedException();
        }
    }
}
