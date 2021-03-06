using AOP_Logging_PostSharp_Sample.Common.Extensions;
using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System.Diagnostics;
using System.Web;

namespace AOP_Logging_PostSharp_Sample.Common.Aspects
{

    [PSerializable]
    public class Log : OnMethodBoundaryAspect
    {


        /// <summary>
        /// Method executed before the body of methods to which this aspect is applied.
        /// </summary>
        /// <param name="args"></param>
        public override void OnEntry(MethodExecutionArgs args)
        {
            args.MethodExecutionTag = Stopwatch.StartNew();
            var logDescription = $"{args.FullMethodName()} - Starting.";
            if (args.Arguments != null && args.Arguments.Count > 0)
            {
                var parameters = args?.Method?.GetParameters()?.ToDictionary(key => key.Name, value => args.Arguments[value.Position]);

                // Serialize to JSON (Newtonesoft lib)
                logDescription += $" PostSharp : args: {JsonConvert.SerializeObject(parameters)}";
            }
            Serilog.Log.Information(logDescription);
        }

        /// <summary>
        /// Method executed after the body of methods to which this aspect is applied, but
        /// only when the method successfully returns (i.e. when no exception flies out the method.).
        /// </summary>
        /// <param name="args"></param>
        public override void OnSuccess(MethodExecutionArgs args)
        {
            Serilog.Log.Information($" PostSharp : {args.FullMethodName()} - Succeeded.");
        }

        /// <summary>
        /// Method executed after the body of methods to which this aspect is applied, even
        /// when the method exists with an exception (this method is invoked from the finally block).
        /// </summary>
        /// <param name="args"></param>
        public override void OnExit(MethodExecutionArgs args)
        {
            var sw = (Stopwatch)args.MethodExecutionTag;
            sw.Stop();
            Serilog.Log.Information($" PostSharp : {args.FullMethodName()} - Elapsed Time : {sw.Elapsed.Milliseconds}  - Exited.");
        }

        /// <summary>
        /// Method executed after the body of methods to which this aspect is applied, in
        /// case that the method resulted with an exception.
        /// </summary>
        /// <param name="args"></param>
        public override void OnException(MethodExecutionArgs args)
        {
            var logDescription = $" PostSharp : {args.FullMethodName()} - Failed.";

            if (args.Exception != null)
            {
                logDescription += $" PostSharp :  message: {args.Exception.Message}";
            }

            Serilog.Log.Error(logDescription);
        }
    }


}



