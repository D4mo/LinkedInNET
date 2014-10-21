﻿
namespace Sparkle.LinkedInNET.Internals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Base class for LinkedIn APIs.
    /// </summary>
    public class BaseApi
    {
        private LinkedInApi linkedInApi;

        internal BaseApi(LinkedInApi linkedInApi)
        {
            this.linkedInApi = linkedInApi;
        }

        internal LinkedInApi LinkedInApi
        {
            get { return this.linkedInApi; }
        }

        internal static string FormatUrl(string format, params string[] values)
        {
            var result = format;

            var dic = new Dictionary<string, string>(values.Length / 2);
            for (int i = 0; i < values.Length; i++)
            {
                if (i % 2 == 0)
                {

                }
                else
                {
                    dic.Add(values[i - 1], values[i]);
                }
            }

            foreach (var key in dic.Keys)
            {
                result = result.Replace("{" + key + "}", dic[key]);
            }

            return result;
        }

        internal void CheckConfiguration(bool apiKey = false, bool apiSecretKey = false)
        {
            var config = this.linkedInApi.Configuration;
            if (config == null)
                throw new InvalidOperationException("Configuration is not set");

            if (apiSecretKey)
                apiKey = true;

            if (apiKey && string.IsNullOrEmpty(config.ApiKey))
                throw new InvalidOperationException("Missing API Key in configuration");

            if (apiSecretKey && string.IsNullOrEmpty(config.ApiSecretKey))
                throw new InvalidOperationException("Missing API Secret Key in configuration");
        }
    }
}