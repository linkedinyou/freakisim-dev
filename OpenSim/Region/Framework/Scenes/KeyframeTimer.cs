using System;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace OpenSim.Region.Framework.Scenes
{
    public class KeyframeTimer
    {
        private static ThreadedClasses.RwLockedDictionary<Scene, KeyframeTimer> m_timers =
            new ThreadedClasses.RwLockedDictionary<Scene, KeyframeTimer>();

        private Timer m_timer;
        private ThreadedClasses.RwLockedDictionary<KeyframeMotion, object> m_motions = new ThreadedClasses.RwLockedDictionary<KeyframeMotion, object>();
        private object m_timerLock = new object();
        private const double m_tickDuration = 50.0;

        public double TickDuration
        {
            get { return m_tickDuration; }
        }

        public KeyframeTimer(Scene scene)
        {
            m_timer = new Timer();
            m_timer.Interval = TickDuration;
            m_timer.AutoReset = true;
            m_timer.Elapsed += OnTimer;
        }

        public void Start()
        {
            lock (m_timer)
            {
                if (!m_timer.Enabled)
                    m_timer.Start();
            }
        }

        private void OnTimer(object sender, ElapsedEventArgs ea)
        {
            if (!Monitor.TryEnter(m_timerLock))
                return;

            try
            {
                foreach (KeyframeMotion m in m_motions.Keys)
                {
                    try
                    {
                        m.OnTimer(TickDuration);
                    }
                    catch (Exception)
                    {
                        // Don't stop processing
                    }
                }
            }
            catch (Exception)
            {
                // Keep running no matter what
            }
            finally
            {
                Monitor.Exit(m_timerLock);
            }
        }

        public static void Add(KeyframeMotion motion)
        {
            // FREAKKI KeyframeTimer timer;

            //if (motion.Scene == null)
            //    return;

            //timer = m_timers.GetOrAddIfNotExists(motion.Scene, delegate()
            //{
            //    timer = new KeyframeTimer(motion.Scene);

            //    if (!SceneManager.Instance.AllRegionsReady)
            //    {
            //        // Start the timers only once all the regions are ready. This is required
            //        // when using megaregions, because the megaregion is correctly configured
            //        // only after all the regions have been loaded. (If we don't do this then
            //        // when the prim moves it might think that it crossed into a region.)
            //        SceneManager.Instance.OnRegionsReadyStatusChange += delegate(SceneManager sm)
            //        {
            //            if (sm.AllRegionsReady)
            //                timer.Start();
            //        };
            //    }

            //    // Check again, in case the regions were started while we were adding the event handler
            //    if (SceneManager.Instance.AllRegionsReady)
            //    {
            //        timer.Start();
            //    }
            //    return timer;
            //});

            //timer.m_motions[motion] = null;
        }

        public static void Remove(KeyframeMotion motion)
        {
            KeyframeTimer timer;

            if (motion.Scene == null)
                return;

            if (m_timers.TryGetValue(motion.Scene, out timer))
            {
                timer.m_motions.Remove(motion);
            }
        }
    }
}