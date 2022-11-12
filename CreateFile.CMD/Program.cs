using System.IO;
using System.CommandLine;
using CreateFile;

RootCommand rootCommand = new("Create a file with specify size");
Argument<FileInfo> fileArg = new(
    name: "filename",
    description: "File name to be created");
Argument<int> sizeArg = new(
    name: "size",
    description: "File size",
    getDefaultValue: () => 1024);
Argument<Unit> unitArg = new(
    name: "unit",
    description: "File size unit",
    getDefaultValue: () => Unit.KB);

rootCommand.AddArgument(fileArg);
rootCommand.AddArgument(sizeArg);
rootCommand.AddArgument(unitArg);

string result;
rootCommand.SetHandler(async (file, size, unit) =>
{
    result = await CreateFileHelper.CreateFileAsync(file, size, unit);
    Console.WriteLine($"\n{result}\n");
}, fileArg, sizeArg, unitArg);

await rootCommand.InvokeAsync(args);

Console.WriteLine("<Press Any Key To End Program>");
Console.ReadKey();