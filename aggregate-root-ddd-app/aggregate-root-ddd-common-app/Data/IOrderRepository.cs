using aggregate_root_ddd_common_app.Entities;

namespace aggregate_root_ddd_common_app.Data
{
    public interface IOrdersRepository
    {
        Task<Order?> GetByIdAsync(long id);
        Task<Order> CreateAsync(Order entity);
        Task<Order> UpdateAsync(Order entity);
    }
}
