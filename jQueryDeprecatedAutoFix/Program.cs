using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace jQueryDeprecatedAutoFix
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Change these values
            string inputFilePath = "/home/tishonator/tishonator_git/themes/tishconsult/js/utilities.js";
            string outputFilePath = "/home/tishonator/Downloads/utilities.js";

            string jsContent = File.ReadAllText(inputFilePath);

            jsContent = AutoFixIsFunction(jsContent);
            jsContent = AutoFixIsArray(jsContent);
            jsContent = AutoFixExpr(jsContent);
            jsContent = AutoFixExprFilters(jsContent);
            jsContent = AutoFixFnClick(jsContent);
            jsContent = AutoFixDelegate(jsContent);
            jsContent = AutoFixFnFocus(jsContent);
            jsContent = AutoFixFnMouseup(jsContent);
            jsContent = AutoFixFnBlur(jsContent);
            jsContent = AutoFixBind(jsContent);
            jsContent = AutoFnSubmit(jsContent);
            jsContent = AutoFnResize(jsContent);
            jsContent = AutoFixType(jsContent);

            File.WriteAllText(outputFilePath, jsContent);
        }

        /*
         * jQuery.isFunction() is deprecated, i.e.
         *
         *  s.isFunction(this._request.abort) -> (typeof this._request.abort === "function")
         */
        public static string AutoFixIsFunction(string input)
        {
            Regex rx = new Regex(@"([\w+|$])\.isFunction\(([^\)]+)\)");

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
                        .Replace(".expr[\':\']", ".expr.pseudos")
                        .Replace(".expr[ \":\" ]", ".expr.pseudos")
                        .Replace(".expr[ \':\' ]", ".expr.pseudos");
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

        /*
         * jQuery.fn.delegate() is deprecated
         * 
         * .delegate( selector, eventName, handlerProxy ) -> .find(selector).on( eventName, handlerProxy )
         */
        public static string AutoFixDelegate(string input)
        {
            while (input.Contains(".delegate("))
            {
                int delegateIndex = input.IndexOf(@".delegate(");

                int iterateIndex = delegateIndex + @".delegate(".Length;
                StringBuilder selectorVariable = new StringBuilder();
                bool isSingleQuoteStringBegin = false;
                bool isDoubleQuoteStringBegin = false;
                while (iterateIndex < input.Length)
                {
                    // handle case of .delegate('a, img', eventType,
                    if (input[iterateIndex] == '\'' && !isDoubleQuoteStringBegin)
                    {
                        if (isSingleQuoteStringBegin)
                        {
                            selectorVariable.Append(input[iterateIndex]);
                            break;
                        }
                        else
                        {
                            selectorVariable.Append(input[iterateIndex]);
                            isSingleQuoteStringBegin = true;
                        }
                    }
                    else if (input[iterateIndex] == '"' && !isSingleQuoteStringBegin)
                    {
                        if (isDoubleQuoteStringBegin)
                        {
                            selectorVariable.Append(input[iterateIndex]);
                            break;
                        }
                        else
                        {
                            selectorVariable.Append(input[iterateIndex]);
                            isDoubleQuoteStringBegin = true;
                        }
                    }
                    else if (input[iterateIndex] == ',' && !isSingleQuoteStringBegin && !isDoubleQuoteStringBegin)
                    {
                        break;
                    }
                    else
                    {
                        selectorVariable.Append(input[iterateIndex]);
                    }

                    ++iterateIndex;
                }

                input = input.Replace(".delegate(" + selectorVariable.ToString() + ",",
                ".find(" + selectorVariable.ToString() + ").on( ")
                            .Replace(".delegate(" + selectorVariable.ToString() + " ,",
                ".find(" + selectorVariable.ToString() + ").on( ");

                Console.WriteLine("Replace " + ".delegate(" + selectorVariable.ToString()
                        + ", with " + ".find(" + selectorVariable.ToString() + ").on( ");
            }

            return input;
        }

        /*
         * jQuery.fn.focus() event shorthand is deprecated
         * 
         * .focus(function(e){ -> .on('focus', function(e){
         *         
         */
        public static string AutoFixFnFocus(string input)
        {
            return input.Replace(".focus( )", ".focus()")
                        .Replace(".focus(function(", ".on('focus', function(")
                        .Replace(".focus( ", ".on('focus', ");
        }

        /*
         * jQuery.fn.mouseup() event shorthand is deprecated
         * 
         * .mouseup(function(e){ -> .on('mouseup', function(e){
         *         
         */
        public static string AutoFixFnMouseup(string input)
        {
            return input.Replace(".mouseup( )", ".mouseup()")
                        .Replace(".mouseup(function(", ".on('mouseup', function(")
                        .Replace(".mouseup( ", ".on('mouseup', ");
        }

        /*
         *  jQuery.fn.blur() event shorthand is deprecated
         * 
         * .blur(function(e){ -> .on('blur', function(e){
         *         
         */
        public static string AutoFixFnBlur(string input)
        {
            return input.Replace(".blur( )", ".blur()")
                        .Replace(".blur(function(", ".on('blur', function(")
                        .Replace(".blur( ", ".on('blur', ");
        }

        /*
         *  jQuery.fn.bind() is deprecated
         * 
         * .bind( -> .on(
         *         
         */
        public static string AutoFixBind(string input)
        {
            return input.Replace(".bind(", ".on(");
        }

        /*
         *  jQuery.fn.submit() event shorthand is deprecated
         * 
         * .submit(function( -> .on('submit', function(
         *         
         */
        public static string AutoFnSubmit(string input)
        {
            return input.Replace(".submit(function(", ".on('submit', function(")
                        .Replace(".submit( function(", ".on('submit', function(");
        }

        /*
         *  jQuery.fn.resize() event shorthand is deprecated
         * 
         * .resize(function( -> .on('resize', function(
         *         
         */
        public static string AutoFnResize(string input)
        {
            return input.Replace(".resize(function(", ".on('resize', function(")
                        .Replace(".resize( function(", ".on('resize', function(");
        }

        /*
         * jQuery.type is deprecated, i.e.
         *
         *  s.type(this._request.abort) -> (typeof this._request.abort)
         */
        public static string AutoFixType(string input)
        {
            Regex rx = new Regex(@"([\w+|$])\.type\(([^\)]+)\)");

            MatchCollection matches = rx.Matches(input);

            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;

                string oldCode = match.Value;
                string newCode = "(typeof " + groups[2] + ")";

                input = input.Replace(oldCode, newCode);

                Console.WriteLine(oldCode + " --> " + newCode);
            }

            return input;
        }
    }
}
