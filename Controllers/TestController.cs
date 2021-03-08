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

using System.Diagnostics;
using System.IO;

namespace rnetpoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private REngineManager _rEngineManager;

        public TestController(REngineManager rEngineManager)
        {
            _rEngineManager = rEngineManager;
        }

        [HttpGet("rscript")]
        public async Task<IActionResult> RunRScript()
        {
            string result = string.Empty;
            try
            {
                var info = new ProcessStartInfo();
                info.FileName = "/usr/lib/R/bin/Rscript";
                info.WorkingDirectory = Path.GetDirectoryName("/usr/lib/R/bin/Rscript");
                info.Arguments = "~/zcppgithub/rnetpoc/test.R";

                info.RedirectStandardInput = false;
                info.RedirectStandardOutput = true;
                info.UseShellExecute = false;
                info.CreateNoWindow = true;

                using (var proc = new Process())
                {
                    proc.StartInfo = info;
                    proc.Start();
                    result = proc.StandardOutput.ReadToEnd();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(new Exception("R Script failed: " + result, ex));
            }
        }

        [HttpGet("{someNumber}")]
        public async Task<IActionResult> TrySomething(string someNumber)
        {
            var engine = _rEngineManager.Instance;

            // .NET Framework array to R vector.
            NumericVector group1 = engine.CreateNumericVector(new double[] { 30.02, 29.99, 30.11, 29.97, 30.01, DateTime.Now.Second });
            engine.SetSymbol("group1", group1);
            // Direct parsing from R script.
            NumericVector group2 = engine.Evaluate("group2 <- c(29.89, 29.93, 29.72, 29.98, 30.02, 29.98, " + someNumber + ")").AsNumeric();
            // Test difference of mean and get the P-value.
            GenericVector testResult = engine.Evaluate("t.test(group1, group2)").AsList();
            double p = testResult["p.value"].AsNumeric().First();
            Console.WriteLine("Group1: [{0}]", string.Join(", ", group1));
            Console.WriteLine("Group2: [{0}]", string.Join(", ", group2));
            Console.WriteLine("P-value = {0:0.000}", p);

            return Ok(p);
        }
    }
}
