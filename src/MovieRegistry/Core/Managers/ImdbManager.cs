using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OMDbSharp;

namespace Core.Managers
{
    public class ImdbManager
    {
        public async Task<ItemList> GetByTitle(string title)
        {
            OMDbClient omdb = new OMDbClient(false);
            return await omdb.GetItemList(title);
        }
    }
}
