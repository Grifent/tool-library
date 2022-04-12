using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    public class MemberCollection : iMemberCollection
    {
        private BSTree memberTree; // Uses a binary search tree to store the members

        public MemberCollection()
        {
            memberTree = new BSTree(); // Initialise member tree
        }
        
        public int Number // get the number of members in the community library
        {
            get { return memberTree.Number; }
        }

        public void add(Member aMember) //add a new member to this member collection
        {
            memberTree.Insert(aMember); // Insert member into BSTree
        }

        public void delete(Member aMember) //delete a given member from this member collection
        {            
            memberTree.Delete(aMember); // Delete member from BSTree            
        }

        public Boolean search(Member aMember) //search a given member in this member collection. Return true if this memeber is in the member collection; return false otherwise.
        {
            return memberTree.Search(aMember);
        }

        public Member[] toArray() //output the members in this collection to an array of iMember
        {
            return memberTree.ToArray();
        }
    }
}
