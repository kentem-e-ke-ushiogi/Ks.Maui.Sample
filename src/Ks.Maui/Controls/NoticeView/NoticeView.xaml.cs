using Ks.Mobile.Notice;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Ks.Maui.Controls;

/// <summary>お知らせ一覧表示ビュー</summary>
public partial class NoticeView : ContentView
{
    /// <summary>インスタンスの生成</summary>
    public NoticeView()
    {
        InitializeComponent();
        Loaded += NoticeView_Loaded;
        AllReadedCommand = CreateAllreadCommand();
    }

    private Command CreateAllreadCommand()
    {
        return new Command(
            execute: async (object parameter) =>
            {
                if (parameter is not ObservableCollection<MobileNoticeModel> items)
                    return;
                bool res = await KsMauiUtilty.GetCurrentPage().DisplayAlert("確認", "すべて既読にしますか？", "既読にする", "キャンセル");
                if (!res)
                    return;
                foreach(var item in items)
                {
                    item.Readed = true;
                }
                Guid[] ids = items.Select(p => p.Id).ToArray();
                MobileNoticeHelper.SetReaded([.. ids]);
                ((Command)AllReadedCommand).ChangeCanExecute();
            },
            canExecute: (object parameter) =>
            {
                if (parameter is not ObservableCollection<MobileNoticeModel> items)
                    return false;
                return items.Any(p => !p.Readed);
            });
    }

    /// <summary>重要なお知らせのみ表示</summary>
    public bool IsImportantOnly { get; set; }

    private async void NoticeView_Loaded(object? sender, EventArgs e)
    {
        try
        {
            MobileNoticeModel[] items;
            if (IsImportantOnly)
                items = await MobileNoticeHelper.GetImportantMobileNotices();
            else
                items = await MobileNoticeHelper.GetMobileNotices();
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

    /// <summary>すべて既読コマンド</summary>
    public ICommand AllReadedCommand { get; private set; }
}