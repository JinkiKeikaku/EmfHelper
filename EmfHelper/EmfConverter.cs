using System;

namespace EmfHelper
{
    public static class EmfConverter
    {
        public static PointS ToPointS(byte[] data, int startOffset)
            => new(BitConverter.ToInt16(data, startOffset), BitConverter.ToInt16(data, startOffset + 2));

        public static PointL ToPointL(byte[] data, int startOffset, bool isCompressed=false)
        {
            if (isCompressed)
            {
                return new(BitConverter.ToInt16(data, startOffset), BitConverter.ToInt16(data, startOffset + 2));
            }
            else
            {
                return new(BitConverter.ToInt32(data, startOffset), BitConverter.ToInt32(data, startOffset + 4));
            }
        }

        public static PointF ToPointF(byte[] data, int startOffset, bool isCompressed)
        {
            if (isCompressed)
            {
                return new(BitConverter.ToInt16(data, startOffset), BitConverter.ToInt16(data, startOffset + 2));
            }
            else
            {
                return new(BitConverter.ToSingle(data, startOffset), BitConverter.ToSingle(data, startOffset + 4));
            }
        }

        public static SizeL ToSizeL(byte[] data, int startOffset)
            => new(BitConverter.ToInt32(data, startOffset), BitConverter.ToInt32(data, startOffset + 4));

        public static RectL ToRectL(byte[] data, int startOffset)
        {
            return new RectL(
                BitConverter.ToInt32(data, startOffset), BitConverter.ToInt32(data, startOffset + 4),
                BitConverter.ToInt32(data, startOffset + 8), BitConverter.ToInt32(data, startOffset + 12)
            );
        }
/*
        public static RectF ToRectF(byte[] data, int startOffset)
        {
            return new RectF(
                BitConverter.ToSingle(data, startOffset),
                BitConverter.ToSingle(data, startOffset + 4),
                BitConverter.ToSingle(data, startOffset + 8),
                BitConverter.ToSingle(data, startOffset + 12)
            );
        }
*/
        public static XForm ToXForm(byte[] data, int startOffset)
        {
            XForm matrix = new XForm();
            matrix.M11 = BitConverter.ToSingle(data, startOffset);
            matrix.M12 = BitConverter.ToSingle(data, startOffset + 4);
            matrix.M21 = BitConverter.ToSingle(data, startOffset + 8);
            matrix.M22 = BitConverter.ToSingle(data, startOffset + 12);
            matrix.Dx = BitConverter.ToSingle(data, startOffset + 16);
            matrix.Dy = BitConverter.ToSingle(data, startOffset + 20);
            return matrix;
        }

        public static ColorRef ToColorRef(byte[] data, int startOffset)
        {
            ColorRef colorRef = new ColorRef();
            colorRef.Red = data[startOffset];
            colorRef.Green = data[startOffset + 1];
            colorRef.Blue = data[startOffset + 2];
            colorRef.Reserved = data[startOffset + 3];
            return colorRef;
        }


        public static LogBrushEx ToLogBrush(byte[] data, int startOffset)
        {
            LogBrushEx logBrush = new LogBrushEx();
            logBrush.Style = EmfUtility.IntToEnum(BitConverter.ToInt32(data, startOffset), BrushStyle.UNKNOWN);
            logBrush.ColorRef = ToColorRef(data, startOffset + 4);
            logBrush.Hatch = BitConverter.ToUInt32(data, startOffset + 8);
            return logBrush;
        }

        public static LogFont ToLogFont(byte[] data, int startOffset)
        {
            LogFont logFont = new LogFont();
            logFont.Height = BitConverter.ToInt32(data, startOffset);
            logFont.Width = BitConverter.ToInt32(data, startOffset + 4);
            logFont.Escapement = BitConverter.ToInt32(data, startOffset + 8);
            logFont.Orientation = BitConverter.ToInt32(data, startOffset + 12);
            logFont.Weight = BitConverter.ToInt32(data, startOffset + 16);
            logFont.Italic = data[startOffset + 20];
            logFont.Underline = data[startOffset + 21];
            logFont.StrikeOut = data[startOffset + 22];
            logFont.CharSet = EmfUtility.IntToEnum(data[startOffset + 23], CharacterSet.UNKNOWN);
            logFont.OutPrecision = data[startOffset + 24];
            logFont.ClipPrecision = data[startOffset + 25];
            logFont.Quality = data[startOffset + 26];
            logFont.PitchAndFamily = data[startOffset + 27];
            logFont.Facename = ToCharArray(data, startOffset + 28, 32);// new char[32];
            //for (int i = 0; i < 32; i++)
            //    logFont.Facename[i] = BitConverter.ToChar(data, startOffset + 28 + i * 2);
            return logFont;
        }

        public static char[] ToCharArray(byte[] data, int startOffset, int count)
        {
            var ret = new char[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = BitConverter.ToChar(data, startOffset + i * 2);
            }
            return ret;
        }

        public static uint[] ToUInt32Array(byte[] data, int startOffset, int count)
        {
            var a = new uint[count];
            var pos = startOffset;
            for (var i = 0; i < count; i++)
            {
                a[i] = BitConverter.ToUInt32(data, pos);
                pos += 4;
            }
            return a;
        }

        public static float[] ToSingleArray(byte[] data, int startOffset, int count)
        {
            var a = new float[count];
            var pos = startOffset;
            for (var i = 0; i < count; i++)
            {
                a[i] = BitConverter.ToSingle(data, pos);
                pos += 4;
            }
            return a;
        }

        public static PointS[] ToPointSArray(byte[] data, int startOffset, int count)
        {
            var a = new PointS[count];
            var pos = startOffset;
            for (var i = 0; i < count; i++)
            {
                a[i] = ToPointS(data, pos);
                pos += 4;
            }
            return a;
        }

        public static PointL[] ToPointLArray(byte[] data, int startOffset, int count, bool isCompressed)
        {
            var a = new PointL[count];
            var pos = startOffset;
            for (var i = 0; i < count; i++)
            {
                a[i] = ToPointL(data, pos, isCompressed);
                pos += isCompressed ? 4 : 8;
            }
            return a;
        }

        public static PointL[] ToPointLArray(byte[] data, int startOffset, int count)
        {
            var a = new PointL[count];
            var pos = startOffset;
            for (var i = 0; i < count; i++)
            {
                a[i] = ToPointL(data, pos);
                pos += 8;
            }
            return a;
        }

        public static PointF[] ToPointFArray(byte[] data, int startOffset, int count, bool isCompressed)
        {
            var a = new PointF[count];
            var pos = startOffset;
            var dp = isCompressed ? 4 : 8;
            for (var i = 0; i < count; i++)
            {
                a[i] = ToPointF(data, pos, isCompressed);
                pos += dp;
            }
            return a;
        }

        public static EmfPlusRect[] ToEmfPlusRectArray(byte[] data, int startOffset, int count)
        {
            var a = new EmfPlusRect[count];
            var pos = startOffset;
            for (var i = 0; i < count; i++)
            {
                a[i] = new EmfPlusRect(data, pos);
                pos += 8;
            }
            return a;
        }
        public static EmfPlusRectF[] ToEmfPlusRectFArray(byte[] data, int startOffset, int count, bool isCompressed)
        {
            var a = new EmfPlusRectF[count];
            var pos = startOffset;
            var dp = isCompressed ? 8 : 16;
            for (var i = 0; i < count; i++)
            {
                a[i] = new EmfPlusRectF(data, pos, isCompressed);
                pos += dp;
            }
            return a;
        }

        public static byte[] ToByteArray(byte[] data, int startOffset, int count)
        {
            var a = new byte[count];
            var pos = startOffset;
            for (var i = 0; i < count; i++)
            {
                a[i] = data[pos];
                pos++;
            }
            return a;
        }

        public static LogPen ToLogPen(byte[] data, int startOffset)
        {
            var a = new LogPen();
            a.Style = (PenStyle)BitConverter.ToUInt32(data, startOffset);
            a.Width = ToPointL(data, startOffset + 4);
            a.ColorRef = ToColorRef(data, startOffset + 12);
            return a;
        }

        public static LogPenEx ToLogPenEx(byte[] data, int startOffset)
        {
            var a = new LogPenEx();
            a.Style = EmfUtility.IntToEnum(BitConverter.ToInt32(data, startOffset), PenStyle.UNKNOWN);
            a.Width = BitConverter.ToUInt32(data, startOffset + 4);
            a.BrushStyle = EmfUtility.IntToEnum(BitConverter.ToInt32(data, startOffset + 8), BrushStyle.UNKNOWN);
            a.ColorRef = ToColorRef(data, startOffset + 12);
            a.Hatch = BitConverter.ToUInt32(data, startOffset + 16);
            a.StyleEntriesN = BitConverter.ToUInt32(data, startOffset + 20);
            a.StyleEntries = ToUInt32Array(data, startOffset + 24, (int)a.StyleEntriesN);
            return a;
        }

    }
}
