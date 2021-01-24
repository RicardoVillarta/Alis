﻿//-------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="AudioSource.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//-------------------------------------------------------------------------------------------------
namespace Alis.Tools
{
    using System;
    using System.IO;
    using SFML.Audio;

    /// <summary>Control the audio of game object.</summary>
    [System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class AudioSource : IComponent
    {
        /// <summary>The audio</summary>
        private readonly Music audio;

        /// <summary>The play on awake</summary>
        private bool playOnAwake;

        /// <summary>Initializes a new instance of the <see cref="AudioSource" /> class.</summary>
        /// <param name="audioFile">The audio file.</param>
        public AudioSource(string audioFile)
        {
            audio = new Music(audioFile);
            playOnAwake = false;
            Debug.Log("Created a new " + GetType());
        }

        /// <summary>Initializes a new instance of the <see cref="AudioSource" /> class.</summary>
        /// <param name="audioFile">The audio file.</param>
        /// <param name="playOnAwake">if set to <c>true</c> [play on awake].</param>
        public AudioSource(string audioFile, bool playOnAwake)
        {
            audio = new Music(audioFile);
            this.playOnAwake = playOnAwake;
        }

        /// <summary>Initializes a new instance of the <see cref="AudioSource" /> class.</summary>
        /// <param name="audioStream">The audio stream.</param>
        public AudioSource(Stream audioStream)
        {
            audio = new Music(audioStream);
            playOnAwake = false;
        }

        /// <summary>Initializes a new instance of the <see cref="AudioSource" /> class.</summary>
        /// <param name="audioStream">The audio stream.</param>
        /// <param name="playOnAwake">if set to <c>true</c> [play on awake].</param>
        public AudioSource(Stream audioStream, bool playOnAwake)
        {
            audio = new Music(audioStream);
            this.playOnAwake = playOnAwake;
        }

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnPlay;

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnStop;

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnPause;

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnRestart;

        /// <summary>Gets or sets a value indicating whether [play on awake].</summary>
        /// <value>
        /// <c>true</c> if [play on awake]; otherwise, <c>false</c>.</value>
        public bool PlayOnAwake { get => playOnAwake; set => playOnAwake = value; }

        /// <summary>Plays this instance.</summary>
        public void Play()
        {
            audio.Play();
            OnPlay.Invoke(null, true);
        }

        /// <summary>Stops this instance.</summary>
        public void Stop() 
        {
            if (audio.Status == SoundStatus.Playing) 
            {
                audio.Stop();
                OnStop.Invoke(null, true);
            }
        }

        /// <summary>Pauses this instance.</summary>
        public void Pause()
        {
            if (audio.Status == SoundStatus.Playing)
            {
                audio.Pause();
                OnPause.Invoke(null, true);
            }
        }

        /// <summary>Restarts this instance.</summary>
        public void Restart() 
        {
            if (audio.Status == SoundStatus.Playing)
            {
                audio.Stop();
            }

            if (audio.Status == SoundStatus.Paused)
            {
                audio.Stop();
            }

            audio.Play();
            OnRestart.Invoke(null, true);
        }

        /// <summary>Starts this instance.</summary>
        public void Start()
        {
            if (playOnAwake) 
            {
                audio.Play();
            }
        }

        /// <summary>Updates this instance.</summary>
        public void Update()
        {
        }

        /// <summary>Gets the debugger display.</summary>
        /// <returns>Debug string</returns>
        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}