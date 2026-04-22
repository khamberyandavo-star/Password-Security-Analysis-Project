using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectS2Blazer.Services
{
    public class PasswordDictionaryService
    {
        private HashSet<string> _passwords = new HashSet<string>();
        private bool isLoaded = false;

        public async Task LoadAsync(HttpClient http)
        {
            if (isLoaded) return;

            var content = await http.GetStringAsync("data/100k-most-used-passwords-NCSC.txt");

            _passwords = content
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim().ToLower())
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToHashSet();

            isLoaded = true;
        }

        public IEnumerable<string> GetAll()
        {
            return _passwords;
        }
    }
}