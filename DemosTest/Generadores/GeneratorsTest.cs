namespace DemosTest.Generadores
{
    [TestClass]
    public class GeneratorsTest
    {
        private int StartedEnumerations = 0;
        private int CompletedEnumerations = 0;

        private IEnumerable<int> GetEvenNumbers_Generator(int max)
        {
            StartedEnumerations++;
            for (int i = 0; i < max; i++)
            {
                if (i % 2 == 0)
                    yield return i;
            }
            CompletedEnumerations++;
        }

        [TestMethod]
        public void Zero_enumerations()
        {
            // Sólo obtenemos el enumerable, pero no lo iteramos nunca
            var collection = GetEvenNumbers_Generator(1_000_000)
                .Where(e => e > 100); // una condición cualquiera

            Assert.AreEqual(expected: 0, actual: StartedEnumerations);
            Assert.AreEqual(expected: 0, actual: CompletedEnumerations);
        }

        [TestMethod]
        public void Multiple_enumerations()
        {
            // cada vez que se llama a Count(), se ejecuta GetEvenNumbers_Generator entero
            var collection = GetEvenNumbers_Generator(1_000_000);
            _ = collection.Count();
            _ = collection.Count();
            _ = collection.Count();
            _ = collection.Count();

            // éste es el caso que puede dar muchos problemas ⚠
            Assert.AreEqual(expected: 4, actual: StartedEnumerations);
            Assert.AreEqual(expected: 4, actual: CompletedEnumerations);
        }

        [TestMethod]
        public void One_enumeration()
        {
            // evaluamos la colección completa para tener la lista y trabajamos sobre ésta, no el generador
            var list = GetEvenNumbers_Generator(1_000_000).ToList();
            // aunque List tenga .Count, llamo a la función por mera simetría, pero sería lo mismo
            _ = list.Count(); 
            _ = list.Count();
            _ = list.Count();
            _ = list.Count();

            // esto evita el problema anterior
            Assert.AreEqual(expected: 1, actual: StartedEnumerations);
            Assert.AreEqual(expected: 1, actual: CompletedEnumerations);
        }

        [TestMethod]
        public void Incompleted_enumeration()
        {
            var collection = GetEvenNumbers_Generator(1_000_000);
            var element = collection.First();
            // la máquina de estados se ha quedado después del primer yield return, esperando a seguir
            // como no ha terminado, no se ha ejecutado el código después del bucle for
            // no ha hecho falta tener el millón de elementos en memoria

            Assert.AreEqual(expected: 1, actual: StartedEnumerations);
            Assert.AreEqual(expected: 0, actual: CompletedEnumerations);
        }

        [TestMethod]
        public void Incompleted_enumeration_foreach()
        {
            // en cada iteración se pide sólo el siguiente elemento de la colección
            foreach (var item in GetEvenNumbers_Generator(1_000_000))
            {
                // cualquier condición de parada como encontrar un elemento concreto, cumplir una condición, etc.
                if (item >= 100)
                    break;
            }
            // en este caso la máquina de estados se ha quedado después del elemento número 100, esperando a seguir
            // no ha hecho falta tener el millón de elementos en memoria

            Assert.AreEqual(expected: 1, actual: StartedEnumerations);
            Assert.AreEqual(expected: 0, actual: CompletedEnumerations);
        }
    }
}
