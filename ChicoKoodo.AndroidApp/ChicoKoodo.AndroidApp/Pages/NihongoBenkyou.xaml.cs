using ChicoKoodo.AndroidApp.Interfaces.Platforms.Android;
using ChicoKoodo.AndroidApp.Models;
using ChicoKoodo.AndroidApp.Pages.Popups;
using ChicoKoodo.AndroidApp.Services;
using CommunityToolkit.Maui.Extensions;

namespace ChicoKoodo.AndroidApp.Pages;

public partial class NihongoBenkyou : ContentPage
{
    private readonly NihongoDataManagementService _nihongoDataManagementService;

    public NihongoBenkyou(NihongoDataManagementService nihongoDataManagementService)
	{
        ArgumentNullException.ThrowIfNull(nihongoDataManagementService, nameof(nihongoDataManagementService));

		InitializeComponent();
        _nihongoDataManagementService = nihongoDataManagementService;
    }

    private async void OnAddNihongoDataClicked(object sender, EventArgs e)
    {
        var popup = new InputNihongoDataPopup(_nihongoDataManagementService);

        await Shell.Current.ShowPopupAsync(popup);
    }

    private async void OnViewNihongoDataClicked(object sender, EventArgs e)
    {
        await Shell.Current.DisplayAlert("Sorry", "This button is not yet supported!", "OK");
    }

    private async void OnPracticeClicked(object sender, EventArgs e)
    {
        var popup = new PracticeNihongoDataOptionPopup();

        popup.OnConfirm = async (type, level) =>
        {
            await Shell.Current.ClosePopupAsync("Ok");
            await Shell.Current.GoToAsync($"/{nameof(NihongoPractice)}?type={type}&level={level}");
        };

        await Shell.Current.ShowPopupAsync(popup);
    }
}