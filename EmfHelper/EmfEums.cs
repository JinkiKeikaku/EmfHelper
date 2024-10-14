using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EmfHelper
{
    public enum EmfRecordType
    {
        None = 0,
        EMR_HEADER = 0x00000001,
        EMR_POLYBEZIER = 0x00000002,
        EMR_POLYGON = 0x00000003,
        EMR_POLYLINE = 0x00000004,
        EMR_POLYBEZIERTO = 0x00000005,
        EMR_POLYLINETO = 0x00000006,
        EMR_POLYPOLYLINE = 0x00000007,
        EMR_POLYPOLYGON = 0x00000008,
        EMR_SETWINDOWEXTEX = 0x00000009,
        EMR_SETWINDOWORGEX = 0x0000000A,
        EMR_SETVIEWPORTEXTEX = 0x0000000B,
        EMR_SETVIEWPORTORGEX = 0x0000000C,
        EMR_SETBRUSHORGEX = 0x0000000D,
        EMR_EOF = 0x0000000E,
        EMR_SETPIXELV = 0x0000000F,
        EMR_SETMAPPERFLAGS = 0x00000010,
        EMR_SETMAPMODE = 0x00000011,
        EMR_SETBKMODE = 0x00000012,
        EMR_SETPOLYFILLMODE = 0x00000013,
        EMR_SETROP2 = 0x00000014,
        EMR_SETSTRETCHBLTMODE = 0x00000015,
        EMR_SETTEXTALIGN = 0x00000016,
        EMR_SETCOLORADJUSTMENT = 0x00000017,
        EMR_SETTEXTCOLOR = 0x00000018,
        EMR_SETBKCOLOR = 0x00000019,
        EMR_OFFSETCLIPRGN = 0x0000001A,
        EMR_MOVETOEX = 0x0000001B,
        EMR_SETMETARGN = 0x0000001C,
        EMR_EXCLUDECLIPRECT = 0x0000001D,
        EMR_INTERSECTCLIPRECT = 0x0000001E,
        EMR_SCALEVIEWPORTEXTEX = 0x0000001F,
        EMR_SCALEWINDOWEXTEX = 0x00000020,
        EMR_SAVEDC = 0x00000021,
        EMR_RESTOREDC = 0x00000022,
        EMR_SETWORLDTRANSFORM = 0x00000023,
        EMR_MODIFYWORLDTRANSFORM = 0x00000024,
        EMR_SELECTOBJECT = 0x00000025,
        EMR_CREATEPEN = 0x00000026,
        EMR_CREATEBRUSHINDIRECT = 0x00000027,
        EMR_DELETEOBJECT = 0x00000028,
        EMR_ANGLEARC = 0x00000029,
        EMR_ELLIPSE = 0x0000002A,
        EMR_RECTANGLE = 0x0000002B,
        EMR_ROUNDRECT = 0x0000002C,
        EMR_ARC = 0x0000002D,
        EMR_CHORD = 0x0000002E,
        EMR_PIE = 0x0000002F,
        EMR_SELECTPALETTE = 0x00000030,
        EMR_CREATEPALETTE = 0x00000031,
        EMR_SETPALETTEENTRIES = 0x00000032,
        EMR_RESIZEPALETTE = 0x00000033,
        EMR_REALIZEPALETTE = 0x00000034,
        EMR_EXTFLOODFILL = 0x00000035,
        EMR_LINETO = 0x00000036,
        EMR_ARCTO = 0x00000037,
        EMR_POLYDRAW = 0x00000038,
        EMR_SETARCDIRECTION = 0x00000039,
        EMR_SETMITERLIMIT = 0x0000003A,
        EMR_BEGINPATH = 0x0000003B,
        EMR_ENDPATH = 0x0000003C,
        EMR_CLOSEFIGURE = 0x0000003D,
        EMR_FILLPATH = 0x0000003E,
        EMR_STROKEANDFILLPATH = 0x0000003F,
        EMR_STROKEPATH = 0x00000040,
        EMR_FLATTENPATH = 0x00000041,
        EMR_WIDENPATH = 0x00000042,
        EMR_SELECTCLIPPATH = 0x00000043,
        EMR_ABORTPATH = 0x00000044,
        EMR_COMMENT = 0x00000046,
        EMR_FILLRGN = 0x00000047,
        EMR_FRAMERGN = 0x00000048,
        EMR_INVERTRGN = 0x00000049,
        EMR_PAINTRGN = 0x0000004A,
        EMR_EXTSELECTCLIPRGN = 0x0000004B,
        EMR_BITBLT = 0x0000004C,
        EMR_STRETCHBLT = 0x0000004D,
        EMR_MASKBLT = 0x0000004E,
        EMR_PLGBLT = 0x0000004F,
        EMR_SETDIBITSTODEVICE = 0x00000050,
        EMR_STRETCHDIBITS = 0x00000051,
        EMR_EXTCREATEFONTINDIRECTW = 0x00000052,
        EMR_EXTTEXTOUTA = 0x00000053,
        EMR_EXTTEXTOUTW = 0x00000054,
        EMR_POLYBEZIER16 = 0x00000055,
        EMR_POLYGON16 = 0x00000056,
        EMR_POLYLINE16 = 0x00000057,
        EMR_POLYBEZIERTO16 = 0x00000058,
        EMR_POLYLINETO16 = 0x00000059,
        EMR_POLYPOLYLINE16 = 0x0000005A,
        EMR_POLYPOLYGON16 = 0x0000005B,
        EMR_POLYDRAW16 = 0x0000005C,
        EMR_CREATEMONOBRUSH = 0x0000005D,
        EMR_CREATEDIBPATTERNBRUSHPT = 0x0000005E,
        EMR_EXTCREATEPEN = 0x0000005F,
        EMR_POLYTEXTOUTA = 0x00000060,
        EMR_POLYTEXTOUTW = 0x00000061,
        EMR_SETICMMODE = 0x00000062,
        EMR_CREATECOLORSPACE = 0x00000063,
        EMR_SETCOLORSPACE = 0x00000064,
        EMR_DELETECOLORSPACE = 0x00000065,
        EMR_GLSRECORD = 0x00000066,
        EMR_GLSBOUNDEDRECORD = 0x00000067,
        EMR_PIXELFORMAT = 0x00000068,
        EMR_DRAWESCAPE = 0x00000069,
        EMR_EXTESCAPE = 0x0000006A,
        EMR_SMALLTEXTOUT = 0x0000006C,
        EMR_FORCEUFIMAPPING = 0x0000006D,
        EMR_NAMEDESCAPE = 0x0000006E,
        EMR_COLORCORRECTPALETTE = 0x0000006F,
        EMR_SETICMPROFILEA = 0x00000070,
        EMR_SETICMPROFILEW = 0x00000071,
        EMR_ALPHABLEND = 0x00000072,
        EMR_SETLAYOUT = 0x00000073,
        EMR_TRANSPARENTBLT = 0x00000074,
        EMR_GRADIENTFILL = 0x00000076,
        EMR_SETLINKEDUFIS = 0x00000077,
        EMR_SETTEXTJUSTIFICATION = 0x00000078,
        EMR_COLORMATCHTOTARGETW = 0x00000079,
        EMR_CREATECOLORSPACEW = 0x0000007A
    };
    public enum StockObjects : uint
    {
        WHITE_BRUSH = 0x80000000,
        LTGRAY_BRUSH = 0x80000001,
        GRAY_BRUSH = 0x80000002,
        DKGRAY_BRUSH = 0x80000003,
        BLACK_BRUSH = 0x80000004,
        NULL_BRUSH = 0x80000005,
        HOLLOW_BRUSH = 0x80000005,
        WHITE_PEN = 0x80000006,
        BLACK_PEN = 0x80000007,
        NULL_PEN = 0x80000008,
        OEM_FIXED_FONT = 0x8000000A,
        ANSI_FIXED_FONT = 0x8000000B,
        ANSI_VAR_FONT = 0x8000000C,
        SYSTEM_FONT = 0x8000000D,
        DEVICE_DEFAULT_FONT = 0x8000000E,
        DEFAULT_PALETTE = 0x8000000F,
        SYSTEM_FIXED_FONT = 0x80000010,
        DEFAULT_GUI_FONT = 0x80000011,
        DC_BRUSH = 0x80000012,
        DC_PEN = 0x80000013
    }

    public enum PolygonFillMode
    {
        ALTERNATE = 0x01,
        WINDING = 0x02
    }

    public enum TextAlignment
    {
        TA_NOUPDATECP = 0x0000,
        TA_LEFT = 0x0000,
        TA_TOP = 0x0000,
        TA_UPDATECP = 0x0001,
        TA_RIGHT = 0x0002,
        TA_CENTER = 0x0006,
        TA_BOTTOM = 0x0008,
        TA_BASELINE = 0x0018,
        TA_RTLREADING = 0x0100,
        TA_CP_MASK = 0x0006,
        TA_HORIZONTAL_MASK = 0x0006,
        TA_VERTICAL_MASK = 0x0018,
    }

    public enum ArcDirection
    {
        AD_COUNTERCLOCKWISE = 0x00000001,
        AD_CLOCKWISE = 0x00000002
    }

    public enum ExtTextOutOptions
    {
        ETO_OPAQUE = 0x0002,
        ETO_CLIPPED = 0x0004,
        ETO_GLYPH_INDEX = 0x0010,
        ETO_RTLREADING = 0x0080,
        ETO_NUMERICSLOCAL = 0x0400,
        ETO_NUMERICSLATIN = 0x0800,
        ETO_PDY = 0x2000
    }

    public enum PenStyle
    {
        UNKNOWN = -1,

        /// <summary>
        /// A pen type that specifies a line with a width of one logical unit and a style that is a solid color.
        /// </summary>
        PS_COSMETIC = 0x00000000,

        /// <summary>
        /// A pen type that specifies a line with a width that is measured in logical units and a style that can contain any of the attributes of a brush.
        /// </summary>
        PS_GEOMETRIC = 0x00010000,

        /// <summary>
        /// A line cap that specifies round ends.
        /// </summary>
        PS_ENDCAP_ROUND = 0x00000000,

        /// <summary>
        /// A line cap that specifies square ends.
        /// </summary>
        PS_ENDCAP_SQUARE = 0x00000100,

        /// <summary>
        /// A line cap that specifies flat ends.
        /// </summary>
        PS_ENDCAP_FLAT = 0x00000200,


        /// <summary>
        /// A line join that specifies round joins.
        /// </summary>
        PS_JOIN_ROUND = 0x00000000,

        /// <summary>
        /// A line join that specifies beveled joins.
        /// </summary>
        PS_JOIN_BEVEL = 0x00001000,

        /// <summary>
        /// A line join that specifies mitered joins when the lengths of the joins are within the current miter length limit.
        /// If the lengths of the joins exceed the miter limit, beveled joins are specified.
        /// </summary>
        PS_JOIN_MITER = 0x00002000,

        /// <summary>
        /// A line style that is a solid color.
        /// </summary>
        PS_SOLID = 0x00000000,

        /// <summary>
        /// A line style that is dashed.
        /// </summary>
        PS_DASH = 0x00000001,

        /// <summary>
        /// A line style that is dotted.
        /// </summary>
        PS_DOT = 0x00000002,

        /// <summary>
        /// A line style that consists of alternating dashes and dots.
        /// </summary>
        PS_DASHDOT = 0x00000003,

        /// <summary>
        /// A line style that consists of dashes and double dots.
        /// </summary>
        PS_DASHDOTDOT = 0x00000004,

        /// <summary>
        /// A line style that is invisible.
        /// </summary>
        PS_NULL = 0x00000005,

        /// <summary>
        /// A line style that is a solid color. When this style is specified in a drawing record that takes a bounding rectangle,
        /// the dimensions of the figure are shrunk so that it fits entirely in the bounding rectangle, considering the width of the pen.
        /// </summary>
        PS_INSIDEFRAME = 0x00000006,

        /// <summary>
        /// A line style that is defined by a styling array, which specifies the lengths of dashes and gaps in the line.
        /// </summary>
        PS_USERSTYLE = 0x00000007,

        /// <summary>
        /// A line style in which every other pixel is set. This style is applicable only to a pen type of PS_COSMETIC.
        /// </summary>
        PS_ALTERNATE = 0x00000008,
    }

    public enum BrushStyle
    {
        UNKNOWN = -1,
        BS_SOLID = 0x0000,
        BS_NULL = 0x0001,
        BS_HATCHED = 0x0002,
        BS_PATTERN = 0x0003,
        BS_INDEXED = 0x0004,
        BS_DIBPATTERN = 0x0005,
        BS_DIBPATTERNPT = 0x0006,
        BS_PATTERN8X8 = 0x0007,
        BS_DIBPATTERN8X8 = 0x0008,
        BS_MONOPATTERN = 0x0009
    }

    public enum HatchStyle
    {
        HS_NONE_BECAUSE_SOLID = 0x0000,
        HS_NONE_BECAUSE_NULL = 0x0001,
        HS_SOLIDCLR = 0x0006,
        HS_DITHEREDCLR = 0x0007,
        HS_SOLIDTEXTCLR = 0x0008,
        HS_DITHEREDTEXTCLR = 0x0009,
        HS_SOLIDBKCLR = 0x000A,
        HS_DITHEREDBKCLR = 0x000B
    }

    public enum BackgroundMode
    {
        TRANSPARENT = 0x0001,
        OPAQUE = 0x0002
    }

    public enum CommentType
    {
        EMR_COMMENT_EMFSPOOL = 0x00000000,
        EMR_COMMENT_EMFPLUS = 0x2B464D45,
        EMR_COMMENT_PUBLIC = 0x43494447,
    }
    public enum GraphicsMode
    {
        UNKNOWN = -1,
        GM_COMPATIBLE = 0x00000001,
        GM_ADVANCED = 0x00000002
    }

    public enum GraphicsVersion
    {
        GraphicsVersion1 = 0x0001,
        GraphicsVersion1_1 = 0x0002
    }

    public enum MapMode
    {
        MM_TEXT = 0x01,
        MM_LOMETRIC = 0x02,
        MM_HIMETRIC = 0x03,
        MM_LOENGLISH = 0x04,
        MM_HIENGLISH = 0x05,
        MM_TWIPS = 0x06,
        MM_ISOTROPIC = 0x07,
        MM_ANISOTROPIC = 0x08
    }

    public enum ModifyWorldTransformMode
    {
        MWT_IDENTITY = 0x01,
        MWT_LEFTMULTIPLY = 0x02,
        MWT_RIGHTMULTIPLY = 0x03,
        MWT_SET = 0x04
    }

    public enum CharacterSet
    {
        UNKNOWN = -1,
        ANSI_CHARSET = 0x00000000,
        DEFAULT_CHARSET = 0x00000001,
        SYMBOL_CHARSET = 0x00000002,
        MAC_CHARSET = 0x0000004D,
        SHIFTJIS_CHARSET = 0x00000080,
        HANGUL_CHARSET = 0x00000081,
        JOHAB_CHARSET = 0x00000082,
        GB2312_CHARSET = 0x00000086,
        CHINESEBIG5_CHARSET = 0x00000088,
        GREEK_CHARSET = 0x000000A1,
        TURKISH_CHARSET = 0x000000A2,
        VIETNAMESE_CHARSET = 0x000000A3,
        HEBREW_CHARSET = 0x000000B1,
        ARABIC_CHARSET = 0x000000B2,
        BALTIC_CHARSET = 0x000000BA,
        RUSSIAN_CHARSET = 0x000000CC,
        THAI_CHARSET = 0x000000DE,
        EASTEUROPE_CHARSET = 0x000000EE,
        OEM_CHARSET = 0x000000FF
    }

    public enum BinaryRasterOperation
    {
        UNKNOWN = -1,
        NONE = 0,
        R2_BLACK = 0x0001,
        R2_NOTMERGEPEN = 0x0002,
        R2_MASKNOTPEN = 0x0003,
        R2_NOTCOPYPEN = 0x0004,
        R2_MASKPENNOT = 0x0005,
        R2_NOT = 0x0006,
        R2_XORPEN = 0x0007,
        R2_NOTMASKPEN = 0x0008,
        R2_MASKPEN = 0x0009,
        R2_NOTXORPEN = 0x000A,
        R2_NOP = 0x000B,
        R2_MERGENOTPEN = 0x000C,
        R2_COPYPEN = 0x000D,
        R2_MERGEPENNOT = 0x000E,
        R2_MERGEPEN = 0x000F,
        R2_WHITE = 0x0010
    }

    public enum StretchMode
    {
        UNKNOWN = -1,
        STRETCH_ANDSCANS = 0x01,
        STRETCH_ORSCANS = 0x02,
        STRETCH_DELETESCANS = 0x03,
        STRETCH_HALFTONE = 0x04
    }
    public enum ICMMode
    {
        UNKNOWN = -1,
        ICM_OFF = 0x01,
        ICM_ON = 0x02,
        ICM_QUERY = 0x03,
        ICM_DONE_OUTSIDEDC = 0x04
    }

    public enum DIBColors
    {
        UNKNOWN = -1,
        DIB_RGB_COLORS = 0x00,
        DIB_PAL_COLORS = 0x01,
        DIB_PAL_INDICES = 0x02
    }
    

    public enum RegionMode
    {
        UNKNOWN = -1,
        RGN_AND = 0x01,
        RGN_OR = 0x02,
        RGN_XOR = 0x03,
        RGN_DIFF = 0x04,
        RGN_COPY = 0x05
    }

    public enum TernaryRasterOperation
    {
        UNKNOWN = -1,
        BLACKNESS = 0x00,
        DPSOON = 0x01,
        DPSONA = 0x02,
        PSON = 0x03,
        SDPONA = 0x04,
        DPON = 0x05,
        PDSXNON = 0x06,
        PDSAON = 0x07,
        SDPNAA = 0x08,
        PDSXON = 0x09,
        DPNA = 0x0A,
        PSDNAON = 0x0B,
        SPNA = 0x0C,
        PDSNAON = 0x0D,
        PDSONON = 0x0E,
        PN = 0x0F,
        PDSONA = 0x10,
        NOTSRCERASE = 0x11,
        SDPXNON = 0x12,
        SDPAON = 0x13,
        DPSXNON = 0x14,
        DPSAON = 0x15,
        PSDPSANAXX = 0x16,
        SSPXDSXAXN = 0x17,
        SPXPDXA = 0x18,
        SDPSANAXN = 0x19,
        PDSPAOX = 0x1A,
        SDPSXAXN = 0x1B,
        PSDPAOX = 0x1C,
        DSPDXAXN = 0x1D,
        PDSOX = 0x1E,
        PDSOAN = 0x1F,
        DPSNAA = 0x20,
        SDPXON = 0x21,
        DSNA = 0x22,
        SPDNAON = 0x23,
        SPXDSXA = 0x24,
        PDSPANAXN = 0x25,
        SDPSAOX = 0x26,
        SDPSXNOX = 0x27,
        DPSXA = 0x28,
        PSDPSAOXXN = 0x29,
        DPSANA = 0x2A,
        SSPXPDXAXN = 0x2B,
        SPDSOAX = 0x2C,
        PSDNOX = 0x2D,
        PSDPXOX = 0x2E,
        PSDNOAN = 0x2F,
        PSNA = 0x30,
        SDPNAON = 0x31,
        SDPSOOX = 0x32,
        NOTSRCCOPY = 0x33,
        SPDSAOX = 0x34,
        SPDSXNOX = 0x35,
        SDPOX = 0x36,
        SDPOAN = 0x37,
        PSDPOAX = 0x38,
        SPDNOX = 0x39,
        SPDSXOX = 0x3A,
        SPDNOAN = 0x3B,
        PSX = 0x3C,
        SPDSONOX = 0x3D,
        SPDSNAOX = 0x3E,
        PSAN = 0x3F,
        PSDNAA = 0x40,
        DPSXON = 0x41,
        SDXPDXA = 0x42,
        SPDSANAXN = 0x43,
        SRCERASE = 0x44,
        DPSNAON = 0x45,
        DSPDAOX = 0x46,
        PSDPXAXN = 0x47,
        SDPXA = 0x48,
        PDSPDAOXXN = 0x49,
        DPSDOAX = 0x4A,
        PDSNOX = 0x4B,
        SDPANA = 0x4C,
        SSPXDSXOXN = 0x4D,
        PDSPXOX = 0x4E,
        PDSNOAN = 0x4F,
        PDNA = 0x50,
        DSPNAON = 0x51,
        DPSDAOX = 0x52,
        SPDSXAXN = 0x53,
        DPSONON = 0x54,
        DSTINVERT = 0x55,
        DPSOX = 0x56,
        DPSOAN = 0x57,
        PDSPOAX = 0x58,
        DPSNOX = 0x59,
        PATINVERT = 0x5A,
        DPSDONOX = 0x5B,
        DPSDXOX = 0x5C,
        DPSNOAN = 0x5D,
        DPSDNAOX = 0x5E,
        DPAN = 0x5F,
        PDSXA = 0x60,
        DSPDSAOXXN = 0x61,
        DSPDOAX = 0x62,
        SDPNOX = 0x63,
        SDPSOAX = 0x64,
        DSPNOX = 0x65,
        SRCINVERT = 0x66,
        SDPSONOX = 0x67,
        DSPDSONOXXN = 0x68,
        PDSXXN = 0x69,
        DPSAX = 0x6A,
        PSDPSOAXXN = 0x6B,
        SDPAX = 0x6C,
        PDSPDOAXXN = 0x6D,
        SDPSNOAX = 0x6E,
        PDXNAN = 0x6F,
        PDSANA = 0x70,
        SSDXPDXAXN = 0x71,
        SDPSXOX = 0x72,
        SDPNOAN = 0x73,
        DSPDXOX = 0x74,
        DSPNOAN = 0x75,
        SDPSNAOX = 0x76,
        DSAN = 0x77,
        PDSAX = 0x78,
        DSPDSOAXXN = 0x79,
        DPSDNOAX = 0x7A,
        SDPXNAN = 0x7B,
        SPDSNOAX = 0x7C,
        DPSXNAN = 0x7D,
        SPXDSXO = 0x7E,
        DPSAAN = 0x7F,
        DPSAA = 0x80,
        SPXDSXON = 0x81,
        DPSXNA = 0x82,
        SPDSNOAXN = 0x83,
        SDPXNA = 0x84,
        PDSPNOAXN = 0x85,
        DSPDSOAXX = 0x86,
        PDSAXN = 0x87,
        SRCAND = 0x88,
        SDPSNAOXN = 0x89,
        DSPNOA = 0x8A,
        DSPDXOXN = 0x8B,
        SDPNOA = 0x8C,
        SDPSXOXN = 0x8D,
        SSDXPDXAX = 0x8E,
        PDSANAN = 0x8F,
        PDSXNA = 0x90,
        SDPSNOAXN = 0x91,
        DPSDPOAXX = 0x92,
        SPDAXN = 0x93,
        PSDPSOAXX = 0x94,
        DPSAXN = 0x95,
        DPSXX = 0x96,
        PSDPSONOXX = 0x97,
        SDPSONOXN = 0x98,
        DSXN = 0x99,
        DPSNAX = 0x9A,
        SDPSOAXN = 0x9B,
        SPDNAX = 0x9C,
        DSPDOAXN = 0x9D,
        DSPDSAOXX = 0x9E,
        PDSXAN = 0x9F,
        DPA = 0xA0,
        PDSPNAOXN = 0xA1,
        DPSNOA = 0xA2,
        DPSDXOXN = 0xA3,
        PDSPONOXN = 0xA4,
        PDXN = 0xA5,
        DSPNAX = 0xA6,
        PDSPOAXN = 0xA7,
        DPSOA = 0xA8,
        DPSOXN = 0xA9,
        D = 0xAA,
        DPSONO = 0xAB,
        SPDSXAX = 0xAC,
        DPSDAOXN = 0xAD,
        DSPNAO = 0xAE,
        DPNO = 0xAF,
        PDSNOA = 0xB0,
        PDSPXOXN = 0xB1,
        SSPXDSXOX = 0xB2,
        SDPANAN = 0xB3,
        PSDNAX = 0xB4,
        DPSDOAXN = 0xB5,
        DPSDPAOXX = 0xB6,
        SDPXAN = 0xB7,
        PSDPXAX = 0xB8,
        DSPDAOXN = 0xB9,
        DPSNAO = 0xBA,
        MERGEPAINT = 0xBB,
        SPDSANAX = 0xBC,
        SDXPDXAN = 0xBD,
        DPSXO = 0xBE,
        DPSANO = 0xBF,
        MERGECOPY = 0xC0,
        SPDSNAOXN = 0xC1,
        SPDSONOXN = 0xC2,
        PSXN = 0xC3,
        SPDNOA = 0xC4,
        SPDSXOXN = 0xC5,
        SDPNAX = 0xC6,
        PSDPOAXN = 0xC7,
        SDPOA = 0xC8,
        SPDOXN = 0xC9,
        DPSDXAX = 0xCA,
        SPDSAOXN = 0xCB,
        SRCCOPY = 0xCC,
        SDPONO = 0xCD,
        SDPNAO = 0xCE,
        SPNO = 0xCF,
        PSDNOA = 0xD0,
        PSDPXOXN = 0xD1,
        PDSNAX = 0xD2,
        SPDSOAXN = 0xD3,
        SSPXPDXAX = 0xD4,
        DPSANAN = 0xD5,
        PSDPSAOXX = 0xD6,
        DPSXAN = 0xD7,
        PDSPXAX = 0xD8,
        SDPSAOXN = 0xD9,
        DPSDANAX = 0xDA,
        SPXDSXAN = 0xDB,
        SPDNAO = 0xDC,
        SDNO = 0xDD,
        SDPXO = 0xDE,
        SDPANO = 0xDF,
        PDSOA = 0xE0,
        PDSOXN = 0xE1,
        DSPDXAX = 0xE2,
        PSDPAOXN = 0xE3,
        SDPSXAX = 0xE4,
        PDSPAOXN = 0xE5,
        SDPSANAX = 0xE6,
        SPXPDXAN = 0xE7,
        SSPXDSXAX = 0xE8,
        DSPDSANAXXN = 0xE9,
        DPSAO = 0xEA,
        DPSXNO = 0xEB,
        SDPAO = 0xEC,
        SDPXNO = 0xED,
        SRCPAINT = 0xEE,
        SDPNOO = 0xEF,
        PATCOPY = 0xF0,
        PDSONO = 0xF1,
        PDSNAO = 0xF2,
        PSNO = 0xF3,
        PSDNAO = 0xF4,
        PDNO = 0xF5,
        PDSXO = 0xF6,
        PDSANO = 0xF7,
        PDSAO = 0xF8,
        PDSXNO = 0xF9,
        DPO = 0xFA,
        PATPAINT = 0xFB,
        PSO = 0xFC,
        PSDNOO = 0xFD,
        DPSOO = 0xFE,
        WHITENESS = 0xFF
    }

}