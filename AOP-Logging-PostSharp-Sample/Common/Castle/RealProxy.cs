using Castle.DynamicProxy;

namespace AOP_Logging_PostSharp_Sample.Common.Castle
{
    public class LoggingInterceptor : IInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public LoggingInterceptor(ILogger<LoggingInterceptor> logger , IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Intercept(IInvocation invocation)
        {
            Serilog.Log.Information($"Calling method with Castle : {invocation.TargetType}.{invocation.Method.Name}.");
            invocation.Proceed();
        }
    }
}
