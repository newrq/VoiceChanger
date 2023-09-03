using NAudio;


namespace VoiceChangerCsharp;

public class VoiceController : IDisposable
{
    public float Pitch { get; set; }
    public float OutVolume { get; set; }
    public VoiceEffects VoiceEffect { get; set; } = VoiceEffects.None;

    private int inDevice = 0;
    private int outDevice = 0;
    

    public void Dispose()
    {
        Console.WriteLine("Disposing program");
    }

    public async Task setPitch(float pith)
    {
        Pitch = pith;
    }

    public async Task Start(int InDevice,int outDevice)
    {
        Console.Clear();
        await Console.Out.WriteLineAsync("Voice controller started...");



        await Console.Out.WriteLineAsync("Press any key to stop it");

        await Task.Run(() =>
        {
            Console.ReadKey();
            
        });
        Dispose();
        
    }

    public async Task ProvideAudio()
    {

    }

    public async Task GetAudioFromDevice()
    {

    }

    public void changeVoice()
    {

    }
    
}