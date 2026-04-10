using BenchmarkDotNet.Attributes;

namespace Demos.Generadores
{
    [MemoryDiagnoser]
    public class Generadores
    {
        private List<int> GetEvenNumbers_Allocated(int max)
        {
            var result = new List<int>();

            for (int i = 0; i < max; i++)
            {
                if (i % 2 == 0)
                    result.Add(i);
            }

            return result;
        }

        private IEnumerable<int> GetEvenNumbers_Generator(int max)
        {
            for (int i = 0; i < max; i++)
            {
                if (i % 2 == 0)
                    yield return i;
            }
        }

        /*
        | Method     | Mean     | Error     | StdDev    | Ratio | Gen0     | Gen1     | Gen2     | Allocated | Alloc Ratio |
        |----------- |---------:|----------:|----------:|------:|---------:|---------:|---------:|----------:|------------:|
        | Allocation | 2.741 ms | 0.0179 ms | 0.0167 ms |  1.00 | 996.0938 | 996.0938 | 996.0938 | 4195087 B |        1.00 |
        | Generator  | 1.535 ms | 0.0051 ms | 0.0048 ms |  0.56 |        - |        - |        - |         - |        0.00 |
         */
        [Benchmark(Baseline = true)]
        public int Allocation()
        {
            return GetEvenNumbers_Allocated(1_000_000).Count;
        }

        [Benchmark]
        public int Generator()
        {
            return GetEvenNumbers_Generator(1_000_000).Count();
        }
    }
}
