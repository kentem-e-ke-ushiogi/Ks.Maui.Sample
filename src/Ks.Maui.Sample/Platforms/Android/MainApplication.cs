using Android.App;
using Android.Runtime;
using Ks.Mobile.Notice;
using Ks.Web.ApiService.Client;
using Ks.Web.ApiService.Models.Controllers.Notice;
using Ks.Web.KsCloud.Models;

namespace Ks.Maui.Sample
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp()
        {

            string appFolder = Context.GetExternalFilesDir(null)!.Path;
            AppFlags? flags = null;
            var client = KsCloudApiClient.FromInMemory();
            var controller = (NoticeInMemoryController)client.Notice;
            controller.NoticeDTOs.AddRange(GetDummyNoticeModelFromServer());
            MobileNoticeHelper.Initialize(appFolder, client, flags);
            return MauiProgram.CreateMauiApp();
        }

        private static NoticeDTO[] GetDummyNoticeModelFromServer()
        {
            List<NoticeDTO> list = [];
            for (int i = 1; i <= 8; i++)
            {
                var item = new NoticeDTO()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-00000000000" + i),
                    Title = "お知らせ" + i,
                    Link = "https://www.kentem.jp/support/20230711_01/",
                    SeverityLevel = Web.KsCloud.Models.Notices.SeverityLevel.None,
                    Date = DateTime.Now.AddDays(i),
                    IsPublished = true,
                    IsNotifyApp = true,
                    CustomerId = null,
                };
                list.Add(item);
            }
            var item2 = new NoticeDTO()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                Title = "【重要】お知らせ9",
                Link = "https://www.kentem.jp/support/20230711_01/",
                SeverityLevel = Web.KsCloud.Models.Notices.SeverityLevel.Important,
                Date = DateTime.Now.AddDays(100),
                IsPublished = true,
                IsNotifyApp = true,
                CustomerId = null,
            };
            list.Add(item2);
            var item3 = new NoticeDTO()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                Title = "【重要】お知らせ10",
                Link = "https://www.kentem.jp/support/20230711_01/",
                SeverityLevel = Web.KsCloud.Models.Notices.SeverityLevel.Important,
                Date = DateTime.Now.AddDays(10),
                IsPublished = true,
                IsNotifyApp = true,
                CustomerId = null,
            };
            list.Add(item3);
            return [.. list];
        }
    }
}
