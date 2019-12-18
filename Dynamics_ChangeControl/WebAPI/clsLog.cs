using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CELLAPI.Service
{
    public static class clsLog
    {
        private static Logger logger;

        static LogLevel Log;

        static string vFileName = "";
        static string vFolderName = ""; 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFilePath">ex) ${basedir}/" + strName + " ${shortdate}.txt</param>
        /// <param name="Lv">Trace,Debug,Info,Warn,Error,Fatal</param>
        /// <returns></returns>
        //public static void Setting_Logger(string strFileName, string strLv)
        //{

        //    //public static readonly LogLevel Debug;
        //    //public static readonly LogLevel Error;
        //    //public static readonly LogLevel Fatal;
        //    //public static readonly LogLevel Info;
        //    //public static readonly LogLevel Off;
        //    //public static readonly LogLevel Trace;
        //    //public static readonly LogLevel Warn;
        //    vFileName = strFileName;

        //    switch (strLv.ToUpper())
        //    {
        //        case "DEBUG":
        //            Log = LogLevel.Debug;
        //            break;
        //        case "ERROR":
        //            Log = LogLevel.Error;
        //            break;
        //        case "FATAL":
        //            Log = LogLevel.Fatal;
        //            break;
        //        case "INFO":
        //            Log = LogLevel.Info;
        //            break;
        //        case "OFF":
        //            Log = LogLevel.Off;
        //            break;
        //        case "TRACE":
        //            Log = LogLevel.Trace;
        //            break;
        //        case "WARN":
        //            Log = LogLevel.Warn;
        //            break;
        //        default:
        //            Log = LogLevel.Off;
        //            break;
        //    }


        //    // Example usage
        //    logger = LogManager.GetLogger("[Default]");
        //}

        public static void Setting_Logger(string strFolderName, string strFileName, string strLv)
        {

            //public static readonly LogLevel Debug;
            //public static readonly LogLevel Error;
            //public static readonly LogLevel Fatal;
            //public static readonly LogLevel Info;
            //public static readonly LogLevel Off;
            //public static readonly LogLevel Trace;
            //public static readonly LogLevel Warn;
            vFileName = strFileName;
            vFolderName = strFolderName;

            switch (strLv.ToUpper())
            {
                case "DEBUG":
                    Log = LogLevel.Debug;
                    break;
                case "ERROR":
                    Log = LogLevel.Error;
                    break;
                case "FATAL":
                    Log = LogLevel.Fatal;
                    break;
                case "INFO":
                    Log = LogLevel.Info;
                    break;
                case "OFF":
                    Log = LogLevel.Off;
                    break;
                case "TRACE":
                    Log = LogLevel.Trace;
                    break;
                case "WARN":
                    Log = LogLevel.Warn;
                    break;
                default:
                    Log = LogLevel.Off;
                    break;
            }


            // Example usage
            logger = LogManager.GetLogger("[Default]");
        }

        public static void Debug(string strName, string strMsg)
        {
            // Step 1. Create configuration object
            LoggingConfiguration config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration

            //ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
            //config.AddTarget("console", consoleTarget);

            FileTarget fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            // Step 3. Set target properties

            //consoleTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";
            //fileTarget.FileName = "${basedir}/" + strName + " ${shortdate}.txt";
            //fileTarget.FileName = "${basedir}/" + vFileName + "_Debug ${shortdate}.txt"; ;
            if(vFolderName != "")
            {
                fileTarget.FileName = "C:\\inetpub\\wwwroot\\LOG_Ex\\" + vFolderName + "\\" + "${date:format=yyyy년}\\${date:format=MM월}\\${shortdate}\\" + vFileName + "_Debug ${shortdate}.txt";
            }
            else
            {
                fileTarget.FileName = "C:\\inetpub\\wwwroot\\LOG_Ex\\" + "${date:format=yyyy년}\\${date:format=MM월}\\${shortdate}\\" + vFileName + "_Debug ${shortdate}.txt";
            }
            
            //fileTarget.Layout = strLayout;
            fileTarget.Layout = "${longdate} ${logger} ${level} ${message}";

            // Step 4. Define rules

            //LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            //config.LoggingRules.Add(rule1);

            LoggingRule rule2 = new LoggingRule("*", Log, fileTarget);
            config.LoggingRules.Add(rule2);

            // Step 5. Activate the configuration

            LogManager.Configuration = config;

            logger = LogManager.GetLogger("[" + strName + "]");
            logger.Debug(strMsg);
        }

        public static void Error(string strName, string strMsg)
        {
            // Step 1. Create configuration object
            LoggingConfiguration config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration

            //ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
            //config.AddTarget("console", consoleTarget);

            FileTarget fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            // Step 3. Set target properties

            //consoleTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";
            //fileTarget.FileName = "${basedir}/" + strName + " ${shortdate}.txt";
            //fileTarget.FileName = "${basedir}/" + vFileName + "_Error ${shortdate}.txt"; ;
            if (vFolderName != "")
            {
                fileTarget.FileName = "C:\\inetpub\\wwwroot\\LOG_Ex\\" + vFolderName + "\\" + "${date:format=yyyy년}\\${date:format=MM월}\\${shortdate}\\" + vFileName + "_Error ${shortdate}.txt";
            }
            else
            {
                fileTarget.FileName = "C:\\inetpub\\wwwroot\\LOG_Ex\\" + "${date:format=yyyy년}\\${date:format=MM월}\\${shortdate}\\" + vFileName + "_Error ${shortdate}.txt";
            }
            //fileTarget.Layout = strLayout;
            fileTarget.Layout = "${longdate} ${logger} ${level} ${message}";

            // Step 4. Define rules

            //LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            //config.LoggingRules.Add(rule1);

            LoggingRule rule2 = new LoggingRule("*", Log, fileTarget);
            config.LoggingRules.Add(rule2);

            // Step 5. Activate the configuration

            LogManager.Configuration = config;

            logger = LogManager.GetLogger("[" + strName + "]");
            logger.Error(strMsg);
        }

        public static void Info(string strName, string strMsg)
        {
            // Step 1. Create configuration object
            LoggingConfiguration config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration

            //ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
            //config.AddTarget("console", consoleTarget);

            FileTarget fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            // Step 3. Set target properties

            //consoleTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";
            //fileTarget.FileName = "${basedir}/" + strName + " ${shortdate}.txt";
            //fileTarget.FileName = "${basedir}/" + vFileName + "_Info ${shortdate}.txt"; ;
            if (vFolderName != "")
            {
                fileTarget.FileName = "C:\\inetpub\\wwwroot\\LOG_Ex\\" + vFolderName + "\\" + "${date:format=yyyy년}\\${date:format=MM월}\\${shortdate}\\" + vFileName + "_Info ${shortdate}.txt";
            }
            else
            {
                fileTarget.FileName = "C:\\inetpub\\wwwroot\\LOG_Ex\\" + "${date:format=yyyy년}\\${date:format=MM월}\\${shortdate}\\" + vFileName + "_Info ${shortdate}.txt";
            }
            //fileTarget.Layout = strLayout;
            fileTarget.Layout = "${longdate} ${logger} ${level} ${message}";

            // Step 4. Define rules

            //LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            //config.LoggingRules.Add(rule1);

            LoggingRule rule2 = new LoggingRule("*", Log, fileTarget);
            config.LoggingRules.Add(rule2);

            // Step 5. Activate the configuration

            LogManager.Configuration = config;

            logger = LogManager.GetLogger("[" + strName + "]");
            logger.Info(strMsg);
        }
        public static void Fatal(string strName, string strMsg)
        {
            // Step 1. Create configuration object
            LoggingConfiguration config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration

            //ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
            //config.AddTarget("console", consoleTarget);

            FileTarget fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            // Step 3. Set target properties

            //consoleTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";
            //fileTarget.FileName = "${basedir}/" + strName + " ${shortdate}.txt";
            //fileTarget.FileName = "${basedir}/" + vFileName + "_Fatal ${shortdate}.txt"; ;
            if (vFolderName != "")
            {
                fileTarget.FileName = "C:\\inetpub\\wwwroot\\LOG_Ex\\" + vFolderName + "\\" + "${date:format=yyyy년}\\${date:format=MM월}\\${shortdate}\\" + vFileName + "_Fatal ${shortdate}.txt";
            }
            else
            {
                fileTarget.FileName = "C:\\inetpub\\wwwroot\\LOG_Ex\\" + "${date:format=yyyy년}\\${date:format=MM월}\\${shortdate}\\" + vFileName + "_Fatal ${shortdate}.txt";
            }
            //fileTarget.Layout = strLayout;
            fileTarget.Layout = "${longdate} ${logger} ${level} ${message}";

            // Step 4. Define rules

            //LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            //config.LoggingRules.Add(rule1);

            LoggingRule rule2 = new LoggingRule("*", Log, fileTarget);
            config.LoggingRules.Add(rule2);

            // Step 5. Activate the configuration

            LogManager.Configuration = config;

            logger = LogManager.GetLogger("[" + strName + "]");
            logger.Fatal(strMsg);
        }
        public static void Trace(string strName, string strMsg)
        {
            // Step 1. Create configuration object
            LoggingConfiguration config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration

            //ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
            //config.AddTarget("console", consoleTarget);

            FileTarget fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            // Step 3. Set target properties

            //consoleTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";
            //fileTarget.FileName = "${basedir}/" + strName + " ${shortdate}.txt";
            //fileTarget.FileName = "${basedir}/" + vFileName + "_Trace ${shortdate}.txt"; ;
            if (vFolderName != "")
            {
                fileTarget.FileName = "C:\\inetpub\\wwwroot\\LOG_Ex\\" + vFolderName + "\\" + "${date:format=yyyy년}\\${date:format=MM월}\\${shortdate}\\" + vFileName + "_Trace ${shortdate}.txt";
            }
            else
            {
                fileTarget.FileName = "C:\\inetpub\\wwwroot\\LOG_Ex\\" + "${date:format=yyyy년}\\${date:format=MM월}\\${shortdate}\\" + vFileName + "_Trace ${shortdate}.txt";
            }
            //fileTarget.Layout = strLayout;
            fileTarget.Layout = "${longdate} ${logger} ${level} ${message}";

            // Step 4. Define rules

            //LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            //config.LoggingRules.Add(rule1);

            LoggingRule rule2 = new LoggingRule("*", Log, fileTarget);
            config.LoggingRules.Add(rule2);

            // Step 5. Activate the configuration

            LogManager.Configuration = config;

            logger = LogManager.GetLogger("[" + strName + "]");
            logger.Trace(strMsg);
        }
        public static void Warn(string strName, string strMsg)
        {
            // Step 1. Create configuration object
            LoggingConfiguration config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration

            //ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
            //config.AddTarget("console", consoleTarget);

            FileTarget fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            // Step 3. Set target properties

            //consoleTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";
            //fileTarget.FileName = "${basedir}/" + strName + " ${shortdate}.txt";
            //fileTarget.FileName = "${basedir}/" + vFileName + "_Warn ${shortdate}.txt"; 
            if (vFolderName != "")
            {
                fileTarget.FileName = "C:\\inetpub\\wwwroot\\LOG_Ex\\" + vFolderName + "\\" + "${date:format=yyyy년}\\${date:format=MM월}\\${shortdate}\\" + vFileName + "_Warn ${shortdate}.txt";
            }
            else
            {
                fileTarget.FileName = "C:\\inetpub\\wwwroot\\LOG_Ex\\" + "${date:format=yyyy년}\\${date:format=MM월}\\${shortdate}\\" + vFileName + "_Warn ${shortdate}.txt";
            }
            //fileTarget.Layout = strLayout;
            fileTarget.Layout = "${longdate} ${logger} ${level} ${message}";

            // Step 4. Define rules

            //LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            //config.LoggingRules.Add(rule1);

            LoggingRule rule2 = new LoggingRule("*", Log, fileTarget);
            config.LoggingRules.Add(rule2);

            // Step 5. Activate the configuration

            LogManager.Configuration = config;

            logger = LogManager.GetLogger("[" + strName + "]");
            logger.Warn(strMsg);
        }
    }
}
