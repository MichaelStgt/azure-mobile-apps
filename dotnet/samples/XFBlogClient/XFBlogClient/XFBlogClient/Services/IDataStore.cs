using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XFBlogClient.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T blogPost);
        Task<bool> UpdateItemAsync(T blogPost);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
