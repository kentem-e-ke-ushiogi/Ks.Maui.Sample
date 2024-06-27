
using Ks.Mobile.Notice;

namespace Ks.Maui.Controls;

/// <summary>お知らせ一覧のデータテンプレート</summary>
public partial class NoticeViewDataTemplete : ContentView
{
    /// <summary>インスタンスの生成</summary>
    public NoticeViewDataTemplete()
    {
        InitializeComponent();
        PropertyChanged += NoticeViewDataTemplete_PropertyChanged;
    }
    
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        UpdateMaxLinesForIOS(LblTitle);
    }

    private static void UpdateMaxLinesForIOS(Label target)
    {
#if IOS
        // iOS の LineBreakMode と Label コントロールの MaxLines がうまく機能しない
        // https://github.com/dotnet/maui/issues/23159
        if (target.Handler?.PlatformView is UIKit.UILabel label)
        {
            // 太字から通常に戻すと省略が消えてしまうため0に戻してます。
            label.Lines = 0;
            label.Lines = target.MaxLines;
        }
#endif
    }
    private void NoticeViewDataTemplete_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == IsReadedProperty.PropertyName)
        {
            BackgroundColor = IsReaded ? Color.FromArgb("#EAEAEA") : Colors.White;
            LblTitle.FontAttributes = IsReaded ? FontAttributes.None : FontAttributes.Bold;
            EllipseBadge.IsVisible = !IsReaded;
            UpdateMaxLinesForIOS(LblTitle);
        }
        else if (e.PropertyName == TitleProperty.PropertyName)
        {
            LblTitle.Text = Title;
        }
        else if (e.PropertyName == DateProperty.PropertyName)
        {
            LblDate.Text = Date.ToString("yyyy.MM.dd");
        }
        else if (e.PropertyName == IsImportantProperty.PropertyName)
        {
            BrdImport.IsVisible = IsImportant;
        }
    }

    /// <summary><see cref="IsReaded"/>のバインディングプロパティ</summary>
    public static readonly BindableProperty IsReadedProperty = BindableProperty.Create(
        nameof(IsReaded),
        typeof(bool),
        typeof(NoticeViewDataTemplete));

    /// <summary><see cref="Title"/>のバインディングプロパティ</summary>
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(NoticeViewDataTemplete));

    /// <summary><see cref="Link"/>のバインディングプロパティ</summary>
    public static readonly BindableProperty LinkProperty = BindableProperty.Create(
        nameof(Link),
        typeof(string),
        typeof(NoticeViewDataTemplete));

    /// <summary><see cref="Date"/>のバインディングプロパティ</summary>
    public static readonly BindableProperty DateProperty = BindableProperty.Create(
        nameof(Date),
        typeof(DateTime),
        typeof(NoticeViewDataTemplete));

    /// <summary><see cref="IsImportant"/>のバインディングプロパティ</summary>
    public static readonly BindableProperty IsImportantProperty = BindableProperty.Create(
        nameof(IsImportant),
        typeof(bool),
        typeof(NoticeViewDataTemplete),
        true);


    /// <summary>既読</summary>
    public bool IsReaded
    {
        get => (bool)GetValue(IsReadedProperty);
        set => SetValue(IsReadedProperty, value);
    }

    /// <summary>タイトル</summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>外部リンク</summary>
    public string Link
    {
        get => (string)GetValue(LinkProperty);
        set => SetValue(LinkProperty, value);
    }

    /// <summary>公開日</summary>
    public DateTime Date
    {
        get => (DateTime)GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }

    /// <summary>重要なお知らせ</summary>
    public bool IsImportant
    {
        get => (bool)GetValue(IsImportantProperty);
        set => SetValue(IsImportantProperty, value);
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var res = await Browser.Default.OpenAsync(Link, BrowserLaunchMode.External);
        if (res)
        {
            IsReaded = true;
            var model = (MobileNoticeModel)BindingContext;
            MobileNoticeHelper.SetReaded([model.Id]);
        }
    }
}