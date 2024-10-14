
using System.Diagnostics.Tracing;
using System.Drawing;

namespace EmfHelper
{
    public class EmfPlusRecord
    {
        public EmfPlusRecordType RecordType;
        public int Flags;
        public int Size;
        public int DataSize;
        public byte[] Data;
        public EmfPlusRecord(EmfPlusRecordType recordType, int flags, int size, int dataSize, byte[] data)
        {
            RecordType = recordType;
            Flags = flags;
            Size = size;
            DataSize = dataSize;
            Data = data;
        }

        public override string ToString()
            => $"{RecordType}(Flags=0x{Flags:X})";
    }

    public struct EmfPlusHeader
    {
        public uint Version;
        public uint EmfPlusFlags;
        public uint LogicalDpiX;
        public uint LogicalDpiY;

        public uint MetafileSignature => (Version & 0xFFFFF000) >> 12;
        public GraphicsVersion GraphicsVersion => (GraphicsVersion)(Version & 0x00000FFF);

        public EmfPlusHeader()
        {
            Version = 0xDBC01002;
            EmfPlusFlags = 1;
            LogicalDpiX = 96;
            LogicalDpiY = 96;
        }
        public EmfPlusHeader(byte[] data)
        {
            Version = BitConverter.ToUInt32(data, 0);
            EmfPlusFlags = BitConverter.ToUInt32(data, 4);
            LogicalDpiX = BitConverter.ToUInt32(data, 8);
            LogicalDpiY = BitConverter.ToUInt32(data, 12);
        }
        public override string ToString()
            => $"Version=0x{Version:X} Flags={EmfPlusFlags:X} DpiX={LogicalDpiX} DpiY={LogicalDpiY} Signature={MetafileSignature} GraphicsVersion={GraphicsVersion}";

    }

    public struct EmfPlusRect
    {
        public short X;
        public short Y;
        public short Width;
        public short Height;
        public EmfPlusRect(byte[] data, int pos)
        {
            X = BitConverter.ToInt16(data, pos);
            Y = BitConverter.ToInt16(data, pos + 2);
            Width = BitConverter.ToInt16(data, pos + 4);
            Height = BitConverter.ToInt16(data, pos + 6);
        }
        public override string ToString()
            => $"X={X}, Y={Y}, W={Width}, H={Height}";
    }

    public struct EmfPlusRectF
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public EmfPlusRectF(byte[] data, int pos, bool isCompressed)
        {
            if (isCompressed)
            {
                X = BitConverter.ToInt16(data, pos);
                Y = BitConverter.ToInt16(data, pos + 2);
                Width = BitConverter.ToInt16(data, pos + 4);
                Height = BitConverter.ToInt16(data, pos + 6);
            }
            else
            {
                X = BitConverter.ToSingle(data, pos);
                Y = BitConverter.ToSingle(data, pos + 4);
                Width = BitConverter.ToSingle(data, pos + 8);
                Height = BitConverter.ToSingle(data, pos + 12);
            }
        }
        public EmfPlusRectF(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            Width = w; Height = h;
        }
        public override string ToString()
            => $"X={X}, Y={Y}, W={Width}, H={Height}";

    }

    public struct EmfPlusStringFormat
    {
        public uint Version;
        public uint StringFormatFlags;
//        public uint FontStyleFlags;
        public uint Language;
        public uint StringAlignment;
        public uint LineAlign;
        public uint DigitSubstitution;
        public uint DigitLanguage;
        public float FirstTabOffset;
        public uint HotkeyPrefix;
        public float LeadingMargin;
        public float TrailingMargin;
        public float Tracking;
        public EmfPlusStringTrimming Trimming;
        public int TabStopCount;
        public int RangeCount;
        public float[] TabStops;
        public EmfPlusCharacterRange[] CharRanges;
        public EmfPlusStringFormat(byte[] data)
        {
            Version = BitConverter.ToUInt32(data, 0);
            StringFormatFlags = BitConverter.ToUInt32(data, 4);
//            FontStyleFlags = BitConverter.ToUInt32(data, 8);
            Language = BitConverter.ToUInt32(data, 8);
            StringAlignment = BitConverter.ToUInt32(data, 12);
            LineAlign = BitConverter.ToUInt32(data, 16);
            DigitSubstitution = BitConverter.ToUInt32(data, 20);
            DigitLanguage = BitConverter.ToUInt32(data, 24);
            FirstTabOffset = BitConverter.ToSingle(data, 28);
            HotkeyPrefix = BitConverter.ToUInt32(data, 32);
            LeadingMargin = BitConverter.ToSingle(data, 36);
            TrailingMargin = BitConverter.ToSingle(data, 40);
            Tracking = BitConverter.ToSingle(data, 44);
            Trimming = EmfUtility.IntToEnum(BitConverter.ToInt32(data, 48), EmfPlusStringTrimming.Unknown);
            TabStopCount = BitConverter.ToInt32(data, 52);
            RangeCount = BitConverter.ToInt32(data, 56);
            TabStops = EmfConverter.ToSingleArray(data, 60, TabStopCount);
            var pos = 60 + TabStopCount * 4;
            CharRanges = new EmfPlusCharacterRange[RangeCount];
            for (int i = 0; i < RangeCount; i++)
            {
                CharRanges[i] = new EmfPlusCharacterRange(data, pos);
                pos += 8;
            }
        }
        public override string ToString()
            => $"Version(0x{Version:X}), StringFormatFlags(0x{StringFormatFlags:X}), " +
            $"Language({Language}), StringAlignment({StringAlignment}), LineAlign({LineAlign}), DigitSubstitution({DigitSubstitution}), " +
            $"DigitLanguage({DigitLanguage}), FirstTabOffset({FirstTabOffset}), HotkeyPrefix({HotkeyPrefix}), " +
            $"LeadingMargin({LeadingMargin}), TrailingMargin({TrailingMargin}), Tracking{Tracking}), Trimming({Trimming}), " +
            $"TabStopCount({TabStopCount}), RangeCount({RangeCount})";
    }

    public struct EmfPlusCharacterRange
    {
        public Int32 First;
        public Int32 Length;
        public EmfPlusCharacterRange(byte[] data, int pos)
        {
            First = BitConverter.ToInt32(data, pos);
            Length = BitConverter.ToInt32(data, pos + 4);
        }
    }

    public struct EmfPlusImage
    {
        public uint Version;
        public EmfPlusImageDataType ImageDataType;
        public byte[] ImageData;
        public EmfPlusImage(byte[] data)
        {
            Version = BitConverter.ToUInt32(data, 0);
            ImageDataType = EmfUtility.IntToEnum(BitConverter.ToInt32(data, 4), EmfPlusImageDataType.Unknown); ;
            ImageData = new byte[data.Length - 8];
            Array.Copy(data, 8, ImageData, 0, data.Length - 8);
        }
        public override string ToString()
            => $"Version(0x{Version:X}), ImageDataType({ImageDataType})";
    }

    public struct EmfPlusBitmap
    {
        public int Width;
        public int Height;
        public int Stride;
        public int PixelFormat;
        public EmfPlusBitmapDataType BitmapDataType;
        public EmfPlusPalette[] Colors;
        public byte[] PixelData;

    }

    public struct EmfPlusPalette
    {
        public EmfPlusPaletteStyleFlags PaletteStyleFlags;
        public int PaletteCount;
        public int[] PaletteEntries;
    }

    public class EmfPlusObject
    {
        public byte ObjectID;
        public EmfPlusObjectType ObjectType;
        public bool IsContinuable;
        public int TotalObjectSize;
        public byte[] ObjectData;
        public EmfPlusObject(byte[] data, int dataSize, int flags)
        {
            ObjectID = (byte)(flags & 0xff);
            var a = (byte)((flags & 0xff00) >> 8);
            ObjectType = EmfUtility.IntToEnum(a & 0x7f, EmfPlusObjectType.Unknown);
            IsContinuable = (a & 0x80) != 0;
            var pos = 0;
            if (IsContinuable)
            {
                TotalObjectSize = dataSize;
                dataSize = BitConverter.ToInt32(data, pos);
                ObjectData = data[4..];
            }
            else
            {
                ObjectData = data;
            }
        }

        public override string ToString()
            => $"ID({ObjectID}), ObjectType({ObjectType}), IsContinuable({IsContinuable})";
    }

    public struct EmfPlusPen
    {
        public UInt32 Version;
        //        public UInt32 Type;   Type is always zero. Not use.
        public Byte[] PenAndBrushData;
        public UInt32 PenDataFlags => BitConverter.ToUInt32(PenAndBrushData, 0);
        public EmfPlusUnitType PenUnit
            => EmfUtility.IntToEnum(BitConverter.ToInt32(PenAndBrushData, 4), EmfPlusUnitType.Unknown);
        public Single PenWidth => BitConverter.ToSingle(PenAndBrushData, 8);

        public EmfPlusPen(byte[] data)
        {
            Version = BitConverter.ToUInt32(data, 0);
            //            Type = BitConverter.ToUInt32(data, 4);
            PenAndBrushData = new byte[data.Length - 8];
            Array.Copy(data, 8, PenAndBrushData, 0, data.Length - 8);
        }

        public override string ToString()
            => $"Version(0x{Version:X}), PenDataFlags(0x{PenDataFlags:X}), PenUnit({PenUnit}), PenWidth({PenWidth})";
    }

    public struct EmfPlusBrush
    {
        public uint Version;
        public EmfPlusBrushType BrushType;
        public byte[] BrushData;
    }

    public struct EmfPlusFont
    {
        public uint Version;
        public float EmSize;
        public EmfPlusUnitType SizeUnit;
        public int FontStyleFlags;
        public string FamilyName;
        public EmfPlusFont(byte[] data)
        {
            Version = BitConverter.ToUInt32(data, 0);
            EmSize = BitConverter.ToSingle(data, 4);
            SizeUnit = EmfUtility.IntToEnum(BitConverter.ToInt32(data, 8), EmfPlusUnitType.Unknown);
            FontStyleFlags = BitConverter.ToInt32(data, 12);
            var count = BitConverter.ToInt32(data, 20);
            FamilyName = EmfUtility.CharsToString(EmfConverter.ToCharArray(data, 24, count));
        }
        public override string ToString()
            => $"Version(0x{Version:X}, EmSize({EmSize}), SizeUnit({SizeUnit}), FontStyleFlags(0x{FontStyleFlags:X}), FamilyName({FamilyName})";
    }

    public struct EmfPlusPath
    {
        public uint Version;
        public uint PathPointCount;
        public uint PathPointFlags;
        public byte[] PathData;
        public EmfPlusPath(byte[] data)
        {
            Version = BitConverter.ToUInt32(data, 0);
            PathPointCount = BitConverter.ToUInt32(data, 4);
            PathPointFlags = BitConverter.ToUInt32(data, 8);

            PathData = new byte[data.Length - 12];
            Array.Copy(data, 12, PathData, 0, data.Length - 12);
        }
        public override string ToString()
            => $"Version(0x{Version:X}, PathPointCount({PathPointCount}), PathPointFlags(0x{PathPointFlags:X})";

    }


}
