using Demos.Structs;

namespace DemosTest.Structs
{
    public struct CounterStruct
    {
        public int Value;
        public void Increment() => Value++;
    }

    public class CounterClass
    {
        public int Value;
        public void Increment() => Value++;
    }

    [TestClass]
    public sealed class StructsTest
    {
        [TestMethod]
        public void Structs_copy()
        {
            var a = new OrderId(42);
            var copyOfa = a;
            copyOfa = new OrderId(99);

            Assert.AreEqual(expected: 42, actual: a);
            Assert.AreEqual(expected: 99, actual: copyOfa);
        }

        [TestMethod]
        public void Structs_mutability_list()
        {
            var c = new CounterStruct { Value = 5 };
            List<CounterStruct> lista = [ c ];
            c.Increment();

            lista[0].Increment();

            Assert.AreEqual(expected: 6, actual: c.Value);
            Assert.AreEqual(expected: 5, actual: lista[0].Value);
        }

        [TestMethod]
        public void Structs_mutability_array()
        {
            var c = new CounterStruct { Value = 5 };
            CounterStruct[] array = [c];
            c.Increment();

            array[0].Increment();

            Assert.AreEqual(expected: 6, actual: c.Value);
            Assert.AreEqual(expected: 6, actual: array[0].Value);
        }

        [TestMethod]
        public void Class_mutability_list()
        {
            var c = new CounterClass { Value = 5 };
            List<CounterClass> lista = [c];
            c.Increment();

            lista[0].Increment();

            Assert.AreEqual(expected: 7, actual: c.Value);
            Assert.AreEqual(expected: 7, actual: lista[0].Value);
        }

        [TestMethod]
        public void Class_mutability_array()
        {
            var c = new CounterClass { Value = 5 };
            CounterClass[] lista = [c];
            c.Increment();

            lista[0].Increment();

            Assert.AreEqual(expected: 7, actual: c.Value);
            Assert.AreEqual(expected: 7, actual: lista[0].Value);
        }
    }
}
