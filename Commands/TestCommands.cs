using Discord;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;

namespace Sine.Commands
{
    class TestCommands : BaseCommandModule
    {
        [Command("test")]
        public async Task Test(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("```Hello World!```").ConfigureAwait(false);
        }
        
        [Command("nig")]
        public async Task NigNog(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("nog").ConfigureAwait(false);
        }

        [Command("respondmessage")]
        public async Task RespondMessage(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync("```" + message.Result.Content + "```");
        }
        
        [Command("respondreaction")]
        public async Task RespondReaction(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Emoji);
        }

        [Command("hello"), Aliases("hi"), Description("Sends an image")]
        public async Task Hello(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            string path = @"..\..\..\Images\Test\hello_world.png";

            await ctx.Channel.SendFileAsync(path, File.OpenRead(path), "Hello", false, null);
        }

        [Command("pepe"), Aliases("feelsbadman"), Description("Feels bad, man.")]
        public async Task Pepe(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            // wrap it into an embed
            var embed = new DiscordEmbedBuilder
            {
                Title = "Pepe",
                ImageUrl = "http://i.imgur.com/44SoSqS.jpg"

                 
            };
            await ctx.RespondAsync(embed: embed);
        }
    }
}
