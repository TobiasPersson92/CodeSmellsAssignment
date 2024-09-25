using System;
using System.IO;
using System.Collections.Generic;
using CodeSmellsAssignment.ConsoleIO;
using CodeSmellsAssignment.Data;
using CodeSmellsAssignment.Logic;
using CodeSmellsAssignment.Common;
using static System.Net.WebRequestMethods;
using Microsoft.Extensions.DependencyInjection;
using CodeSmellsAssignment;

namespace CleanCodeAssignment
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConsoleInterface, ConsoleInterface>()
                .AddSingleton<IGameLogic, GameLogic>()
                .AddSingleton<IFileIO>(new FileIO("result.txt"))
                .AddSingleton<Game>()
                .BuildServiceProvider();

            var game = serviceProvider.GetService<Game>();
            game.Run();

        }
    }
}