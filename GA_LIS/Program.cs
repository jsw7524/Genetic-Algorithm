using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_LIS
{
    class LIS
    {
        public List<int> Subsequence = new List<int>();
    }

    class GA_LIS : GeneticAlgorithm<LIS>
    {
        int MaxLengthLIS;
        List<int> Sequence;
        public GA_LIS(List<int> s) :base(256)
        {
            Sequence = s;
            MaxLengthLIS = Sequence.Count();
        }

        protected override void FitTest(DNA<LIS> dna)
        {
            int counter = 0, PV = 0, maxRecord=0;
            for (int i = 0; i < MaxLengthLIS; i++)
            {
                if (dna.Gene.Subsequence[i] == 1)
                {
                    if (Sequence[i] > PV)
                    {
                        PV = Sequence[i];
                        counter++;
                    }
                    else
                    {
                        //if (counter > maxRecord)
                        //{
                        //    maxRecord = counter;
                        //}
                        counter = 0;
                        PV = 0;
                        break;
                    }

                }
            }
            dna.Fitness = counter*10; 
        }

        protected override void InitializeDNA()
        {
            for (int t = 0; t < GenePoolSize; t++)
            {
                DNA<LIS> dna = new DNA<LIS>();
                dna.Gene = new LIS();
                for (int r = 0; r < MaxLengthLIS; r++)
                {
                    dna.Gene.Subsequence.Add(rnd.Next() % 2);
                }
                FitTest(dna);
                GenePool.Add(dna);
            }
        }

        protected override DNA<LIS> MakeChild(DNA<LIS> d1, DNA<LIS> d2)
        {
            int pivot = rnd.Next() % MaxLengthLIS;
            DNA<LIS> dna = new DNA<LIS>();
            dna.Gene = new LIS();
            for (int i = 0; i < MaxLengthLIS; i++)
            {
                dna.Gene.Subsequence.Add(0);
                if (d1.Gene.Subsequence[i] == 1 && d2.Gene.Subsequence[i] == 1)
                {
                    dna.Gene.Subsequence[i] = 1;
                }
                else if (d1.Gene.Subsequence[i] == 0 && d2.Gene.Subsequence[i] == 0)
                {
                    dna.Gene.Subsequence[i] = 0;
                }
                else
                {
                    dna.Gene.Subsequence[i] = rnd.Next() % 2;
                }

            }

                //dna.Gene.Subsequence.AddRange(d1.Gene.Subsequence.GetRange(0, pivot));
                //dna.Gene.Subsequence.AddRange(d2.Gene.Subsequence.GetRange(pivot, MaxLengthLIS- pivot));
            FitTest(dna);
            return dna;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GA_LIS Question = new GA_LIS(new List<int>{ 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 });
            Question.solve(10);
            Console.ReadKey();
        }
    }
}
