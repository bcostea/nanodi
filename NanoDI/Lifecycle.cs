using System;
using NanoDI.Exceptions;
using System.Collections.Generic;

namespace NanoDI
{
    public sealed class Lifecycle
    {
        private State state = State.None;
        List<ILifecycleObserver> observers = new List<ILifecycleObserver>();

        public void BeforeInitialize()
        {
            foreach (ILifecycleObserver observer in observers)
            {
                observer.BeforeInitialize();
            }
        }

        public void AfterInitialize()
        {

            foreach (ILifecycleObserver observer in observers)
            {
                observer.AfterInitialize();
            }
        }

        public void BeforeDestroy()
        {
            foreach (ILifecycleObserver observer in observers)
            {
                observer.BeforeDestroy();
            }
        }


        public void AfterDestroy()
        {
            foreach (ILifecycleObserver observer in observers)
            {
                observer.AfterDestroy();
            }
        }

        public void Register(ILifecycleObserver observer)
        {
            observers.Add(observer);
        }

        public void Unregister(ILifecycleObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        public void Initialized()
        {
            state = State.Initialized;
            AfterInitialize();
        }

        public void Destroyed()
        {
            state = State.Destroyed;
            AfterDestroy();
        }

        public void NotInitializedRequired()
        {
            if (State.Initialized.Equals(state))
            {
                throw new CompositionException("Application context already initialized");
            }
        }

        public void InitializedRequired()
        {
            if (State.None.Equals(state) || State.Destroyed.Equals(state))
            {
                throw new CompositionException("Application context not initialized");
            }
        }

    }
}
