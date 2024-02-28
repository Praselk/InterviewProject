using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProject.Task1
{
    //A basic implementation of a non-thread-safe signleton. Should not use in multi-thread applications
    public sealed class NonThreadSafeSingleton
    {
        private static NonThreadSafeSingleton instance = null;

        private NonThreadSafeSingleton()
        {
        }

        public static NonThreadSafeSingleton Instance
        {
            get
            {
                if (instance == null)
                    instance = new NonThreadSafeSingleton();

                return instance;
            }
        }
    }

    // Simple thread-safe signleton. Has performance penalties because of accessing the lock object every time
    public sealed class SimpleThreadSafetySingleton
    {
        private static SimpleThreadSafetySingleton instance = null;
        private static readonly object lockObject = new object();

        private SimpleThreadSafetySingleton()
        {
        }

        public static SimpleThreadSafetySingleton Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                        instance = new SimpleThreadSafetySingleton();

                    return instance;
                }
            }
        }
    }

    // No need to access the lock object every time
    public sealed class DoubleCheckLockingSingleton
    {
        private static DoubleCheckLockingSingleton instance = null;
        private static readonly object lockObject = new object();

        private DoubleCheckLockingSingleton()
        {
        }

        public static DoubleCheckLockingSingleton Instance
        {
            get
            {
                if (instance == null)
                    lock (lockObject)
                    {
                        if (instance == null)
                            instance = new DoubleCheckLockingSingleton();
                    }

                return instance;
            }
        }
    }

    //Thread-safe with no lock, but initializes "instance" even on an attempt to access other fields/properties in ThreadSafeNoLockSingleton
    public sealed class ThreadSafeNoLockSingleton
    {
        private static readonly ThreadSafeNoLockSingleton instance = new ThreadSafeNoLockSingleton();

        private ThreadSafeNoLockSingleton()
        {
        }

        public static ThreadSafeNoLockSingleton Instance => instance;
    }

    //Extended version of the previous singleton, which does not instantiate the "instance" object on accessing other fields/properties
    public sealed class ThreadSafeNoLockSingletonEx
    {
        private ThreadSafeNoLockSingletonEx()
        {
        }

        public static ThreadSafeNoLockSingletonEx Instance => InnerClass.instance;

        private class InnerClass
        {
            internal static readonly ThreadSafeNoLockSingletonEx instance = new ThreadSafeNoLockSingletonEx();
        }
    }

    //Using the Lazy<T> type to create a singleton
    public sealed class LazyBasedSingleton
    {
        private static readonly Lazy<LazyBasedSingleton> lazy = new Lazy<LazyBasedSingleton>(() => new LazyBasedSingleton());

        private LazyBasedSingleton()
        {
        }

        public static LazyBasedSingleton Instance => lazy.Value;
    }
}
