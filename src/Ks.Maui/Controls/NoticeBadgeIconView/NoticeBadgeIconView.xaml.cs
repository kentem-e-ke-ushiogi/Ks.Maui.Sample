using Ks.Mobile.Notice;
using System.Windows.Input;

namespace Ks.Maui.Controls;

/// <summary>バッジ付きベルアイコンビュー</summary>
public partial class NoticeBadgeIconView : ContentView
{
    /// <summary>インスタンスの生成</summary>
    public NoticeBadgeIconView()
    {
        InitializeComponent();
        UpdateViewCommand = new Command(async () =>
        {
            await UpdateViewAsync();
        });
        Loaded += NoticeBadgeIconView_Loaded;
    }

    private async void NoticeBadgeIconView_Loaded(object? sender, EventArgs e)
    {
        await UpdateViewAsync();
    }

    private async Task UpdateViewAsync()
    {
        bool res = false;
        try
        {
            res = await MobileNoticeHelper.HasUnreadNotice();
        }
        catch
        {
            // エラーは無視
        }
        finally
        {
            IsBadgeVisible = res;
            OnPropertyChanged(nameof(IsBadgeVisible));
        }
    }

    /// <summary><see cref="ClickedCommand"/>のバインディングプロパティ</summary>
    public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(
        nameof(ClickedCommand),
        typeof(ICommand),
        typeof(NoticeBadgeIconView),
        propertyChanged: (bindable, oldValue, newValue) => ((NoticeBadgeIconView)bindable).ClickedCommand = (ICommand)newValue);

    /// <summary>ボタンクリックコマンド</summary>
    public ICommand ClickedCommand
    {
        get => (ICommand)GetValue(ClickedCommandProperty);
        set => SetValue(ClickedCommandProperty, value);
    }

    /// <summary>未読バッジの表示</summary>
    public bool IsBadgeVisible { get; private set; } = false;

    /// <summary>お知らせアイコン更新コマンド</summary>
    public ICommand UpdateViewCommand { get; private set; }

    private void Button_Clicked(object sender, EventArgs e)
    {
        ClickedCommand?.Execute(null);
    }
}