using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{	public class BSTreeNode // Adapted from the Wk5 tute
	{
		private Member item; // value
		private BSTreeNode lchild; // reference to its left child 
		private BSTreeNode rchild; // reference to its right child

		public BSTreeNode(Member item)
		{
			this.item = item;
			lchild = null;
			rchild = null;
		}

		public Member Item
		{
			get { return item; }
			set { item = value; }
		}

		public BSTreeNode LChild
		{
			get { return lchild; }
			set { lchild = value; }
		}

		public BSTreeNode RChild
		{
			get { return rchild; }
			set { rchild = value; }
		}
	}

	public class BSTree // Adapted from Wk5 tute
    {
		private BSTreeNode root;
		private List<Member> treeList;

		public BSTree()
		{
			root = null;
			treeList = new List<Member>();
		}

		public int Number
        {
			get
			{
				InOrderTraverse();
				return treeList.Count;
			}
        }

		public bool IsEmpty()
		{
			return root == null;
		}

		public bool Search(Member item)
		{
			return Search(item, root);
		}

		private bool Search(Member item, BSTreeNode r)
		{
			if (r != null)
			{
				if (item.CompareTo(r.Item) == 0)
					return true;
				else
					if (item.CompareTo(r.Item) < 0)
					return Search(item, r.LChild);
				else
					return Search(item, r.RChild);
			}
			else
				return false;
		}

		public void Insert(Member item)
		{
			if (root == null)
				root = new BSTreeNode(item);
			else
				Insert(item, root);
		}

		// pre: ptr != null
		// post: item is inserted to the binary search tree rooted at ptr
		private void Insert(Member item, BSTreeNode ptr)
		{
			if (item.CompareTo(ptr.Item) < 0)
			{
				if (ptr.LChild == null)
					ptr.LChild = new BSTreeNode(item);
				else
					Insert(item, ptr.LChild);
			}
			else
			{
				if (ptr.RChild == null)
					ptr.RChild = new BSTreeNode(item);
				else
					Insert(item, ptr.RChild);
			}
		}

		// there are three cases to consider:
		// 1. the node to be deleted is a leaf
		// 2. the node to be deleted has only one child 
		// 3. the node to be deleted has both left and right children
		public void Delete(Member item)
		{
			// search for item and its parent
			BSTreeNode ptr = root; // search reference
			BSTreeNode parent = null; // parent of ptr
			while ((ptr != null) && (item.CompareTo(ptr.Item) != 0))
			{
				parent = ptr;
				if (item.CompareTo(ptr.Item) < 0) // move to the left child of ptr
					ptr = ptr.LChild;
				else
					ptr = ptr.RChild;
			}

			if (ptr != null) // if the search was successful
			{
				// case 3: item has two children
				if ((ptr.LChild != null) && (ptr.RChild != null))
				{
					// find the right-most node in left subtree of ptr
					if (ptr.LChild.RChild == null) // a special case: the right subtree of ptr.LChild is empty
					{
						ptr.Item = ptr.LChild.Item;
						ptr.LChild = ptr.LChild.LChild;
					}
					else
					{
						BSTreeNode p = ptr.LChild;
						BSTreeNode pp = ptr; // parent of p
						while (p.RChild != null)
						{
							pp = p;
							p = p.RChild;
						}
						// copy the item at p to ptr
						ptr.Item = p.Item;
						pp.RChild = p.LChild;
					}
				}
				else // cases 1 & 2: item has no or only one child
				{
					BSTreeNode c;
					if (ptr.LChild != null)
						c = ptr.LChild;
					else
						c = ptr.RChild;

					// remove node ptr
					if (ptr == root) //need to change root
						root = c;
					else
					{
						if (ptr == parent.LChild)
							parent.LChild = c;
						else
							parent.RChild = c;
					}
				}

			}
		}

		private void InOrderTraverse()
		{
			// Clear the list of members and begin traversal
			treeList.Clear();
			InOrderTraverse(root);
		}

		private void InOrderTraverse(BSTreeNode root)
		{
			if (root != null)
			{
				InOrderTraverse(root.LChild);
				treeList.Add(root.Item);
				InOrderTraverse(root.RChild);
			}
		}

		public Member[] ToArray()
        {
			// Run InOrderTraverse to get up to date array
			InOrderTraverse();
			return treeList.ToArray();
		}

		public void Clear()
		{
			root = null;
		}
	}
}
