using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using RDotNet;

namespace rnetpoc
{
    public class REngineManager : IDisposable
    {
        private REngine rEngine = null;

        public REngineManager()
        {
            Initialize();
        }

        public REngine Instance
        {
            get
            {
                if (rEngine == null)
                {
                    Initialize();
                }
                rEngine.ClearGlobalEnvironment();
                return rEngine;
            }
        }

        private void Initialize()
        {
            REngine.SetEnvironmentVariables();
            rEngine = REngine.GetInstance();
            // REngine requires explicit initialization.
            // You can set some parameters.

            //RDotNet.StartupParameter param = new StartupParameter();
            //param.Interactive = false;
            //param.Quiet = true;
            rEngine.Initialize();
            setRworkdirectory();
        }

        private void setRworkdirectory()
        {
            var scriptFolder = AppContext.BaseDirectory + "Scripts";
            scriptFolder = scriptFolder.Replace(@"\", "/");
            string result = "DEFAULT";
            CharacterVector testResult = rEngine.Evaluate(@"getwd()").AsCharacter();
            result = testResult.First();
            Console.WriteLine("getwd========:" + result);

            rEngine.Evaluate("setwd(\"" + scriptFolder + "\")");
            testResult = rEngine.Evaluate(@"getwd()").AsCharacter();
            result = testResult.First();
            Console.WriteLine("getwd AGAIN========:" + result);
        }

        public void Dispose()
        {
            if (rEngine != null)
            {
                // you should always dispose of the REngine properly.
                // After disposing of the engine, you cannot reinitialize nor reuse it
                rEngine.Dispose();
            }
        }
    }
}