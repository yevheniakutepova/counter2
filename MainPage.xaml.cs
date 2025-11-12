using Counter.Models;

namespace Counter;

public partial class MainPage : ContentPage
{
    private List<CounterModel> counters = new();
    private CounterService counterService = new CounterService();

    public MainPage()
    {
        InitializeComponent();
        LoadCounters();
        RenderCounters();
    }

    private void LoadCounters()
    {
        counters = counterService.LoadCounters();

        if (counters.Count == 0)
        {
            counters.Add(new CounterModel("licznik"));
            counterService.SaveCounters(counters);
        }
    }

    private void SaveCounters()
    {
        counterService.SaveCounters(counters);
    }

    private void OnAddCounterClicked(object sender, EventArgs e)
    {
        string name = CounterNameEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            DisplayAlert("Błąd", "Wpisz nazwę licznika", "OK");
            return;
        }

        counters.Add(new CounterModel(name));
        CounterNameEntry.Text = string.Empty;
        SaveCounters();
        RenderCounters();
    }

    private void RenderCounters()
    {
        CountersStack.Children.Clear();

        foreach (var counter in counters)
        {
            var nameLabel = new Label
            {
                Text = counter.Name,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.Black,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.WordWrap,
                MaxLines = 2
            };

            var valueLabel = new Label
            {
                Text = counter.Value.ToString(),
                FontSize = 36,
                TextColor = Colors.Black,
                HorizontalOptions = LayoutOptions.Center
            };

            var minusButton = new Button
            {
                Text = "–",
                FontSize = 24,
                WidthRequest = 60,
                BackgroundColor = Color.FromArgb("#DDDDDD"),
                TextColor = Colors.Black,
                CornerRadius = 12
            };

            var plusButton = new Button
            {
                Text = "+",
                FontSize = 24,
                WidthRequest = 60,
                BackgroundColor = Color.FromArgb("#4CAF50"),
                TextColor = Colors.White,
                CornerRadius = 12
            };

            minusButton.Clicked += (s, e) =>
            {
                counter.Decrement();
                valueLabel.Text = counter.Value.ToString();
                SaveCounters();
            };

            plusButton.Clicked += (s, e) =>
            {
                counter.Increment();
                valueLabel.Text = counter.Value.ToString();
                SaveCounters();
            };

            var buttons = new HorizontalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 20,
                Children = { minusButton, valueLabel, plusButton }
            };

            var frame = new Frame
            {
                CornerRadius = 15,
                BorderColor = Colors.LightGray,
                BackgroundColor = Color.FromArgb("#F9F9F9"),
                Padding = new Thickness(10),
                Content = new VerticalStackLayout
                {
                    Spacing = 10,
                    Children = { nameLabel, buttons }
                },
                Shadow = new Shadow
                {
                    Brush = Brush.Black,
                    Opacity = 0.1f,
                    Radius = 4,
                    Offset = new Point(2, 2)
                }
            };

            CountersStack.Children.Add(frame);
        }
    }
}

