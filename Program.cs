using ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        CommandHandler handler = new CommandHandler();
        while (true)
        {
            Console.Write("Enter command: ");
            string commandLine = Console.ReadLine();
            handler.ExecuteCommand(commandLine);
        }
    }
}