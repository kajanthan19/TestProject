using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestVersion
{
    /// <summary>
    /// A task for git to get the current commit hash.
    /// </summary>
    public class GitVersion : GitClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GitVersion"/> class.
        /// </summary>
        public GitVersion()
        {
            Command = "rev-parse";
            Short = true;
            Revision = "HEAD";
        }

        /// <summary>
        /// Gets or sets the revision to get the version from. Default is HEAD.
        /// </summary>
        public string Revision { get; set; }

        /// <summary>
        /// Gets or sets the commit hash.
        /// </summary>
        [Output]
        public string CommitHash { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to abbreviate to a shorter unique name.
        /// </summary>
        /// <value>
        ///   <c>true</c> if short; otherwise, <c>false</c>.
        /// </value>
        public bool Short { get; set; }

        /// <summary>
        /// Generates the arguments.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void GenerateArguments(CommandLineBuilder builder)
        {
            builder.AppendSwitch("--verify");

            if (Short)
                builder.AppendSwitch("--short");

            base.GenerateArguments(builder);

            builder.AppendSwitch(Revision);
        }

        /// <summary>
        /// Parses a single line of text to identify any errors or warnings in canonical format.
        /// </summary>
        /// <param name="singleLine">A single line of text for the method to parse.</param>
        /// <param name="messageImportance">A value of <see cref="T:Microsoft.Build.Framework.MessageImportance"/> that indicates the importance level with which to log the message.</param>
        protected override void LogEventsFromTextOutput(string singleLine, MessageImportance messageImportance)
        {
            bool isError = messageImportance == StandardErrorLoggingImportance;

            if (isError)
                base.LogEventsFromTextOutput(singleLine, messageImportance);
            else
                CommitHash = singleLine.Trim();
        }
    }
}
