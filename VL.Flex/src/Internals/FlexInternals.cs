using System.Text.Json;
using System.Text.Json.Serialization;
using YogaSharp;

namespace VL.Flex.Internals
{

    internal class FlexInternals : IDisposable
    {
        public unsafe YGNode* _bulkNode = YGNode.New();
        /// <summary>
        /// Holds a bulk node, used in ResetStyle.
        /// </summary>
        public unsafe YGNode* BulkNode { get => _bulkNode; }

        /// <summary>
        /// Holds a json serializer used in debug nodes
        /// </summary>
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
