using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;

namespace Coblis_TudorCristian_Lab7
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
