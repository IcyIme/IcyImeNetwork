using Blazored.LocalStorage;

namespace IcyImeNetwork.Services
{
    public class LanguageService
    {
        private readonly ILocalStorageService _localStorage;

        public LanguageService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> GetCurrentLanguageAsync()
        {
            var language = await _localStorage.GetItemAsync<string>("language");
            return string.IsNullOrEmpty(language) ? "en" : language; // Default to 'en' if not set
        }

        public async Task SetLanguageAsync(string language)
        {
            await _localStorage.SetItemAsync("language", language);
        }
    }
}
