using Quiz.Domain.Models.Base;

namespace Quiz.Domain.Helpers;

public static class OrderingHelpers
{
    public static int GetMaxOrderNumber<TOrderable>(ICollection<TOrderable> collection)
        where TOrderable : OrderableEntity
    {
        return collection.Any() ? collection.Max(x => x.OrderNumber) : 0;
    }

    public static ICollection<TOrderable> ReOrderCollection<TOrderable>(IEnumerable<TOrderable> collection)
        where TOrderable : OrderableEntity
    {
        var fixedCollection = collection.ToList();
        var maxOrderNumber = GetMaxOrderNumber(fixedCollection);
        var availableOrderNumbers = new List<int>();
        for (var orderNumber = 1; orderNumber < maxOrderNumber; orderNumber++)
        {
            var question = fixedCollection.FirstOrDefault(x => x.OrderNumber == orderNumber);
            if (question is null)
            {
                availableOrderNumbers.Add(orderNumber);
            }
            else
            {
                if (!availableOrderNumbers.Any())
                {
                    continue;
                }

                AssignMinimalAvailableOrderNumber(availableOrderNumbers, question);
            }
        }

        return fixedCollection;
    }

    private static void AssignMinimalAvailableOrderNumber<TOrderable>(ICollection<int> availableOrderNumbers, TOrderable question)
        where TOrderable : OrderableEntity
    {
        var minimalAvailableOrderNumber = availableOrderNumbers.Min(x => x);
        if (question.OrderNumber > minimalAvailableOrderNumber)
        {
            question.OrderNumber = minimalAvailableOrderNumber;
            availableOrderNumbers.Remove(minimalAvailableOrderNumber);
        }
    }
}