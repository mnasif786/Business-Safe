using System;
using System.Configuration;
using System.Timers;
using BusinessSafe.EscalationService.Activation;
using BusinessSafe.EscalationService.EscalateTasks;
using StructureMap;

namespace BusinessSafe.EscalationService
{
    public class EscalationService
    {
        private readonly Timer _timer;
        private bool _isProcessingFlag = false;

        public EscalationService(int? pollingIntervalSeconds = null)
        {
            //Process();
            _timer = new Timer(pollingIntervalSeconds ?? PollingIntervalInMilliSeconds) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) =>
            {
                var isEscalationOn = bool.Parse(ConfigurationManager.AppSettings["isEscalationOn"]);
                if (!isEscalationOn)
                {
                    Log4NetHelper.Log.Info("Escalation is not processing");
                    return;
                }
             
                if(!_isProcessingFlag)
                    Process();
            };
        }

        public int PollingIntervalInMilliSeconds
        {
            get
            {
                var value = int.Parse(ConfigurationManager.AppSettings["pollingIntervalMinutes"]);
                return value * 60 * 1000;
            }
        }

        public void Start()
        {
            if (_timer != null)
                _timer.Start();
        }

        public void Stop()
        {
            if (_timer != null)
                _timer.Stop();
        }

        private void Process()
        {
            // Uncomment to enable debugging easily otherwise get hit by other threads
            //if (_timer != null)
            //{
            //    _timer.Stop();
            //}

            Log4NetHelper.Log.Debug(string.Format("Started Process EscalationTasks at {0}", DateTime.Now));

            try
            {
                _isProcessingFlag = true;

                var escalationTasks = ObjectFactory.GetAllInstances<IEscalate>();

                foreach (var escalateTask in escalationTasks)
                {
                    escalateTask.Execute();
                }

                _isProcessingFlag = false;
            }
            catch (Exception ex)
            {
                Log4NetHelper.Log.Error(ex);
            }

            Log4NetHelper.Log.Debug(string.Format("Finished Process EscalationTasks at {0}", DateTime.Now));
        }
    }
}