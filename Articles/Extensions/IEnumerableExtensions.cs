namespace Articles.Extensions
{
    public static class IEnumerableExtensions
    {
        static IEnumerable<int> GetRandomIndexes(int count)
        {
            List<int> randomIndexes = new();
            while (count != randomIndexes.Count)
            {
                int randomIndex = Random.Shared.Next(count);
                if (!randomIndexes.Contains(randomIndex))
                    randomIndexes.Add(randomIndex);
            }
            return randomIndexes;
        }

        static IEnumerable<T> GetCollectionByRandomIndexes<T>(IEnumerable<T> collection, IEnumerable<int> randomIndexes)
        {
            int collectionCount = collection.Count(), indexesCount = randomIndexes.Count();
            if (collectionCount != indexesCount)
                throw new IndexOutOfRangeException();

            var collectionList = collection.ToList();
            var randomIndexesList = randomIndexes.ToList();

            List<T> collectionByRandomIndexes = new();
            for (int i = 0; i < collectionCount; i++)
                collectionByRandomIndexes.Add(collectionList[randomIndexesList[i]]);

            return collectionByRandomIndexes;
        }

        public static IEnumerable<T> GetJumbledCollection<T>(this IEnumerable<T> collection)
        {
            if (collection.Any())
            {
                int count = collection.Count();
                IEnumerable<int> randomIndexes = GetRandomIndexes(count);
                return GetCollectionByRandomIndexes(collection, randomIndexes);
            }
            return collection;
        }
    }
}
