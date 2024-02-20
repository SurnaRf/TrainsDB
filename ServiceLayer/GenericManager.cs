using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class GenericManager<T, K> : IDb<T, K> where K : IConvertible
    {
        private readonly IDb<T, K> context;

        public GenericManager(IDb<T, K> context)
        {
            this.context = context;
        }

        public async Task CreateAsync(T item)
        {
            await context.CreateAsync(item);
        }

        public async Task<T> ReadAsync(K key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await context.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<ICollection<T>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await context.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(T item, bool useNavigationalProperties = false)
        {
            await context.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(K key)
        {
            await context.DeleteAsync(key);
        }
    }
}
