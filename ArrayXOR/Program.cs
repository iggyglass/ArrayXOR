using System;
using GenericTrie;

namespace ArrayXOR
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = GenArray(100000);

            Console.WriteLine("Linear:");
            Console.WriteLine(MaxXORLinear(array));

            Console.WriteLine("Polynomial:");
            Console.WriteLine(MaxXORPolynomial(array));
        }

        static int[] GenArray(int n)
        {
            int[] array = new int[n];
            Random rng = new Random();

            for (int i = 0; i < n; i++)
            {
                array[i] = rng.Next();
            }

            return array;
        }
        
        // Returns an array of ints where each is 0 or 1, representing the initial value's binary representation
        static int[] GetBitArray(int value)
        {
            int[] bits = new int[32];

            for (int i = bits.Length - 1; i > -1; i--)
            {
                bits[i] = value & 0x01;
                value >>= 1;
            }

            return bits;
        }

        static int MaxXORLinear(int[] array)
        {
            Trie<int, int> trie = new Trie<int, int>();
            int max = -1;

            // Build Trie
            for (int i = 0; i < array.Length; i++)
            {
                // Convert to binary array with length of 32 (because that's how big an int is)
                int[] number = GetBitArray(array[i]);

                trie.Insert(number, array[i]);
            }

            for (int i = 0; i < array.Length; i++)
            {
                TrieNode<int, int> current = trie.Root;

                // We want to find what is as close as possible to ~array[i]
                for (int j = 31; j > -1; j--)
                {
                    int mask = 1 << j;

                    if ((array[i] & mask) == 0)
                    {
                        current = current.Children.ContainsKey(1) ? current.Children[1] : current.Children[0];
                    }
                    else
                    {
                        current = current.Children.ContainsKey(0) ? current.Children[0] : current.Children[1];
                    }
                }

                // Check against maximum xor
                int xor = array[i] ^ current.Value;
                if (xor > max)
                {
                    max = xor;
                }
            }

            return max;
        }

        static int MaxXORPolynomial(int[] array)
        {
            int maxValue = -1;

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    int xor = array[i] ^ array[j];
                    if (xor > maxValue) maxValue = xor;
                }
            }

            return maxValue;
        }
    }
}
