namespace DemosTest.Generadores
{
    [TestClass]
    public class GeneratorsTest
    {
        private int enumerations = 0;

        private IEnumerable<int> GetEvenNumbers_Generator(int max)
        {
            for (int i = 0; i < max; i++)
            {
                if (i % 2 == 0)
                    yield return i;
            }
            enumerations++;
        }

        [TestMethod]
        public void Zero_executions()
        {
            // Sólo obtenemos el enumerable, pero no lo iteramos nunca
            var collection = GetEvenNumbers_Generator(1_000_000);

            Assert.AreEqual(expected: 0, actual: enumerations);
        }

        [TestMethod]
        public void Multiple_executions()
        {
            // cada vez que se llama a GetEvenNumbers_Generator
            var collection = GetEvenNumbers_Generator(1_000_000);
            _ = collection.Count();
            _ = collection.Count();
            _ = collection.Count();
            _ = collection.Count();

            Assert.AreEqual(expected: 4, actual: enumerations);
        }

        [TestMethod]
        public void One_execution()
        {
            // evaluamos la colección completa para tener la lista y trabajamos sobre ésta, no el generador
            var list = GetEvenNumbers_Generator(1_000_000).ToList();
            // aunque List tenga .Count, llamo a la función por mera simetría, pero sería lo mismo
            _ = list.Count(); 
            _ = list.Count();
            _ = list.Count();
            _ = list.Count();

            Assert.AreEqual(expected: 1, actual: enumerations);
        }

        [TestMethod]
        public void Incompleted_execution()
        {
            var collection = GetEvenNumbers_Generator(1_000_000);
            var element = collection.First();
            // la máquina de estados se ha quedado después del primer yield return, esperando a seguir
            // como no ha terminado, no se ha ejecutado el código después del bucle for

            Assert.AreEqual(expected: 0, actual: enumerations);
        }
    }
}
