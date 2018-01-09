using GeneticAlgorithm.Models;
using GeneticAlgorithm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneticAlgorithm.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            Log log_ = new Log();  
            
            List<string> ExampleValues = new List<string>();
            ExampleValues.Add("00000110");
            ExampleValues.Add("11101110");
            ExampleValues.Add("00100000");
            ExampleValues.Add("00110101");

            log_.log_time = DateTime.Now;
            log_.iteration = 0;
            log_.a = ExampleValues[0];
            log_.b = ExampleValues[1];
            log_.c = ExampleValues[2];
            log_.d = ExampleValues[3];

            Session["CurrentValues"] = ExampleValues;
            Session["isDone"] = false;

            return View(new Items { log_ = log_});
        }

        public ActionResult NewIteration(int iteration)
        {
            Log log_ = new Log();
            List<string> current_values = new List<string>();
            current_values = (List<string>)Session["CurrentValues"];
            Boolean isDone = (Boolean)Session["isDone"];

            current_values = GeneticAlgorithmFuncs.run(current_values);

            if(current_values[0] == "NotOk")
            {
                return Content("NotOk");
            }            
            else if (current_values[0] == "Done")
            {
                if (!isDone)
                {
                    log_.iteration = iteration;
                    Session["isDone"] = true;
                    return PartialView("_Iteration", new Items { log_ = log_, isDone = true });
                }
                else
                {
                    return Content("NotOk");
                }                
            }
            else
            {
                log_.iteration = iteration;
                log_.a = current_values[0];
                log_.b = current_values[1];
                log_.c = current_values[2];
                log_.d = current_values[3];
                log_.log_time = DateTime.Now;
                Session["CurrentValues"] = current_values;

                return PartialView("_Iteration", new Items { log_ = log_, isDone = false });
            }           
        }
    }
}
