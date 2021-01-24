using System.Collections.Generic;

namespace GenericTrie
{
    public class Trie<TPiece, TValue>
    {

        public TrieNode<TPiece, TValue> Root { get; private set; }

        public Trie()
        {
            Root = new TrieNode<TPiece, TValue>(default);
        }

        public void Insert(TPiece[] pieces, TValue value)
        {
            TrieNode<TPiece, TValue> current = Root;

            for (int i = 0; i < pieces.Length; i++)
            {
                if (current.Children.ContainsKey(pieces[i]))
                {
                    current = current.Children[pieces[i]];
                }
                else
                {
                    TrieNode<TPiece, TValue> node = new TrieNode<TPiece, TValue>(pieces[i]);
                    current.Children.Add(pieces[i], node);

                    current = node;
                }
            }

            current.IsEnd = true;
            current.Value = value;
        }

        public bool Remove(TPiece[] pieces)
        {
            TrieNode<TPiece, TValue> current = Search(pieces);

            if (!current.IsEnd) return false;

            current.IsEnd = false;
            return true;
        }

        public TrieNode<TPiece, TValue> Search(TPiece[] prefix)
        {
            TrieNode<TPiece, TValue> current = Root;

            for (int i = 0; i < prefix.Length; i++)
            {
                if (current.Children.ContainsKey(prefix[i]))
                {
                    current = current.Children[prefix[i]];
                }
                else
                {
                    break;
                }
            }

            if (current == Root) return null;

            return current;
        }

        public List<TValue> GetAllMatchingValues(TPiece[] prefix)
        {
            List<TValue> values = new List<TValue>();
            TrieNode<TPiece, TValue> current = Root;

            for (int i = 0; i < prefix.Length; i++)
            {
                if (current.Children.ContainsKey(prefix[i]))
                {
                    current = current.Children[prefix[i]];
                }
                else
                {
                    return values;
                }
            }

            values.AddRange(getValues(current));

            return values;
        }

        public List<TValue> GetAllValues()
        {
            return getValues(Root);
        }

        private List<TValue> getValues(TrieNode<TPiece, TValue> start)
        {
            List<TValue> values = new List<TValue>();
            Queue<TrieNode<TPiece, TValue>> nodes = new Queue<TrieNode<TPiece, TValue>>();
            TrieNode<TPiece, TValue> current = start;

            while (nodes.Count > 0)
            {
                if (current.IsEnd)
                {
                    values.Add(current.Value);
                }

                foreach (var kvp in current.Children)
                {
                    nodes.Enqueue(kvp.Value);
                }

                current = nodes.Dequeue();
            }

            return values;
        }
    }
}
