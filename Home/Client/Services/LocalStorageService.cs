using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Client.Services
{
    public interface ILocalStorageService
    {
        Task<string> GetItem(string key);
        Task RemoveItem(string key);
        Task SetItem(string key, string value);
    }

    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public async Task<string> GetItem(string key)
        {
            var item = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            return item;
        }

        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task SetItem(string key, string value)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }
    }
}
