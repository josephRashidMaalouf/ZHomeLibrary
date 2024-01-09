using ZHomeLibraryShellApp.Language;

namespace ZHomeLibraryShellApp.Managers;

public static class LanguageManager
{
    public static ILanguage CurrentLanguage { get; set; } = new English();
    public static event Action<ILanguage> LanguageChanged;

    public static async Task OnLanguageChanged(ILanguage language)
    {
        CurrentLanguage = language;
        LanguageChanged?.Invoke(language);
    }
}