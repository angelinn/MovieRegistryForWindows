using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OMDbSharp;

namespace MovieRegistry.Managers
{
    public class ImdbManager
    {
        public async Task<ItemList> GetByTitle(string title)
        {
            return await omdb.GetItemList(title);
        }

        public async Task<Item> GetById(string id)
        {
            return await omdb.GetItemByID(id);
        }

        private OMDbClient omdb = new OMDbClient(false);
    }
}
