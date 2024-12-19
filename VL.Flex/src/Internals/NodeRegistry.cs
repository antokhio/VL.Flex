using YogaSharp;

namespace VL.Flex.Internals
{
    /// <summary>
    /// Internal registry, that used during native callbacks.
    /// </summary>
    public unsafe class NodeRegistry
    {
        protected Dictionary<nint, WeakReference> _nodes = new();
        public void AddNode(FlexBase node)
        {
            _nodes.Add((nint)node.Handle, new WeakReference(node, false));
        }

        public FlexBase? GetNode(YGNode* ptr)
        {
            if (_nodes.TryGetValue((nint)ptr, out var reference))
            {
                return reference.Target as FlexBase;
            }

            return null;
        }

        public void RemoveNode(YGNode* ptr) => _nodes.Remove((nint)ptr);
    }
}
