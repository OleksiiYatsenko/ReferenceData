using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualizationCollections;

namespace ReferenceData
{
    public class UserProvider : IItemsProvider<UserFullInfo>
    {
        private readonly int _count;
        private List<UserFullInfo> users;

        public UserProvider(int count)
        {
            _count = count;
            UserServiceWrapper usw = new UserServiceWrapper();
            users = new List<UserFullInfo>(usw.GetItems());
        }

        /// <summary>
        /// Fetches the total number of items available.
        /// </summary>
        /// <returns></returns>
        public int FetchCount()
        {
            //Trace.WriteLine("FetchCount");
            //Thread.Sleep(_fetchDelay);
            return _count;
        }

        /// <summary>
        /// Fetches a range of items.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The number of items to fetch.</param>
        /// <returns></returns>
        public IList<UserFullInfo> FetchRange(int startIndex, int count)
        {
            List<UserFullInfo> list = new List<UserFullInfo>();
            for (int i = startIndex; i < startIndex + count; i++)
            {
                list.Add(users[i]);
            }
            return list;
        }
    }
}
