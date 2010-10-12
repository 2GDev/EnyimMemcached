using Enyim.Caching;
using NUnit.Framework;

namespace MemcachedTest
{
	/// <summary>
	///This is a test class for Enyim.Caching.MemcachedClient and is intended
	///to contain all Enyim.Caching.MemcachedClient Unit Tests
	///</summary>
	[TestFixture]
	public class NorthScaleMembaseClientTest : BinaryMemcachedClientTest
	{
		protected override MemcachedClient GetClient()
		{
			var client = new Membase.MembaseClient("test/membase", null);
			client.FlushAll();

			return client;
		}

		[TestFixtureSetUp]
		public void Init()
		{
			log4net.Config.XmlConfigurator.Configure();
		}

		[TestCase]
		public override void MultiGetTest()
		{
			base.MultiGetTest();
		}
	}
}

#region [ License information          ]
/* ************************************************************
 * 
 *    Copyright (c) 2010 Attila Kisk�, enyim.com
 *    
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *    
 *        http://www.apache.org/licenses/LICENSE-2.0
 *    
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *    
 * ************************************************************/
#endregion
