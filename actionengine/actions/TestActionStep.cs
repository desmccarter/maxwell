using logging;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bddobjects;

namespace actionengine.actions
{
    public class TestActionStep
    {
        protected static Hashtable map = new Hashtable();

        /// <summary>
        /// Gets the internally mapped key for the given
        /// BDD parameter
        /// </summary>
        protected static string GetCacheKey(string bddParameter, int index)
        {
            return bddParameter.ToUpper().Trim() + "__" + index;
        }

        /// <summary>
        /// Returns true if BDD object exists on hashmap
        /// </summary>
        /// <param name="bddParameter"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected static bool CacheContainsBddParameter(string bddParameter, int index)
        {
            string cacheKey = GetCacheKey(bddParameter, index);

            return map.ContainsKey(cacheKey);
        }

        /// <summary>
        /// Get BDD object from hashmap 
        /// </summary>
        /// <param name="bddParameter"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected static IDomainObject GetCachedBddObject(string bddParameter, int index)
        {
            string cacheKey = GetCacheKey(bddParameter, index);

            IDomainObject bddObject = (IDomainObject)map[cacheKey];

            return bddObject;
        }

        protected static void CacheBddObject(string bddParameter, int index,
            IDomainObject bddObject)
        {
            string cacheKey = GetCacheKey(bddParameter, index);

            if (map.ContainsKey(cacheKey))
            {
                map.Remove(cacheKey);
            }

            map.Add(cacheKey, bddObject);
        }

        protected static void DeleteCachedBddObject(string bddParameter, int index)
        {
            string cacheKey = GetCacheKey(bddParameter, index);

            if (map.ContainsKey(cacheKey))
            {
                map.Remove(cacheKey);
            }
        }

        protected static void ClearCachedObjects()
        {
            map.Clear();
        }

        protected static IDomainObject TranslateBddParameterToBddObject(string bddParameter,
            int index)
        {
            IDomainObject bddObject = null;

            if (bddParameter == null)
            {
                return null;
            }

            // *** check to see whether the page exists
            // *** already within our static map ...

            if (CacheContainsBddParameter(bddParameter, index))
            {
                try
                {
                    // *** this object exists in the cache, so simply
                    // *** retrieve it ...

                    bddObject = GetCachedBddObject(bddParameter, index);
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to get page object using sentence '" + bddParameter +
                        "' from map: " + e.ToString());
                }
            }

            return bddObject;
        }

        protected static void AssertIsTrue(bool istrue, string message)
        {
            try
            {
                Assert.IsTrue(istrue, message);
            }
            catch (Exception e)
            {
                Log.Err(e.Message);

                throw e;
            }
        }

        protected static void AssertAreEqual(object expected, object actual, string message)
        {
            try
            {
                Assert.AreEqual(expected, actual, message);
            }
            catch (Exception e)
            {
                Log.Err(e.Message);

                throw e;
            }
        }

        protected static void AssertIsNotNull(object expected, string message)
        {
            try
            {
                Assert.IsNotNull(expected, message);
            }
            catch (Exception e)
            {
                Log.Err(e.Message);

                throw e;
            }
        }

        protected static void AssertFail(string message)
        {
            Log.Err(message);

            Assert.Fail(message);
        }
    }
}
