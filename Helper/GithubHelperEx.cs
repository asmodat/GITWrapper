using GITWrapper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AsmodatStandard;
using AsmodatStandard.Extensions;
using System.Globalization;

namespace GITWrapper
{
    public static class GitHubHelperEx
    {

        private static string GetParamFromUrl(string url, string trigger, int shift, bool throwIfNotFound = true, bool caseSensitive = true)
        {
            if (shift < 0 || trigger.IsNullOrEmpty() || url.IsNullOrEmpty() || !url.Contains("/") || !url.Contains(trigger, CompareOptions.IgnoreCase))
                throw new Exception($"Url '{url ?? "undefined"}' is undefined or not a valid '{trigger ?? "undefined"}' URL");

            var parts = url.Split('/');
            for (int i = 0; i < parts.Length; i++)
            {
                var part = caseSensitive ? parts[i].Trim() : parts[i].Trim().ToLower();
                if (part == trigger && parts.Length > i + shift)
                {
                    var prop = caseSensitive ? parts[i + shift]?.Trim() : parts[i + shift]?.Trim()?.ToLower();
                    if (prop.IsNullOrEmpty())
                        throw new Exception($"Url '{url ?? "undefined"}' did not contain expected property");

                    return prop;
                }

            }

            if(throwIfNotFound)
                throw new Exception($"Property '{trigger ?? "undefined"} + {shift}' was not found in the following Url: '{url ?? "undefined"}'.");

            return null;
        }

        /// <summary>
        /// Expected Formet: https://github.com/user/repo/tree/branch/
        /// </summary>
        public static string GetUserFromUrl(string url)
            => GetParamFromUrl(url: url, trigger: "github.com", shift: 1);

        /// <summary>
        /// Expected Formet: https://github.com/user/repo/tree/branch/
        /// </summary>
        public static string GetRepoFromUrl(string url)
            => GetParamFromUrl(url: url, trigger: "github.com", shift: 2);

        /// <summary>
        /// Expected Formet: https://github.com/user/repo/tree/branch/
        /// </summary>
        public static string GetBranchFromUrl(string url)
            => GetParamFromUrl(url: url, trigger: "github.com", shift: 4);


        public static string GetFileFromUrl(string url)
        {
            var i = 4;
            string location = null;
            string tmp = null;
            do
            {
                tmp = GetParamFromUrl(url: url, trigger: "github.com", shift: ++i, throwIfNotFound: false);
                if (!tmp.IsNullOrEmpty())
                    location += $"{tmp}/";

            } while (!tmp.IsNullOrEmpty());

            return location?.TrimEnd("/");
        }
    }
}
