using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using RDotNet;
using rnetpoc.Global;

namespace rnetpoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecController : ControllerBase
    {
        private REngineManager _rEngineManager;

        public ExecController(REngineManager rEngineManager)
        {
            _rEngineManager = rEngineManager;
        }

        [HttpGet("{scriptType}")]
        public async Task<IActionResult> TrySomething(string scriptType)
        {
            try {
                var engine = _rEngineManager.Instance;
                
                string result = "EMPTY!";

                if ("lm".Equals(scriptType))
                {
                    var inputfile = engine.CreateCharacter("Data/TestData_lm.csv");
                    engine.SetSymbol("inputfile", inputfile);
                    engine.Evaluate("testdata <- read.csv(inputfile, encoding=\"UTF - 8\")");
                    engine.Evaluate("Weight <- testdata$Weight");
                    engine.Evaluate("Height <- testdata$Height");
                    engine.Evaluate("relation <- lm(Weight~Height)");
                    engine.Evaluate("a <- data.frame(Height= 170)");
                    var r = engine.Evaluate("predict(relation,a)").AsNumeric();
                    result = r[0].ToString();
                }
                else if ("lme4".Equals(scriptType))
                {
                    var inputfile = engine.CreateCharacter("Data/TestData_lm.csv");
                    engine.SetSymbol("inputfile", inputfile);
                    engine.Evaluate("testdata <- read.csv(inputfile, encoding=\"UTF - 8\")");
                    engine.Evaluate("Weight <- testdata$Weight");
                    engine.Evaluate("Height <- testdata$Height");
                    engine.Evaluate("relation <- lm(Weight~Height)");
                    engine.Evaluate("a <- data.frame(Height= 170)");
                    var r = engine.Evaluate("predict(relation,a)").AsList();
                }
                else if ("glm".Equals(scriptType))
                {
                   
                }
                //else
                //{
                //    result = RScriptRunner.RunFromCmd(scriptName, @"C:\Program Files\R\R-4.0.4\bin\Rscript.exe", null);
                //}
                
                return Ok(result);

            } catch (Exception ex){
                Console.WriteLine(ex.ToString());
            }
            

            return StatusCode(500);
        }


    }
}
