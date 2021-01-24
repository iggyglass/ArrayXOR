using System;
using System.Collections.Generic;
using System.Text;

namespace GenericTrie
{
    public class TrieNode<TPiece, TValue>
    {

        public TPiece Letter { get; private set; }
        public Dictionary<TPiece, TrieNode<TPiece, TValue>> Children { get; internal set; }
        public bool IsEnd { get; internal set; }
        public TValue Value { get; internal set; }

        public TrieNode(TPiece c)
        {
            Children = new Dictionary<TPiece, TrieNode<TPiece, TValue>>();
            Letter = c;
            IsEnd = false;
        }
    }
}
