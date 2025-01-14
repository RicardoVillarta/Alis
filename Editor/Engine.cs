﻿//----------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Engine.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//----------------------------------------------------------------------------------------------------
namespace Alis.Editor
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading;
    using Alis.Editor.UI;
    using Alis.Core;

    /// <summary>Manage the engine</summary>
    internal class Engine
    {
        /// <summary>The platform</summary>
        private Platform platform;

        /// <summary>The architecture</summary>
        private Architecture architecture;

        /// <summary>The graphics</summary>
        private Graphics graphics;

        /// <summary>The main window</summary>
        private MainWindow mainWindow;

        /// <summary>The information</summary>
        private Info info;

        /// <summary>Initializes a new instance of the <see cref="Engine" /> class.</summary>
        /// <param name="args">The arguments.</param>
        public Engine(string[] args)
        {
            Logger.Log("Starting Alis...");
            Logger.Log(args.Length > 0 ? " > args:" + string.Join("\n", args) : string.Empty);
        }

        /// <summary>Gets a value indicating whether [first instance].</summary>
        /// <value>
        /// <c>true</c> if [first instance]; otherwise, <c>false</c>.</value>
        private static bool FirstInstance
        {
            get
            {
                _ = new Mutex(true, Assembly.GetEntryAssembly().FullName, out bool created);
                return created;
            }
        }

        /// <summary>Gets the detect platform.</summary>
        /// <value>The detect platform.</value>
        private static Platform DetectPlatform 
        {
            get 
            {
                return
                    RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Platform.Windows :
                    RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? Platform.MacOS :
                    RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? Platform.Linux :
                    Platform.Unsupported;
            }
        }

        /// <summary>Gets the detect architecture.</summary>
        /// <value>The detect architecture.</value>
        private static Architecture DetectArchitecture 
        {
            get 
            {
                return 
                    IntPtr.Size == 8 ? Architecture.X64 :
                    IntPtr.Size == 4 ? Architecture.X86 :
                    Architecture.Unsupported;
            }
        }

        /// <summary>Starts this instance.</summary>
        /// <returns>Return false or true to indicate the exit value</returns>
        public int Start()
        {
            if (!FirstInstance)
            {
                Logger.Error("There is already an 'Alis instance' running.");
                return -1;
            }

            platform = DetectPlatform;
            if (platform.Equals(Platform.Unsupported)) 
            {
                Logger.Error("Platform unsupported. Please use Windows or MacOS or Linux system.");
                return -1;
            }
            
            architecture = DetectArchitecture;
            if (architecture.Equals(Architecture.Unsupported)) 
            {
                Logger.Error("unsupported architecture. Please use x86 or x64 architecture system.");
                return -1;
            }

            bool dirext11 = Veldrid.GraphicsDevice.IsBackendSupported(Veldrid.GraphicsBackend.Direct3D11);
            bool metal = Veldrid.GraphicsDevice.IsBackendSupported(Veldrid.GraphicsBackend.Metal);
            bool opengl = Veldrid.GraphicsDevice.IsBackendSupported(Veldrid.GraphicsBackend.OpenGL);
            bool vulkan = Veldrid.GraphicsDevice.IsBackendSupported(Veldrid.GraphicsBackend.Vulkan);

            if (platform.Equals(Platform.Windows)) 
            {
                graphics = dirext11 ? Graphics.Directx11 :
                           opengl ? Graphics.OpenGL :
                           vulkan ? Graphics.Vulkan :
                           Graphics.Unsupported;

                if (graphics.Equals(Graphics.Unsupported))
                {
                    Logger.Error("Unsupported graphics for windows. Please install ( opengl 2.0+ or Directx 11+ or Vulkan 1.0+ ).");
                    return -1;
                }
            }

            if (platform.Equals(Platform.MacOS))
            {
                graphics = opengl ? Graphics.OpenGL :
                           metal ? Graphics.Metal :
                           vulkan ? Graphics.Vulkan :
                           Graphics.Unsupported;

                if (graphics.Equals(Graphics.Unsupported))
                {
                    Logger.Error("Unsupported graphics for macos. Please install ( opengl 2.0+ or metal 1.0+ or Vulkan 1.0+ ).");
                    return -1;
                }
            }

            if (platform.Equals(Platform.Linux))
            {
                graphics = opengl ? Graphics.OpenGL :
                           vulkan ? Graphics.Vulkan :
                           Graphics.Unsupported;

                if (graphics.Equals(Graphics.Unsupported))
                {
                    Logger.Error("Unsupported graphics for linux. Please install ( opengl 2.0+ or Vulkan 1.0+ ).");
                    return -1;
                }
            }

            info = new Info(platform, architecture, graphics);
            Logger.Log("Info Platform: " + platform.ToString() + " " + architecture.ToString() + " " + graphics.ToString());

            mainWindow = new MainWindow();           
            if (!mainWindow.Start(info)) 
            {
                Logger.Error("Failed to start the main window. ");
                return -1;
            }

            return 0;
        }
    }
}