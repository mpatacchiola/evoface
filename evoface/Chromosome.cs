using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace EvolutionaryFaceForm
{
    class Chromosome
    {

        public List<byte> nucleotideList = new List<byte>();

        public Chromosome()
        {

        }

        public Chromosome(int size)
        {
            for(int i =0; i< size; i++)
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                Byte[] b = new Byte[1];
                rnd.NextBytes(b);
                Byte myByte = b[0];
                nucleotideList.Add(myByte);
            }

        }


        /*
        // It returns a clone of the Chromosome
        //
        */
        public Chromosome GetClone()
        {
            Chromosome Clone = new Chromosome();
            ;
            for (int elemCounter = 0; elemCounter < nucleotideList.Count; elemCounter++)
            {
                Clone.nucleotideList.Add( nucleotideList[elemCounter] );
            }

            return Clone;
        }

        /*
        // It randomizes the Chromosome
        //
        */
        public void Randomize()
        {
            for (int i = 0; i < nucleotideList.Count; i++)
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                Byte[] b = new Byte[1];
                rnd.NextBytes(b);
                nucleotideList[i] = b[0];
            }
        }

        /*
        // It mutates the Chromosome
        //
        // @input probability is a double >=0 and <=100
        */
        public bool Mutate(double probability)
        {
            if (probability < 0) probability = 0;
            if (probability > 100) probability = 100;
            bool isMutated = false;
            for (int counter = 0; counter<nucleotideList.Count; counter++)
            {
                byte myByte = nucleotideList[counter];
                //var bits = new BitArray(myByte);
                var bits = new BitArray(new byte[] { myByte });
                //Cycle for change the single bit of the DNA
                for (int i = 0; i < bits.Length; i++)
                {
                    Random r = new Random(Guid.NewGuid().GetHashCode());
                    if (r.NextDouble() < (probability / 100))
                    {
                        bits[i] = !bits[i]; //the mutation switch the bit
                        isMutated = true;
                    }
                }
                //From BitArray to Byte[]
                byte[] ret = new byte[bits.Length / 8];
                bits.CopyTo(ret, 0);

                //Assigning the new Byte[] to the nucleotide
                nucleotideList[counter] = ret[0];
            }
                
            return isMutated;
        }

        /*
        // It Return a normalized value in a given range
        //
        // @input index is the position of the byte in the list
        */
        public int GetNucleotide(int index)
        {
            return nucleotideList[index];
        }

        /*
        // It Return a normalized value in a given range
        //
        // @input index is the position of the byte in the list
        // @input MinValue is the low boundary
        // @input MaxValue is the high boundary
        */
        public int GetNucleotide(int index, int MinValue, int MaxValue)
        {
            byte InputValue = nucleotideList[index];
            double IntFromByte = (double)InputValue;
            int OutputValue = (int)((IntFromByte / 255) * (MaxValue - MinValue)) + MinValue;
            return OutputValue;
        }


    }
}
