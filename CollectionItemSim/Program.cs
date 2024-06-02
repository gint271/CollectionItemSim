using CollectionItemSim.Models;
var random = new Random();

// Test average number of completed collection items with a limited suppply of kits if used randomly.
List<int> completedItemsPerTrial = new List<int>();

for (int i = 0; i < 1000; i++)
{
    int[] kitInventory = { 1000, 1000, 0 };
    int completedItems = 0;
    var collectionItem = new Item(Rarity.BLUE);

    while (kitInventory.Sum() > 0)
    {
        int pickedItem;

        // Change picked item assignment below to test different strategies.
        pickedItem = random.Next(0, 2);
        /*if (kitInventory[2] > 0)
        {
            pickedItem = 2;
        }
        else if (kitInventory[1] > 0)
        {
            pickedItem = 1;
        }
        else
        {
            pickedItem = 0;
        }*/

        kitInventory[pickedItem] -= 10;

        Rarity kitRarity = pickedItem switch
        {
            0 => Rarity.BLUE,
            1 => Rarity.PURPLE,
            2 => Rarity.YELLOW,
            _ => throw new InvalidOperationException("Invalid kit rarity")
        };

        collectionItem.UseMaintenanceKit(kitRarity);

        if (collectionItem.level == 15)
        {
            completedItems++;
            collectionItem = new Item(Rarity.BLUE);
        }
    }

    completedItemsPerTrial.Add(completedItems);
}

Console.WriteLine($"Average number of completed collection items: {completedItemsPerTrial.Average()}");