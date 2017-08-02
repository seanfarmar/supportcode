namespace CentralSystem.Framework.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Helper class to parse command line string value.
    /// 
    /// Supported command line formats:
    /// -[CommandName] /[FlagName] /[ArgumentName1]="[ArgumentValue1]" /[ArgumentName2]=[ArgumentValue2] or 
    /// -[CommandName] /[FlagName] /[ArgumentName1]:[ArgumentValue1] /[ArgumentName2]:[ArgumentValue2]
    /// 
    /// For detection:
    /// - [CommandName] - use the function IsTrue([CommandName])
    /// - [FlagName] - use the function IsTrue([FlagName])
    /// - [ArgumentValue1] - Single([ArgumentName])
    /// - Optional parameter - Exists([ArgumentName])
    /// </summary>
    public sealed class CommandLineParser
    {

        #region Members

        /// <summary>
        /// Parameters
        /// </summary>
        private readonly Dictionary<string, Collection<string>> parameters;

        /// <summary>
        /// Waiting parameter
        /// </summary>
        private string waitingParameter;

        #endregion

        #region Constructors

        public CommandLineParser(IEnumerable<string> arguments)
        {
            parameters = new Dictionary<string, Collection<string>>(StringComparer.OrdinalIgnoreCase);

            string[] parts;

            //Splits on beginning of arguments ( - and -- and / )
            //And on assignment operators ( = and : )
            var argumentSplitter = new Regex(@"^-{1,2}|^/|=|:",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var argument in arguments)
            {
                parts = argumentSplitter.Split(argument, 3);
                switch (parts.Length)
                {
                    case 1:
                        AddValueToWaitingArgument(parts[0]);
                        break;
                    case 2:
                        AddWaitingArgumentAsFlag();

                        //Because of the split index 0 will be a empty string
                        waitingParameter = parts[1];
                        break;
                    case 3:
                        AddWaitingArgumentAsFlag();

                        //Because of the split index 0 will be a empty string
                        string valuesWithoutQuotes = RemoveMatchingQuotes(parts[2]);

                        AddListValues(parts[1], valuesWithoutQuotes.Split(','));
                        break;
                }
            }

            AddWaitingArgumentAsFlag();
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Splits the command line. When main(string[] args) is used escaped quotes (ie a path "c:\folder\")
        /// Will consume all the following command line arguments as the one argument. 
        /// This function ignores escaped quotes making handling paths much easier.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <returns>Command Line parser instance.</returns>
        public static CommandLineParser Parse(string commandLine)
        {
            return new CommandLineParser(SplitCommandLine(commandLine));
        }

        /// <summary>
        /// Splits the command line. When main(string[] args) is used escaped quotes (ie a path "c:\folder\")
        /// Will consume all the following command line arguments as the one argument. 
        /// This function ignores escaped quotes making handling paths much easier.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <returns>List of arguments</returns>
        public static string[] SplitCommandLine(string commandLine)
        {
            var translatedArguments = new StringBuilder(commandLine);
            var escaped = false;
            for (var i = 0; i < translatedArguments.Length; i++)
            {
                if (translatedArguments[i] == '"')
                {
                    escaped = !escaped;
                }
                if (translatedArguments[i] == ' ' && !escaped)
                {
                    translatedArguments[i] = '\n';
                }
            }

            var toReturn = translatedArguments.ToString().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = RemoveMatchingQuotes(toReturn[i]);
            }
            return toReturn;
        }

        /// <summary>
        /// Remove matching quotes
        /// </summary>
        /// <param name="stringToTrim">String to trim</param>
        /// <returns>Corrected string</returns>
        public static string RemoveMatchingQuotes(string stringToTrim)
        {
            var firstQuoteIndex = stringToTrim.IndexOf('"');
            var lastQuoteIndex = stringToTrim.LastIndexOf('"');
            while (firstQuoteIndex != lastQuoteIndex)
            {
                stringToTrim = stringToTrim.Remove(firstQuoteIndex, 1);
                stringToTrim = stringToTrim.Remove(lastQuoteIndex - 1, 1); //-1 because we've shifted the indices left by one
                firstQuoteIndex = stringToTrim.IndexOf('"');
                lastQuoteIndex = stringToTrim.LastIndexOf('"');
            }

            return stringToTrim;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get
            {
                return parameters.Count;
            }
        }

        /// <summary>
        /// Adds the specified argument.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="value">The value.</param>
        public void Add(string argument, string value)
        {
            if (!parameters.ContainsKey(argument))
                parameters.Add(argument, new Collection<string>());

            parameters[argument].Add(value);
        }

        /// <summary>
        /// Add the single argument value.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="value">The value.</param>
        public void AddSingle(string argument, string value)
        {
            if (!parameters.ContainsKey(argument))
                parameters.Add(argument, new Collection<string>());
            else
                throw new ArgumentException(string.Format("Argument {0} has already been defined", argument));

            parameters[argument].Add(value);
        }

        /// <summary>
        /// Remove the argument.
        /// </summary>
        /// <param name="argument">The argument.</param>
        public void Remove(string argument)
        {
            if (parameters.ContainsKey(argument))
                parameters.Remove(argument);
        }

        /// <summary>
        /// Determines whether the specified argument is true.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <returns>
        ///     <c>true</c> if the specified argument is true; otherwise, <c>false</c>.
        /// </returns>
        public bool IsTrue(string argument)
        {
            AssertSingle(argument);

            var arg = this[argument];

            return arg != null && arg[0].Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Get the single argument.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <returns>The single value.</returns>
        public string Single(string argument)
        {
            AssertSingle(argument);

            //only return value if its NOT true, there is only a single item for that argument
            //and the argument is defined
            if (this[argument] != null && !IsTrue(argument))
                return this[argument][0];

            return null;
        }

        /// <summary>
        /// True - the argument exists.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <returns>True - argument exists</returns>
        public bool Exists(string argument)
        {
            return (this[argument] != null && this[argument].Count > 0);
        }

        /// <summary>
        /// Gets the <see cref="System.Collections.ObjectModel.Collection&lt;T&gt;"/> with the specified parameter.
        /// </summary>
        /// <value></value>
        public Collection<string> this[string parameter]
        {
            get
            {
                return parameters.ContainsKey(parameter) ? parameters[parameter] : null;
            }
        }
        
        #endregion

        #region Private Methods

        /// <summary>
        /// Add the list of argument values.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="values">The values.</param>
        private void AddListValues(string argument, IEnumerable<string> values)
        {
            foreach (var listValue in values)
            {
                Add(argument, listValue);
            }
        }

        /// <summary>
        /// Add the waiting argument as flag.
        /// </summary>
        private void AddWaitingArgumentAsFlag()
        {
            if (waitingParameter == null) return;

            AddSingle(waitingParameter, "true");
            waitingParameter = null;
        }

        /// <summary>
        /// Add value to waiting argument.
        /// </summary>
        /// <param name="value">The argument value.</param>
        private void AddValueToWaitingArgument(string value)
        {
            if (waitingParameter == null) return;

            value = RemoveMatchingQuotes(value);

            Add(waitingParameter, value);
            waitingParameter = null;
        }

        /// <summary>
        /// Assert single argument state.
        /// </summary>
        /// <param name="argument">The argument.</param>
        private void AssertSingle(string argument)
        {
            if (this[argument] != null && this[argument].Count > 1)
                throw new ArgumentException(string.Format("{0} has been specified more than once, expecting single value", argument));
        }

        #endregion

    }
}
