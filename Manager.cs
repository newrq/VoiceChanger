using NAudio.Wave;
using NAudio;
using NAudio.CoreAudioApi;


namespace VoiceChangerCsharp;

public class Manager
{
    public delegate void Error(Exception ex);
    public event Error OnManagerError;

    public delegate void OperationSucc(string message);

    public event OperationSucc onOperationSucc;
    
    
    private VoiceController vc = new VoiceController();
    private ConsoleManager cm;
    
    private bool hearSelf = false;
    public int outDevice = 0;
    public int inDevice = 0;
    
    public float Volume{ get; set;}
    
    
    
    public Manager(bool hearSelf)
    {
        this.hearSelf = hearSelf;
        OnManagerError += OnOnError;
        onOperationSucc+= OnonOperationSucc;
    }

    private void OnonOperationSucc(string mess)
    {
        Console.Clear();
        Console.WriteLine(mess);
    }

    private void OnOnError(Exception errMes)
    {
        throw new Exception($"ERROR : {errMes.Message}");
    }


    public async void init()
    {
        Console.WriteLine("Chose your output device");

            for (int i = 0; i < DirectSoundOut.Devices.Count(); i++)
            {
                Console.WriteLine($"{i}: {DirectSoundOut.Devices.ToList()[i].Description}");
                
            }

            Console.WriteLine("ВАЖНО: 0 - Это дефолтное устройство вывода звука \nWARNING: 0 - default audio device");

        outDevice = Convert.ToInt32(Console.ReadLine())-1;
        Console.WriteLine("Testing");
        try
        {
            await Test();
        }
        catch (Exception e)
        {
            Console.Clear();
            Console.WriteLine("ERROR \n|\n|\n|\n↓");
            OnManagerError(e);
            throw;
        }

        onOperationSucc("Test passed successful");

        Console.Clear() ;
        await Console.Out.WriteLineAsync("Выберите ваш микрофон");

        for (int i = 0; i < WaveInEvent.DeviceCount; i++)
        {
            Console.WriteLine($"{i}: {WaveInEvent.GetCapabilities(i).ProductName}");

        }
        Console.WriteLine("ВАЖНО: 0 - Это дефолтное устройство вывода звука \nWARNING: 0 - default audio device");

        try
        {
            inDevice = Convert.ToInt32(Console.ReadLine());
        }catch(Exception e) {
            await Console.Out.WriteLineAsync(e.Message);
            throw e;
        }

        Console.Clear();

        cm = new ConsoleManager(vc,this);
        
        
        
                
        
    }

    private async Task Test()
    {
        using ( var testAudio = new AudioFileReader(@"D:\Projects\VoiceChangerCsharp\VoiceChangerCsharp\Sounds\test.m4a"))
         using (var outpudDevice = new WaveOutEvent(){DeviceNumber = outDevice, Volume = this.Volume })
        {
            Console.WriteLine("Sound init");
            outpudDevice.Init(testAudio);
            Console.WriteLine("Sound playing");
            outpudDevice.Play();
            while (outpudDevice.PlaybackState == PlaybackState.Playing)
            {
                await Task.Delay(1000);
                Console.WriteLine("sound playing... 1 sec ");
            }
            await Task.Delay(3000);
            Console.WriteLine("Sound played");
        }
    }

    public async Task setVolume(float vol)
    {
        if (vol < 0.0f || vol > 1.0f)
        {
            Volume = 1f;
            Console.WriteLine("Volume min 0.0f , max 1.0f");
        }
        Volume = vol;
    }

}