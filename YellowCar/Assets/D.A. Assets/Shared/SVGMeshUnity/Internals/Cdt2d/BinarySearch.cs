﻿/*
The MIT License (MIT)

Copyright (c) 2013-2015 Mikola Lysenko

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;

namespace DA_Assets.SVGMeshUnity.Internals.Cdt2d
{
    public static class BinarySearch
    {
        // https://github.com/mikolalysenko/binary-search-bounds

        public interface IComparer<G, E>
        {
            int Compare(G x, E y);
        }

        public static int GE<G, E>(G[] a, E y, IComparer<G, E> c, int l, int h)
        {
            var i = h + 1;
            while (l <= h)
            {
                var m = (int) (uint) (l + h) >> 1;
                var x = a[m];
                if (c.Compare(x, y) >= 0)
                {
                    i = m;
                    h = m - 1;
                }
                else
                {
                    l = m + 1;
                }
            }

            return i;
        }

        public static int GE<G>(G[] a, G y, int l, int h) where G : IComparable<G>
        {
            var i = h + 1;
            while (l <= h)
            {
                var m = (int) (uint) (l + h) >> 1;
                var x = a[m];
                if (x.CompareTo(y) >= 0)
                {
                    i = m;
                    h = m - 1;
                }
                else
                {
                    l = m + 1;
                }
            }

            return i;
        }

        public static int GT<G, E>(G[] a, E y, IComparer<G, E> c, int l, int h)
        {
            var i = h + 1;
            while (l <= h)
            {
                var m = (int) (uint) (l + h) >> 1;
                var x = a[m];
                if (c.Compare(x, y) > 0)
                {
                    i = m;
                    h = m - 1;
                }
                else
                {
                    l = m + 1;
                }
            }

            return i;
        }

        public static int GT<G>(G[] a, G y, int l, int h) where G : IComparable<G>
        {
            var i = h + 1;
            while (l <= h)
            {
                var m = (int) (uint) (l + h) >> 1;
                var x = a[m];
                if (x.CompareTo(y) > 0)
                {
                    i = m;
                    h = m - 1;
                }
                else
                {
                    l = m + 1;
                }
            }

            return i;
        }

        public static int LT<G, E>(G[] a, E y, IComparer<G, E> c, int l, int h)
        {
            var i = l - 1;
            while (l <= h)
            {
                var m = (int) (uint) (l + h) >> 1;
                var x = a[m];
                if (c.Compare(x, y) < 0)
                {
                    i = m;
                    l = m + 1;
                }
                else
                {
                    h = m - 1;
                }
            }

            return i;
        }

        public static int LT<G>(G[] a, G y, int l, int h) where G : IComparable<G>
        {
            var i = l - 1;
            while (l <= h)
            {
                var m = (int) (uint) (l + h) >> 1;
                var x = a[m];
                if (x.CompareTo(y) < 0)
                {
                    i = m;
                    l = m + 1;
                }
                else
                {
                    h = m - 1;
                }
            }

            return i;
        }

        public static int LE<G, E>(G[] a, E y, IComparer<G, E> c, int l, int h)
        {
            var i = l - 1;
            while (l <= h)
            {
                var m = (int) (uint) (l + h) >> 1;
                var x = a[m];
                if (c.Compare(x, y) <= 0)
                {
                    i = m;
                    l = m + 1;
                }
                else
                {
                    h = m - 1;
                }
            }

            return i;
        }

        public static int LE<G>(G[] a, G y, int l, int h) where G : IComparable<G>
        {
            var i = l - 1;
            while (l <= h)
            {
                var m = (int) (uint) (l + h) >> 1;
                var x = a[m];
                if (x.CompareTo(y) <= 0)
                {
                    i = m;
                    l = m + 1;
                }
                else
                {
                    h = m - 1;
                }
            }

            return i;
        }

        public static int EQ<G, E>(G[] a, E y, IComparer<G, E> c, int l, int h)
        {
            while (l <= h)
            {
                var m = (int) (uint) (l + h) >> 1;
                var x = a[m];
                var p = c.Compare(x, y);
                if (p == 0)
                {
                    return m;
                }

                if (p <= 0)
                {
                    l = m + 1;
                }
                else
                {
                    h = m - 1;
                }
            }

            return -1;
        }

        public static int EQ<G>(G[] a, G y, int l, int h) where G : IComparable<G>
        {
            while (l <= h)
            {
                var m = (int) (uint) (l + h) >> 1;
                var x = a[m];
                var p = x.CompareTo(y);
                if (p == 0)
                {
                    return m;
                }

                if (p <= 0)
                {
                    l = m + 1;
                }
                else
                {
                    h = m - 1;
                }
            }

            return -1;
        }
    }
}