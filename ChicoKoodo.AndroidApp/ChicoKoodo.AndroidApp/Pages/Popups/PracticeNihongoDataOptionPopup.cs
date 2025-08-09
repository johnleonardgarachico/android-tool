using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;

namespace ChicoKoodo.AndroidApp.Pages.Popups
{
    public class PracticeNihongoDataOptionPopup : Popup
    {
        public Action<string, string>? OnConfirm;

        public PracticeNihongoDataOptionPopup()
        {
            var typePicker = new Picker
            {
                Title = "Select Type",
                ItemsSource = new List<string> { "Grammar", "Vocabulary" },
                WidthRequest = 180
            };

            var levelPicker = new Picker
            {
                Title = "Select Level",
                ItemsSource = new List<string> { "N5", "N4", "N3", "N2", "N1" },
                WidthRequest = 180
            };

            var okButton = new Button
            {
                Text = "Ok",
                BackgroundColor = Colors.LightGreen,
                Command = new Command(() =>
                {
                    // You can hook this up to logic later
                    var selectedType = typePicker.SelectedItem?.ToString() ?? "";
                    var selectedLevel = levelPicker.SelectedItem?.ToString() ?? "";

                    OnConfirm?.Invoke(selectedType, selectedLevel);
                })
            };

            var cancelButton = new Button
            {
                Text = "Cancel",
                BackgroundColor = Colors.LightGray,
                Command = new Command(() =>
                {
                    Shell.Current.ClosePopupAsync("Canceled");
                })
            };

            Content = new Border
            {
                Padding = 20,
                BackgroundColor = Colors.White,
                StrokeShape = new RoundRectangle { CornerRadius = 12 },
                Content = new VerticalStackLayout
                {
                    Spacing = 10,
                    Children =
                {
                    new HorizontalStackLayout
                    {
                        Spacing = 10,
                        Children =
                        {
                            new Label
                            {
                                Text = "Type:",
                                WidthRequest = 60,
                                VerticalTextAlignment = TextAlignment.Center
                            },
                            typePicker
                        }
                    },
                    new HorizontalStackLayout
                    {
                        Spacing = 10,
                        Children =
                        {
                            new Label
                            {
                                Text = "Level:",
                                WidthRequest = 60,
                                VerticalTextAlignment = TextAlignment.Center
                            },
                            levelPicker
                        }
                    },
                    new HorizontalStackLayout
                    {
                        Spacing = 20,
                        HorizontalOptions = LayoutOptions.Center,
                        Children =
                        {
                            okButton,
                            cancelButton
                        }
                    }
                }
                }
            };
        }
    }

}
