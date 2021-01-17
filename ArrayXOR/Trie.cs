using System.Collections.Generic;

namespace ArrayXOR
{
    public class Trie
    {

        public TrieNode Root = new TrieNode('$');

        public void Insert(string word, int value)
        {
            TrieNode current = Root;

            for (int i = 0; i < word.Length; i++)
            {
                if (current.Children.ContainsKey(word[i]))
                {
                    current = current.Children[word[i]];
                }
                else
                {
                    TrieNode node = new TrieNode(word[i]);
                    current.Children.Add(word[i], node);

                    current = node;
                }
            }

            current.IsWord = true;
            current.Value = value;
        }

        public bool Remove(string word)
        {
            TrieNode current = Search(word);

            if (!current.IsWord) return false;

            current.IsWord = false;
            return true;
        }

        public TrieNode Search(string prefix)
        {
            TrieNode current = Root;

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

            return current;
        }

        public List<string> GetAllMatchingPrefix(string prefix)
        {
            List<string> words = new List<string>();
            TrieNode current = Root;

            for (int i = 0; i < prefix.Length; i++)
            {
                if (current.Children.ContainsKey(prefix[i]))
                {
                    current = current.Children[prefix[i]];
                }
                else
                {
                    return words;
                }
            }

            if (current.IsWord) words.Add(prefix);

            foreach (var node in current.Children)
            {
                words.AddRange(getWords(node.Value, prefix));
            }

            return words;
        }

        public List<string> GetAllWords()
        {
            List<string> words = new List<string>();

            foreach (var node in Root.Children)
            {
                words.AddRange(getWords(node.Value, ""));
            }

            return words;
        }

        private List<string> getWords(TrieNode node, string prefix)
        {
            List<string> words = new List<string>();

            prefix += node.Letter;
            if (node.IsWord) words.Add(prefix);

            foreach (var kvp in node.Children)
            {
                words.AddRange(getWords(kvp.Value, prefix));
            }

            return words;
        }
    }
}