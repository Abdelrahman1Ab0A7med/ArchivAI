using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivAI.Application.Interfaces
{
    public interface ICacheService
    {
        Task<T?> GetASync<T>(string key);
        Task SetAsync<T>(string key, T value , TimeSpan? expiry = null);
        Task RemoveAsync(string key);
        Task RemoveByPrefixAsync(string prefix);
    }
}
