using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SkalvaBank.Domain;

namespace SkalvaBank.Web
{
    public class CookieFilterOperation
    {
        public CookieFilterOperation()
        {
        }

        public CookieFilterOperation(int? idCategorie, DateTime? dateSelected, bool activer, HttpResponse response)
        {
            IdCategorie = idCategorie;
            DateSelected = dateSelected;
            Activer = activer;
            response.Cookies.Append(Constant.COOKIE_FILTER, JsonConvert.SerializeObject(this));
        }

        public int? IdCategorie { get; set; }
        public DateTime? DateSelected { get; set; }
        public bool Activer { get; set; }

        public CookieFilterOperation GetCookieFilterOperation(HttpRequest request)
        {
            if (request.Cookies.ContainsKey(Constant.COOKIE_FILTER))
            {
                return JsonConvert.DeserializeObject<CookieFilterOperation>(request.Cookies[Constant.COOKIE_FILTER]);
            }
            else
            {
                return new CookieFilterOperation();
            }
        }
        public void ActiverCookieFilterOperation(HttpRequest request, HttpResponse response)
        {
            CookieFilterOperation cookie = GetCookieFilterOperation(request);
            cookie.Activer = true;
            response.Cookies.Append(Constant.COOKIE_FILTER, JsonConvert.SerializeObject(cookie));
        }
    }
}