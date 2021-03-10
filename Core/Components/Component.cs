﻿//-------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Component.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//-------------------------------------------------------------------------------------------------
namespace Alis.Core.Components
{
    using System;
    using Newtonsoft.Json;

    /// <summary>Define a component</summary>
    public abstract class Component 
    {
        /// <summary>The game object</summary>
        private GameObject gameObject;

        /// <summary>The is enabled</summary>
        private bool active = true;

        /// <summary>Initializes a new instance of the <see cref="Component" /> class.</summary>
        protected Component()
        {
            OnCreate?.Invoke(this, true);
            Create();
        }

        /// <summary>Finalizes an instance of the <see cref="Component" /> class.</summary>
        ~Component()
        {
            OnDestroy?.Invoke(this, true);
            Destroy();
        }

        /// <summary>Occurs when [on create].</summary>
        public event EventHandler<bool> OnCreate;

        /// <summary>Occurs when [on enable].</summary>
        public event EventHandler<bool> OnEnable;

        /// <summary>Occurs when [on disable].</summary>
        public event EventHandler<bool> OnDisable;

        /// <summary>Occurs when [on destroy].</summary>
        public event EventHandler<bool> OnDestroy;

        /// <summary>Gets or sets the game object.</summary>
        /// <value>The game object.</value>
        [JsonIgnore]
        public GameObject GameObject { get => gameObject; set => gameObject = value; }

        /// <summary>Gets or sets a value indicating whether this <see cref="Component" /> is active.</summary>
        /// <value>
        /// <c>true</c> if active; otherwise, <c>false</c>.</value>
        [JsonProperty]
        public bool Active
        {
            get => active; 
            set
            {
                active = value;
                if (active)
                {
                    OnEnable?.Invoke(this, true);
                    Enable();
                }
                else 
                {
                    OnDisable?.Invoke(this, true);
                    Disable();
                }
            }
        }

        /// <summary>Creates this instance.</summary>
        public virtual void Create()
        {
        }

        /// <summary>Enable this instance.</summary>
        public virtual void Enable() 
        {
        }

        /// <summary>Awakes this instance.</summary>
        public virtual void Awake()
        {
        }

        /// <summary>Before the update.</summary>
        public virtual void BeforeUpdate() 
        {
        }

        /// <summary>Start this instance.</summary>
        public abstract void Start();

        /// <summary>Update this instance.</summary>
        public abstract void Update();

        /// <summary>After the update.</summary>
        public virtual void AfterUpdate() 
        {
        }

        /// <summary>Disable this instance.</summary>
        public virtual void Disable() 
        {
        }

        /// <summary>Destroy this instance.</summary>
        public virtual void Destroy() 
        {
        }
    }
}