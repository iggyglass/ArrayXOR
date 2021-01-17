using System;

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
        
        static int MaxXORLinear(int[] array)
        {
            Trie trie = new Trie();
            int max = -1;

            // Build Trie
            for (int i = 0; i < array.Length; i++)
            {
                // Convert to binary string with length of 32 (because that's how big an int is)
                string number = Convert.ToString(array[i], 2).PadLeft(32, '0');

                trie.Insert(number, array[i]);
            }

            for (int i = 0; i < array.Length; i++)
            {
                TrieNode current = trie.Root;

                // We want to find what is as close as possible to ~array[i]
                for (int j = 31; j > -1; j--)
                {
                    int mask = 1 << j;

                    if ((array[i] & mask) == 0)
                    {
                        current = current.Children.ContainsKey('1') ? current.Children['1'] : current.Children['0'];
                    }
                    else
                    {
                        current = current.Children.ContainsKey('0') ? current.Children['0'] : current.Children['1'];
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
