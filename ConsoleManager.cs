using System.Net.Http.Headers;

namespace VoiceChangerCsharp;

public class ConsoleManager
{

    private Manager manager;
    private VoiceController vc;
    static private bool commandReg = false;
    
    public ConsoleManager(VoiceController voiceController, Manager manager)
    {
        Console.Clear();
        vc = voiceController;
        this.manager = manager;
        Console.WriteLine("Console manager init");
        init();
    }


    private async Task init()
    {
        await Console.Out.WriteLineAsync("Initing console manager");
        commandReg = true;
        await CommandHandler();
    }
    
    private async Task CommandInit(int num)
    {
        commandReg = false;
        switch (num)
        {
            case (int)Commands.CLOSE:
                commandReg = false;
                await Close();
                break;

            case (int)Commands.START:
                commandReg = false;
                await Start();
                break;

            default:
                await Console.Out.WriteLineAsync("Нету такой комманды");
                commandReg = true;
                CommandHandler();
                break;
        }
    }
    
    private async Task CommandHandler()
    {
        Console.Clear();
        int userNum;
        while (commandReg)
        {
            await Console.Out.WriteLineAsync("Entter command number");
            await Console.Out.WriteLineAsync(await GetCommands());
            try
            {
                 userNum = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            await CommandInit(userNum);
            
        }
    }

    private async Task Close()
    {
        await Console.Out.WriteLineAsync("Closing...");
        await Task.Run(() =>
        {
            Environment.Exit(Environment.ExitCode);

        });
    }

    private async Task Start()
    {
        commandReg = false;
        Console.Clear();
        await Console.Out.WriteLineAsync("Пожалуйста , проверте правильность конфига на изменения голоса...");
        await Console.Out.WriteLineAsync("Приготовтесь к включения изменения голоса...");
        for (int i = 5; i >= 0; i--)
        {
            await Console.Out.WriteLineAsync($"Включение изменения голоса через: {i}");
            await Task.Delay(1000);
        }
        await vc.Start(manager.inDevice,manager.outDevice);
        
    }
    

    private async Task<string> GetCommands()
    {
        string msg = "";
        for (int i = 0; i < Enum.GetValues(typeof(Commands)).Length; i++)
        {
            msg += $"{Enum.GetName(typeof(Commands), i)}  =  {i} \n";
        }

        return msg;
    }
    
    
    
}