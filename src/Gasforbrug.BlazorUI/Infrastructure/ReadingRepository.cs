using Gasforbrug.BlazorUI.Data;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Gasforbrug.BlazorUI.Infrastructure
{
    public class ReadingRepository
    {
        private readonly ProtectedLocalStorage browserStorage;
        private List<ReadingStorageModel> readings = new();

        public ReadingRepository(ProtectedLocalStorage browserStorage)
        {
            this.browserStorage = browserStorage ?? throw new ArgumentNullException(nameof(browserStorage));
        }

        public async Task<List<ReadingStorageModel>> Get()
        {
            await Load();
            return readings;
        }

        public async Task Add(ReadingStorageModel reading)
        {
            readings.Add(reading);
            await Save();
        }

        public async Task Remove(Guid id)
        {
            var readingToRemove = readings.FirstOrDefault(x => x.Id == id);

            if (readingToRemove is null)
                return;

            readings.Remove(readingToRemove);
            await Save();        
        }

        private async Task Load()
        {
            var fromStorage = await browserStorage.GetAsync<List<ReadingStorageModel>>("readings");
            var result = fromStorage.Success ? fromStorage.Value : new List<ReadingStorageModel>();

            readings = result ?? new List<ReadingStorageModel>();
        }

        private async Task Save()
        {
            await browserStorage.SetAsync("readings", readings);
        }
    }
}
