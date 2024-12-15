using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Threading.Tasks;

namespace IcyImeNetwork.Services
{
    public class MultiResourceStringLocalizer : IStringLocalizer
    {
        private readonly IStringLocalizer _indexLocalizer;
        private readonly IStringLocalizer _homeLocalizer;

        public MultiResourceStringLocalizer(IStringLocalizerFactory factory)
        {
            // Initialize string localizers for different resource folders
            _indexLocalizer = factory.Create("Index", "Resources/Index");
            _homeLocalizer = factory.Create("Home", "Resources/Home");
        }

        public LocalizedString this[string name]
        {
            get
            {
                // Try to get the value from each resource file
                var indexString = _indexLocalizer[name];
                if (indexString.ResourceNotFound)
                {
                    return _homeLocalizer[name]; // Fallback to the Home resource if not found
                }
                return indexString;
            }
        }

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures)
        {
            return _indexLocalizer.GetAllStrings(includeAncestorCultures)
                .Concat(_homeLocalizer.GetAllStrings(includeAncestorCultures));
        }
    }
}
