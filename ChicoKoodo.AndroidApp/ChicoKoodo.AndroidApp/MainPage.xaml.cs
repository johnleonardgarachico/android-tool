
using ChicoKoodo.AndroidApp.Interfaces.Platforms.Android;
using ChicoKoodo.AndroidApp.Models;
using ChicoKoodo.AndroidApp.Pages;
using System.ComponentModel;
using System.Text.Json;

namespace ChicoKoodo.AndroidApp
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private readonly IFileHelper _fileHelper;

        private string _score;
        public string FileContent
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged(nameof(FileContent));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public MainPage(IFileHelper fileHelper)
        {
            InitializeComponent();
            _fileHelper = fileHelper;

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Something went wrong. {ex.Message}", "OK");
            }
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            var sampleData = new NihongoData
            {
                Id = Guid.NewGuid(),
                Type = "Grammar",
                Level = "N5",
                Topic = "～に行きます",
                NihongoSentence = "映画を身に行きます",
                EnglishSentence = "I'm going to watch a movie",
                Reference = "Youtube - Japanese Shadowing"
            };

            sampleData.Helpers.Add("映画 ー えいが ー Movie");
            sampleData.OtherCorrectNihongoSentences.Add("えいがをみにいきます");

            var serialized = JsonSerializer.Serialize(sampleData, 
                Utilities.JsonSerializerHelper.NihongoSerializerOption());
            var targetPath = Path.Combine("Nihongo", sampleData.Level, sampleData.Type);

            await _fileHelper.SaveFileAsync(sampleData.Id.ToString(), targetPath, serialized);

            var readFile = await _fileHelper.ReadFileAsync(sampleData.Id.ToString(), targetPath);

            FileContent = readFile!;

            // This can be use to read stuffs?? What accent does this even use?? 
            // Will explore in the future
            SemanticScreenReader.Announce("");
        }

        private async void OnNavigateNihongoBenkyou(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NihongoBenkyou));
        }
    }

}
