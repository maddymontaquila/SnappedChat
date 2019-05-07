using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnappedChat.Models;

namespace SnappedChat.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Captain America", Description="Have you seen Bucky..." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Tony (Iron Man)", Description="Hey kid, got a minute?" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Dr. Banner", Description="I found these calming videos..." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Thor", Description="Stop! Hammer time!" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Groot", Description="I am groot. I am... groot!" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "King T'Challa", Description="We can still heal you..." }
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}