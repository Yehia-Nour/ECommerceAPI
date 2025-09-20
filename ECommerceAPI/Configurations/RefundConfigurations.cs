using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceAPI.Configurations
{
    public class RefundConfigurations : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            //throw new NotImplementedException();
        }
    }
}
