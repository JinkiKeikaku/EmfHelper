using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmfHelper
{
    public static class EmfUtility
    {
        internal static T IntToEnum<T>(int intValue, T defaultValue) where T : Enum
        {
            return Enum.IsDefined(typeof(T), intValue) ? (T)Enum.ToObject(typeof(T), intValue) : defaultValue;
        }

        public static string PointsToString(IReadOnlyList<PointL> points, int maxPoints)
        {
            var sb = new StringBuilder();
            var n = points.Count < maxPoints ? points.Count : maxPoints;
            for (int i = 0; i < n; i++)
            {
                sb.Append($"({points[i]})");
            }
            if (points.Count >= maxPoints) sb.Append("…");
            return sb.ToString();
        }

        public static string PointsToString(IReadOnlyList<PointF> points, int maxPoints)
        {
            var sb = new StringBuilder();
            var n = points.Count < maxPoints ? points.Count : maxPoints;
            for (int i = 0; i < n; i++)
            {
                sb.Append($"({points[i]})");
            }
            if (points.Count >= maxPoints) sb.Append("…");
            return sb.ToString();
        }

        /// <summary>
        /// charガタの配列を文字列にする。
        /// new string(chars)の場合、charsに\0が含まれていた場合問題がある。
        /// char配列中に\0があればそれ以降を無視する。
        /// </summary>
        public static string CharsToString(char[] chars)
        {
            var sa = new string(chars).Split('\0');
            return sa.Length > 0 ? sa[0] : "";
        }

        /// <summary>
        /// 圧縮された相対座標（PointR）の配列をを復元する。
        /// 情報がないのでこれは暫定実装。この形式の座標列を見たことがないのでわからない。
        /// データを作っても上手く表示されないのでこの実装は間違っている。
        /// </summary>
        public static List<PointL> PointRListToPointLList(byte[] data, int numPoints)
        {
            var ret = new List<PointL>();
            if (numPoints == 0) return ret;
            var x = 0;
            var y = 0;
            var pos = 0;
            for (var i = 0; i < numPoints; i++)
            {
                byte dx = data[pos];
                if ((dx & 0x80) == 0)
                {
                    if ((dx & 0x40) != 0) dx |= 0x80;
                    x += dx;
                    pos++;
                }
                else
                {
                    byte[] tmp = new byte[] { data[pos + 1], (byte)(dx & 0x7f) };
                    if ((tmp[1] & 0x40) != 0) tmp[1] |= 0x80;
                    x += BitConverter.ToInt16(tmp, 0);
                    pos += 2;
                }
                byte dy = data[pos];
                if ((dy & 0x80) == 0)
                {
                    if ((dy & 0x40) != 0) dy |= 0x80;
                    y += dy;
                    pos++;
                }
                else
                {
                    byte[] tmp = new byte[] { data[pos + 1], (byte)(dy & 0x7f) };
                    if ((tmp[1] & 0x40) != 0) tmp[1] |= 0x80;
                    y += BitConverter.ToInt16(tmp, 0);
                    pos += 2;
                }
                ret.Add(new PointL(x, y));
            }
            return ret;

        }

    }
}
