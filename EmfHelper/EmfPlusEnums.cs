using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmfHelper
{
    public enum EmfPlusRecordType
    {
        None = 0,
        EmfPlusRecordTypeMin = 0x4001,
        EmfPlusHeader = 0x4001,
        EmfPlusEndOfFile = 0x4002,
        EmfPlusComment = 0x4003,
        EmfPlusGetDC = 0x4004,
        EmfPlusMultiFormatStart = 0x4005,
        EmfPlusMultiFormatSection = 0x4006,
        EmfPlusMultiFormatEnd = 0x4007,
        EmfPlusObject = 0x4008,
        EmfPlusClear = 0x4009,
        EmfPlusFillRects = 0x400A,
        EmfPlusDrawRects = 0x400B,
        EmfPlusFillPolygon = 0x400C,
        EmfPlusDrawLines = 0x400D,
        EmfPlusFillEllipse = 0x400E,
        EmfPlusDrawEllipse = 0x400F,
        EmfPlusFillPie = 0x4010,
        EmfPlusDrawPie = 0x4011,
        EmfPlusDrawArc = 0x4012,
        EmfPlusFillRegion = 0x4013,
        EmfPlusFillPath = 0x4014,
        EmfPlusDrawPath = 0x4015,
        EmfPlusFillClosedCurve = 0x4016,
        EmfPlusDrawClosedCurve = 0x4017,
        EmfPlusDrawCurve = 0x4018,
        EmfPlusDrawBeziers = 0x4019,
        EmfPlusDrawImage = 0x401A,
        EmfPlusDrawImagePoints = 0x401B,
        EmfPlusDrawString = 0x401C,
        EmfPlusSetRenderingOrigin = 0x401D,
        EmfPlusSetAntiAliasMode = 0x401E,
        EmfPlusSetTextRenderingHint = 0x401F,
        EmfPlusSetTextContrast = 0x4020,
        EmfPlusSetInterpolationMode = 0x4021,
        EmfPlusSetPixelOffsetMode = 0x4022,
        EmfPlusSetCompositingMode = 0x4023,
        EmfPlusSetCompositingQuality = 0x4024,
        EmfPlusSave = 0x4025,
        EmfPlusRestore = 0x4026,
        EmfPlusBeginContainer = 0x4027,
        EmfPlusBeginContainerNoParams = 0x4028,
        EmfPlusEndContainer = 0x4029,
        EmfPlusSetWorldTransform = 0x402A,
        EmfPlusResetWorldTransform = 0x402B,
        EmfPlusMultiplyWorldTransform = 0x402C,
        EmfPlusTranslateWorldTransform = 0x402D,
        EmfPlusScaleWorldTransform = 0x402E,
        EmfPlusRotateWorldTransform = 0x402F,
        EmfPlusSetPageTransform = 0x4030,
        EmfPlusResetClip = 0x4031,
        EmfPlusSetClipRect = 0x4032,
        EmfPlusSetClipPath = 0x4033,
        EmfPlusSetClipRegion = 0x4034,
        EmfPlusOffsetClip = 0x4035,
        EmfPlusDrawDriverstring = 0x4036,
        EmfPlusStrokeFillPath = 0x4037,
        EmfPlusSerializableObject = 0x4038,
        EmfPlusSetTSGraphics = 0x4039,
        EmfPlusSetTSClip = 0x403A,
        EmfPlusRecordTypeMax = 0x403A,
    }

    public enum EmfPlusObjectType
    {
        Unknown = -1,
        Invalid = 0x00000000,
        Brush = 0x00000001,
        Pen = 0x00000002,
        Path = 0x00000003,
        Region = 0x00000004,
        Image = 0x00000005,
        Font = 0x00000006,
        StringFormat = 0x00000007,
        ImageAttributes = 0x00000008,
        CustomLineCap = 0x00000009
    }

    public enum EmfPlusPenDataFlag
    {
        Transform = 0x01,
        StartCap = 0x02,
        EndCap = 0x04,
        Join = 0x08,
        MiterLimit = 0x10,
        LineStyle = 0x20,
        DashedLineCap = 0x40,
        DashedLineOffset = 0x80,
        DashedLine = 0x100,
        NonCenter = 0x200,
        CompoundLine = 0x400,
        CustomStartCap = 0x800,
        CustomEndCap = 0x1000,
    }

    public enum EmfPlusLineStyle
    {
        Unknown = -1,
        Solid = 0x00000000,
        Dash = 0x00000001,
        Dot = 0x00000002,
        DashDot = 0x00000003,
        DashDotDot = 0x00000004,
        Custom = 0x00000005
    }

    public enum EmfPlusUnitType
    {
        Unknown = -1,
        World = 0x00,
        Display = 0x01,
        Pixel = 0x02,
        Point = 0x03,
        Inch = 0x04,
        Document = 0x05,
        Millimeter = 0x06
    }

    public enum EmfPlusBrushType
    {
        Unknown = -1,
        SolidColor = 0x00000000,
        HatchFill = 0x00000001,
        TextureFill = 0x00000002,
        PathGradient = 0x00000003,
        LinearGradient = 0x00000004
    }

    public enum EmfPlusPathPointType
    {
        Unknown = -1,
        Start = 0x00,
        Line = 0x01,
        Bezier = 0x03
    }
    public enum EmfPlusPathPointTypeFlag
    {
        DashMode = 0x01,
        PathMarker = 0x02,
        CloseSubpath = 0x08,
    }

    public enum EmfPlusStringTrimming
    {
        Unknown = -1,
        None = 0x00000000,
        Character = 0x00000001,
        Word = 0x00000002,
        EllipsisCharacter = 0x00000003,
        EllipsisWord = 0x00000004,
        EllipsisPath = 0x00000005
    }

    public enum EmfPlusStringFormatFlags
    {
        None=0,
        DirectionRightToLeft = 0x01,
        DirectionVertical = 0x02,
        NoFitBlackBox = 0x04,
        DisplayFormatControl = 0x20,
        NoFontFallback = 0x400,
        MeasureTrailingSpaces = 0x800,
        NoWrap = 0x1000,
        LineLimit = 0x2000,
        NoClip = 0x4000,
        BypassGDI = unchecked((int)0x80000000)
    }
    public enum EmfPlusFontStyleFlags
    {
        Bold = 0x01,
        Italic = 0x02,
        Underline = 0x04,
        Strikeout = 0x08,
    }
    public enum EmfPlusImageDataType
    {
        Unknown = 0x00000000,
        Bitmap = 0x00000001,
        Metafile = 0x00000002
    }

    public enum EmfPlusBitmapDataType
    {
        Unknown = -1,
        Pixel = 0x00000000,
        Compressed = 0x00000001
    }

    public enum EmfPlusPixelFormat
    {
        FormatUndefined = 0x00000000,
        Format1bppIndexed = 0x00030101,
        Format4bppIndexed = 0x00030402,
        Format8bppIndexed = 0x00030803,
        Format16bppGrayScale = 0x00101004,
        Format16bppRGB555 = 0x00021005,
        Format16bppRGB565 = 0x00021006,
        Format16bppARGB1555 = 0x00061007,
        Format24bppRGB = 0x00021808,
        Format32bppRGB = 0x00022009,
        Format32bppARGB = 0x0026200A,
        Format32bppPARGB = 0x000E200B,
        Format48bppRGB = 0x0010300C,
        Format64bppARGB = 0x0034400D,
        Format64bppPARGB = 0x001A400E
    }

    public enum EmfPlusPaletteStyleFlags
    {
        Unknown = 0,
        HasAlpha = 0x01,
        GrayScale = 0x02,
        Halftone = 0x04
    }

    public enum EmfPlusBrushDataFlags
    {
        None = 0,
        Transform = 0x00000002,
        PresetColors= 0x00000004,
        BlendFactorsH= 0x00000008,
        BlendFactorsV= 0x00000010,
        IsGammaCorrected= 0x00000080,
    }

    public enum EmfPlusWrapMode
    {
        Unknown = -1,
        Tile = 0x00000000,
        TileFlipX = 0x00000001,
        TileFlipY = 0x00000002,
        TileFlipXY = 0x00000003,
        Clamp = 0x00000004
    }

    public enum EmfPlusSmoothingMode
    {
        Unknown=-1,
        Default = 0x00,
        HighSpeed = 0x01,
        HighQuality = 0x02,
        None = 0x03,
        AntiAlias8x4 = 0x04,
        AntiAlias8x8 = 0x05
    }
}
