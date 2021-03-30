﻿//-------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="VideoGame.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//-------------------------------------------------------------------------------------------------
namespace Alis.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Alis.Tools;
    using Newtonsoft.Json;
    using SFML.Audio;
    using SFML.System;

    /// <summary>Define the game.</summary>
    public class VideoGame
    {
        /// <summary>The configuration</summary> 
        [NotNull]
        private Config config;

        /// <summary>The scene manager</summary>
        [NotNull]
        private SceneManager sceneManager;

        /// <summary>The render</summary>
        [NotNull]
        private Render render;

        /// <summary>The input</summary>
        [NotNull]
        private Input input;

        /// <summary>The is running</summary>
        [NotNull]
        private bool isRunning;

        /// <summary>The is new frame</summary>
        [NotNull]
        private bool isNewFrame;

        /// <summary>The is stopped</summary>
        [NotNull]
        private bool isStopped;

        /// <summary>The clock</summary>
        [NotNull]
        private Clock clock = new Clock();

        /// <summary>Initializes a new instance of the <see cref="VideoGame" /> class.</summary>
        /// <param name="config">The configuration.</param>
        /// <param name="scenes">The scenes.</param>
        [JsonConstructor]
        internal VideoGame([NotNull] Config config, [NotNull] List<Scene> scenes)
        {
            this.config = config;

            render = new Render(config);
            input = new Input(config);

            this.clock = new Clock();
            isRunning = true;

            sceneManager = new SceneManager(scenes);

            OnCreate += VideoGame_OnCreate;
            OnAwake += VideoGame_OnAwake;
            OnStart += VideoGame_OnStart;
            OnUpdate += VideoGame_OnUpdate;
            OnFixedUpdate += VideoGame_OnFixedUpdate;
            OnStop += VideoGame_OnStop;
            OnExit += VideoGame_OnExit;
            OnDestroy += VideoGame_OnDestroy;

            OnCreate.Invoke(this, true);
        }

        /// <summary>Initializes a new instance of the <see cref="VideoGame" /> class.</summary>
        /// <param name="config">The configuration.</param>
        /// <param name="scenes">The scene.</param>        
        public VideoGame([NotNull] Config config, [NotNull] params Scene[] scenes)
        {
            this.config = config;

            render = new Render(config);
            input = new Input(config);

            this.clock = new Clock();
            isRunning = true;

            sceneManager = new SceneManager(scenes);

            OnCreate += VideoGame_OnCreate;
            OnAwake += VideoGame_OnAwake;
            OnStart += VideoGame_OnStart;
            OnUpdate += VideoGame_OnUpdate;
            OnFixedUpdate += VideoGame_OnFixedUpdate;
            OnStop += VideoGame_OnStop;
            OnExit += VideoGame_OnExit;
            OnDestroy += VideoGame_OnDestroy;


            OnCreate.Invoke(this, true);
        }

        /// <summary>Finalizes an instance of the <see cref="VideoGame" /> class.</summary>
        ~VideoGame() => OnDestroy.Invoke(this, true);

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnCreate;

        /// <summary>Occurs when [on awake].</summary>
        public event EventHandler<bool> OnAwake;

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnStart;

        /// <summary>Occurs when [on update].</summary>
        public event EventHandler<bool> OnUpdate;

        /// <summary>Occurs when [on fixed update].</summary>
        public event EventHandler<bool> OnFixedUpdate;

        /// <summary>Occurs when [on exit].</summary>
        public event EventHandler<bool> OnStop;

        /// <summary>Occurs when [on exit].</summary>
        public event EventHandler<bool> OnExit;

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnDestroy;

        /// <summary>Gets or sets the configuration.</summary>
        /// <value>The configuration.</value>
        [NotNull]
        [JsonProperty("Config" +
            "")]
        public Config Config { get => config; set => config = value; }

        /// <summary>Gets or sets the render.</summary>
        /// <value>The render.</value>
        [NotNull]
        [JsonIgnore]
        public Render Render { get => render; set => render = value; }

        /// <summary>Gets or sets the input.</summary>
        /// <value>The input.</value>
        [NotNull]
        [JsonIgnore]
        public Input Input { get => input; set => input = value; }

        /// <summary>Gets or sets the scene manager.</summary>
        /// <value>The scene manager.</value>
        [NotNull]
        [JsonProperty("SceneManager")]
        public SceneManager SceneManager { get => sceneManager; set => sceneManager = value; }

        /// <summary>Gets or sets a value indicating whether this instance is new frame.</summary>
        /// <value>
        /// <c>true</c> if this instance is new frame; otherwise, <c>false</c>.</value>
        [NotNull]
        [JsonIgnore]
        public bool IsNewFrame { get => isNewFrame; set => isNewFrame = value; }

        /// <summary>Loads the of file.</summary>
        /// <param name="file">The file.</param>
        [return: NotNull]
        public static VideoGame LoadOfFile(string file) => LocalData.Load<VideoGame>(file);

        /// <summary>Loads the of file.</summary>
        /// <param name="file">The file.</param>
        [return: NotNull]
        public static void RunOfFile() => LocalData.Load<VideoGame>("Data", Environment.CurrentDirectory + "/Data").Run();

        /// <summary>Runs this instance.</summary>
        /// <returns>return true</returns>
        public void Run() 
        {
            Awake();
            Start();
            while (isRunning) 
            {
                if (!isStopped)
                {
                    if (isNewFrame)
                    {
                        Update();
                    }

                    FixedUpdate();
                }
            }

            Exit();
        }

        public byte[] PreviewRender()
        {
            Task.WaitAll(input.Update(), sceneManager.Update());

            return render.FrameBytes();
        }

        /// <summary>Awakes this instance.</summary>
        /// <returns>Init the videogame</returns>
        private void Awake()
        {
            Task.WaitAll(input.Awake(), sceneManager.Awake());
            
            render.Awake();

            OnAwake?.Invoke(this, true);
        }

        /// <summary>Starts this instance.</summary>
        /// <returns>Return true if start all.</returns>
        private void Start()
        {
            Task.WaitAll(input.Start(), sceneManager.Start());
            render.Start();

            OnStart?.Invoke(this, true);
        }

        /// <summary>Update every frame the videogame.</summary>
        private void Update()
        {
            Task.WaitAll(input.Update(), sceneManager.Update());
            render.Update();

            OnUpdate?.Invoke(this, true);
        }

        /// <summary>Update every time the videogame.</summary>
        private void FixedUpdate() 
        {
            Task.WaitAll(input.FixedUpdate(), sceneManager.FixedUpdate());
            render.Update();

            OnFixedUpdate?.Invoke(this, true);
        }

        /// <summary>Stops this instance.</summary>
        private void Stop()
        {
            OnStop?.Invoke(this, true);
        }

        /// <summary>Exit the videogame.</summary>
        private void Exit() 
        {
            Task.WaitAll(input.Exit(), sceneManager.Exit());
            
            render.Exit();
            OnExit?.Invoke(this, true);
        }

        #region DefineEvents

        /// <summary>Video the game on create.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void VideoGame_OnCreate([NotNull] object sender, [NotNull] bool e) => Logger.Info();

        /// <summary>Video the game on destroy.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void VideoGame_OnDestroy([NotNull] object sender, [NotNull] bool e) => Logger.Info();

        /// <summary>Video the game on start.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void VideoGame_OnStart([NotNull] object sender, [NotNull] bool e) => Logger.Info();

        /// <summary>Video the game on update.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void VideoGame_OnUpdate([NotNull] object sender, [NotNull] bool e) => Logger.Info();

        /// <summary>Videoes the game on exit.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void VideoGame_OnExit([NotNull] object sender, [NotNull] bool e) => Logger.Info();

        /// <summary>Videoes the game on stop.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void VideoGame_OnStop([NotNull] object sender, [NotNull] bool e) => Logger.Info();

        /// <summary>Videoes the game on fixed update.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void VideoGame_OnFixedUpdate([NotNull] object sender, [NotNull] bool e) => Logger.Info();

        /// <summary>Videoes the game on awake.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void VideoGame_OnAwake([NotNull] object sender, [NotNull] bool e) => Logger.Info();

        #endregion
    }
}



/*//-------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="VideoGame.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//-------------------------------------------------------------------------------------------------
namespace Alis.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Alis.Tools;
    using Newtonsoft.Json;
    using SFML.Audio;
    using SFML.System;

    /// <summary>Define the game.</summary>
    public class VideoGame
    {
        /// <summary>The configuration</summary> 
        [NotNull]
        private Config config;

        /// <summary>The scene manager</summary>
        [NotNull]
        private SceneManager sceneManager;

        /// <summary>The render</summary>
        [NotNull]
        private Render render;

        /// <summary>The input</summary>
        [NotNull]
        private Input input;

        /// <summary>The is running</summary>
        [NotNull]
        private bool isRunning;

        /// <summary>The clock</summary>
        [NotNull]
        private Clock clock = new Clock();

        /// <summary>Initializes a new instance of the <see cref="VideoGame" /> class.</summary>
        /// <param name="config">The configuration.</param>
        /// <param name="scenes">The scenes.</param>
        [JsonConstructor]
        public VideoGame([NotNull] Config config, [NotNull] List<Scene> scenes)
        {
            this.config = config;

            render = new Render(config);
            input = new Input(config);

            this.clock = new Clock();
            isRunning = true;

            sceneManager = new SceneManager(scenes);

            OnCreate += VideoGame_OnCreate;
            OnStart += VideoGame_OnStart;
            OnUpdate += VideoGame_OnUpdate;
            OnDestroy += VideoGame_OnDestroy;

            OnCreate.Invoke(this, true);
        }

        /// <summary>Initializes a new instance of the <see cref="VideoGame" /> class.</summary>
        /// <param name="config">The configuration.</param>
        /// <param name="scenes">The scene.</param>        
        public VideoGame([NotNull] Config config, [NotNull] params Scene[] scenes)
        {
            this.config = config;

            render = new Render(config);
            input = new Input(config);

            this.clock = new Clock();
            isRunning = true;

            sceneManager = new SceneManager(scenes);

            OnCreate += VideoGame_OnCreate;
            OnStart += VideoGame_OnStart;
            OnUpdate += VideoGame_OnUpdate;
            OnDestroy += VideoGame_OnDestroy;

            OnCreate.Invoke(this, true);
        }

        /// <summary>Finalizes an instance of the <see cref="VideoGame" /> class.</summary>
        ~VideoGame() => OnDestroy.Invoke(this, true);

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnCreate;

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnStart;

        /// <summary>Occurs when [on update].</summary>
        public event EventHandler<bool> OnUpdate;

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnDestroy;

        /// <summary>Gets or sets the configuration.</summary>
        /// <value>The configuration.</value>
        [NotNull]
        public Config Config { get => config; set => config = value; }

        /// <summary>Gets or sets the render.</summary>
        /// <value>The render.</value>
        [NotNull]
        [JsonIgnore]
        public Render Render { get => render; set => render = value; }

        /// <summary>Gets or sets the input.</summary>
        /// <value>The input.</value>
        [NotNull]
        [JsonIgnore]
        public Input Input { get => input; set => input = value; }

        /// <summary>Gets or sets the scene manager.</summary>
        /// <value>The scene manager.</value>
        [NotNull]
        public SceneManager SceneManager { get => sceneManager; set => sceneManager = value; }

        /// <summary>Loads the of file.</summary>
        /// <param name="file">The file.</param>
        [return: NotNull]
        public static VideoGame LoadOfFile(string file) 
        {
            return LocalData.Load<VideoGame>(file);
        }

        /// <summary>Loads the of file.</summary>
        /// <param name="file">The file.</param>
        [return: NotNull]
        public static void RunOfFile()
        {
            LocalData.Load<VideoGame>("Data", Environment.CurrentDirectory + "/Data").Run();
        }


        /// <summary>Runs this instance.</summary>
        /// <returns>return true</returns>
        public void Run()
        {
            Start();
            Update();
        }

        /// <summary>Starts the asynchronous.</summary>
        /// <returns>Start the videogame.</returns>
        [return: NotNull]
        private void Start()
        {
            Task.Run(() =>
            {
                var watch = new Stopwatch();
                watch.Start();

                OnStart.Invoke(this, true);

                Task.WaitAll(input.Start(), sceneManager.Start());

                watch.Stop();
                Logger.Log($" Time to Start Videogame: " + watch.ElapsedMilliseconds + " ms");
            }).Wait();

            render.Start();
        }

        private bool firstTime = true;

        public byte[] PreviewRender() 
        {
            Task.WaitAll(input.Update(), sceneManager.Update());

            return render.FrameBytes();
        }

        /// <summary>Updates this instance.</summary>
        private void Update()
        {
            while (isRunning) 
            {
                if (clock.ElapsedTime.AsMilliseconds() >= 0.1f)
                {
                    var watch = new Stopwatch();
                    watch.Start();

                    Task.Run(() =>
                    {
                        OnUpdate.Invoke(this, true);
                        Task.WaitAll(input.Update(), sceneManager.Update());
                    }).Wait();

                    //isRunning = false;

                    watch.Stop();
                    Logger.Log($" Time to Update One Frame Videogame: " + (watch.ElapsedMilliseconds) + " ms");

                    render.Update();

                    //Task.Delay(1000).Wait();
                    clock.Restart();
                }

               
            }

            render.Exit();

            GC.Collect();
            GC.WaitForFullGCComplete();
        }

        #region DefineEvents

        /// <summary>Video the game on create.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void VideoGame_OnCreate([NotNull] object sender, [NotNull] bool e) => Logger.Info();

        /// <summary>Video the game on destroy.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void VideoGame_OnDestroy([NotNull] object sender, [NotNull] bool e) => Logger.Info();

        /// <summary>Video the game on start.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void VideoGame_OnStart([NotNull] object sender, [NotNull] bool e) => Logger.Info();

        /// <summary>Video the game on update.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void VideoGame_OnUpdate([NotNull] object sender, [NotNull] bool e)
        {
        }

        #endregion
    }
}
*/