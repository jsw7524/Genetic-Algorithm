using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class DNA<T>
    {
        public T Gene;
        public decimal Fitness;
    }

    public abstract class GeneticAlgorithm<T>
    {
        protected int GenePoolSize;
        protected List<DNA<T>> GenePool;
        protected List<DNA<T>> tmp;
        protected Random rnd;
        protected abstract DNA<T> MakeChild(DNA<T> d1, DNA<T> d2);
        protected abstract void FitTest(DNA<T> Cell);
        protected abstract void InitializeDNA();
        protected abstract void Mutate(DNA<T> dna);

        public DNA<T> Get1stInGenePool()
        {
            return GenePool.First();
        }

        public GeneticAlgorithm(int size)
        {
            GenePoolSize = size;
            GenePool = new List<DNA<T>>();
            tmp = new List<DNA<T>>();
            rnd = new Random();
        }
        void Mating()
        {
            for (int i=0; i< GenePoolSize;i++)
            {
                DNA<T> d1=FretchDNA();
                DNA<T> d2 = FretchDNA();
                DNA<T> child = MakeChild(d1, d2);
                Mutate(child);
                FitTest(child);
                tmp.Add(child);
            }

        }
        protected DNA<T> FretchDNA()
        {
            decimal sumFitness =GenePool.Sum(d => d.Fitness);
            decimal target = Convert.ToDecimal((1.0/20.0)*rnd.NextDouble())* sumFitness;
            decimal counter = 0;
            foreach ( var g in GenePool)
            {
                counter += g.Fitness;
                if (counter >= target)
                {
                    return g;
                }
            }
            return null;
        }

        void Replace()
        {
            GenePool.Clear();
            GenePool.AddRange(tmp.OrderByDescending(d => d.Fitness));
            tmp.Clear();
        }
        public void solve(int generations)
        {
            InitializeDNA();
            Replace();
            for (int i = 0; i < generations; i++)
            {
                Mating();
                Replace();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
