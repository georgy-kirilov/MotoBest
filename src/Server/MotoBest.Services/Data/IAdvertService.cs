using MotoBest.Services.Normalizing;

namespace MotoBest.Services.Data;

public interface IAdvertService
{
    Task AddAsync(NormalizedAdvert advert);
}
