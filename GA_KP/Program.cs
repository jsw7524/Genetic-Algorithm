using GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_KP
{
    public class Item
    {
        public string Name;
        public int weight;
        public int Value;
    }

    public class Knapsack
    {
        public List<Item> insideItems = new List<Item>();
    }

    public class GAKP : GeneticAlgorithm<Knapsack>
    {
        public int MaxWeight;
        public int numberItems;

        public List<Item> Items;
        public GAKP(List<Item> k, int limit) : base(10000)
        {
            MaxWeight = limit;
            numberItems = k.Count();
            Items = k;
        }

        protected override void FitTest(DNA<Knapsack> dna)
        {
            int totalValue = 0;
            int totalWeight = 0;
            foreach (Item n in dna.Gene.insideItems)
            {
                totalWeight += n.weight;
                if (totalWeight > MaxWeight)
                {
                    totalValue = 0;
                    break;
                }
                totalValue += n.Value;
            }
            dna.Fitness = totalValue;

        }

        protected override void InitializeDNA()
        {
            for (int t = 0; t < GenePoolSize; t++)
            {
                DNA<Knapsack> dna = new DNA<Knapsack>();
                dna.Gene = new Knapsack();
                for (int r = 0; r < numberItems; r++)
                {
                    if (rnd.Next() % 2 == 1)
                    {
                        dna.Gene.insideItems.Add(Items[r]);
                    }
                }
                FitTest(dna);
                tmp.Add(dna);
            }
        }

        protected override DNA<Knapsack> MakeChild(DNA<Knapsack> d1, DNA<Knapsack> d2)
        {
            DNA<Knapsack> dna = new DNA<Knapsack>();
            dna.Gene = new Knapsack();
            var dictD1 = d1.Gene.insideItems.ToDictionary(n => n.Name);

            foreach (Item n in d2.Gene.insideItems)
            {
                if (dictD1.ContainsKey(n.Name))
                {
                    dna.Gene.insideItems.Add(n);
                }
                else
                {
                    if (rnd.Next() % 2 == 1)
                    {
                        dna.Gene.insideItems.Add(n);
                    }
                }
            }
            FitTest(dna);
            return dna;
        }

        protected override void Mutate(DNA<Knapsack> dna)
        {
            if (rnd.Next() % 1000 == 6)
            {
                int i = rnd.Next() % numberItems;

                if (dna.Gene.insideItems.Contains(Items[i]))
                {
                    dna.Gene.insideItems.Remove(Items[i]);
                }
                else
                {
                    dna.Gene.insideItems.Append(Items[i]);
                }
            }
        }
    }

    class Program
    {


        //0	李子	4KG NT$4500
        //1	蘋果	5KG NT$5700
        //2	橘子	2KG NT$2250
        //3	草莓	1KG NT$1100
        //4	甜瓜	6KG NT$6700

        static void Main(string[] args)
        {
            //List<Item> items = new List<Item>{
            //    new Item() { Name="1",weight=23,Value=505},
            //    new Item() { Name="2",weight=26,Value=352},
            //    new Item() { Name="3",weight=20,Value=458},
            //    new Item() { Name="4",weight=18,Value=220},
            //    new Item() { Name="5",weight=32,Value=354},
            //    new Item() { Name="6",weight=27,Value=414},
            //    new Item() { Name="7",weight=29,Value=498},
            //    new Item() { Name="8",weight=26,Value=545},
            //    new Item() { Name="9",weight=30,Value=473},
            //    new Item() { Name="10",weight=27,Value=543},
            //};

            //GAKP kp = new GAKP(items, 67);
            //kp.solve(10);
            //Console.WriteLine(kp.Get1stInGenePool().Gene.insideItems.Sum(n => n.Value));
            //Console.ReadKey();

            List<Item> items = new List<Item>{
                new Item() { Name="1",weight=382745,Value= 825594},
                new Item() { Name="2",weight=799601,Value=1677009},
                new Item() { Name="3",weight=909247,Value=1676628},
                new Item() { Name="4",weight=729069,Value=1523970},
                new Item() { Name="5",weight=467902,Value=943972},
                new Item() { Name="6",weight=44328,Value=97426},
                new Item() { Name="7",weight=34610,Value=69666},
                new Item() { Name="8",weight=698150,Value=1296457},
                new Item() { Name="9",weight=823460,Value=1679693},
                new Item() { Name="10",weight=903959,Value=1902996},
                new Item() { Name="11",weight=853665,Value=1844992},
                new Item() { Name="12",weight=551830,Value=1049289},
                new Item() { Name="13",weight=610856,Value=1252836},
                new Item() { Name="14",weight=670702,Value=1319836},
                new Item() { Name="15",weight=488960,Value=953277},
                new Item() { Name="16",weight=951111,Value=2067538},
                new Item() { Name="17",weight=323046,Value=675367},
                new Item() { Name="18",weight=446298,Value=853655},
                new Item() { Name="19",weight=931161,Value=1826027},
                new Item() { Name="20",weight=31385,Value=65731},
                new Item() { Name="21",weight=496951,Value=901489},
                new Item() { Name="22",weight=264724,Value=577243},
                new Item() { Name="23",weight=224916,Value=466257},
                new Item() { Name="24",weight=169684,Value=369261},
            };

            GAKP kp = new GAKP(items, 6404180);
            kp.solve(10);
            Console.WriteLine(kp.Get1stInGenePool().Gene.insideItems.Sum(n => n.Value));
            Console.ReadKey();
            //optimal profit of 13549094
        }
    }
}
