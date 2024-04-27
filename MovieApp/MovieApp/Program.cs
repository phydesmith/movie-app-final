﻿using Microsoft.EntityFrameworkCore.Internal;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Dao;
using System;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Services;
using MovieLibraryOO;

namespace MovieApp
{
    internal class Program
    {
        /// <summary>
        /// Entry point for the MainService.  It is unlikely you will need to update this.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            try
            {
                var startup = new Startup();
                var serviceProvider = startup.ConfigureServices();
                var service = serviceProvider.GetService<IMainService>();

                service?.Invoke();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}

