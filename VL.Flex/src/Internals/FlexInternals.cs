using System.Text.Json;
using System.Text.Json.Serialization;
using YogaSharp;

namespace VL.Flex.Internals
{
    /// <summary>
    /// Holds a bulk node, used in reset style, json formatter to avoid recreation
    /// </summary>
    public class FlexInternals : IDisposable
    {
        public unsafe YGNode* _bulkNode = YGNode.New();
        public unsafe YGNode* BulkNode { get => _bulkNode; }

        public JsonSerializerOptions SerializerOptions
        {
            get
            {
                var opts = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
                };

                opts.Converters.Add(new JsonStringEnumConverter());

                return opts;
            }
        }

        public void Dispose()
        {
            unsafe
            {
                if (_bulkNode != null)
                {
                    _bulkNode->FinalizeNode();
                }
            }

            GC.SuppressFinalize(this);
        }
    }
}
