using System.Diagnostics;

namespace EmfHelper
{
    static class EmfPlusParser
    {
        static public void Parse(IEmfPlusReadListener listener, byte[] commentData)
        {
            using var mem = new MemoryStream(commentData);
            using var br = new BinaryReader(mem);
            br.ReadInt32();//Skip 4 byte
            while (br.PeekChar() != -1)
            {
                var emr = br.ReadBytes(12);
                var recordType = EmfUtility.IntToEnum(BitConverter.ToUInt16(emr, 0), EmfPlusRecordType.None);
                var flags = BitConverter.ToInt16(emr, 2);
                var size = BitConverter.ToInt32(emr, 4);
                var dataSize = BitConverter.ToInt32(emr, 8);
                var data = br.ReadBytes(size - 12);
                var record = new EmfPlusRecord(recordType, flags, size, dataSize, data);
                switch (recordType)
                {
                    case EmfPlusRecordType.EmfPlusHeader:
                        var header = new EmfPlusHeader(data);
                        listener.OnEmfPlusHeader(record, header);
                        break;
                    case EmfPlusRecordType.EmfPlusGetDC:
                        listener.OnEmfPlusGetDC(record);
                        break;
                    case EmfPlusRecordType.EmfPlusEndOfFile:
                        listener.OnEmfPlusEndOfFile(record);
                        break;
                    case EmfPlusRecordType.EmfPlusSetAntiAliasMode:
                    {
                        var mode = EmfUtility.IntToEnum((record.Flags >> 1), EmfPlusSmoothingMode.Unknown);
                        listener.OnEmfPlusSetAntiAliasMode(record, mode, (record.Flags & 1) !=0);
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusClear:
                        listener.OnEmfPlusClear(record, BitConverter.ToInt32(data, 0));
                        break;
                    case EmfPlusRecordType.EmfPlusObject:
                        listener.OnEmfPlusObject(record, new EmfPlusObject(data, dataSize, flags));
                        break;
                    case EmfPlusRecordType.EmfPlusMultiplyWorldTransform:
                        listener.OnEmfPlusMultiplyWorldTransform(
                            record, EmfConverter.ToSingleArray(data, 0, 6), (flags & 0x2000) != 0);
                        break;
                    case EmfPlusRecordType.EmfPlusResetWorldTransform:
                        listener.OnEmfPlusResetWorldTransform(record);
                        break;
                    case EmfPlusRecordType.EmfPlusSetWorldTransform:
                        listener.OnEmfPlusSetWorldTransform(record, EmfConverter.ToSingleArray(data, 0, 6));
                        break;
                    case EmfPlusRecordType.EmfPlusScaleWorldTransform:
                    {
                        var sx = BitConverter.ToSingle(data, 0);
                        var sy = BitConverter.ToSingle(data, 4);
                        var isPostMultiplied = (flags & 0x2000) != 0;
                        listener.OnEmfPlusScaleWorldTransform(record, sx, sy, isPostMultiplied);
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusRotateWorldTransform:
                    {
                        var a = BitConverter.ToSingle(data, 0);
                        var isPostMultiplied = (flags & 0x2000) != 0;
                        listener.OnEmfPlusRotateWorldTransform(record, a, isPostMultiplied);
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusTranslateWorldTransform:
                    {
                        var dx = BitConverter.ToSingle(data, 0);
                        var dy = BitConverter.ToSingle(data, 4);
                        var isPostMultiplied = (flags & 0x2000) != 0;
                        listener.OnEmfPlusTranslateWorldTransform(record, dx, dy, isPostMultiplied);
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusSave:
                        listener.OnEmfPlusSave(record, BitConverter.ToInt32(data, 0));
                        break;
                    case EmfPlusRecordType.EmfPlusRestore:
                        listener.OnEmfPlusRestore(record, BitConverter.ToInt32(data, 0));
                        break;

                    case EmfPlusRecordType.EmfPlusDrawLines:
                    {
                        var objId = flags & 0xff;
                        var isClosed = (flags & 0x2000) != 0;
                        var count = BitConverter.ToInt32(data, 0);
                        if ((flags & 0x0800) != 0)//P flag
                        {
                            listener.OnEmfPlusDrawLines(record, data[4..], count, isClosed, objId);
                        }
                        else
                        {
                            var compressed = (flags & 0x4000) != 0;
                            listener.OnEmfPlusDrawLines(record, EmfConverter.ToPointFArray(data, 4, count, compressed), isClosed, objId);
                        }
                    }
                    break;

                    case EmfPlusRecordType.EmfPlusFillPolygon:
                    {
                        var brushId = BitConverter.ToInt32(data, 0);
                        var isBrushIdColor = (flags & 0x8000) != 0;
                        var count = BitConverter.ToInt32(data, 4);
                        if ((flags & 0x0800) != 0)//P flag
                        {
                            listener.OnEmfPlusFillPolygon(record, data[8..], count, brushId, isBrushIdColor);
                        }
                        else
                        {
                            var compressed = (flags & 0x4000) != 0;
                            listener.OnEmfPlusFillPolygon(
                                record,
                                EmfConverter.ToPointFArray(data, 8, count, compressed),
                                brushId, isBrushIdColor);
                        }
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusDrawRects:
                    {
                        var objId = flags & 0xff;
                        var count = BitConverter.ToInt32(data, 0);
                        var compressed = (flags & 0x4000) != 0;
                        listener.OnEmfPlusDrawRects(
                            record, EmfConverter.ToEmfPlusRectFArray(data, 4, count, compressed), objId);
                    }
                    break;

                    case EmfPlusRecordType.EmfPlusFillRects:
                    {
                        var brushId = BitConverter.ToInt32(data, 0);
                        var isBrushIdColor = (flags & 0x8000) != 0;
                        var count = BitConverter.ToInt32(data, 4);
                        var compressed = (flags & 0x4000) != 0;
                        listener.OnEmfPlusFillRects(
                            record,
                            EmfConverter.ToEmfPlusRectFArray(data, 8, count, compressed),
                            brushId, isBrushIdColor);
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusDrawEllipse:
                    {
                        var objId = flags & 0xff;
                        var compressed = (flags & 0x4000) != 0;
                        listener.OnEmfPlusDrawEllipse(record, new EmfPlusRectF(data, 0, compressed), objId);
                    }
                    break;

                    case EmfPlusRecordType.EmfPlusFillEllipse:
                    {
                        var brushId = BitConverter.ToInt32(data, 0);
                        var isBrushIdColor = (flags & 0x8000) != 0;
                        var compressed = (flags & 0x4000) != 0;
                        listener.OnEmfPlusFillEllipse(record, new EmfPlusRectF(data, 4, compressed), brushId, isBrushIdColor);
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusDrawArc:
                    {
                        var objId = flags & 0xff;
                        var startAngle = BitConverter.ToSingle(data, 0);
                        var sweepAngle = BitConverter.ToSingle(data, 4);
                        var compressed = (flags & 0x4000) != 0;
                        listener.OnEmfPlusDrawArc(record, new EmfPlusRectF(data, 8, compressed), startAngle, sweepAngle, objId);
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusDrawPie:
                    {
                        var objId = flags & 0xff;
                        var startAngle = BitConverter.ToSingle(data, 0);
                        var sweepAngle = BitConverter.ToSingle(data, 4);
                        var compressed = (flags & 0x4000) != 0;
                        listener.OnEmfPlusDrawPie(record, new EmfPlusRectF(data, 8, compressed), startAngle, sweepAngle, objId);
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusFillPie:
                    {
                        var brushId = BitConverter.ToInt32(data, 0);
                        var isBrushIdColor = (flags & 0x8000) != 0;
                        var startAngle = BitConverter.ToSingle(data, 4);
                        var sweepAngle = BitConverter.ToSingle(data, 8);
                        var compressed = (flags & 0x4000) != 0;
                        listener.OnEmfPlusFillPie(record, new EmfPlusRectF(data, 12, compressed), startAngle, sweepAngle, brushId, isBrushIdColor);
                    }
                    break;

                    case EmfPlusRecordType.EmfPlusDrawBeziers:
                    {
                        var objId = flags & 0xff;
                        var count = BitConverter.ToInt32(data, 0);
                        if ((flags & 0x0800) != 0)//P flag
                        {
                            listener.OnEmfPlusDrawBeziers(record, data[4..], count, objId);
                        }
                        else
                        {
                            var compressed = (flags & 0x4000) != 0;
                            listener.OnEmfPlusDrawBeziers(record, EmfConverter.ToPointFArray(data, 4, count, compressed), objId);
                        }
                    }
                    break;

                    case EmfPlusRecordType.EmfPlusDrawCurve:
                    {
                        var objId = flags & 0xff;
                        var t = BitConverter.ToSingle(data, 0);
                        var offset = BitConverter.ToInt32(data, 4);
                        var numSegments = BitConverter.ToInt32(data, 8);
                        var count = BitConverter.ToInt32(data, 12);
                        var compressed = (flags & 0x4000) != 0;
                        var pts = EmfConverter.ToPointFArray(data, 16, count, compressed);
                        listener.OnEmfPlusDrawCurve(record, pts, offset, numSegments, t, objId);
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusDrawClosedCurve:
                    {
                        var objId = flags & 0xff;
                        var t = BitConverter.ToSingle(data, 0);
                        var count = BitConverter.ToInt32(data, 4);
                        if ((flags & 0x0800) != 0)//P flag
                        {
                            listener.OnEmfPlusDrawClosedCurve(record, data[8..], count, t, objId);
                        }
                        else
                        {
                            var compressed = (flags & 0x4000) != 0;
                            var pts = EmfConverter.ToPointFArray(data, 8, count, compressed);
                            listener.OnEmfPlusDrawClosedCurve(record, pts, t, objId);
                        }
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusFillClosedCurve:
                    {
                        var brushId = BitConverter.ToInt32(data, 0);
                        var isBrushIdColor = (flags & 0x8000) != 0;
                        var isWinding = (flags & 0x2000) != 0;
                        var t = BitConverter.ToSingle(data, 4);
                        var count = BitConverter.ToInt32(data, 8);
                        if ((flags & 0x0800) != 0)//P flag
                        {
                            listener.OnEmfPlusFillClosedCurve(record, data[12..], count, t, isWinding, brushId, isBrushIdColor);
                        }
                        else
                        {
                            var compressed = (flags & 0x4000) != 0;
                            listener.OnEmfPlusFillClosedCurve(
                                record,
                                EmfConverter.ToPointFArray(data, 12, count, compressed),
                                t, isWinding, brushId, isBrushIdColor);
                        }
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusDrawString:
                    {
                        var brushId = BitConverter.ToInt32(data, 0);
                        var brushIdIsColor = (flags & 0x8000) != 0;
                        var objectId = flags & 0xff;
                        var formatID = BitConverter.ToInt32(data, 4);
                        var length = BitConverter.ToUInt32(data, 8);
                        var layoutRect = new EmfPlusRectF(data, 12, false);
                        var stringData = EmfConverter.ToCharArray(data, 28, (int)length);
                        listener.OnEmfPlusDrawString(record, objectId, formatID, layoutRect, stringData, brushId, brushIdIsColor);

                    }
                    break;

                    case EmfPlusRecordType.EmfPlusDrawImage:
                    {
                        var objectId = flags & 0xff;
                        var isCompressed = (flags & 0x4000) != 0;
                        var imageAttributesID = BitConverter.ToInt32(data, 0);
                        var srcUnit = EmfUtility.IntToEnum(BitConverter.ToInt32(data, 4), EmfPlusUnitType.Unknown);
                        var srcRect = new EmfPlusRectF(data, 8, false);
                        var rectData = new EmfPlusRectF(data, 24, isCompressed);
                        listener.OnEmfPlusDrawImage(record, rectData, objectId, imageAttributesID, srcUnit, srcRect);
                    }
                    break;

                    case EmfPlusRecordType.EmfPlusDrawImagePoints:
                    {
                        var objectId = flags & 0xff;
                        var isCompressed = (flags & 0x4000) != 0;
                        var imageAttributesID = BitConverter.ToInt32(data, 0);
                        var srcUnit = EmfUtility.IntToEnum(BitConverter.ToInt32(data, 4), EmfPlusUnitType.Unknown);
                        var srcRect = new EmfPlusRectF(data, 8, false);
                        var count = BitConverter.ToInt32(data, 24);
                        if ((flags & 0x0800) != 0)//P flag
                        {
                            listener.OnEmfPlusDrawImagePoints(record, data[28..], count, objectId, imageAttributesID, srcUnit, srcRect);
                        }
                        else
                        {
                            var pts = EmfConverter.ToPointFArray(data, 28, count, isCompressed);
                            listener.OnEmfPlusDrawImagePoints(record, pts, objectId, imageAttributesID, srcUnit, srcRect);
                        }
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusDrawPath:
                    {
                        var objId = flags & 0xff;
                        listener.OnEmfPlusDrawPath(record, objId, BitConverter.ToInt32(data, 0));
                    }
                    break;
                    case EmfPlusRecordType.EmfPlusFillPath:
                    {
                        var objId = flags & 0xff;
                        var brushId = BitConverter.ToInt32(data, 0);
                        var isBrushIdColor = (flags & 0x8000) != 0;
                        listener.OnEmfPlusFillPath(record, objId, brushId, isBrushIdColor);
                    }
                    break;
                    default:
                        listener.OnEmfPlusNotImplementRecord(record);
                        break;
                }
            }
        }
    }
}
