﻿using Newtonsoft.Json;
using System.Text;

namespace HttpClient.Core.Web.Http.Extensions
{
    internal static class ObjectExtensions
    {
        public static StringContent ToStringContent(this object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
    }
}
