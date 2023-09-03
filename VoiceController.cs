using NAudio;


namespace VoiceChangerCsharp;

public class VoiceController : IDisposable
{
    public float Pitch { get; set; }
    public float OutVolume { get; set; }
    public VoiceEffects[] voiceEffects { get; set; }

    private int inDevice = 0;
    private int outDevice = 0;
    
    public void Dispose()
    {
        Console.WriteLine("Disposing program...");
        //TODO: make disposing
        
        Console.Clear();
    }

    public async Task setPitch(float pith)
    {
        Pitch = pith;
    }

    public async Task Start(int InDevice,int outDevice)
    {
        Console.Clear();
        await Console.Out.WriteLineAsync("Voice controller started...");

        // TODO: MAKE IT

        await Console.Out.WriteLineAsync("Press any key to stop it");

        await Task.Run(() =>
        {
            Console.ReadKey();
            
        });
        Dispose();
        
    }

    // TODO: Do everything in the bottom

    public async Task ProvideAudio()
    {

    }

    public async Task GetAudioFromDevice()
    {

    }

    public void changeVoice()
    {

    }

    public void GetAudioEffect()
    {

    }

    public void GetSettings()
    {

    }
}