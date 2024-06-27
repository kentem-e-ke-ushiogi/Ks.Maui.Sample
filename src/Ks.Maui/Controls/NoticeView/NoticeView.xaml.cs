using Ks.Mobile.Notice;
using System.Collections.ObjectModel;

namespace Ks.Maui.Controls;

/// <summary>お知らせ一覧表示ビュー</summary>
public partial class NoticeView : ContentView
{
    /// <summary>インスタンスの生成</summary>
    public NoticeView()
    {
        InitializeComponent();
        Loaded += NoticeView_Loaded;
    }

    private async void NoticeView_Loaded(object? sender, EventArgs e)
    {
        try
        {
            MobileNoticeModel[] items = await MobileNoticeHelper.GetMobileNotices();
            Items = new ObservableCollection<MobileNoticeModel>(items);
            OnPropertyChanged(nameof(Items));
            UpdateEmptyView(null);
        }
        catch (Exception ex)
        {
            UpdateEmptyView(ex);
        }
    }

    private void UpdateEmptyView(Exception? ex)
    {
        if (ex == null)
            EmptyViewText = "お知らせはありません。";
        else
            EmptyViewText = "インターネット接続がありません。";
        OnPropertyChanged(nameof(EmptyViewText));
    }

    /// <summary> 表示するアイテムのコレクション</summary>
    public ObservableCollection<MobileNoticeModel> Items { get; private set; } = [];

    /// <summary>データなし時のテキスト</summary>
    public string EmptyViewText { get; private set; } = "";

}