using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPR.Core
{
    public class PPRTimeManager
    {
        private bool isLooping = false;

        private Dictionary<int, List<PPRTimerData>> timerActions = new();
        private List<PPRAlarmData> activeAlarms = new();

        private PPROfflineTime pprOfflineTime;

        private int counter;
        private int alarmCounter;
        private int offlineSeconds;

        public PPRTimeManager()
        {
            TimerLoop();

            PPRManager.Instance.SaveManager.Load(delegate (PPROfflineTime data)
            {
                pprOfflineTime = data ?? new PPROfflineTime
                {
                    LastCheck = DateTime.Now
                };

                PPRManager.Instance.SaveManager.Save(pprOfflineTime);
                CheckOfflineTime();
            });

            PPRManager.Instance.EventManager.AddListener(PPREvents.game_pause, OnPause);
        }

        private void OnPause(object pauseStatus)
        {
            if (!(bool)pauseStatus)
            {
                CheckOfflineTime();
            }
        }

        private void CheckOfflineTime()
        {
            var timePassed = DateTime.Now - pprOfflineTime.LastCheck;
            offlineSeconds = (int)timePassed.TotalSeconds;
            pprOfflineTime.LastCheck = DateTime.Now;
            PPRManager.Instance.SaveManager.Save(pprOfflineTime);

            PPRDebug.Log($"Last offline time is {offlineSeconds}");

            PPRManager.Instance.EventManager.InvokeEvent(PPREvents.offline_time_refreshed, offlineSeconds);
        }

        public int GetLastOfflineTimeSeconds()
        {
            return offlineSeconds;
        }

        private async Task TimerLoop()
        {
            isLooping = true;

            while (isLooping)
            {
                await Task.Delay(1000);
                InvokeTime();
            }

            isLooping = false;
        }

        private void InvokeTime()
        {
            counter++;

            foreach (var timers in timerActions)
            {
                foreach (var timer in timers.Value)
                {
                    var offsetCounter = counter - timer.StartCounter;

                    if (offsetCounter % timers.Key == 0)
                    {
                        timer.TimerAction.Invoke();
                    }
                }
            }

            for (var index = 0; index < activeAlarms.Count; index++)
            {
                var alarmData = activeAlarms[index];

                if (DateTime.Compare(alarmData.AlarmTime, DateTime.Now) < 0)
                {
                    alarmData.AlarmAction.Invoke();
                    activeAlarms.Remove(alarmData);
                }
            }
        }

        public void SubscribeTimer(int intervalSeconds, Action onTickAction)
        {
            if (!timerActions.ContainsKey(intervalSeconds))
            {
                timerActions.Add(intervalSeconds, new List<PPRTimerData>());
            }

            timerActions[intervalSeconds].Add(new PPRTimerData(counter, onTickAction));
        }

        public void UnSubscribeTimer(int intervalSeconds, Action onTickAction)
        {
            timerActions[intervalSeconds].RemoveAll(x => x.TimerAction == onTickAction);
        }

        public int SetAlarm(int seconds, Action onAlarmAction)
        {
            alarmCounter++;

            var alarmData = new PPRAlarmData
            {
                ID = alarmCounter,
                AlarmTime = DateTime.Now.AddSeconds(seconds),
                AlarmAction = onAlarmAction
            };

            activeAlarms.Add(alarmData);
            return alarmCounter;
        }

        public void DisableAlarm(int alarmID)
        {
            activeAlarms.RemoveAll(x => x.ID == alarmID);
        }

        public int GetLeftOverTime(OfflineTimeTypes timeType)
        {
            if (!pprOfflineTime.LeftOverTimes.ContainsKey(timeType))
            {
                return 0;
            }

            return pprOfflineTime.LeftOverTimes[timeType];
        }

        public void SetLeftOverTime(OfflineTimeTypes timeType, int timeAmount)
        {
            pprOfflineTime.LeftOverTimes[timeType] = timeAmount;
        }

        ~PPRTimeManager()
        {
            isLooping = false;
            PPRManager.Instance.EventManager.RemoveListener(PPREvents.game_pause, OnPause);
        }
    }

    public class PPRTimerData
    {
        public Action TimerAction;
        public int StartCounter;

        public PPRTimerData(int counter, Action onTickAction)
        {
            TimerAction = onTickAction;
            StartCounter = counter;
        }
    }

    public class PPRAlarmData
    {
        public int ID;
        public DateTime AlarmTime;
        public Action AlarmAction;
    }

    [Serializable]
    public class PPROfflineTime : IPPRSaveData
    {
        public DateTime LastCheck;
        public Dictionary<OfflineTimeTypes, int> LeftOverTimes = new();
    }

    public enum OfflineTimeTypes
    {
        DailyBonus,
        ExtraBonus
    }
}