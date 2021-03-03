﻿//-------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Config.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//-------------------------------------------------------------------------------------------------
namespace Alis.Core
{
    using Newtonsoft.Json;
    using System;
    using Alis.Tools;

    /// <summary>Define the config of videogame</summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Config
    {
        /// <summary>The name</summary>
        private string name;

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnCreate;

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnDestroy;

        /// <summary>Occurs when [on change name].</summary>
        public event EventHandler<bool> OnChangeName;

        /// <summary>Initializes a new instance of the <see cref="Config" /> class.</summary>
        /// <param name="name">The name of videogame.</param>
        [JsonConstructor()]
        public Config(string name)
        {
            this.name = name ?? "AlisGame";

            OnCreate += Config_OnCreate;
            OnDestroy += Config_OnDestroy; 
            OnChangeName += Config_OnChangeName;

            OnCreate?.Invoke(null, true);

            Debug.Log("Memory used after create object {" + GC.GetTotalMemory(true) + "}");
        }

        /// <summary>Finalizes an instance of the <see cref="Config" /> class.</summary>
        ~Config()
        {
            GC.Collect();
            OnDestroy?.Invoke(null, true);
            Debug.Log("Memory used after full collection: {" + GC.GetTotalMemory(true) + "}");
        }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [JsonProperty]
        public string Name
        {
            get { return name; }
            set
            {
                OnChangeName?.Invoke(null, true);
                name = value;
            }
        }

        /// <summary>Configurations the on create.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void Config_OnCreate(object sender, bool e)
        {
            Debug.EventLog("Create new " + this.GetType() + " instancie. {" + this.GetHashCode() + "}");
        }

        /// <summary>Configurations the on destroy.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void Config_OnDestroy(object sender, bool e)
        {
            Debug.EventLog("Destroy " + this.GetType() + " instancie. {" + this.GetHashCode() + "}");
        }

        /// <summary>Configurations the name of the on change.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void Config_OnChangeName(object sender, bool e)
        {
            Debug.EventLog("Change name of videogame to " + name);
        }
    }
}