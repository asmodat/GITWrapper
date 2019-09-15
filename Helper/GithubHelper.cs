using GITWrapper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GITWrapper
{
    public partial class GitHubHelper
    {
        private GitHubRepoConfig _config { get; set; }

        public GitHubHelper(GitHubRepoConfig config)
        {
            _config = config;
        }
    }
}
