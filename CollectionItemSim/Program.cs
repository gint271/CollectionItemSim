using CollectionItemSim.Models;

var collectionItem = new Item(Rarity.BLUE);

while (collectionItem.level < 15)
{
    collectionItem.UseMaintenanceKit(Rarity.YELLOW);
}