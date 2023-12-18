using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace FixPro.Helpers
{
    public static class Messages
    {


        public async static void ShowSuccessSnackBar(string message)
        {
            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.White,
                    Message = message
                },
                BackgroundColor = Color.FromHex("#b66dff"),
                Duration = TimeSpan.FromSeconds(3),
                Actions = new[] { new SnackBarActionOptions() }
            };
            await Xamarin.Forms.Application.Current.MainPage.DisplaySnackBarAsync(options);
        }
    }
}
