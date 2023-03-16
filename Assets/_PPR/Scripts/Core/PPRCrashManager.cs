using Firebase.Crashlytics;
using System;

namespace PPR.Core
{
    public partial class PPRCrashManager
    {
        public PPRCrashManager()
        {
            PPRDebug.Log($"PPRCrashManager");
            Crashlytics.ReportUncaughtExceptionsAsFatal = true;
        }

        public void LogExceptionHandling(string message)
        {
            Crashlytics.LogException(new Exception(message));
            PPRDebug.LogException(message);
        }

        public void LogBreadcrumb(string message)
        {
            Crashlytics.Log(message);
            PPRDebug.Log(message);
        }
    }
}
