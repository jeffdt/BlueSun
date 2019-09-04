﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigeonEngine.utilities.extensions {
	public static class DoubleExtensions {
		public static double Clamp(this double value, double min, double max ) {
			if (value < min) {
				return min;
			}

			if (value > max) {
				return max;
			}

			return value;
		}
	}
}
