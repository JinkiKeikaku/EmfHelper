using EmfHelper;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EmfAnalyzer
{
    internal class EmfReader : IEmfReadListener, IEmfPlusReadListener
    {
        public List<string> Result = new();
        public void Read(string path)
        {
            Result.Clear();
            var parser = new EmfParser(this, this);
            using var r = File.OpenRead(path);
            using var br = new BinaryReader(r);
            parser.Read(br);
        }

        public void OnEmfAngleArc(EmfRecord record, PointL center, uint radius, float start, float sweep)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Center({center}),Radius({radius}),Start({start}),Sweep({sweep})");
        }

        public void OnEmfArc(EmfRecord record, EmrArc ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  {ppl}");
        }

        public void OnEmfArcTo(EmfRecord record, EmrArc ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  {ppl}");
        }

        public void OnEmfBeginPath(EmfRecord record)
        {
            Result.Add($"■ {record}");
        }
        public void OnEmfBitBlt(EmfRecord record, EmrBitBlt ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  {ppl}");
        }

        public void OnEmfChord(EmfRecord record, EmrArc ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  {ppl}");
        }

        public void OnEmfCloseFigure(EmfRecord record)
        {
            Result.Add("■ " + record.ToString());
        }

        public bool OnEmfComment(EmfRecord record, EmrComment comment)
        {
            Result.Add($"■ {record}");
            Result.Add($"  {comment}");
            return false;
        }

        public void OnEmfCreateBrushIndirect(EmfRecord record, EmrCreateBrushIndirect ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  {ppl}");
        }

        public void OnEmfCreatePen(EmfRecord record, EmrCreatePen ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  {ppl}");
        }

        public void OnEmfDeleteObject(EmfRecord record, uint ihObject)
        {
            Result.Add($"■ {record}");
            Result.Add($"  id({ihObject})");
        }

        public void OnEmfEllipse(EmfRecord record, RectL box)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Box{box}");
        }

        public void OnEmfEndPath(EmfRecord record)
        {
            Result.Add("■ " + record.ToString());
        }

        public void OnEmfEof(EmfRecord record)
        {
            Result.Add("■ " + record.ToString());
        }

        public void OnEmfExtCreateFontIndirect(EmfRecord record, EmrExtCreateFontIndirectW ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  {ppl}");
        }

        public void OnEmfExtCreatePen(EmfRecord record, EmrExtCreatePen ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  {ppl}");
        }

        public void OnEmfExtTextOutW(EmfRecord record, EmrExtTextOutW ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  {ppl}");
        }

        public void OnEmfFillPath(EmfRecord record, RectL bounds)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Bounds({bounds})");
        }

        public void OnEmfHeader(EmfRecord record, EmfHeader header)
        {
            Result.Add($"■ {record}");
            Result.Add($"  {header}");
        }

        public void OnEmfLineTo(EmfRecord record, PointL p)
        {
            Result.Add($"■ {record}");
            Result.Add($"  P({p})");
        }

        public void OnEmfSetWorldTransform(EmfRecord record, XForm xform)
        {
            Result.Add($"■ {record}");
            Result.Add($"  XForm({xform})");
        }

        public void OnEmfModifyWorldTransform(EmfRecord record, XForm xform, ModifyWorldTransformMode mode)
        {
            Result.Add($"■ {record}");
            Result.Add($"  XForm({xform}), Mode({mode})");
        }

        public void OnEmfMoveToEx(EmfRecord record, PointL p)
        {
            Result.Add($"■ {record}");
            Result.Add($"  P({p})");
        }

        public void OnEmfPie(EmfRecord record, EmrArc ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Arc({ppl})");
        }

        public void OnEmfPolyBezier(EmfRecord record, EmrPolygon ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  PolyBezier({ppl})");
        }

        public void OnEmfPolyBezierTo(EmfRecord record, EmrPolygon ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  PolyBezierTo({ppl})");
        }

        public void OnEmfPolygon(EmfRecord record, EmrPolygon ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Polygon({ppl})");
        }

        public void OnEmfPolyline(EmfRecord record, EmrPolygon ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Polyline({ppl})");
        }

        public void OnEmfPolylineTo(EmfRecord record, EmrPolygon ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  PolylineTo({ppl})");
        }

        public void OnEmfPolyPolygon(EmfRecord record, EmrPolyPolyline ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  PolyPolygon({ppl})");
        }

        public void OnEmfPolyPolyline(EmfRecord record, EmrPolyPolyline ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  PolyPolyline({ppl})");
        }

        public void OnEmfRectangle(EmfRecord record, RectL box)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Box({box})");
        }

        public void OnEmfRestoreDC(EmfRecord record, int savedDC)
        {
            Result.Add($"■ {record}");
            Result.Add($"  savedDC({savedDC})");
        }

        public void OnEmfRoundRect(EmfRecord record, RectL box, SizeL corner)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Box({box}), Corner({corner})");
        }

        public void OnEmfSaveDC(EmfRecord record)
        {
            Result.Add($"■ {record}");
        }

        public void OnEmfSelectClipPath(EmfRecord record, RegionMode mode)
        {
            Result.Add($"■ {record}");
            Result.Add($"  RegionMode({mode})");
        }

        public void OnEmfSelectObject(EmfRecord record, uint ihObject)
        {
            Result.Add($"■ {record}");
            Result.Add($"  id({ihObject})");
        }


        public void OnEmfSetRop2(EmfRecord record, BinaryRasterOperation op)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Rop({op})");
        }

        public void OnEmfSetArcDirection(EmfRecord record, ArcDirection direction)
        {
            Result.Add($"■ {record}");
            Result.Add($"  ArcDirection({direction})");
        }

        public void OnEmfSetBkColor(EmfRecord record, ColorRef bkColor)
        {
            Result.Add($"■ {record}");
            Result.Add($"  BkColor({bkColor})");
        }

        public void OnEmfSetBkMode(EmfRecord record, BackgroundMode bkMode)
        {
            Result.Add($"■ {record}");
            Result.Add($"  BackgroundMode({bkMode})");
        }

        public void OnEmfSetIcmMode(EmfRecord record, ICMMode mode)
        {
            Result.Add($"■ {record}");
            Result.Add($"  ICMMode({mode})");
        }

        public void OnEmfSetMapMode(EmfRecord record, MapMode mode)
        {
            Result.Add($"■ {record}");
            Result.Add($"  MapMode({mode})");
        }

        public void OnEmfSetMiterLimit(EmfRecord record, UInt32 miterLimit)
        {
            Result.Add($"■ {record}");
            Result.Add($"  MiterLimit({miterLimit})");
        }

        public void OnEmfSetPolyFillMode(EmfRecord record, PolygonFillMode polyFillMode)
        {
            Result.Add($"■ {record}");
            Result.Add($"  PolyFillMode({polyFillMode})");
        }
        public void OnEmfSetStretchBltMode(EmfRecord record, StretchMode mode)
        {
            Result.Add($"■ {record}");
            Result.Add($"  StretchMode({mode})");
        }

        public void OnEmfSetTextAlign(EmfRecord record, TextAlignment textAlign)
        {
            Result.Add($"■ {record}");
            Result.Add($"  TextAlign({textAlign})");
        }

        public void OnEmfSetTextColor(EmfRecord record, ColorRef textColor)
        {
            Result.Add($"■ {record}");
            Result.Add($"  TextColor({textColor})");
        }

        public void OnEmfSetViewportExtEx(EmfRecord record, SizeL s)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Extent ({s})");
        }

        public void OnEmfSetViewportOrg(EmfRecord record, PointL org)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Origin({org})");
        }

        public void OnEmfSetWindowExtEx(EmfRecord record, SizeL s)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Extent ({s})");
        }

        public void OnEmfSetWindowOrg(EmfRecord record, PointL org)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Origin({org})");
        }

        public void OnEmfStretchDibBits(EmfRecord record, EmrStretchDibits ppl)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Dib({ppl})");
        }

        public void OnEmfStrokeAndFillPath(EmfRecord record, RectL bounds)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Bounds({bounds})");
        }

        public void OnEmfStrokePath(EmfRecord record, RectL bounds)
        {
            Result.Add($"■ {record}");
            Result.Add($"  Bounds({bounds})");
        }

        public void OnNotImplementRecord(EmfRecord record)
        {
            Result.Add($"■ {record}");
        }

        public void OnEnterEmfPlusRecords()
        {
            Result.Add($"*** Enter Emf+ ***");
        }

        public void OnExitEmfPlusRecords()
        {
            Result.Add($"*** Exit Emf+ ***");
        }

        public void OnEmfPlusHeader(EmfPlusRecord record, EmfPlusHeader header)
        {
            Result.Add($"● {record}");
            Result.Add($"  {header}");
        }

        public void OnEmfPlusGetDC(EmfPlusRecord record)
        {
            Result.Add($"● {record}");
        }

        public void OnEmfPlusEndOfFile(EmfPlusRecord record)
        {
            Result.Add($"● {record}");
        }

        public void OnEmfPlusObject(EmfPlusRecord record, EmfPlusObject obj)
        {
            Result.Add($"● {record}");
            Result.Add($"  Object({obj})");
            switch (obj.ObjectType)
            {
                case EmfPlusObjectType.Pen:
                {
                    var pen = new EmfPlusPen(obj.ObjectData);
                    Result.Add($"  Pen({pen})");
                }
                break;
                case EmfPlusObjectType.Font:
                {
                    var font = new EmfPlusFont(obj.ObjectData);
                    Result.Add($"  Font({font})");
                }
                break;
                case EmfPlusObjectType.Image:
                {
                    var image = new EmfPlusImage(obj.ObjectData);
                    Result.Add($"  Image({image})");
                }
                break;
                case EmfPlusObjectType.Path:
                {
                    var path = new EmfPlusPath(obj.ObjectData);
                    Result.Add($"  Path({path})");
                }
                break;
                case EmfPlusObjectType.StringFormat:
                {
                    var str = new EmfPlusStringFormat(obj.ObjectData);
                    Result.Add($"  StringFormat({str})");
                }
                break;
            }
        }

        public void OnEmfPlusClear(EmfPlusRecord record, int color)
        {
            Result.Add($"● {record}");
            Result.Add($"  color(0x{color:X})");
        }

        public void OnEmfPlusDrawLines(EmfPlusRecord record, IReadOnlyList<EmfHelper.PointF> points, bool isClosed, int objectId)
        {
            Result.Add($"● {record}");
            Result.Add($"  points(Count={points.Count}), isClosed({isClosed}), objectId({objectId})");
        }

        public void OnEmfPlusDrawLines(EmfPlusRecord record, byte[] data, int numPoints, bool isClosed, int objectId)
        {
            Result.Add($"● {record}");
            Result.Add($"  points(numPoints={numPoints}), isClosed({isClosed}), objectId({objectId})");
        }

        public void OnEmfPlusFillPolygon(EmfPlusRecord record, IReadOnlyList<EmfHelper.PointF> points, int brushId, bool isBrushIdColor)
        {
            Result.Add($"● {record}");
            Result.Add($"  points(Count={points.Count}), brush(Id=0x{brushId:X}, isColor={isBrushIdColor})");
        }

        public void OnEmfPlusFillPolygon(EmfPlusRecord record, byte[] data, int numPoints, int brushId, bool isBrushIdColor)
        {
            Result.Add($"● {record}");
            Result.Add($"  points(numPoints={numPoints}), brush(Id=0x{brushId:X}, isColor={isBrushIdColor})");
        }

        public void OnEmfPlusDrawRects(EmfPlusRecord record, IReadOnlyList<EmfPlusRectF> rects, int objectId)
        {
            Result.Add($"● {record}");
            Result.Add($"  rects(Count={rects.Count}), objectId({objectId})");
        }

        public void OnEmfPlusFillRects(EmfPlusRecord record, IReadOnlyList<EmfPlusRectF> rects, int brushId, bool isBrushIdColor)
        {
            Result.Add($"● {record}");
            Result.Add($"  rects(Count={rects.Count}), brush(Id=0x{brushId:X}, isColor={isBrushIdColor})");
        }

        public void OnEmfPlusDrawEllipse(EmfPlusRecord record, EmfPlusRectF rect, int objectId)
        {
            Result.Add($"● {record}");
            Result.Add($"  rect({rect}), objectId({objectId})");
        }

        public void OnEmfPlusFillEllipse(EmfPlusRecord record, EmfPlusRectF rect, int brushId, bool isBrushIdColor)
        {
            Result.Add($"● {record}");
            Result.Add($"  rect({rect}), brush(Id=0x{brushId:X}, isColor={isBrushIdColor})");
        }

        public void OnEmfPlusDrawArc(EmfPlusRecord record, EmfPlusRectF rect, float startAngle, float sweepAngle, int objectId)
        {
            Result.Add($"● {record}");
            Result.Add($"  rect({rect}), startAngle({startAngle}), sweepAngle({sweepAngle}), objectId({objectId})");
        }

        public void OnEmfPlusDrawPie(EmfPlusRecord record, EmfPlusRectF rect, float startAngle, float sweepAngle, int objectId)
        {
            Result.Add($"● {record}");
            Result.Add($"  rect({rect}), startAngle({startAngle}), sweepAngle({sweepAngle}), objectId({objectId})");
        }

        public void OnEmfPlusFillPie(EmfPlusRecord record, EmfPlusRectF rect, float startAngle, float sweepAngle, int brushId, bool isBrushIdColor)
        {
            Result.Add($"● {record}");
            Result.Add($"  rect({rect}), startAngle({startAngle}), sweepAngle({sweepAngle}), brush(Id=0x{brushId:X}, isColor={isBrushIdColor})");
        }

        public void OnEmfPlusDrawPath(EmfPlusRecord record, int objectId, int penId)
        {
            Result.Add($"● {record}");
            Result.Add($"  objectId({objectId}), penId({penId})");
        }

        public void OnEmfPlusFillPath(EmfPlusRecord record, int objectId, int brushId, bool isBrushIdColor)
        {
            Result.Add($"● {record}");
            Result.Add($"  objectId({objectId}), brush(Id=0x{brushId:X}, isColor={isBrushIdColor})");
        }

        public void OnEmfPlusDrawBeziers(EmfPlusRecord record, IReadOnlyList<EmfHelper.PointF> points, int objectId)
        {
            Result.Add($"● {record}");
            Result.Add($"  points(Count={points.Count}), objectId({objectId})");
        }

        public void OnEmfPlusDrawBeziers(EmfPlusRecord record, byte[] data, int numPoints, int objectId)
        {
            Result.Add($"● {record}");
        }

        public void OnEmfPlusDrawCurve(EmfPlusRecord record, IReadOnlyList<EmfHelper.PointF> points, int offset, int numSegments, float tension, int objectId)
        {
            Result.Add($"● {record}");
            Result.Add($"  points(Count={points.Count}), offset({offset}), numSegments({numSegments}), tension({tension}), objectId={objectId}");
        }

        public void OnEmfPlusDrawClosedCurve(EmfPlusRecord record, IReadOnlyList<EmfHelper.PointF> points, float tension, int objectId)
        {
            Result.Add($"● {record}");
            Result.Add($"  points(Count={points.Count}), tension({tension}), objectId({objectId}");
        }

        public void OnEmfPlusDrawClosedCurve(EmfPlusRecord record, byte[] data, int numPoints, float tension, int objectId)
        {
            Result.Add($"● {record}");
        }

        public void OnEmfPlusFillClosedCurve(EmfPlusRecord record, IReadOnlyList<EmfHelper.PointF> points, float tension, bool isWindingFill, int brushId, bool isBrushIdColor)
        {
            Result.Add($"● {record}");
            Result.Add($"  points(Count={points.Count}), tension({tension}), isWindingFill({isWindingFill}), brush(Id=0x{brushId:X}, isColor={isBrushIdColor})");
        }

        public void OnEmfPlusFillClosedCurve(EmfPlusRecord record, byte[] data, int numPoints, float tension, bool isWindingFill, int brushId, bool isBrushIdColor)
        {
            Result.Add($"● {record}");
        }

        public void OnEmfPlusDrawString(EmfPlusRecord record, int objectId, int formatID, EmfPlusRectF layoutRect, char[] stringData, int brushId, bool isBrushIdColor)
        {
            Result.Add($"● {record}");
            Result.Add($"  layoutRect({layoutRect}), string({EmfUtility.CharsToString(stringData)}), formatID({formatID})");
        }

        public void OnEmfPlusDrawImage(EmfPlusRecord record, EmfPlusRectF rectData, int objectId, int imageAttributesID, EmfPlusUnitType srcUnit, EmfPlusRectF srcRect)
        {
            Result.Add($"● {record}");
            Result.Add($"  rectData({rectData}), objectId({objectId}), imageAttributesID({imageAttributesID}), srcUnit({srcUnit}), srcRect({srcRect})");
        }

        public void OnEmfPlusDrawImagePoints(EmfPlusRecord record, IReadOnlyList<EmfHelper.PointF> points, int objectId, int imageAttributesID, EmfPlusUnitType srcUnit, EmfPlusRectF srcRect)
        {
            Result.Add($"● {record}");
            Result.Add($"  points({EmfUtility.PointsToString(points, 5)}), objectId({objectId}), imageAttributesID({imageAttributesID}), srcUnit({srcUnit}), srcRect({srcRect})");
        }

        public void OnEmfPlusDrawImagePoints(EmfPlusRecord record, byte[] data, int numPoints, int objectId, int imageAttributesID, EmfPlusUnitType srcUnit, EmfPlusRectF srcRect)
        {
            Result.Add($"● {record}");
            Result.Add($"  numPoints({numPoints}, objectId({objectId}), imageAttributesID({imageAttributesID}), srcUnit({srcUnit}), srcRect({srcRect})");
        }

        public void OnEmfPlusResetWorldTransform(EmfPlusRecord record)
        {
            Result.Add($"● {record}");
        }

        public void OnEmfPlusMultiplyWorldTransform(EmfPlusRecord record, float[] matrix, bool isPostMultiplied)
        {
            Result.Add($"● {record}");
            Result.Add($"  matrix({string.Join(", ", matrix)}), isPostMultiplied={isPostMultiplied}");
        }

        public void OnEmfPlusSetWorldTransform(EmfPlusRecord record, float[] matrix)
        {
            Result.Add($"● {record}");
            Result.Add($"  matrix({string.Join(", ", matrix)})");
        }

        public void OnEmfPlusScaleWorldTransform(EmfPlusRecord record, float sx, float sy, bool isPostMultiplied)
        {
            Result.Add($"● {record}");
            Result.Add($"  sx={sx}, sy={sy}, isPostMultiplied={isPostMultiplied}");
        }

        public void OnEmfPlusRotateWorldTransform(EmfPlusRecord record, float angle, bool isPostMultiplied)
        {
            Result.Add($"● {record}");
            Result.Add($"  angle={angle}, isPostMultiplied={isPostMultiplied}");
        }

        public void OnEmfPlusTranslateWorldTransform(EmfPlusRecord record, float dx, float dy, bool isPostMultiplied)
        {
            Result.Add($"● {record}");
            Result.Add($"  dx={dx}, dy={dy},isPostMultiplied={isPostMultiplied}");
        }

        public void OnEmfPlusSave(EmfPlusRecord record, int stackIndex)
        {
            Result.Add($"● {record}");
            Result.Add($"  stackIndex={stackIndex}");
        }

        public void OnEmfPlusRestore(EmfPlusRecord record, int stackIndex)
        {
            Result.Add($"● {record}");
            Result.Add($"  stackIndex={stackIndex}");
        }

        public void OnEmfPlusSetAntiAliasMode(EmfPlusRecord record, EmfPlusSmoothingMode mode, bool isAntiAliasing)
        {
            Result.Add($"● {record}");
            Result.Add($"  SmoothingMode({mode}), AntiAliasing({isAntiAliasing})");
        }


        public void OnEmfPlusNotImplementRecord(EmfPlusRecord record)
        {
            Result.Add($"● {record}");
        }
    }
}
