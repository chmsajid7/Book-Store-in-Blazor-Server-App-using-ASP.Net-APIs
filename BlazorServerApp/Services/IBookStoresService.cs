using BookStoresWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp.Services
{
    interface IBookStoresService<T>
    {
        Task<List<T>> GetAllAsync(string requestUri);
        Task<ResponseDTO<T>> GetAllAsyncWithResult(string requestUri, string valueParamName);
        Task<T> GetByIdAsync(string requestUri, int Id);
        Task<T> SaveAsync(string requestUri, T obj);
        Task<T> UpdateAsync(string requestUri, int Id, T obj);
        Task<bool> DeleteAsync(string requestUri, int Id);
    }
}
