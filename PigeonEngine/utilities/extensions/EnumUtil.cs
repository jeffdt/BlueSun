using System;
using System.Collections.Generic;

namespace PigeonEngine.utilities.extensions {
	public static class EnumUtil {
		public static IEnumerable<T> GetValues<T>() {
			return (T[]) Enum.GetValues(typeof(T));
		}
	}
}
