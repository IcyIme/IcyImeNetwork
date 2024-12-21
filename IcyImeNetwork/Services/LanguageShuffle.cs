using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IcyImeNetwork.Services
{
    public class LanguageShuffle : IDisposable
    {
        private readonly List<List<string>> _texts = new List<List<string>>();
        private int _currentIndex;
        private Timer? _timer;
        private TaskCompletionSource<string>? _tcs;

        public LanguageShuffle()
        {
            // Initialize the text list and current index
            List<string> chooseLang = new List<string> { "Choose your language", "Vyberte si jazyk" };
            _texts.Add(chooseLang);
            _currentIndex = 0;
        }

        /// <summary>
        /// Starts the service to return strings at the specified interval.
        /// </summary>
        /// <param name="intervalMilliseconds">The interval in milliseconds.</param>
        /// <param name="content">The index of the language set in the list.</param>
        /// <returns>An asynchronous task that provides the switched text.</returns>
        public Task<string> GetNextTextAsync(int intervalMilliseconds, int content)
        {
            _tcs = new TaskCompletionSource<string>();

            if (_timer == null)
            {
                // Start the timer to switch text at regular intervals
                _timer = new Timer(SwitchText, content, 0, intervalMilliseconds);
            }

            return _tcs.Task;
        }

        private void SwitchText(object? state)
        {
            if (state is int content)
            {
                // Ensure the content index is valid
                if (content >= 0 && content < _texts.Count)
                {
                    var newText = _texts[content][_currentIndex];
                    _currentIndex = (_currentIndex + 1) % _texts[content].Count;

                    // Complete the task with the new text
                    _tcs?.SetResult(newText);

                    // Prepare for the next request
                    _tcs = new TaskCompletionSource<string>();
                }
                else
                {
                    _tcs?.SetException(new ArgumentOutOfRangeException(nameof(content), "Invalid content index"));
                }
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
