using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenSim.Framework.Console
{
    public class Parser
    {
        // If an unquoted portion ends with an element matching this regex
        // and the next element contains a space, then we have stripped
        // embedded quotes that should not have been stripped
        private static Regex optionRegex = new Regex("^--[a-zA-Z0-9-]+=$");

        public static string[] Parse(string text)
        {
            List<string> result = new List<string>();

            int index;

            string[] unquoted = text.Split(new char[] {'"'});

            for (index = 0 ; index < unquoted.Length ; index++)
            {
                if (index % 2 == 0)
                {
                    string[] words = unquoted[index].Split(new char[] {' '});

                    bool option = false;
                    foreach (string w in words)
                    {
                        if (w != String.Empty)
                        {
                            if (optionRegex.Match(w) == Match.Empty)
                                option = false;
                            else
                                option = true;
                            result.Add(w);
                        }
                    }
                    // The last item matched the regex, put the quotes back
                    if (option)
                    {
                        // If the line ended with it, don't do anything
                        if (index < (unquoted.Length - 1))
                        {
                            // Get and remove the option name
                            string optionText = result[result.Count - 1];
                            result.RemoveAt(result.Count - 1);

                            // Add the quoted value back
                            optionText += "\"" + unquoted[index + 1] + "\"";

                            // Push the result into our return array
                            result.Add(optionText);

                            // Skip the already used value
                            index++;
                        }
                    }
                }
                else
                {
                    result.Add(unquoted[index]);
                }
            }

            return result.ToArray();
        }
    }
}