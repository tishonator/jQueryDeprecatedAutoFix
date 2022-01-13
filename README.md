# jQueryDeprecatedAutoFix

The app updates obsolete jQuery code usage to recommended alternatives. Currently, it resolves the following errors:

- jQuery.isFunction() is deprecated
- jQuery.isArray is deprecated; use Array.isArray
- jQuery.expr[\':\'] is deprecated; use jQuery.expr.pseudos
- jQuery.expr.filters is deprecated; use jQuery.expr.pseudos
- jQuery.fn.click() event shorthand is deprecated
- jQuery.fn.delegate() is deprecated
- jQuery.fn.focus() event shorthand is deprecated
- jQuery.fn.mouseup() event shorthand is deprecated
- jQuery.fn.blur() event shorthand is deprecated


# How to Use

1. The app is an C# Console Application which needs to be compiled and executed. I use MonoDevelop (https://www.monodevelop.com/) but it can run on Visual Studio as well.

In this example, I will use MonoDevelop. You can download and install it.

2. Open jQueryDeprecatedAutoFix.sln with MonoDevelop (or Visual Studio)

3. Edit file paths in file: 

jQueryDeprecatedAutoFix/jQueryDeprecatedAutoFix/Program.cs

![image](https://user-images.githubusercontent.com/11191328/147613005-ff48e7e5-f976-4061-a9e6-d36740fd853b.png)

inputFilePath should contain a valid path to a file you want to update.

outputFilePath should contain a valid path to a updated file location. I prefer it to be different than inputFilePath and then to compare both of the files.

4. Compile the App and execute

![image](https://user-images.githubusercontent.com/11191328/147613051-0fb7b691-744b-4ae0-af8e-45c059736800.png)


5. You will see logs in console i.e.

![image](https://user-images.githubusercontent.com/11191328/147612871-3f60fdbe-b131-4df6-b70c-1be7ab8b2691.png)

![image](https://user-images.githubusercontent.com/11191328/147612892-f128dce8-7d90-415e-a63e-2cbcd475affd.png)


