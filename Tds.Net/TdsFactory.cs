using System;

namespace FreeTds
{
    internal class TdsFactory
    {
        readonly LazyFactory _lazyFactory;

        public TdsFactory(LazyFactory lazyFactory) => _lazyFactory = lazyFactory;

        //public Node CreateNode(GumboNode node, Node parent = null)
        //{
        //    switch (node.type)
        //    {
        //        case GumboNodeType.GUMBO_NODE_DOCUMENT: return new Document((GumboDocumentNode)node, this);
        //        case GumboNodeType.GUMBO_NODE_ELEMENT:
        //        case GumboNodeType.GUMBO_NODE_TEMPLATE: return new Element((GumboElementNode)node, parent, this);
        //        case GumboNodeType.GUMBO_NODE_TEXT:
        //        case GumboNodeType.GUMBO_NODE_CDATA:
        //        case GumboNodeType.GUMBO_NODE_COMMENT:
        //        case GumboNodeType.GUMBO_NODE_WHITESPACE: return new Text((GumboTextNode)node, parent);
        //        default: throw new NotImplementedException($"Node type '{node.type}' is not implemented");
        //    }
        //}

        //public Attribute CreateAttribute(GumboAttribute attribute, Element parent)
        //{
        //    var r = new Attribute(attribute, parent);
        //    if (string.Equals(r.Name, "id", StringComparison.OrdinalIgnoreCase))
        //        AddElementById(r.Value, parent);
        //    return r;
        //}

        public Lazy<T> CreateLazy<T>(Func<T> factoryMethod) => _lazyFactory.Create(factoryMethod);
    }
}
