using System;
using System.IO;
using System.Text.RegularExpressions;

namespace jQueryDeprecatedAutoFix
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Change these values
            string inputFilePath = "/home/tishonator/tishonator_git/themes/tlocksmith/js/utilities.js";
            string outputFilePath = "/home/tishonator/Downloads/utilities.js";

            string jsContent = File.ReadAllText(inputFilePath);

            jsContent = AutoFixIsFunction(jsContent);
            jsContent = AutoFixIsArray(jsContent);
            jsContent = AutoFixExpr(jsContent);
            jsContent = AutoFixExprFilters(jsContent);
            jsContent = AutoFixFnClick(jsContent);

            File.WriteAllText(outputFilePath, jsContent);
        }

        /*
         * jQuery.isFunction() is deprecated, i.e.
         *
         *  s.isFunction(this._request.abort) -> (typeof this._request.abort === "function")
         */
        public static string AutoFixIsFunction(string input)
        {
            Regex rx = new Regex(@"(\w+)\.isFunction\(([^\)]+)\)");

            MatchCollection matches = rx.Matches(input);

            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;

                string oldCode = match.Value;
                string newCode = "(typeof " + groups[2] + " === \"function\")";

                input = input.Replace(oldCode, newCode);

                Console.WriteLine(oldCode + " --> " + newCode);
            }

            return input;
        }

        /*
         * jQuery.isArray is deprecated; use Array.isArray, i.e.
         *
         *  s.isArray(this.obj) -> Array.isArray(this.obj)
         */
        public static string AutoFixIsArray(string input)
        {
            Regex rx = new Regex(@"(\w+)\.isArray\(([^\)]+)\)");

            MatchCollection matches = rx.Matches(input);

            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;

                // check if it's not Array.isArray
                if (!groups[1].ToString().Equals("Array"))
                {
                    string oldCode = match.Value;
                    string newCode = "Array.isArray(" + groups[2] + ")";

                    input = input.Replace(oldCode, newCode);

                    Console.WriteLine(oldCode + " --> " + newCode);
                }
            }

            return input;
        }

        /*
         * jQuery.expr[\':\'] is deprecated; use jQuery.expr.pseudos, i.e.
         *
         *  t.expr[":"] -> t.expr.pseudos
         */
        public static string AutoFixExpr(string input)
        {
            return input.Replace(".expr[\":\"]", ".expr.pseudos")
                        .Replace(".expr[\':\']", ".expr.pseudos");
        }

        /*
         * jQuery.expr.filters is deprecated; use jQuery.expr.pseudos, i.e.
         *
         *  t.expr.filters -> t.expr.pseudos
         */
        public static string AutoFixExprFilters(string input)
        {
            return input.Replace(".expr.filters", ".expr.pseudos");
        }

        /*
         * jQuery.fn.click() event shorthand is deprecated
         * 
         * .click(function(e){ -> .on('click', function(e){
         *         
         */
        public static string AutoFixFnClick(string input)
        {
            return input.Replace(".click(function(", ".on('click', function(")
                        .Replace(".click( function(", ".on('click', function(");
        }
    }
}
