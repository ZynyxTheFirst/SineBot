using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using Sine.Commands;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sine
{
    class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public DiscordActivity Activity { get; set; }

        public async Task RunAsync()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var ConfigJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = ConfigJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;

            var activity = new DiscordActivity
            {
                ActivityType = ActivityType.Watching,
                Name = "https://github.com/ZynyxTheFirst/SineBot"
            };

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] {ConfigJson.Prefix},
                EnableDms = false,
                EnableMentionPrefix = true,
                DmHelp = false,
                CaseSensitive = false
                
            };
            
            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<TestCommands>();

            //await Client.UpdateStatusAsync();
            
            await Client.ConnectAsync(activity, UserStatus.Online);

            await Task.Delay(-1);
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
