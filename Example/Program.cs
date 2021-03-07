﻿using Alis.Core;
using Alis.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace SFML
{
    /// <summary>Example of videogame.</summary>
    public class Program
    {

        /// <summary>Defines the entry point of the application.</summary>
        /// <param name="args">The arguments.</param>
        public static async Task Main(string[] args)
        {
            string name = Test_Normal(100);
            string name2 = await Test_Task(100);

            Console.WriteLine(name);
            Console.WriteLine(name2);

            Console.ReadLine();
        }

        private static async Task<string> Test_Task(int size)
        {
            var watch = new Stopwatch();
            watch.Start();

            List<Task> tasks = new List<Task>();

            await Task.Run(() => 
            {
                for (int i = 0; i < size; i++)
                {
                    tasks.Add(ProcessAsync());
                }
            });

            await Task.WhenAll(tasks);

            watch.Stop();
            return $"Total Test_Task Time: " + watch.ElapsedMilliseconds + " ms";
        }

        private static async Task ProcessAsync() 
        {
            await Task.Delay(100);
        }

        private static string Test_Normal(int size) 
        {
            var watch = new Stopwatch();
            watch.Start();

            for (int i =0; i < size;i++) 
            {
                Thread.Sleep(100);
            }

            watch.Stop();
            return $"Total Test_Normal Time: " + watch.ElapsedMilliseconds + " ms";
        }


        /*
        private static async Task<string> Test_Task(List<Scene> scenes)
        {
            var watch = new Stopwatch();
            watch.Start();

            List<Task> launcher = new List<Task>();

            for (int i = 0; i < scenes.Count;i++) 
            {
                launcher.Add(Launch(scenes[i]));
            }

            await Task.WhenAll(launcher);

            watch.Stop();
            return $"Total Test_With_Task Time: " + watch.ElapsedMilliseconds + " ms";
        }

        private static async Task Launch(Scene scene) 
        {
            scene.Update();
        } 


        public static string Test_Normal(List<Scene> scenes) 
        {
            var watch = new Stopwatch();
            string result = "";
            watch.Start();

            foreach (Scene scene in scenes) 
            {
                scene.Update();
            }

            watch.Stop();
            return $"Total Test_Normal Time: " + watch.ElapsedMilliseconds + " ms";
        }
        */

    }
}
