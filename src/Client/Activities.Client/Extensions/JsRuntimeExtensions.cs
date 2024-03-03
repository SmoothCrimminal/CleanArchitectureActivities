using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace Activities.Client.Extensions
{
    public static class JsRuntimeExtensions
    {
        public static async Task AddItemToLocalStorage<T>(this IJSRuntime jsRuntime, T item, string storageName)
        {
            var jsonString = JsonConvert.SerializeObject(item);

            await jsRuntime.InvokeVoidAsync("localStorage.setItem", storageName, jsonString);
        }

        public static async Task<T?> GetItemFromLocalStorage<T>(this IJSRuntime jsRuntime, string storageName)
        {
            var storageItem = await jsRuntime.InvokeAsync<string>("localStorage.getItem", storageName);

            var result = JsonConvert.DeserializeObject<T>(storageItem);
            return result;
        }
    }
}
