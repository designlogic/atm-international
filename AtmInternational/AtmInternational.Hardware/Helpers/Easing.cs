using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmInternational.Hardware.Helpers
{

	/// <summary>
	/// https://gist.github.com/Fonserbc/3d31a25e87fdaa541ddf
	/// </summary>
	public class Easing
    {
		public delegate float EasingFunction(float k);

			public static float Linear(float k)
			{
				return k;
			}

			public class Quadratic
			{
				public static float In(float k)
				{
					return k * k;
				}

				public static float Out(float k)
				{
					return k * (2f - k);
				}

				public static float InOut(float k)
				{
					if ((k *= 2f) < 1f) return 0.5f * k * k;
					return -0.5f * ((k -= 1f) * (k - 2f) - 1f);
				}

				/* 
				 * Quadratic.Bezier(k,0) behaves like Quadratic.In(k)
				 * Quadratic.Bezier(k,1) behaves like Quadratic.Out(k)
				 *
				 * If you want to learn more check Alan Wolfe's post about it http://www.demofox.org/bezquad1d.html
				 */
				public static float Bezier(float k, float c)
				{
					return c * 2 * k * (1 - k) + k * k;
				}
			};

			public class Cubic
			{
				public static float In(float k)
				{
					return k * k * k;
				}

				public static float Out(float k)
				{
					return 1f + ((k -= 1f) * k * k);
				}

				public static float InOut(float k)
				{
					if ((k *= 2f) < 1f) return 0.5f * k * k * k;
					return 0.5f * ((k -= 2f) * k * k + 2f);
				}
			};

			public class Quartic
			{
				public static float In(float k)
				{
					return k * k * k * k;
				}

				public static float Out(float k)
				{
					return 1f - ((k -= 1f) * k * k * k);
				}

				public static float InOut(float k)
				{
					if ((k *= 2f) < 1f) return 0.5f * k * k * k * k;
					return -0.5f * ((k -= 2f) * k * k * k - 2f);
				}
			};

			public class Quintic
			{
				public static float In(float k)
				{
					return k * k * k * k * k;
				}

				public static float Out(float k)
				{
					return 1f + ((k -= 1f) * k * k * k * k);
				}

				public static float InOut(float k)
				{
					if ((k *= 2f) < 1f) return 0.5f * k * k * k * k * k;
					return 0.5f * ((k -= 2f) * k * k * k * k + 2f);
				}
			};

		

			public class Back
			{
				static float s = 1.70158f;
				static float s2 = 2.5949095f;

				public static float In(float k)
				{
					return k * k * ((s + 1f) * k - s);
				}

				public static float Out(float k)
				{
					return (k -= 1f) * k * ((s + 1f) * k + s) + 1f;
				}

				public static float InOut(float k)
				{
					if ((k *= 2f) < 1f) return 0.5f * (k * k * ((s2 + 1f) * k - s2));
					return 0.5f * ((k -= 2f) * k * ((s2 + 1f) * k + s2) + 2f);
				}
			};

			public class Bounce
			{
				public static float In(float k)
				{
					return 1f - Out(1f - k);
				}

				public static float Out(float k)
				{
					if (k < (1f / 2.75f))
					{
						return 7.5625f * k * k;
					}
					else if (k < (2f / 2.75f))
					{
						return 7.5625f * (k -= (1.5f / 2.75f)) * k + 0.75f;
					}
					else if (k < (2.5f / 2.75f))
					{
						return 7.5625f * (k -= (2.25f / 2.75f)) * k + 0.9375f;
					}
					else
					{
						return 7.5625f * (k -= (2.625f / 2.75f)) * k + 0.984375f;
					}
				}

				public static float InOut(float k)
				{
					if (k < 0.5f) return In(k * 2f) * 0.5f;
					return Out(k * 2f - 1f) * 0.5f + 0.5f;
				}
			};
		}
}
