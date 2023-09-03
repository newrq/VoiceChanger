using VoiceChangerCsharp;

Manager mng = new Manager(true);
await mng.setVolume(0.1f);
mng.init();
await Task.Delay(-1);
