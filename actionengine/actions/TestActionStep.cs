using logging;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.org.hs2.shareddomainobjects;

namespace actionengine.actions
{
    public class TestActionStep : ITestObject
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
        /// <param name="objectName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected static bool CacheContainsObject(string objectName, int index)
        {
            string cacheKey = GetCacheKey(objectName, index);

            return map.ContainsKey(cacheKey);
        }

        /// <summary>
        /// Get BDD object from hashmap 
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected static IDomainObject GetCachedObject(string objectName, int index)
        {
            string cacheKey = GetCacheKey(objectName, index);

            IDomainObject bddObject = (IDomainObject)map[cacheKey];

            return bddObject;
        }

        protected static void CacheObject(string objectName, int index,
            IDomainObject bddObject)
        {
            string cacheKey = GetCacheKey(objectName, index);

            if (map.ContainsKey(cacheKey))
            {
                map.Remove(cacheKey);
            }

            map.Add(cacheKey, bddObject);
        }

        protected static void DeleteCachedObject(string objectName, int index)
        {
            string cacheKey = GetCacheKey(objectName, index);

            if (map.ContainsKey(cacheKey))
            {
                map.Remove(cacheKey);
            }
        }

        protected static void ClearCachedObjects()
        {
            map.Clear();
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
