using ChicoKoodo.AndroidApp.Models;
using ChicoKoodo.AndroidApp.Services;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;

namespace ChicoKoodo.AndroidApp.Pages.Popups
{
    public class InputNihongoDataPopup : Popup
    {
        private readonly Entry _level = 
            new() { Placeholder = "", WidthRequest = 400 };

        private readonly Entry _type = 
            new() { Placeholder = "", WidthRequest = 400 };

        private readonly Entry _topic = 
            new() { Placeholder = "", WidthRequest = 400 };

        private readonly Entry _reference = 
            new() { Placeholder = "", WidthRequest = 400 };

        private readonly Entry _nihongoSentence = 
            new() { Placeholder = "", WidthRequest = 400 };

        private readonly Entry _englishSentence = 
            new() { Placeholder = "", WidthRequest = 400 };

        public InputNihongoDataPopup(NihongoDataManagementService managementService)
        {
            ApplyTemplate(managementService.InputTemplate);
            
            CanBeDismissedByTappingOutsideOfPopup = false;
            Padding = 20;
            BackgroundColor = Colors.White;
            Content = new VerticalStackLayout
            {
                Spacing = 10,
                Children =
                {
                    new HorizontalStackLayout
                    {
                        Children =
                        {
                            new Label
                            {
                                Text = "Enter Level:" ,
                                VerticalTextAlignment = TextAlignment.Center
                            },
                            _level
                        }
                    },
                    new HorizontalStackLayout
                    {
                        Children =
                        {
                            new Label
                            {
                                Text = "Enter Type:",
                                VerticalTextAlignment = TextAlignment.Center
                            },
                            _type
                        }
                    },
                    new HorizontalStackLayout
                    {
                        Children =
                        {
                            new Label
                            {
                                Text = "Enter Topic:",
                                VerticalTextAlignment = TextAlignment.Center
                            },
                            _topic
                        }
                    },
                    new HorizontalStackLayout
                    {
                        Children =
                        {
                            new Label
                            {
                                Text = "Enter Reference:",
                                VerticalTextAlignment = TextAlignment.Center
                            },
                            _reference
                        }
                    },
                    new HorizontalStackLayout
                    {
                        Children =
                        {
                            new Label
                            {
                                Text = "Enter Nihongo:",
                                VerticalTextAlignment = TextAlignment.Center
                            },
                            _nihongoSentence
                        }
                    },
                    new HorizontalStackLayout
                    {
                        Children =
                        {
                            new Label
                            {
                                Text = "Enter English:",
                                VerticalTextAlignment = TextAlignment.Center
                            },
                            _englishSentence
                        }
                    },
                    new Button
                    {
                        Text = "Save",
                        Command = new Command(async () =>
                        {
                            var nihongoData = GetNihongoData();

                            try
                            {
                                nihongoData.Validate();
                            }
                            catch (Exception ex)
                            {
                                await Shell.Current.DisplayAlert("Invalid", ex.Message, "OK");
                                return;
                            }

                            await managementService.SaveNihongoDataAsync(nihongoData);

                            await Shell.Current.DisplayAlert("Success", "Data Saved Successfully!", "OK");
                            await Shell.Current.ClosePopupAsync("Completed Successfully");
                        })
                    },
                    new Button
                    {
                        Text = "Set As Default",
                        Command = new Command(async () =>
                        {
                            managementService.InputTemplate = new()
                            {
                                Level = _level.Text,
                                Topic = _topic.Text,
                                Type = _type.Text,
                                Reference = _reference.Text,
                            };

                            await Shell.Current.DisplayAlert("Success", "Template Saved Successfully!", "OK");
                            await Shell.Current.ClosePopupAsync("Completed Successfully");
                        })
                    },
                    new Button
                    {
                        Text = "Cancel",
                        Command = new Command(() => Shell.Current.ClosePopupAsync("Canceled"))
                    }
                }
            };
        }
        private void ApplyTemplate(InputNihongoDataTemplate template)
        {
            if (!string.IsNullOrWhiteSpace(template.Type))
            {
                _type.Text = template.Type;
            }

            if (!string.IsNullOrWhiteSpace(template.Level))
            {
                _level.Text = template.Level;
            }

            if (!string.IsNullOrWhiteSpace(template.Reference))
            {
                _reference.Text = template.Reference;
            }

            if (!string.IsNullOrWhiteSpace(template.Topic))
            {
                _topic.Text = template.Topic;
            }
        }

        private NihongoData GetNihongoData()
        {
            return new NihongoData
            {
                Id = Guid.NewGuid(),
                Type = _type.Text,
                Level = _level.Text,
                Topic = _topic.Text,
                Reference = _reference.Text,
                NihongoSentence = _nihongoSentence.Text,
                EnglishSentence = _englishSentence.Text
            };
        }
    }
}
