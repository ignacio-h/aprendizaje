using BenchmarkDotNet.Attributes;

namespace Demos.Decimals
{
    [MemoryDiagnoser]
    //[RankColumn]
    public class DecimalsBase
    {
        private List<int> _data;

        [GlobalSetup]
        public void Setup()
        {
            _data = Enumerable.Range(1, 10_000).ToList();
        }

        [Benchmark(Baseline = true)]
        public int LinqSum() => _data.Sum();

        [Benchmark]
        public int ForLoopSum()
        {
            int sum = 0;
            foreach (var item in _data) sum += item;
            return sum;
        }
    }

    [MemoryDiagnoser]
    //[RankColumn]
    public class DecimalsParams
    {
        [Params(100, 10_000, 1_000_000)]
        public int N;

        private int[] _array;
        private HashSet<int> _hashSet;

        [GlobalSetup]
        public void Setup()
        {
            _array = [.. Enumerable.Range(0, N)];
            _hashSet = [.. _array];
        }

        [Benchmark(Baseline = true)]
        public bool ArrayContains() => _array.Contains(N / 2);

        [Benchmark]
        public bool HashSetContains() => _hashSet.Contains(N / 2);
    }

    [MemoryDiagnoser]
    //[RankColumn]
    public class DecimalsVsDouble
    {
        [Params(100, 10_000, 1_000_000)]
        public int N;

        [Benchmark(Baseline = true)]
        public double Double() 
        {
            double d = 1.0;
            for (int i = 0; i < N; i++)
            {
                d++;
            }

            return d;
        }

        [Benchmark]
        public decimal Decimal()
        {
            decimal d = 1.0m;
            for (int i = 0; i < N; i++)
            {
                d++;
            }

            return d;
        }
    }

    [MemoryDiagnoser]
    //[RankColumn]
    public class DecimalsVsDoubleList
    {
        [Params(100, 10_000, 1_000_000)]
        public int N;

        [Benchmark(Baseline = true)]
        public double[] Double()
        {
            double[] d = [.. Enumerable.Range(0, N)];

            return d;
        }

        [Benchmark]
        public decimal[] Decimal()
        {
            decimal[] d = [.. Enumerable.Range(0, N)];

            return d;
        }
    }
}
