using System.Collections.Generic;

namespace ZTStudio
{
    /// <summary>
/// List class. Originally contained an additional BlnForceUpdateInfo Parameter on most main methods, but the function has been deprecated.
/// Since it may be useful however, it has not been removed as of now.
/// </summary>
/// <typeparam name="T"></typeparam>
    public class List<T> : System.Collections.Generic.List<T>
    {

        /// <summary>
    /// Implements a custom parameter. Deprecated?
    /// </summary>
    /// <param name="Item"></param>
    /// <param name="BlnForceUpdateInfo"></param>
        public void Add(T Item, bool BlnForceUpdateInfo = true)
        {
            base.Add(Item);
            if (BlnForceUpdateInfo == true)
            {
                // MdlZTStudioUI.UpdateInfo("List - item added. Overload.")
            }
        }

        /// <summary>
    /// Implements a custom parameter. Deprecated?
    /// </summary>
    /// <param name="Range"></param>
    /// <param name="BlnForceUpdateInfo"></param>
        public void AddRange(IEnumerable<T> Range, bool BlnForceUpdateInfo = true)
        {
            base.AddRange(Range);
            if (BlnForceUpdateInfo == true)
            {
                // MdlZTStudioUI.UpdateInfo("List - item range added. Overload.")
            }
        }

        /// <summary>
    /// Implements a custom parameter. Deprecated?
    /// </summary>
    /// <param name="Index"></param>
    /// <param name="Item"></param>
    /// <param name="BlnForceUpdateInfo"></param>
        public void Insert(int Index, T Item, bool BlnForceUpdateInfo = true)
        {
            base.Insert(Index, Item);
            if (BlnForceUpdateInfo == true)
            {
                // MdlZTStudioUI.UpdateInfo("List - item inserted. Overload.")
            }
        }

        public void Remove(T item, bool BlnForceUpdateInfo = true)
        {
            base.Remove(item);
            if (BlnForceUpdateInfo == true)
            {
                MdlZTStudioUI.UpdateFrameInfo("List - item added. Removed.");
            }
        }

        public void RemoveAt(int index, bool BlnForceUpdateInfo = true)
        {
            base.RemoveAt(index);
            if (BlnForceUpdateInfo == true)
            {
                MdlZTStudioUI.UpdateFrameInfo("List - item added. Removed at.");
            }
        }

        public void Clear(bool BlnForceUpdateInfo = true)
        {
            base.Clear();
            if (BlnForceUpdateInfo == true)
            {
                MdlZTStudioUI.UpdateFrameInfo("List - item added. Cleared.");
            }
        }

        /// <summary>
    /// Number of items to remove (from the start)
    /// </summary>
    /// <param name="IntItems">Number of items to remove (from the start)</param>
        public void Skip(int IntItems)
        {
            RemoveRange(0, IntItems);
        }
    }
}