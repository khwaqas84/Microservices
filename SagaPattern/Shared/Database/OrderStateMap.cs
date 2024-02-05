using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Database
{
    public class OrderStateMap:SagaClassMap<OrderState>
    {
        protected override void Configure(EntityTypeBuilder<OrderState> entity, ModelBuilder model)
        {
            entity.Property(x=>x.CurrentState).HasMaxLength(100);
            entity.Property(x => x.Product).HasMaxLength(100);
            base.Configure(entity, model);
        }
    }
}
