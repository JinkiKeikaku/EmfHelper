namespace EmfHelper
{
    public interface IEmfReadListener
    {
        void OnEmfHeader(EmfRecord record, EmfHeader header);
        void OnEmfEof(EmfRecord record);
        bool OnEmfComment(EmfRecord record, EmrComment comment);
        void OnEmfSetBkColor(EmfRecord record, ColorRef bkColor);
        void OnEmfSetBkMode(EmfRecord record, BackgroundMode bkMode);
        void OnEmfSetPolyFillMode(EmfRecord record, PolygonFillMode polyFillMode);
        void OnEmfSetTextAlign(EmfRecord record, TextAlignment textAlign);
        void OnEmfSetTextColor(EmfRecord record, ColorRef textColor);
        void OnEmfSetArcDirection(EmfRecord record, ArcDirection direction);
        void OnEmfSaveDC(EmfRecord record) ;
        void OnEmfRestoreDC(EmfRecord record, Int32 savedDC) ;
        void OnEmfSetWindowOrg(EmfRecord record, PointL org) ;
        void OnEmfSetViewportOrg(EmfRecord record, PointL org) ;
        void OnEmfSetWindowExtEx(EmfRecord record, SizeL s);
        void OnEmfSetViewportExtEx(EmfRecord record, SizeL s);
        void OnEmfSetMapMode(EmfRecord record, MapMode mode);
        void OnEmfModifyWorldTransform(EmfRecord record, XForm xform, ModifyWorldTransformMode mode);
        void OnEmfSetRop2(EmfRecord record, BinaryRasterOperation op);
        void OnEmfSetMiterLimit(EmfRecord record, UInt32 miterLimit);
        void OnEmfSetStretchBltMode(EmfRecord record, StretchMode mode);
        void OnEmfSetIcmMode(EmfRecord record, ICMMode mode);
        void OnEmfSelectClipPath(EmfRecord record, RegionMode mode);

        void OnEmfMoveToEx(EmfRecord record, PointL p);
        void OnEmfLineTo(EmfRecord record, PointL p);

        void OnEmfPolyPolyline(EmfRecord record, EmrPolyPolyline ppl);
//        void OnEmfPolyPolyline16(EmfRecord record, EmrPolyPolyline16 ppl);
        void OnEmfPolyPolygon(EmfRecord record, EmrPolyPolyline ppl);
//        void OnEmfPolyPolygon16(EmfRecord record, EmrPolyPolyline16 ppl);

        void OnEmfPolygon(EmfRecord record, EmrPolygon ppl);
//        void OnEmfPolygon16(EmfRecord record, EmrPolygon16 ppl);
        void OnEmfPolyline(EmfRecord record, EmrPolygon ppl);
//        void OnEmfPolyline16(EmfRecord record, EmrPolygon16 ppl);
        void OnEmfPolylineTo(EmfRecord record, EmrPolygon ppl);
//        void OnEmfPolylineTo16(EmfRecord record, EmrPolygon16 ppl);

        void OnEmfPolyBezier(EmfRecord record, EmrPolygon ppl);
//        void OnEmfPolyBezier16(EmfRecord record, EmrPolygon16 ppl);
        void OnEmfPolyBezierTo(EmfRecord record, EmrPolygon ppl);
//        void OnEmfPolyBezierTo16(EmfRecord record, EmrPolygon16 ppl);

        void OnEmfRectangle(EmfRecord record, RectL box);
        void OnEmfRoundRect(EmfRecord record, RectL box, SizeL corner);
        void OnEmfEllipse(EmfRecord record, RectL box);
        void OnEmfAngleArc(EmfRecord record, PointL center, UInt32 radius, float start, float sweep);
        void OnEmfArc(EmfRecord record, EmrArc ppl);
        void OnEmfArcTo(EmfRecord record, EmrArc ppl);
        void OnEmfPie(EmfRecord record, EmrArc ppl);
        void OnEmfChord(EmfRecord record, EmrArc ppl);

        void OnEmfExtTextOutW(EmfRecord record, EmrExtTextOutW ppl);

        void OnEmfBitBlt(EmfRecord record, EmrBitBlt ppl);
        void OnEmfStretchDibBits(EmfRecord record, EmrStretchDibits ppl);

        void OnEmfSelectObject(EmfRecord record, UInt32 ihObject);
        void OnEmfDeleteObject(EmfRecord record, UInt32 ihObject);
        void OnEmfCreatePen(EmfRecord record, EmrCreatePen ppl);
        void OnEmfExtCreatePen(EmfRecord record, EmrExtCreatePen ppl);
        void OnEmfCreateBrushIndirect(EmfRecord record, EmrCreateBrushIndirect ppl);
        void OnEmfExtCreateFontIndirect(EmfRecord record, EmrExtCreateFontIndirectW ppl);

        void OnEmfBeginPath(EmfRecord record);
        void OnEmfEndPath(EmfRecord record);
        void OnEmfCloseFigure(EmfRecord record);

        void OnEmfStrokePath(EmfRecord record, RectL bounds);
        void OnEmfStrokeAndFillPath(EmfRecord record, RectL bounds);
        void OnEmfFillPath(EmfRecord record, RectL bounds);

        void OnNotImplementRecord(EmfRecord record);

    }
}
