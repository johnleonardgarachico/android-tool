using ChicoKoodo.AndroidApp.Models;
using ChicoKoodo.AndroidApp.Services;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ChicoKoodo.AndroidApp.Pages;

[QueryProperty(nameof(Type), "type")]
[QueryProperty(nameof(Level), "level")]
public partial class NihongoPractice : ContentPage, INotifyPropertyChanged
{
    public string Type { get; set; }

    public string Level { get; set; }

    private string _feedback;
    public string Feedback
    {
        get => _feedback;
        set
        {
            if (_feedback != value)
            {
                _feedback = value;
                OnPropertyChanged(nameof(Feedback));
            }
        }
    }

    private int _currentScore;
    private int _totalScore;
    private string _score;

    public string Score
    {
        get => _score;
        set
        {
            if (_score != value)
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }
    }

    private string _question;
    public string Question
    {
        get => $"Enter the Japanese Translation for the sentence below:\n\n\n{_question}";
        set
        {
            if (_question != value)
            {
                _question = value;
                OnPropertyChanged(nameof(Question));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private readonly NihongoDataManagementService _nihongoDataManagementService;

    private List<NihongoData> _dataForPractice;

    private int _questionCurrentIndex;
    public NihongoPractice(NihongoDataManagementService nihongoDataManagementService)
	{
        ArgumentNullException.ThrowIfNull(nihongoDataManagementService);

		InitializeComponent();
        _nihongoDataManagementService = nihongoDataManagementService;

        BindingContext = this;
    }
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        _questionCurrentIndex = 0;
        FeedbackLabel.Text = string.Empty;
        AnswerEntry.Text = string.Empty;
        _dataForPractice = (await _nihongoDataManagementService.GetNihongoDataAsync(Type, Level)).ToList();

        Shuffle(_dataForPractice);

        Question = _dataForPractice[_questionCurrentIndex].EnglishSentence;
        _totalScore = _dataForPractice.Count;
        _currentScore = 0;
        Score = $"Score: {_currentScore} / {_totalScore}";
    }

    private async void OnSubmitAnswerClicked(object sender, EventArgs e)
    {
        if (_dataForPractice.Count <= _questionCurrentIndex)
        {
            // All Questions are completed
            // What should we do here?
            await Shell.Current.DisplayAlert("Information", "You have completed all questions", "OK");
            return;
        }

        if (IsAnswerCorrect(AnswerEntry.Text))
        {
            _currentScore++;
            await Shell.Current.DisplayAlert("Grapes!", "Your Answer is Correct!", "OK");
        }
        else
        {
            await Shell.Current.DisplayAlert("Not Grapes!", "Wrong Answer", "OK");
            var feedbackStringBuilder = new StringBuilder();
            feedbackStringBuilder.Append("Previous Correct Answer: ");
            feedbackStringBuilder.AppendLine(_dataForPractice[_questionCurrentIndex].NihongoSentence);
            feedbackStringBuilder.Append("You entered: ");
            feedbackStringBuilder.Append(AnswerEntry.Text);
            FeedbackLabel.Text = feedbackStringBuilder.ToString();
        }

        _questionCurrentIndex++;

        if (_dataForPractice.Count > _questionCurrentIndex)
        {
            AnswerEntry.Text = string.Empty;
            Question = _dataForPractice[_questionCurrentIndex].EnglishSentence;
        }

        Score = $"Score: {_currentScore} / {_totalScore}";
    }

    private bool IsAnswerCorrect(string answer)
    {
        return _dataForPractice[_questionCurrentIndex].NihongoSentence == AnswerEntry.Text;
    }

    private static void Shuffle<T>(List<T> list)
    {
        Random rng = new();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}