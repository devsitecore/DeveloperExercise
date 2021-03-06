﻿// <copyright file="StringExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Common.Extensions
{
    using System;
    using System.IO;
    using System.Reflection;

    public static class StringExtensions
    {
        public static string ConvertToApplicationRootPath(this string path)
        {
            if (path.StartsWith("~"))
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var appPath = Uri.UnescapeDataString(uri.Path);

                appPath = Path.GetDirectoryName(appPath);
                appPath = Path.GetDirectoryName(appPath);

                path = path.Replace("~", appPath);
            }

            return path;
        }
    }
}
