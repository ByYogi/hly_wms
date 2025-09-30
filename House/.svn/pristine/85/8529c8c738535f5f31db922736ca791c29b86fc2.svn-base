using System;
using System.Linq;
using System.Text;

namespace House.Business.Log
{
    public static class BusStackTraceHelper
    {
        private static string GetFormattedStackTraceStr(Exception ex)
        {
            if (ex == null) return string.Empty;

            var result = new StringBuilder();
            var currentException = ex;
            var exceptionIndex = 1;

            while (currentException != null)
            {
                // 输出异常标题（对于内层异常）
                if (exceptionIndex > 1)
                {
                    result.AppendLine();
                    result.AppendLine("├─────────────────────────────────────────────");
                    result.AppendLine($"│ 🔗 内层异常 #{exceptionIndex - 1}");
                    result.AppendLine("├─────────────────────────────────────────────");
                }

                result.AppendLine($"异常类型: {currentException.GetType().Name}");
                result.AppendLine($"异常消息: {currentException.Message}");
                result.AppendLine("业务堆栈跟踪:");
                result.AppendLine("┌─────────────────────────────────────────────");

                var stackTrace = new System.Diagnostics.StackTrace(currentException, true);
                var frames = stackTrace.GetFrames() ?? Array.Empty<System.Diagnostics.StackFrame>();

                // 过滤掉System和Microsoft命名空间的框架，只保留业务代码
                var businessFrames = frames
                    .Where(frame =>
                    {
                        var namespaceName = frame.GetMethod()?.DeclaringType?.Namespace;
                        return namespaceName != null &&
                               !namespaceName.StartsWith("System.") &&
                               !namespaceName.StartsWith("Microsoft.") &&
                               !namespaceName.StartsWith("MS.") &&
                               !namespaceName.StartsWith("Internal.");
                    })
                    .Reverse() // 倒序排列
                    .ToList();

                if (businessFrames.Count == 0)
                {
                    // 如果没有业务相关的堆栈帧，显示所有堆栈帧（倒序）
                    var allFrames = frames.Reverse().Take(10).ToList(); // 限制显示数量
                    if (allFrames.Count == 0)
                    {
                        result.AppendLine("│ 无堆栈跟踪信息");
                    }
                    else
                    {
                        for (int i = 0; i < allFrames.Count; i++)
                        {
                            var frame = allFrames[i];
                            var method = frame.GetMethod();
                            var fileName = frame.GetFileName();
                            var lineNumber = frame.GetFileLineNumber();
                            var declaringType = method?.DeclaringType;
                            var parameters = method?.GetParameters();

                            var paramNames = parameters != null && parameters.Length > 0
                                ? string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}"))
                                : "";

                            result.AppendLine($"│ [{i + 1}] {declaringType?.FullName}.{method?.Name}({paramNames})");

                            if (!string.IsNullOrEmpty(fileName) && lineNumber > 0)
                            {
                                var shortFileName = System.IO.Path.GetFileName(fileName);
                                result.AppendLine($"│    文件: {shortFileName} 行号: {lineNumber}");
                            }

                            if (i < allFrames.Count - 1)
                            {
                                result.AppendLine("│");
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < businessFrames.Count; i++)
                    {
                        var frame = businessFrames[i];
                        var method = frame.GetMethod();
                        var fileName = frame.GetFileName();
                        var lineNumber = frame.GetFileLineNumber();
                        var declaringType = method?.DeclaringType;
                        var parameters = method?.GetParameters();

                        var paramNames = parameters != null && parameters.Length > 0
                            ? string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}"))
                            : "";

                        result.AppendLine($"│ [{i + 1}] {declaringType?.FullName}.{method?.Name}({paramNames})");

                        if (!string.IsNullOrEmpty(fileName) && lineNumber > 0)
                        {
                            var shortFileName = System.IO.Path.GetFileName(fileName);
                            result.AppendLine($"│    文件: {shortFileName} 行号: {lineNumber}");
                        }

                        if (i < businessFrames.Count - 1)
                        {
                            result.AppendLine("│");
                        }
                    }
                }

                result.AppendLine("└─────────────────────────────────────────────");

                // 处理下一个内层异常
                currentException = currentException.InnerException;
                exceptionIndex++;

                // 防止无限循环，最多处理10层内层异常
                if (exceptionIndex > 10)
                {
                    result.AppendLine();
                    result.AppendLine("  已到达内层异常显示上限（10层）");
                    break;
                }
            }

            return result.ToString();
        }


        // 扩展方法版本
        public static string FormatErr(this Exception ex)
        {
            try
            {
                return GetFormattedStackTraceStr(ex);
            }
            catch 
            {
                return ex.Message + "\n\t" + ex.StackTrace;
            }
        }
    }
}
