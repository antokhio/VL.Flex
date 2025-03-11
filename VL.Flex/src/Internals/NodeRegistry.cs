using YogaSharp;

namespace VL.Flex.Internals
{
    /// <summary>
    /// Internal registry, that used during native callbacks.
    /// </summary>
    internal unsafe class NodeRegistry
    {
        protected Dictionary<nint, WeakReference> _nodes = new();
        public void RegisterNode<TNode, TStyle>(FlexNodeBase<TNode, TStyle> node)
        {
            _nodes.Add((nint)node.Handle, new WeakReference(node, false));
        }

        public FlexNode? GetNode(YGNode* ptr)
        {
            if (_nodes.TryGetValue((nint)ptr, out var reference))
            {
                return reference.Target as FlexNode;
            }

            return null;
        }

        public void UnregisterNode(YGNode* ptr) => _nodes.Remove((nint)ptr);
    }
}
