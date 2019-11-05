using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace JillSharp
{
    public class MyCommands
    {

        [Command("author"), Description("Displays information about my creator."), Aliases("creator")]
        public async Task Author(CommandContext ctx)
        {
            await ctx.RespondAsync("Created and maintained with love by FakeSummer#2112 <3");
        }

        [Command("flipcoin"), Description("I'll flip a coin to help you make alcohol-related decisions."), Aliases("flip", "coinflip", "coin")]
        public async Task FlipCoin(CommandContext ctx)
        {
            Random rnd = new Random();
            int flip = rnd.Next(0, 2) + 1;
            if (flip == 1)
            {
                await ctx.RespondAsync("Heads!");
            }
            else
            {
                await ctx.RespondAsync("Tails!");
            }
        }

        [Command("stats"), Description("I'll tell you a little about myself and who uses me."), Aliases("statistics", "info")]
        public async Task GuildInfoAsync(CommandContext ctx)
        {
            int memberCountTotal = 0;
            foreach (DiscordGuild members in ctx.Client.Guilds.Values)
            {
                memberCountTotal += members.MemberCount;
            }

            var eb = new DiscordEmbedBuilder()
                    .WithColor(new DiscordColor("#C1272D"))
                    .WithTitle("Jill#")
                    .WithDescription("A bartender bot with a large database of drink recipes and search functionality.")
                .AddField("Developer", "[FakeSummer](https://github.com/FakeSummer)")
                .AddField("Feedback", "Comments, questions, suggestions? DM FakeSummer#2112")
                .AddField("Environment",
                    $"*OS:* {Environment.OSVersion.VersionString}" +
                    $"\n*Framework:* {Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName}" +
                    $"\n*DSharpPlus:* {ctx.Client.VersionString}" +
                    $"\n*ServerCount:* {ctx.Client.Guilds.Count}" +
                    $"\n*MembersServed:* {memberCountTotal}")
                    .Build();

            await ctx.RespondAsync(embed: eb);
        }
        
        [Command("serverlist"), Hidden]
        public async Task GuildList(CommandContext ctx)
        {
            StringBuilder guildString = new StringBuilder();
            foreach (DiscordGuild gl in ctx.Client.Guilds.Values)
            {
                guildString.Append(gl.Name + ": " + gl.MemberCount + "\n");
            }
            string guildNames = guildString.ToString();
            await ctx.RespondAsync(guildNames);
        }



        [Command("r"), Description("Use this to search for a specific drink's recipe information."), Aliases("recipe")]
        public async Task Recipe(CommandContext ctx, params string[] drinkArg)
        {
            var drink = string.Join(" ", drinkArg).ToLower();
            foreach (Drink recipe in Program.drinks)
            {
                if (recipe.DrinkName == drink)
                {
                    await ctx.RespondAsync(recipe.DrinkImage);
                    await ctx.RespondAsync(recipe.DrinkRecipe);
                }
            }
        }

        [Command("drinklist"), Description("I'll display a list of all the drinks I know"), Aliases("list")]
        public async Task DrinkList(CommandContext ctx)
        {
            StringBuilder finalList = new StringBuilder();
            foreach (Drink list in Program.drinks)
            {
                finalList.Append(list.DrinkName + ", ");
            }

            string drinkList = finalList.ToString();

            // Changing last comma to a period

            int lastComma = drinkList.LastIndexOf(",");
            drinkList = drinkList.Substring(0, lastComma) + ".";
            await ctx.RespondAsync(drinkList);
        }

        [Command("i"), Description("Use this to search for drinks that contain a specific ingredient."), Aliases("ingredient")]
        public async Task SearchByIngredient(CommandContext ctx, params string[] ingredientArg)
        {
            var ingredient = string.Join(" ", ingredientArg).ToLower();

            StringBuilder drinksWithIngredient = new StringBuilder();
            foreach (Drink ing in Program.drinks)
            {
                if (ing.DrinkRecipe.ToLower().Contains(ingredient))
                {
                    drinksWithIngredient.Append(ing.DrinkName + ", ");
                }
            }

                string ingDrinks = "Here is a list of drinks that contain " + ingredient + ": " + drinksWithIngredient.ToString();

                // Comma replace again

                int lastComma2 = ingDrinks.LastIndexOf(",");
                ingDrinks = ingDrinks.Substring(0, lastComma2) + ".";
                await ctx.RespondAsync(ingDrinks);
        }

        /*[Command("status"), Hidden]
        public async Task StatusAsync(CommandContext ctx, string statusInfo)
        {
            var game = new DiscordGame(statusInfo);
            await ctx.Client.UpdateStatusAsync(game, null, null);
        }*/

    }
}



