using PostSharp.Aspects;

namespace AOP_Logging_PostSharp_Sample.Common.Extensions
{
    public static class MethodExecutionArgsExtensions
    {
        public static string FullMethodName(this MethodExecutionArgs args) => $"{args.Method?.DeclaringType?.FullName}_{args.Method?.Name}";
    }
}
