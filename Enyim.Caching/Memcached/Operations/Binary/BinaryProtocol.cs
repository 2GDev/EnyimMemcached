using System;
using Enyim.Caching.Memcached;

namespace Enyim.Caching.Memcached.Operations.Binary
{
	/// <summary>
	/// Memcached client.
	/// </summary>
	internal class BinaryProtocol : IProtocolImplementation
	{
		private ServerPool pool;

		public BinaryProtocol(ServerPool pool)
		{
			this.pool = pool;
		}

		void IDisposable.Dispose()
		{
			this.Dispose();
		}

		/// <summary>
		/// Releases all resources allocated by this instance
		/// </summary>
		/// <remarks>Technically it's not really neccesary to call this, since the client does not create "really" disposable objects, so it's safe to assume that when 
		/// the AppPool shuts down all resources will be released correctly and no handles or such will remain in the memory.</remarks>
		public void Dispose()
		{
			if (this.pool == null)
				throw new ObjectDisposedException("MemcachedClient");

			((IDisposable)this.pool).Dispose();
			this.pool = null;
		}

		private bool TryGet(string key, out object value)
		{
			using (var g = new Memcached.Operations.Binary.GetOperation(this.pool, key))
			{
				var retval = g.Execute2();
				value = retval ? g.Result : null;

				return retval;
			}
		}

		bool IProtocolImplementation.Store(StoreMode mode, string key, object value, uint expires)
		{
			// TODO allow nulls?
			if (value == null) return false;

			using (var s = new Memcached.Operations.Binary.StoreOperation(pool, (StoreCommand)mode, key, value, expires))
			{
				return s.Execute2();
			}
		}

		bool IProtocolImplementation.TryGet(string key, out object value)
		{
			return TryGet(key, out value);
		}

		object IProtocolImplementation.Get(string key)
		{
			object retval;

			TryGet(key, out retval);

			return retval;
		}

		ulong IProtocolImplementation.Mutate(MutationMode mode, string key, ulong defaultValue, ulong delta, uint expiration)
		{
			using (var m = new Binary.MutatorOperation(this.pool, mode, key, defaultValue, delta, expiration))
			{
				return m.Execute2() ? m.Result : m.Result;
			}
		}

		bool IProtocolImplementation.Delete(string key)
		{
			using (var g = new Memcached.Operations.Binary.DeleteOperation(this.pool, key))
			{
				return g.Execute2();
			}
		}
	}
}

#region [ License information          ]
/* ************************************************************
 *
 * Copyright (c) Attila Kisk�, enyim.com
 *
 * This source code is subject to terms and conditions of 
 * Microsoft Permissive License (Ms-PL).
 * 
 * A copy of the license can be found in the License.html
 * file at the root of this distribution. If you can not 
 * locate the License, please send an email to a@enyim.com
 * 
 * By using this source code in any fashion, you are 
 * agreeing to be bound by the terms of the Microsoft 
 * Permissive License.
 *
 * You must not remove this notice, or any other, from this
 * software.
 *
 * ************************************************************/
#endregion