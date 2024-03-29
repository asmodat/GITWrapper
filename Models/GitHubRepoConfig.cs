﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GITWrapper.Models
{
    public class GitHubRepoConfig
    {
        public string user { get; set; }
        public string branch { get; set; }
        public string repository { get; set; }
        public string userAgent { get; set; } = "Asmodat Launcher Toolkit";
        public string accessToken { get; set; }
    }
}
