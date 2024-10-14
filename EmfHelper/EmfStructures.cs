using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;

namespace EmfHelper
{
    public class EmfRecord
    {
        public EmfRecordType RecordType;
        public byte[] Data;

        public EmfRecord(EmfRecordType recordType, byte[] data)
        {
            RecordType = recordType;
            Data = data;
        }
        public override string ToString()
            => $"{RecordType}";
    }

    public struct PointS
    {
        public Int16 X;
        public Int16 Y;
        public PointS(Int16 x, Int16 y)
        {
            X = x; Y = y;
        }
        public PointS() : this(0, 0) { }

        public override string ToString()
            => $"X={X}, Y={Y}";

    }

    public struct PointL
    {
        public Int32 X;
        public Int32 Y;
        public PointL(Int32 x, Int32 y)
        {
            X = x;Y = y;
        }
        public PointL() : this(0, 0) { }

        public override string ToString()
            => $"X={X}, Y={Y}";
    }

    public struct PointF
    {
        public Single X;
        public Single Y;

        public PointF()
        {
            X = 0; Y = 0;
        }

        public PointF(Single x, Single y)
        {
            this.X = x;
            this.Y = y;
        }
        public override string ToString()
            => $"X={X}, Y={Y}";
    }

    public struct SizeS
    {
        public Int16 Width;
        public Int16 Height;

        public SizeS() : this(0, 0) { }
        public SizeS(Int16 w, Int16 h)
        {
            Width = w; Height = h;
        }
        public override string ToString()
            => $"W={Width}, H={Height}";
    }

    public struct SizeL
    {
        public Int32 Width;
        public Int32 Height;
        public SizeL() : this(0, 0) { }
        public SizeL(Int32 w, Int32 h)
        {
            Width = w; Height = h;  
        }
        public override string ToString()
            => $"W={Width}, H={Height}";
    }

    /*
        public struct RectS
        {
            public Int16 X;
            public Int16 Y;
            public Int16 Width;
            public Int16 Height;

            public RectS() : this(0, 0, 0, 0) { }
            public RectS(Int16 left, Int16 top, Int16 right, Int16 bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public override string ToString()
                => $"X={X} Y={Y} Width={Width} Height={Height}";
        }
    */
    public struct RectL
    {
        public Int32 Left;
        public Int32 Top;
        public Int32 Right;
        public Int32 Bottom;

        public RectL() : this(0, 0, 0, 0) { }
        public RectL(Int32 left, Int32 top, Int32 right, Int32 bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
        public override string ToString()
            => $"L={Left}, T={Top}, R={Right}, B={Bottom}";
    }
/*
    public struct RectF
    {
        public Single X;
        public Single Y;
        public Single Width;
        public Single Height;
        public RectF() : this(0, 0, 0, 0) { }
        public RectF(Single x, Single y, Single width, Single height)
        {
            X = x; Y = y; Width = width; Height = height;
        }
    }
*/


    public struct XForm
    {
        public Single M11;
        public Single M12;
        public Single M21;
        public Single M22;
        public Single Dx;
        public Single Dy;
        public override string ToString()
            => $"M11={M11}, M12={M12}, M21={M21}, M22={M22}, Dx={Dx}, Dy={Dy}";
    }

    public struct ColorRef
    {
        public Byte Red;
        public Byte Green;
        public Byte Blue;
        public Byte Reserved;
        public override string ToString()
            => $"RGB({Red}, {Green}, {Blue})";
    }

    public struct LogPalEntry
    {
        public Byte Reserved;
        public Byte Blue;
        public Byte Green;
        public Byte Red;
        public override string ToString()
            => $"RGB({Red}, {Green}, {Blue})";
    }

    public struct LogPenEx
    {
        public PenStyle Style;
        public UInt32 Width;
        public BrushStyle BrushStyle;
        public ColorRef ColorRef;
        public UInt32 Hatch;
        public UInt32 StyleEntriesN;
        public UInt32[] StyleEntries;
        public override string ToString()
            => $"Style({Style}), Width({Width}), BrushStyle({BrushStyle}), ColorRef({ColorRef}), Hatch({Hatch}), StyleEntriesN({StyleEntriesN})";
    }

    public struct LogBrushEx
    {
        public BrushStyle Style;
        public ColorRef ColorRef;
        public UInt32 Hatch;
        public override string ToString()
            => $"Style({Style}), ColorRef({ColorRef}), Hatch({Hatch})";
    }

    public struct LogFont
    {
        public Int32 Height;
        public Int32 Width;
        public Int32 Escapement;
        public Int32 Orientation;
        public Int32 Weight;
        public Byte Italic;
        public Byte Underline;
        public Byte StrikeOut;
        public CharacterSet CharSet;
        public Byte OutPrecision;
        public Byte ClipPrecision;
        public Byte Quality;
        public Byte PitchAndFamily;
        public Char[] Facename;
        public override string ToString()
            => $"Height({Height}), Width({Width}), Escapement({Escapement}), Orientation({Orientation}), "+
            $"Weight({Weight}), Italic({Italic}), Underline({Underline}), StrikeOut({StrikeOut}), " + 
            $"CharSet({CharSet}), OutPrecision({OutPrecision}), ClipPrecision({ClipPrecision}), "+
            $"Quality({Quality}), PitchAndFamily({PitchAndFamily}), Facename({EmfUtility.CharsToString(Facename)})";
    }


    public struct EmfHeader
    {
        public RectL Bounds;
        public RectL Frame;
        public UInt32 Signature;
        public UInt32 Version;
        public UInt16 VersionMajor { get { return (UInt16)(Version >> 16); } }
        public UInt16 VersionMinor { get { return (UInt16)(Version & 0xFFFF); } }
        public UInt32 Bytes;
        public UInt32 Records;
        public UInt16 Handles;
        public UInt16 Reserved;
        public UInt32 DescriptionN;
        public UInt32 DescriptionOff;
        public UInt32 PalEntriesN;
        public SizeL Device;
        public SizeL Millimeters;
        public UInt32 PixelFormatCB;
        public UInt32 PixelFormatOff;
        public UInt32 OpenGL;
        public SizeL Micrometers;
        public static int Index_DescriptionN = 52;
        public static int Index_DescriptionOff = 56;
        public static int Index_Records = 44;
        public override string ToString()
            => $"Bounds({Bounds}), Frame({Frame}), VersionMajor({VersionMajor}), VersionMinor({VersionMinor}), "+
            $"PalEntriesN({PalEntriesN}), Device({Device}), Millimeters({Millimeters}), OpenGL({OpenGL})";
    }

    public struct LogFontEx
    {
        public LogFont LogFont;
        public Char[] Fullname;
        public override string ToString()
            => $"LogFont({LogFont}), Fullname({EmfUtility.CharsToString(Fullname)})";
    }

    public struct EmrText
    {
        public PointL Reference;
        public UInt32 CharsN;
        public UInt32 StringOff;
        public UInt32 Options;
        public RectL ClipBounds;
        public UInt32 DxOff;
        public Char[] UnusedChars;
        public Char[] Chars;
        public Byte[] UnusedDx;
        public Byte[] Dx;
        public override string ToString()
            => $"Reference({Reference}), Options({Options}), ClipBounds({ClipBounds}), Chars({EmfUtility.CharsToString(Chars)})";
    }

    public struct EmrCreatePen
    {
        public UInt32 IhPen;
        public LogPen LogPen;
        public override string ToString()
            => $"id({IhPen}), LogPen({LogPen})";
    }

    public struct EmrExtCreatePen
    {
        public UInt32 IhPen;
        public UInt32 DibHeaderOff;
        public UInt32 DibHeaderSize;
        public UInt32 DibBitsOff;
        public UInt32 DibBitsSize;
        public LogPenEx LogPen;
        public Byte[] BitmapHeader;
        public Byte[] BitmapBits;

        public override string ToString()
            => $"id({IhPen}), DibHeaderOff({DibHeaderOff}), DibHeaderSize({DibHeaderSize}), DibBitsOff({DibBitsOff}), "+
            $"DibBitsSize({DibBitsSize}), LogPen({LogPen})";
    }

    public struct EmrCreateBrushIndirect
    {
        public UInt32 IhBrush;
        public LogBrushEx LogBrush;
        public override string ToString()
            => $"id({IhBrush}), LogBrush({LogBrush})";
    }

    public struct EmrExtTextOutW
    {
        public RectL Bounds;
        public GraphicsMode GraphicsMode;
        public Single ExScale;
        public Single EyScale;
        public EmrText Text;
        public override string ToString()
            => $"Bounds({Bounds}), GraphicsMode({GraphicsMode}), ExScale({ExScale}), EyScale({EyScale}), Text({Text})";
    }


    public struct EmrExtCreateFontIndirectW
    {
        public UInt32 IhFont;
        public LogFont LogFont;
        public override string ToString()
            => $"IhFont({IhFont}), LogFontRef({LogFont})";
    }

    public struct EmrEof
    {
        public UInt32 PalEntriesN;
        public UInt32 PalEntriesOff;
        public LogPalEntry[] PaletteBuffer;
        public UInt32 SizeLast;
        public override string ToString()
            => $"IhFont({PalEntriesN}), PalEntriesOff({PalEntriesOff}), PaletteBuffer({PaletteBuffer}), SizeLast({SizeLast})";

    }

    public struct EmrArc
    {
        public RectL Box;
        public PointL Start;
        public PointL End;
        public override string ToString()
            => $"Box({Box}), Start({Start}), End({End})";


    }

    public struct EmrPolyPolyline
    {
        public RectL Bounds;
        public UInt32 NumberOfPolylines;
        public UInt32 Count;
        public UInt32[] PolylinePointCount;
        public PointL[] Points;
        public override string ToString()
            =>$"Bounds({Bounds}), NumberOfPolylines({NumberOfPolylines}), Count({Count}), Points({EmfUtility.PointsToString(Points, 5)})";
    }

    public struct EmrPolygon
    {
        public RectL Bounds;
        public UInt32 Count;
        public PointL[] Points;
        public override string ToString()
            =>$"Bounds({Bounds}), Count({Count}), Points({EmfUtility.PointsToString(Points, 5)})";
    }

    public struct LogPen
    {
        public PenStyle Style;
        public PointL Width;
        public ColorRef ColorRef;
        public override string ToString()
            => $"Style({Style}), Width({Width}), ColorRef({ColorRef})";
    }

    public struct EmrBitBlt
    {
        public RectL Bounds;
        public Int32 xDest;
        public Int32 yDest;
        public Int32 cxDest;
        public Int32 cyDest;
        public UInt32 BitBltRasterOperation;
        public Int32 xSrc;
        public Int32 ySrc;
        public XForm XformSrc;
        public ColorRef BkColorSrc;
        public DIBColors UsageSrc;
        public UInt32 offBmiSrc;
        public UInt32 cbBmiSrc;
        public UInt32 offBitsSrc;
        public UInt32 cbBitsSrc;
        public byte[] BmiSrc;
        public byte[] BitsSrc;
        public override string ToString()
            => $"Bounds({Bounds}), Dest(x={xDest}, y={yDest}, cx={cxDest}, cy={cyDest}), " +
            $"BitBltRasterOperation(0x{BitBltRasterOperation:X}), Src(x={xSrc}, y={ySrc}), XformSrc({XformSrc}), " +
            $"BkColorSrc({BkColorSrc}), UsageSrc({UsageSrc}), offBmiSrc({offBmiSrc}), cbBmiSrc({cbBmiSrc}), " +
            $"offBitsSrc({offBitsSrc}), cbBitsSrc({cbBitsSrc})";
    }


    public struct EmrStretchDibits
    {
        public RectL Bounds;
        public Int32 xDest;
        public Int32 yDest;
        public Int32 cxDest;
        public Int32 cyDest;
        public Int32 xSrc;
        public Int32 ySrc;
        public Int32 cxSrc;
        public Int32 cySrc;
        public UInt32 offBmiSrc;
        public UInt32 cbBmiSrc;
        public UInt32 offBitsSrc;
        public UInt32 cbBitsSrc;
        public DIBColors UsageSrc;
        public UInt32 BitBltRasterOperation;
        public Byte[] BmiSrc;
        public Byte[] BitsSrc;
        public override string ToString()
            => $"Bounds({Bounds}), Dest(x={xDest}, y={yDest}, cx={cxDest}, cy={cyDest}), " +
            $"Src(x={xSrc}, y={ySrc}, cx={cxSrc}, cy={cySrc}), UsageSrc({UsageSrc}), "+
            $"BitBltRasterOperation(0x{BitBltRasterOperation:X})";
    }
}
