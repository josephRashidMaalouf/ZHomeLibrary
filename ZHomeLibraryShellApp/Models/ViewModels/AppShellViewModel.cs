using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using ZHomeLibraryShellApp.Language;
using ZHomeLibraryShellApp.Managers;

namespace ZHomeLibraryShellApp.Models.ViewModels;

public partial class AppShellViewModel : ObservableObject, INotifyPropertyChanged
{

    [RelayCommand]
    public async Task ChangeToEnglish()
    {
        await LanguageManager.OnLanguageChanged(new English());
    }
    [RelayCommand]
    public async Task ChangeToSwedish()
    {
        await LanguageManager.OnLanguageChanged(new Swedish());
    }
}