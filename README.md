# jQueryDeprecatedAutoFix

The app updates obsolete jQuery code to recommended alternatives. Currently, it resolves the following errors:

jQuery.isFunction() is deprecated
jQuery.isArray is deprecated; use Array.isArray
jQuery.expr[\':\'] is deprecated; use jQuery.expr.pseudos
jQuery.expr.filters is deprecated; use jQuery.expr.pseudos


# How to Use

1. The app is an C# Console Application which needs to be compiled and executed. I use MonoDevelop (https://www.monodevelop.com/) but it can run on Visual Studio as well.

In this example, I will use MonoDevelop. You can download and install it.

2. Open jQueryDeprecatedAutoFix.sln with MonoDevelop (or Visual Studio)

3. Edit file paths in file: 
jQueryDeprecatedAutoFix/jQueryDeprecatedAutoFix/Program.cs

// Change these values
string inputFilePath = "/home/tishonator/tishonator_git/themes/tishcommerce/js/utilities.js";
string outputFilePath = "/home/tishonator/Downloads/utilities.js";

inputFilePath should contain a valid path to a file you want to update.

outputFilePath should contain a valid path to a updated file location. I prefer it to be different than inputFilePath and then to compare both of the files.

4. Compile the App and execute

5. You will see logs in console i.e.

