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
    public class ScriptController : ControllerBase
    {
        private REngineManager _rEngineManager;

        public ScriptController(REngineManager rEngineManager)
        {
            _rEngineManager = rEngineManager;
        }

        [HttpGet("{scriptType}")]
        public async Task<IActionResult> TrySomething(string scriptType)
        {
            try {
                var engine = _rEngineManager.Instance;
                
                var scriptName = "RTest_lm.R";
                string result = "EMPTY!";

                if ("lm".Equals(scriptType))
                {
                    scriptName = "RTest_lm.R";
                    GenericVector testResult = engine.Evaluate(@"source('" + scriptName + "')").AsList();

                    result = engine.GetSymbol("result").AsNumeric().First().ToString();

                }
                else if ("lme4".Equals(scriptType))
                {
                    scriptName = "RTest_lme4.R";

                    IntegerVector testResult = engine.Evaluate(@"source('" + scriptName + "')").AsInteger();

                   // GenericVector testResult = engine.Evaluate(@"source('" + scriptName + "')").AsList();
                    //var r = testResult[0].AsInteger();

                   // var r1 = testResult[0].AsDataFrame();
                   // var r2 = testResult[1].AsLogical();

                   // result = Util.TestDataFrame(r1);
                }
                else if ("glm".Equals(scriptType))
                {
                    scriptName = "RTest_glm.R";
                    GenericVector testResult = engine.Evaluate(@"source('" + scriptName + "')").AsList();
                    var r1 = testResult[0].AsCharacter();
                    result = Util.TestCharacterVector(r1);
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
