using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace AdvancedTodoApp.Application.Common.Helpers
{
    public static class LoggingHelper
    {
        // List of property names that should be hidden/redacted due to sensitivity of data.
        private static readonly string[] SensitiveFields = { "Password" };

        public static string RedactSensitiveData(object request)
        {
            // jsonNode is a generic JSON element(object, array or value) that can be dynamically manpulated
            // Convert request into JsonNode so we can inspect and modify without knowing the exact type of original
            var jsonNode = JsonSerializer.SerializeToNode(request);

            if (jsonNode is JsonObject jsonObject)
            {
                foreach (var field in SensitiveFields)
                {
                    if (jsonObject.ContainsKey(field))
                    {
                        jsonObject[field] = "**REDACTED**";
                    }
                }
            }
            // Convert the modified JsonNode back to a JSON string for logging purposes.
            // Return empty JSON object if for some reason jsonNode is null.
            return jsonNode?.ToJsonString() ?? "{}";
        }
    }
}
