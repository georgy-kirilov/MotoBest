using MotoBest.Services.Normalization;

namespace MotoBest.Services.Data;

public interface IAdvertService
{
    Task AddAsync(NormalizedAdvert advert);
}
