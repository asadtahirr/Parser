string ReplacePeriods(string input)
{
    return input.Replace(".", "STOP");
}

// set current working directory
Directory.SetCurrentDirectory("C:\\Users\\Owner\\OneDrive\\Desktop\\Parser");

// get the current working directory path
string currentWorkingDirectory = Directory.GetCurrentDirectory();

// get all available files' paths in the current working directory
string[] filePaths = Directory.GetFiles(currentWorkingDirectory);

string requiredFilePath = null;

foreach (string filePath in filePaths)
{
    if (filePath.EndsWith("theMachineStops.txt"))
    {
        requiredFilePath = filePath;
    }
}

if (requiredFilePath == null)
{
    Console.WriteLine("File not found");
}
else
{
    // read the required file's contents and store it in a variable

    string contents = null;

    try
    {
        using (StreamReader reader = new StreamReader("theMachineStops.txt"))
        {
            contents = await reader.ReadToEndAsync();

            reader.Close();
        }

    }
    catch (Exception exception) when (
        exception is ArgumentException
        || exception is ArgumentNullException
    ) {
        Console.WriteLine($"File read error: {exception.Message}");
    }
    catch (Exception exception) when (
        exception is ArgumentOutOfRangeException
        || exception is ObjectDisposedException
        || exception is InvalidOperationException
    ) {
        Console.WriteLine($"File read error: {exception.Message}");
    }


    if (string.IsNullOrEmpty(contents))
    {
        Console.WriteLine("Error");
    }
    else
    {
        string updatedContents = ReplacePeriods(contents);

        try
        {
            using (StreamWriter writer = File.CreateText("TelegramCopy.txt"))
            {
                await writer.WriteAsync(updatedContents);

                writer.Close();
            }
        }
        catch (Exception exception) when (
            exception is UnauthorizedAccessException
            || exception is ArgumentException
            || exception is ArgumentNullException
            || exception is PathTooLongException
            || exception is DirectoryNotFoundException
            || exception is NotSupportedException
        ) {
            Console.WriteLine($"New file create error: {exception.Message}");
        }
        catch (Exception exception) when (
            exception is ObjectDisposedException
            || exception is InvalidOperationException
        ) {
            Console.WriteLine($"File save error: {exception.Message}");
        }

        Console.WriteLine("Operation successful.");
    }
}
