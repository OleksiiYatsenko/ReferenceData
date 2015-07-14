using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualizationCollections;
using Microsoft.Practices.Unity;
using System.Threading;
using System.Diagnostics;

namespace ReferenceData
{
    public class UserProvider : IItemsProvider<UserFullInfo>
    {
        private readonly int _count;
        private readonly int _fetchDelay;
        private static readonly List<UserFullInfo> users;

        static UserProvider()
        {
            UserServiceWrapper usw = App.Container.Resolve<UserServiceWrapper>();
            users = new List<UserFullInfo>(usw.GetItems());
        }

        public UserProvider()
        {
            //_count = count;

            _count = users.Count;
            _fetchDelay = 1000;
        }

        /// <summary>
        /// Fetches the total number of items available.
        /// </summary>
        /// <returns></returns>
        public int FetchCount()
        {
            Trace.WriteLine("FetchCount");
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
            Trace.WriteLine("FetchRange: " + startIndex + "," + count);
            //Thread.Sleep(_fetchDelay);
            List<UserFullInfo> list = new List<UserFullInfo>();

            int range = startIndex + count;

            for (int i = startIndex; i < range && i < users.Count; i++)
            {
                list.Add(users[i]);
            }
            return list;
        }
    }
}
