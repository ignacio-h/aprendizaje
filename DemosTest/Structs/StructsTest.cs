using Demos.Structs;

namespace DemosTest.Structs
{
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

        struct CounterStructMutable
        {
            public int Value;
            public void Increment() => Value++;
        }

        [TestMethod]
        public void Structs_mutability_list()
        {
            var c = new CounterStructMutable { Value = 5 };
            List<CounterStructMutable> lista = [ c ]; // se guarda una COPIA de c, no una referencia
            c.Increment();

            // Aquí está el bug, se incrementa UNA NUEVA COPIA, no el elemento de la lista
            lista[0].Increment();

            Assert.AreEqual(expected: 6, actual: c.Value);
            // Por eso sigue siendo 5
            Assert.AreEqual(expected: 5, actual: lista[0].Value);
        }

        [TestMethod]
        public void Structs_mutability_array()
        {
            var c = new CounterStructMutable { Value = 5 };
            // se vuelve a guardar una copia de c
            CounterStructMutable[] array = [c];
            c.Increment();

            // El get del indexador del array sí devuelve UNA REFERENCIA del elemento, no una copia
            array[0].Increment();

            Assert.AreEqual(expected: 6, actual: c.Value);
            // por eso ahora sí que vale 6
            Assert.AreEqual(expected: 6, actual: array[0].Value);
        }

        readonly record struct CounterStruct(int Value)
        {
            // en vez de devolver void y mutar el propio elemento, devuelve una copia con el valor incrementado
            public CounterStruct Increment() => new(Value + 1);
        }

        [TestMethod]
        public void Structs_NOT_mutability_list()
        {
            var c = new CounterStruct{ Value = 5 };
            List<CounterStruct> lista = [c]; // se guarda una COPIA de c, no una referencia
            c.Increment(); // no se guarda el resultado, así que c sigue siendo 5, usado así SEGUIRÍA SIENDO UN BUG

            // Ahora sí se actualiza el valor original con el resultado del incremento
            lista[0] = lista[0].Increment();

            Assert.AreEqual(expected: 5, actual: c.Value);
            Assert.AreEqual(expected: 6, actual: lista[0].Value);
        }

        [TestMethod]
        public void Structs_NOT_mutability_array()
        {
            var c = new CounterStruct { Value = 5 };
            // se vuelve a guardar una copia de c
            CounterStruct[] array = [c];
            c = c.Increment(); // ahora sí lo asignamos para actualizar

            // independientemente de que devuelva una referencia o una copia, el nuevo valor se asigna
            array[0] = array[0].Increment();

            Assert.AreEqual(expected: 6, actual: c.Value);
            Assert.AreEqual(expected: 6, actual: array[0].Value);
        }

        class CounterClass(int value)
        {
            public int Value { get; private set; } = value;

            public void Increment() => Value++;
        }

        [TestMethod]
        public void Class_mutability_list()
        {
            var c = new CounterClass(5);
            // se guarda una REFERENCIA a c, no una copia
            List<CounterClass> lista = [c];
            // ambos se aumentan
            c.Increment();

            // ambos se aumentan
            lista[0].Increment();

            // por eso ambos valen 7
            Assert.AreEqual(expected: 7, actual: c.Value);
            Assert.AreEqual(expected: 7, actual: lista[0].Value);
        }

        [TestMethod]
        public void Class_mutability_array()
        {
            // misma explicación que el test anterior
            var c = new CounterClass(5);
            CounterClass[] lista = [c];
            c.Increment();

            lista[0].Increment();

            Assert.AreEqual(expected: 7, actual: c.Value);
            Assert.AreEqual(expected: 7, actual: lista[0].Value);
        }
    }
}
