using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using FixPro.Models;
using System.Threading.Tasks;
using Akavache;
using System.Reactive.Linq;
using Xamarin.Forms;
using Xamarin.Essentials;


namespace FixPro.Services.Data
{
    public interface IServicesService
    {
        Task<ImageSource> AccountPhoto();
        Task<string> UserToken();
    }


    public class ServicesService : IServicesService
    {
        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        ImageSource Photo;
        string ServiceKey = "ServiceKey";

        EmployeeModel loginModel;
        string MUserToken;
      public  static string UserTokenServiceKey = "UserTokenServiceKey";

        public async Task<ImageSource> AccountPhoto()
        {
            Photo = Controls.StaticMembers.AccountImg;
            await BlobCache.LocalMachine.InsertObject(ServiceKey, Photo, DateTimeOffset.Now.AddYears(1));
            return Photo;
        }


        public async Task<string> UserToken()
        {
            try
            {
                MUserToken = await BlobCache.LocalMachine.GetObject<string>(UserTokenServiceKey);
            }
            catch (Exception)
            {
                MUserToken = null;
            }

            if (MUserToken == null)
            {
                if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    if (Helpers.Settings.Phone != "" && Helpers.Settings.Password != "")
                    {
                        var loginModel = await ORep.GetAsync<EmployeeModel>("api/Login/GetLogin?" + "UserName=" + Helpers.Settings.UserName + "&" + "Password=" + Helpers.Settings.Password + "&" + "PlayerId=" + Helpers.Settings.PlayerId);

                        if (loginModel != null)
                        {
                            MUserToken = loginModel.GernToken;

                            await BlobCache.LocalMachine.InsertObject(UserTokenServiceKey, loginModel.GernToken, DateTimeOffset.Now.AddHours(24));

                            return loginModel.GernToken;
                        }
                    }
                }        
            }
            else
            {
                return MUserToken;
            }

            return MUserToken;
        }
    }
}
