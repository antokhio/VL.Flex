using System.Text.Json;
using System.Text.Json.Serialization;
using YogaSharp;

namespace VL.Flex
{


    public class FlexInternals
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
    }

    public class FlexInternalsLocator
    {
        private static FlexInternals _internals = new FlexInternals();
        public static FlexInternals GetInternals()
        {
            if (_internals == null)
            {
                _internals = new FlexInternals();
            }
            return _internals;
        }
    }
}
