using System.Diagnostics;

namespace EmfHelper
{


    public class EmfParser
    {
        /// <summary>
        /// Provide the possibility to suppress individual records.
        /// </summary>
        private IEmfReadListener mListener;
        private IEmfPlusReadListener? mPlusListener;
        private bool mEmfPlusParsing = false;
        public EmfParser(IEmfReadListener listener, IEmfPlusReadListener? plusListener = null)
        {
            mListener = listener;
            mPlusListener = plusListener;
        }

        public void Read(BinaryReader br)
        {
            mEmfPlusParsing = false;
            byte[] emr = br.ReadBytes(8);
            EmfRecordType type = EmfUtility.IntToEnum(BitConverter.ToInt32(emr, 0), EmfRecordType.None);
            int length = BitConverter.ToInt32(emr, 4);
            byte[] recordData = br.ReadBytes(length - 8);
            Parse(type, length, recordData);
            while (br.PeekChar() != -1)
            {
                emr = br.ReadBytes(8);
                type = EmfUtility.IntToEnum(BitConverter.ToInt32(emr, 0), EmfRecordType.None);
                length = BitConverter.ToInt32(emr, 4);
                recordData = br.ReadBytes(length - 8);
                Parse(type, length, recordData);
            }
        }

        private void Parse(EmfRecordType recordType, int length, byte[] data)
        {
            var record = new EmfRecord(recordType, data);
            Debug.WriteLine($"{GetType().Name}: Parse(): RecordType ={recordType}", GetType().Name);
            switch (recordType)
            {
                case EmfRecordType.EMR_HEADER:
                {
                    EmfHeader header = new();
                    header.Bounds = EmfConverter.ToRectL(data, 0);
                    header.Frame = EmfConverter.ToRectL(data, 16);
                    header.Signature = BitConverter.ToUInt32(data, 32);
                    header.Version = BitConverter.ToUInt32(data, 36);
                    header.Bytes = BitConverter.ToUInt32(data, 40);
                    header.Records = BitConverter.ToUInt32(data, EmfHeader.Index_Records);
                    header.Handles = BitConverter.ToUInt16(data, 48);
                    header.Reserved = BitConverter.ToUInt16(data, 50);
                    header.DescriptionN = BitConverter.ToUInt32(data, EmfHeader.Index_DescriptionN);
                    header.DescriptionOff = BitConverter.ToUInt32(data, EmfHeader.Index_DescriptionOff);
                    header.PalEntriesN = BitConverter.ToUInt32(data, 60);
                    header.Device = EmfConverter.ToSizeL(data, 64);
                    header.Millimeters = EmfConverter.ToSizeL(data, 72);
                    if (length >= 100)
                    {
                        header.PixelFormatCB = BitConverter.ToUInt32(data, 80);
                        header.PixelFormatOff = BitConverter.ToUInt32(data, 84);
                        header.OpenGL = BitConverter.ToUInt32(data, 88);
                    }
                    if (length >= 108)
                    {
                        header.Micrometers = EmfConverter.ToSizeL(data, 92);
                    }
                    mListener.OnEmfHeader(record, header);
                }
                break;
                case EmfRecordType.EMR_EOF:
                    mListener.OnEmfEof(record);
                    //eofなので終わり。以降にあるパレットは無視。
                    return;
                case EmfRecordType.EMR_COMMENT:
                {
                    var comment = new EmrComment();
                    comment.DataSize = BitConverter.ToInt32(data, 0);
                    comment.CommentData = EmfConverter.ToByteArray(data, 4, comment.DataSize);
//                    for (int i = 0; i < comment.DataSize; i++) comment.CommentData[i] = data[4 + i];
                    if (!mListener.OnEmfComment(record, comment))
                    {
                        if (comment.IsEmfPlus && mPlusListener != null)
                        {
                            mPlusListener.OnEnterEmfPlusRecords();
                            ParseEmfPlus(comment.CommentData);
                            mPlusListener.OnExitEmfPlusRecords();
                        }
                    }
                }
                break;
                case EmfRecordType.EMR_SELECTCLIPPATH:
                    mListener.OnEmfSelectClipPath(record, EmfUtility.IntToEnum(BitConverter.ToInt32(data, 0), RegionMode.UNKNOWN));
                    break;
                case EmfRecordType.EMR_SETWINDOWORGEX:
                    mListener.OnEmfSetWindowOrg(record, EmfConverter.ToPointL(data, 0));
                    break;
                case EmfRecordType.EMR_SETVIEWPORTORGEX:
                    mListener.OnEmfSetViewportOrg(record, EmfConverter.ToPointL(data, 0));
                    break;
                case EmfRecordType.EMR_SETWINDOWEXTEX:
                    mListener.OnEmfSetWindowExtEx(record, EmfConverter.ToSizeL(data, 0));
                    break;
                case EmfRecordType.EMR_SETVIEWPORTEXTEX:
                    mListener.OnEmfSetViewportExtEx(record, EmfConverter.ToSizeL(data, 0));
                    break;
                case EmfRecordType.EMR_SETBKCOLOR:
                    mListener.OnEmfSetBkColor(record, EmfConverter.ToColorRef(data, 0));
                    break;
                case EmfRecordType.EMR_SETBKMODE:
                    mListener.OnEmfSetBkMode(record, (BackgroundMode)BitConverter.ToUInt32(data, 0));
                    break;
                case EmfRecordType.EMR_SETPOLYFILLMODE:
                    mListener.OnEmfSetPolyFillMode(record, (PolygonFillMode)BitConverter.ToUInt32(data, 0));
                    break;
                case EmfRecordType.EMR_SETTEXTALIGN:
                    mListener.OnEmfSetTextAlign(record, (TextAlignment)BitConverter.ToUInt32(data, 0));
                    break;
                case EmfRecordType.EMR_SETMITERLIMIT:
                    mListener.OnEmfSetMiterLimit(record, BitConverter.ToUInt32(data, 0));
                    break;
                case EmfRecordType.EMR_SETSTRETCHBLTMODE:
                    mListener.OnEmfSetStretchBltMode(record, EmfUtility.IntToEnum(BitConverter.ToInt32(data, 0), StretchMode.UNKNOWN));
                    break;
                case EmfRecordType.EMR_SETICMMODE:
                    mListener.OnEmfSetIcmMode(record, EmfUtility.IntToEnum(BitConverter.ToInt32(data, 0), ICMMode.UNKNOWN));
                    break;



                case EmfRecordType.EMR_POLYLINE:
                case EmfRecordType.EMR_POLYLINE16:
                {
                    var ppl = new EmrPolygon();
                    ppl.Bounds = EmfConverter.ToRectL(data, 0);
                    ppl.Count = BitConverter.ToUInt32(data, 16);
                    ppl.Points = EmfConverter.ToPointLArray(
                        data, 20, (int)ppl.Count, 
                        recordType== EmfRecordType.EMR_POLYLINE16);
                    mListener.OnEmfPolyline(record, ppl);
                }
                break;
                case EmfRecordType.EMR_POLYLINETO:
                case EmfRecordType.EMR_POLYLINETO16:
                {
                    var ppl = new EmrPolygon();
                    ppl.Bounds = EmfConverter.ToRectL(data, 0);
                    ppl.Count = BitConverter.ToUInt32(data, 16);
                    ppl.Points = EmfConverter.ToPointLArray(
                        data, 20, (int)ppl.Count, 
                        recordType == EmfRecordType.EMR_POLYLINETO16);
                    mListener.OnEmfPolylineTo(record, ppl);
                }
                break;
                case EmfRecordType.EMR_POLYGON:
                case EmfRecordType.EMR_POLYGON16:
                {
                    var ppl = new EmrPolygon();
                    ppl.Bounds = EmfConverter.ToRectL(data, 0);
                    ppl.Count = BitConverter.ToUInt32(data, 16);
                    ppl.Points = EmfConverter.ToPointLArray(
                        data, 20, (int)ppl.Count, 
                        recordType == EmfRecordType.EMR_POLYGON16);
                    mListener.OnEmfPolygon(record, ppl);
                }
                break;

                case EmfRecordType.EMR_POLYPOLYLINE:
                case EmfRecordType.EMR_POLYPOLYLINE16:
                {
                    var ppl = new EmrPolyPolyline();
                    ppl.Bounds = EmfConverter.ToRectL(data, 0);
                    ppl.NumberOfPolylines = BitConverter.ToUInt32(data, 16);
                    ppl.Count = BitConverter.ToUInt32(data, 20);
                    ppl.PolylinePointCount = EmfConverter.ToUInt32Array(data, 24, (int)ppl.NumberOfPolylines);
                    ppl.Points = EmfConverter.ToPointLArray(
                        data, 24 + (int)ppl.NumberOfPolylines * 4, (int)ppl.Count, 
                        recordType == EmfRecordType.EMR_POLYPOLYLINE16);
                    mListener.OnEmfPolyPolyline(record, ppl);
                }
                break;

                case EmfRecordType.EMR_POLYPOLYGON:
                case EmfRecordType.EMR_POLYPOLYGON16:
                {
                    var ppl = new EmrPolyPolyline();
                    ppl.Bounds = EmfConverter.ToRectL(data, 0);
                    ppl.NumberOfPolylines = BitConverter.ToUInt32(data, 16);
                    ppl.Count = BitConverter.ToUInt32(data, 20);
                    ppl.PolylinePointCount = EmfConverter.ToUInt32Array(data, 24, (int)ppl.NumberOfPolylines);
                    ppl.Points = EmfConverter.ToPointLArray(
                        data, 24 + (int)ppl.NumberOfPolylines * 4, (int)ppl.Count,
                        recordType == EmfRecordType.EMR_POLYPOLYGON16);
                    mListener.OnEmfPolyPolygon(record, ppl);
                }
                break;
                case EmfRecordType.EMR_POLYBEZIER:
                case EmfRecordType.EMR_POLYBEZIER16:
                {
                    var ppl = new EmrPolygon();
                    ppl.Bounds = EmfConverter.ToRectL(data, 0);
                    ppl.Count = BitConverter.ToUInt32(data, 16);
                    ppl.Points = EmfConverter.ToPointLArray(
                        data, 20, (int)ppl.Count,
                        recordType == EmfRecordType.EMR_POLYBEZIER16);
                    mListener.OnEmfPolyBezier(record, ppl);
                }
                break;

                case EmfRecordType.EMR_POLYBEZIERTO:
                case EmfRecordType.EMR_POLYBEZIERTO16:
                {
                    var ppl = new EmrPolygon();
                    ppl.Bounds = EmfConverter.ToRectL(data, 0);
                    ppl.Count = BitConverter.ToUInt32(data, 16);
                    ppl.Points = EmfConverter.ToPointLArray(
                        data, 20, (int)ppl.Count,
                        recordType == EmfRecordType.EMR_POLYBEZIERTO16);
                    mListener.OnEmfPolyBezierTo(record, ppl);
                }
                break;
                case EmfRecordType.EMR_MOVETOEX:
                    mListener.OnEmfMoveToEx(record, EmfConverter.ToPointL(data, 0));
                    break;
                case EmfRecordType.EMR_LINETO:
                    mListener.OnEmfLineTo(record, EmfConverter.ToPointL(data, 0));
                    break;
                case EmfRecordType.EMR_RECTANGLE:
                    mListener.OnEmfRectangle(record, EmfConverter.ToRectL(data, 0));
                    break;
                case EmfRecordType.EMR_ROUNDRECT:
                    mListener.OnEmfRoundRect(record, EmfConverter.ToRectL(data, 0), EmfConverter.ToSizeL(data, 16));
                    break;
                case EmfRecordType.EMR_ELLIPSE:
                    mListener.OnEmfEllipse(record, EmfConverter.ToRectL(data, 0));
                    break;
                case EmfRecordType.EMR_ARC:
                {
                    var ppl = new EmrArc();
                    ppl.Box = EmfConverter.ToRectL(data, 0);
                    ppl.Start = EmfConverter.ToPointL(data, 16);
                    ppl.End = EmfConverter.ToPointL(data, 24);
                    mListener.OnEmfArc(record, ppl);
                }
                break;
                case EmfRecordType.EMR_ARCTO:
                {
                    var ppl = new EmrArc();
                    ppl.Box = EmfConverter.ToRectL(data, 0);
                    ppl.Start = EmfConverter.ToPointL(data, 16);
                    ppl.End = EmfConverter.ToPointL(data, 24);
                    mListener.OnEmfArcTo(record, ppl);

                }
                break;
                case EmfRecordType.EMR_ANGLEARC:
                {
                    mListener.OnEmfAngleArc(
                        record,
                        EmfConverter.ToPointL(data, 0),
                        BitConverter.ToUInt32(data, 8),
                        BitConverter.ToSingle(data, 12),
                        BitConverter.ToSingle(data, 16)
                    );
                }
                break;
                case EmfRecordType.EMR_PIE:
                {
                    var ppl = new EmrArc();
                    ppl.Box = EmfConverter.ToRectL(data, 0);
                    ppl.Start = EmfConverter.ToPointL(data, 16);
                    ppl.End = EmfConverter.ToPointL(data, 24);
                    mListener.OnEmfPie(record, ppl);
                }
                break;
                case EmfRecordType.EMR_CHORD:
                {
                    var ppl = new EmrArc();
                    ppl.Box = EmfConverter.ToRectL(data, 0);
                    ppl.Start = EmfConverter.ToPointL(data, 16);
                    ppl.End = EmfConverter.ToPointL(data, 24);
                    mListener.OnEmfChord(record, ppl);
                }
                break;

                case EmfRecordType.EMR_BEGINPATH:
                    mListener.OnEmfBeginPath(record);
                    break;
                case EmfRecordType.EMR_ENDPATH:
                    mListener.OnEmfEndPath(record);
                    break;
                case EmfRecordType.EMR_CLOSEFIGURE:
                    mListener.OnEmfCloseFigure(record);
                    break;
                case EmfRecordType.EMR_STROKEPATH:
                    mListener.OnEmfStrokePath(record, EmfConverter.ToRectL(data, 0));
                    break;
                case EmfRecordType.EMR_STROKEANDFILLPATH:
                    mListener.OnEmfStrokeAndFillPath(record, EmfConverter.ToRectL(data, 0));
                    break;
                case EmfRecordType.EMR_FILLPATH:
                    mListener.OnEmfFillPath(record, EmfConverter.ToRectL(data, 0));
                    break;
                case EmfRecordType.EMR_EXTTEXTOUTW:
                {
                    var ppl = new EmrExtTextOutW();
                    ppl.Bounds = EmfConverter.ToRectL(data, 0);
                    ppl.GraphicsMode = EmfUtility.IntToEnum(BitConverter.ToInt32(data, 16), GraphicsMode.UNKNOWN);
                    ppl.ExScale = BitConverter.ToSingle(data, 20);
                    ppl.EyScale = BitConverter.ToSingle(data, 24);
                    ppl.Text.Reference = EmfConverter.ToPointL(data, 28);
                    ppl.Text.CharsN = BitConverter.ToUInt32(data, 36);
                    ppl.Text.StringOff = BitConverter.ToUInt32(data, 40);
                    ppl.Text.Options = BitConverter.ToUInt32(data, 44);
                    ppl.Text.ClipBounds = EmfConverter.ToRectL(data, 48);
                    ppl.Text.DxOff = BitConverter.ToUInt32(data, 64);
                    uint unusedCharsN = (ppl.Text.StringOff - 68 - /* record header size */ 8) / 2;
                    ppl.Text.UnusedChars = EmfConverter.ToCharArray(data, 68, (int)unusedCharsN);
                    ppl.Text.Chars = EmfConverter.ToCharArray(data, (int)ppl.Text.StringOff - 8, (int)ppl.Text.CharsN);
                    mListener.OnEmfExtTextOutW(record, ppl);
                }
                break;
                case EmfRecordType.EMR_BITBLT:
                {
                    var ppl = new EmrBitBlt();
                    ppl.Bounds = EmfConverter.ToRectL(data, 0);
                    ppl.xDest = BitConverter.ToInt32(data, 16);
                    ppl.yDest = BitConverter.ToInt32(data, 20);
                    ppl.cxDest = BitConverter.ToInt32(data, 24);
                    ppl.cyDest = BitConverter.ToInt32(data, 28);
                    ppl.BitBltRasterOperation = BitConverter.ToUInt32(data, 32);
                    ppl.xSrc = BitConverter.ToInt32(data, 36);
                    ppl.ySrc = BitConverter.ToInt32(data, 40);
                    ppl.XformSrc = EmfConverter.ToXForm(data, 44);
                    ppl.BkColorSrc = EmfConverter.ToColorRef(data, 68);
                    ppl.UsageSrc = EmfUtility.IntToEnum(BitConverter.ToInt32(data, 72), DIBColors.UNKNOWN);
                    ppl.offBmiSrc = BitConverter.ToUInt32(data, 76);
                    ppl.cbBmiSrc = BitConverter.ToUInt32(data, 80);
                    ppl.offBitsSrc = BitConverter.ToUInt32(data, 84);
                    ppl.cbBitsSrc = BitConverter.ToUInt32(data, 88);
                    ppl.BmiSrc = EmfConverter.ToByteArray(data, (int)ppl.offBmiSrc - 8, (int)ppl.cbBmiSrc);
                    ppl.BitsSrc = EmfConverter.ToByteArray(data, (int)ppl.offBitsSrc - 8, (int)ppl.cbBitsSrc);
                    mListener.OnEmfBitBlt(record, ppl);
                }
                break;

                case EmfRecordType.EMR_STRETCHDIBITS:
                {
                    var ppl = new EmrStretchDibits();
                    ppl.Bounds = EmfConverter.ToRectL(data, 0);
                    ppl.xDest = BitConverter.ToInt32(data, 16);
                    ppl.yDest = BitConverter.ToInt32(data, 20);
                    ppl.xSrc = BitConverter.ToInt32(data, 24);
                    ppl.ySrc = BitConverter.ToInt32(data, 28);
                    ppl.cxSrc = BitConverter.ToInt32(data, 32);
                    ppl.cySrc = BitConverter.ToInt32(data, 36);
                    ppl.offBmiSrc = BitConverter.ToUInt32(data, 40);
                    ppl.cbBmiSrc = BitConverter.ToUInt32(data, 44);
                    ppl.offBitsSrc = BitConverter.ToUInt32(data, 48);
                    ppl.cbBitsSrc = BitConverter.ToUInt32(data, 52);
                    ppl.UsageSrc = EmfUtility.IntToEnum(BitConverter.ToInt32(data, 56), DIBColors.UNKNOWN);
                    ppl.BitBltRasterOperation = BitConverter.ToUInt32(data, 60);
                    ppl.cxDest = BitConverter.ToInt32(data, 64);
                    ppl.cyDest = BitConverter.ToInt32(data, 68);
                    ppl.BmiSrc = EmfConverter.ToByteArray(data, (int)ppl.offBmiSrc - 8, (int)ppl.cbBmiSrc);
                    ppl.BitsSrc = EmfConverter.ToByteArray(data, (int)ppl.offBitsSrc - 8, (int)ppl.cbBitsSrc);
                    mListener.OnEmfStretchDibBits(record, ppl);
                }
                break;
                case EmfRecordType.EMR_SETROP2:
                {
                    var op = EmfUtility.IntToEnum(BitConverter.ToInt32(data, 0), BinaryRasterOperation.UNKNOWN);
                    mListener.OnEmfSetRop2(record, op);
                }
                break;
                case EmfRecordType.EMR_SETARCDIRECTION:
                    mListener.OnEmfSetArcDirection(record, (ArcDirection)BitConverter.ToUInt32(data, 0));
                    break;
                case EmfRecordType.EMR_SETTEXTCOLOR:
                    mListener.OnEmfSetTextColor(record, EmfConverter.ToColorRef(data, 0));
                    break;
                case EmfRecordType.EMR_SELECTOBJECT:
                    mListener.OnEmfSelectObject(record, BitConverter.ToUInt32(data, 0));
                    break;
                case EmfRecordType.EMR_DELETEOBJECT:
                    mListener.OnEmfDeleteObject(record, BitConverter.ToUInt32(data, 0));
                    break;
                case EmfRecordType.EMR_SAVEDC:
                    mListener.OnEmfSaveDC(record);
                    break;
                case EmfRecordType.EMR_RESTOREDC:
                    mListener.OnEmfRestoreDC(record, BitConverter.ToInt32(data, 0));
                    break;
                case EmfRecordType.EMR_SETMAPMODE:
                    mListener.OnEmfSetMapMode(record, (MapMode)BitConverter.ToInt32(data, 0));
                    break;
                case EmfRecordType.EMR_EXTCREATEFONTINDIRECTW:
                {
                    var ppl = new EmrExtCreateFontIndirectW();
                    ppl.IhFont = BitConverter.ToUInt32(data, 0);
                    var elwSize = (uint)(data.Length - 4);
                    if (elwSize <= 320)
                    {
                        LogFont logFont = EmfConverter.ToLogFont(data, 4);
                        ppl.LogFont = logFont;
                    }
                    else
                    {
                        LogFontEx logFontEx = new LogFontEx();
                        logFontEx.LogFont = EmfConverter.ToLogFont(data, 4);
                        logFontEx.Fullname = EmfConverter.ToCharArray(data, 96, 64);
                        //Encoding.Unicode.GetString(data, 96, 64).ToCharArray();
                        ppl.LogFont = logFontEx.LogFont;
                    }
                    mListener.OnEmfExtCreateFontIndirect(record, ppl);
                }
                break;
                case EmfRecordType.EMR_CREATEPEN:
                {
                    var ppl = new EmrCreatePen();
                    ppl.IhPen = BitConverter.ToUInt32(data, 0);
                    ppl.LogPen = EmfConverter.ToLogPen(data, 4);
                    mListener.OnEmfCreatePen(record, ppl);
                }
                break;
                case EmfRecordType.EMR_EXTCREATEPEN:
                {
                    var ppl = new EmrExtCreatePen();
                    ppl.IhPen = BitConverter.ToUInt32(data, 0);
                    ppl.DibHeaderOff = BitConverter.ToUInt32(data, 4);
                    ppl.DibHeaderSize = BitConverter.ToUInt32(data, 8);
                    ppl.DibBitsOff = BitConverter.ToUInt32(data, 12);
                    ppl.DibBitsSize = BitConverter.ToUInt32(data, 16);
                    ppl.LogPen = EmfConverter.ToLogPenEx(data, 20);
                    ppl.BitmapHeader = EmfConverter.ToByteArray(data, (int)ppl.DibHeaderOff - 8, (int)ppl.DibHeaderSize);
                    ppl.BitmapBits = EmfConverter.ToByteArray(data, (int)ppl.DibBitsOff - 8, (int)ppl.DibBitsSize);
                    mListener.OnEmfExtCreatePen(record, ppl);
                }
                break;
                case EmfRecordType.EMR_CREATEBRUSHINDIRECT:
                {
                    var ppl = new EmrCreateBrushIndirect();
                    ppl.IhBrush = BitConverter.ToUInt32(data, 0);
                    ppl.LogBrush = EmfConverter.ToLogBrush(data, 4);
                    mListener.OnEmfCreateBrushIndirect(record, ppl);
                }
                break;
                case EmfRecordType.EMR_MODIFYWORLDTRANSFORM:
                {
                    mListener.OnEmfModifyWorldTransform(
                        record,
                        EmfConverter.ToXForm(data, 0),
                        (ModifyWorldTransformMode)BitConverter.ToUInt32(data, 24)
                        );
                }
                break;
                default:
                    mListener.OnNotImplementRecord(record);
                    break;
            }
        }

        private void ParseEmfPlus(byte[] commentData)
        {
            if (mPlusListener == null) return;
            EmfPlusParser.Parse(mPlusListener, commentData);
        }
    }
}
