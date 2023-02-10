using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocearReminder
{
    public static class Extensions
    {
        public static void MoveUp(this TreeNode node)
        {
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index - 1, node);
                    view.SelectedNode = parent.Nodes[index - 1];
                }
            }
            else if (node.TreeView.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index > 0)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index - 1, node);
                    view.SelectedNode = view.Nodes[index - 1];
                }
            }
        }

        public static void MoveDown(this TreeNode node)
        {

            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index < parent.Nodes.Count - 1)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index + 1, node);
                    view.SelectedNode = parent.Nodes[index + 1];
                }
            }
            else if (view != null && view.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index < view.Nodes.Count - 1)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index + 1, node);
                    view.SelectedNode = view.Nodes[index + 1];
                }
            }
        }
        public static void MoveToFather(this TreeNode node)
        {
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null && parent.Parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                int fatherindex = parent.Parent.Nodes.IndexOf(parent);
                parent.Nodes.RemoveAt(index);
                parent.Parent.Nodes.Insert(fatherindex + 1, node);
                view.SelectedNode = parent.Parent.Nodes[fatherindex + 1];
            }
            else if (node.TreeView.Nodes.Contains(node.Parent)) //root node
            {
                int index = node.Parent.Nodes.IndexOf(node);
                int fatherindex = node.TreeView.Nodes.IndexOf(node.Parent);
                node.Parent.Nodes.RemoveAt(index);
                view.Nodes.Insert(fatherindex + 1, node);
                view.SelectedNode = view.Nodes[fatherindex + 1];
            }
        }

        public static void MoveToChildren(this TreeNode node)
        {

            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes[index - 1].Nodes.Insert(parent.Nodes[index - 1].Nodes.Count, node);
                    view.SelectedNode = parent.Nodes[index - 1].Nodes[parent.Nodes[index - 1].Nodes.Count - 1];
                }
            }
            else if (node.TreeView.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index > 0)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes[index - 1].Nodes.Insert(view.Nodes[index - 1].Nodes.Count, node);
                    view.SelectedNode = view.Nodes[index - 1].Nodes[view.Nodes[index - 1].Nodes.Count - 1];
                }
            }
        }
        public static XElement ToXELement(this XmlNode source)
        {
            return XElement.Parse(source.OuterXml);
        }
    }
}
