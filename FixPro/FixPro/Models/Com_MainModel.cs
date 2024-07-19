using System;
using System.Collections.Generic;
using System.Text;

namespace FixPro.Models
{
    public class Com_MainModel
    {
        public int Id { get; set; }
        public string TwilioAccountSid { get; set; }
        public string TwilioauthToken { get; set; }
        public string TwilioFromPhoneNumber { get; set; }
        public string RealtyRapidApi { get; set; }
        public string AddressAutoCompleteKey { get; set; }
    }
}
