﻿using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestVersion
{
    /// <summary>
    /// A task for Git commands.
    /// </summary>
    public class GitClient : ToolTask
    {
        /// <summary>
        /// Gets or sets the command to run.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the raw arguments to pass to the git command.
        /// </summary>
        public string Arguments { get; set; }


        /// <summary>
        /// Gets or sets the local or working path for git command.
        /// </summary>
        public string LocalPath { get; set; }

        private string FindToolPath(string toolName)
        {
            return string.Empty;
        }

        /// <summary>
        /// Returns a string value containing the command line arguments to pass directly to the executable file.
        /// </summary>
        /// <returns>
        /// A string value containing the command line arguments to pass directly to the executable file.
        /// </returns>
        protected override string GenerateCommandLineCommands()
        {
            var commandLine = new CommandLineBuilder();
            GenerateCommand(commandLine);
            GenerateArguments(commandLine);
            return commandLine.ToString();
        }

        /// <summary>
        /// Generates the command.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected virtual void GenerateCommand(CommandLineBuilder builder)
        {
            builder.AppendSwitch(Command);
        }

        /// <summary>
        /// Generates the arguments.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected virtual void GenerateArguments(CommandLineBuilder builder)
        {
            if (!string.IsNullOrEmpty(Arguments))
                builder.AppendSwitch(Arguments);
        }

        /// <summary>
        /// Returns the fully qualified path to the executable file.
        /// </summary>
        /// <returns>
        /// The fully qualified path to the executable file.
        /// </returns>
        protected override string GenerateFullPathToTool()
        {
            if (string.IsNullOrEmpty(ToolPath))
                ToolPath = FindToolPath(ToolName);

            return Path.Combine(ToolPath, ToolName);
        }

        /// <summary>
        /// Logs the starting point of the run to all registered loggers.
        /// </summary>
        /// <param name="message">A descriptive message to provide loggers, usually the command line and switches.</param>
        protected override void LogToolCommand(string message)
        {
            Log.LogCommandLine(MessageImportance.Low, message);
        }

        /// <summary>
        /// Gets the <see cref="T:Microsoft.Build.Framework.MessageImportance"></see> with which to log errors.
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:Microsoft.Build.Framework.MessageImportance"></see> with which to log errors.</returns>
        protected override MessageImportance StandardOutputLoggingImportance
        {
            get { return MessageImportance.Normal; }
        }

        /// <summary>
        /// Gets the name of the executable file to run.
        /// </summary>
        /// <returns>
        /// The name of the executable file to run.
        /// </returns>
        protected override string ToolName
        {
            get { return "git.exe"; }
        }

        /// <summary>
        /// Indicates whether all task paratmeters are valid.
        /// </summary>
        /// <returns>
        /// true if all task parameters are valid; otherwise, false.
        /// </returns>
        protected override bool ValidateParameters()
        {
            if (string.IsNullOrEmpty(Command))
            {
              //  Log.LogError(Properties.Resources.ParameterRequired, "GitClient", "Command");
                return false;
            }
            return base.ValidateParameters();
        }

        /// <summary>
        /// Returns the directory in which to run the executable file.
        /// </summary>
        /// <returns>
        /// The directory in which to run the executable file, or a null reference (Nothing in Visual Basic) if the executable file should be run in the current directory.
        /// </returns>
        protected override string GetWorkingDirectory()
        {
            if (string.IsNullOrEmpty(LocalPath))
                return base.GetWorkingDirectory();

            return LocalPath;
        }
    }
}
